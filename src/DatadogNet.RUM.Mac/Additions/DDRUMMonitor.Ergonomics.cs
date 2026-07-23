// Nullable annotations are enabled per file rather than for the project: the generated binding
// sources are not written against a nullable context, and switching the whole project over would
// bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatadogCore;
using Foundation;

namespace DatadogRUM
{
	public partial class DDRUMMonitor
	{
		/// <summary>Starts a RUM view and returns a scope that stops it when disposed.</summary>
		/// <param name="key">Identifies the view. The same key must be used to stop it.</param>
		/// <param name="name">The name reported to Datadog. Defaults to <paramref name="key"/>.</param>
		/// <param name="attributes">Attributes attached to the view.</param>
		/// <returns>A scope that stops the view when disposed.</returns>
		/// <remarks>
		/// The bound API is a pair of calls matched by key, and a view left open by an early return
		/// or an exception stays open for the rest of the session - every subsequent action and
		/// error is attributed to it. A <see langword="using"/> statement makes that structurally
		/// impossible:
		/// <code>
		/// using (RUM.Monitor.StartView ("checkout")) {
		///     // ... work; the view is stopped however this block is left
		/// }
		/// </code>
		/// The scope stops the view with no attributes. To attach attributes at stop time, call
		/// <see cref="StopView(string, IReadOnlyDictionary{string, object})"/> yourself; disposing
		/// the scope afterwards is harmless, since it stops only once.
		/// </remarks>
		public RUMViewScope StartView (string key, string? name = null, IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (key is null)
				throw new ArgumentNullException (nameof (key));

			StartViewWithKey (key, name ?? key, DatadogAttributes.From (attributes));
			return new RUMViewScope (this, key);
		}

		/// <summary>Stops a RUM view previously started with the same key.</summary>
		public void StopView (string key, IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (key is null)
				throw new ArgumentNullException (nameof (key));

			StopViewWithKey (key, DatadogAttributes.From (attributes));
		}

		/// <summary>Records an instantaneous user action.</summary>
		public void AddAction (DDRUMActionType type, string name, IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (name is null)
				throw new ArgumentNullException (nameof (name));

			AddActionWithType (type, name, DatadogAttributes.From (attributes));
		}

		/// <summary>Records an error from a message.</summary>
		public void AddError (
			string message,
			DDRUMErrorSource source = DDRUMErrorSource.Source,
			string? stack = null,
			IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (message is null)
				throw new ArgumentNullException (nameof (message));

			AddErrorWithMessage (message, stack, source, DatadogAttributes.From (attributes));
		}

		/// <summary>Records an error from an <see cref="Exception"/>.</summary>
		/// <remarks>
		/// The bound <c>AddErrorWithError</c> takes an <see cref="NSError"/>, which a managed
		/// exception is not. This reports the exception's type and message, and passes
		/// <see cref="Exception.StackTrace"/> as the stack so errors group by where they were
		/// thrown rather than by message alone.
		/// </remarks>
		public void AddError (
			Exception exception,
			DDRUMErrorSource source = DDRUMErrorSource.Source,
			IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (exception is null)
				throw new ArgumentNullException (nameof (exception));

			AddErrorWithMessage (
				$"{exception.GetType ().FullName}: {exception.Message}",
				exception.StackTrace,
				source,
				DatadogAttributes.From (attributes));
		}

		/// <summary>Adds attributes to the active view, which propagate to its child events.</summary>
		/// <remarks>
		/// New in dd-sdk-ios 3.0: view-level attributes are inherited by the resources, actions,
		/// errors and long tasks recorded while the view is active, so they no longer have to be
		/// repeated on every call.
		/// </remarks>
		public void AddViewAttributes (IReadOnlyDictionary<string, object?> attributes)
		{
			if (attributes is null)
				throw new ArgumentNullException (nameof (attributes));

			AddViewAttributes (DatadogAttributes.From (attributes));
		}

		/// <summary>Removes view-level attributes previously added.</summary>
		public void RemoveViewAttributes (params string[] keys)
		{
			if (keys is null)
				throw new ArgumentNullException (nameof (keys));

			RemoveViewAttributesForKeys (keys);
		}

		/// <summary>Adds an attribute to every subsequent RUM event.</summary>
		/// <remarks>
		/// The generated overload takes an <see cref="NSObject"/>, so setting a single attribute
		/// means hand-wrapping the value — which is exactly what <see cref="DatadogAttributes"/>
		/// exists to avoid for the dictionary-taking members.
		/// </remarks>
		public void AddAttribute (string key, object? value)
		{
			if (key is null)
				throw new ArgumentNullException (nameof (key));

			AddAttributeForKey (key, DatadogAttributes.ToNSObject (value, key));
		}

		/// <summary>Adds an attribute to the active view, which propagates to its child events.</summary>
		public void AddViewAttribute (string key, object? value)
		{
			if (key is null)
				throw new ArgumentNullException (nameof (key));

			AddViewAttributeForKey (key, DatadogAttributes.ToNSObject (value, key));
		}

		/// <summary>Records that a feature flag was evaluated, so RUM events can be split by variant.</summary>
		public void AddFeatureFlagEvaluation (string name, object? value)
		{
			if (name is null)
				throw new ArgumentNullException (nameof (name));

			AddFeatureFlagEvaluationWithName (name, DatadogAttributes.ToNSObject (value, name));
		}

		/// <summary>
		/// The id of the current RUM session, or <see langword="null"/> if there is none — because
		/// RUM is not enabled, or because this session was dropped by sampling.
		/// </summary>
		/// <remarks>
		/// Worth attaching to a support ticket: it is what turns "the app was slow" into a session
		/// you can watch. The generated member answers through a completion block on the SDK's own
		/// queue; this is the same call in the shape C# already has for asynchrony.
		/// </remarks>
		public Task<string?> GetCurrentSessionIdAsync ()
		{
			// RunContinuationsAsynchronously: the completion arrives on whichever queue the SDK
			// answers on, and a synchronous continuation would run the caller's await-resumption
			// there too.
			var completion = new TaskCompletionSource<string?> (TaskCreationOptions.RunContinuationsAsynchronously);

			CurrentSessionIDWithCompletion (sessionId => completion.TrySetResult (sessionId?.ToString ()));

			return completion.Task;
		}
	}

	/// <summary>Keeps a RUM view open for the lifetime of a <see langword="using"/> block.</summary>
	/// <remarks>
	/// Returned by <see cref="DDRUMMonitor.StartView"/>. Disposing stops the view; disposing again
	/// does nothing, so stopping the view by hand first and then letting the scope fall out of
	/// scope is safe.
	/// </remarks>
	public sealed class RUMViewScope : IDisposable
	{
		readonly DDRUMMonitor monitor;
		bool stopped;

		internal RUMViewScope (DDRUMMonitor monitor, string key)
		{
			this.monitor = monitor;
			Key = key;
		}

		/// <summary>The key the view was started with.</summary>
		public string Key { get; }

		/// <summary>Stops the view, unless it has already been stopped.</summary>
		public void Dispose ()
		{
			if (stopped)
				return;

			stopped = true;
			monitor.StopViewWithKey (Key, DatadogAttributes.Empty);
		}
	}
}
