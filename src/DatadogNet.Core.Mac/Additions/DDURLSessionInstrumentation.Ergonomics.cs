// Nullable annotations are enabled per file rather than for the project: the generated binding
// sources are not written against a nullable context, and switching the whole project over would
// bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using Foundation;
using ObjCRuntime;

namespace DatadogCore
{
	public partial class DDURLSessionInstrumentationConfiguration
	{
		/// <summary>Creates a configuration for instrumenting <typeparamref name="TDelegate"/>.</summary>
		/// <typeparam name="TDelegate">
		/// Your <c>NSUrlSession</c> delegate. It must derive from <see cref="NSObject"/>, implement
		/// <see cref="INSUrlSessionDataDelegate"/>, and carry a <see cref="RegisterAttribute"/> so
		/// the Objective-C runtime knows about it.
		/// </typeparam>
		public static DDURLSessionInstrumentationConfiguration Create<TDelegate> ()
			where TDelegate : NSObject, INSUrlSessionDataDelegate
			=> new (ClassHandleOf (typeof (TDelegate)));

		/// <inheritdoc cref="Create{TDelegate}"/>
		public static DDURLSessionInstrumentationConfiguration Create (Type delegateType)
			=> new (ClassHandleOf (delegateType));

		/// <summary>The delegate class being instrumented, as a managed <see cref="Type"/>.</summary>
		public Type? DelegateType => Class.Lookup (new Class (DelegateClass));

		/// <summary>
		/// Resolves a managed type to its Objective-C class handle, failing loudly if it has none.
		/// </summary>
		/// <remarks>
		/// This is the whole reason these overloads exist. The native API takes an Objective-C
		/// <c>Class</c>, which reaches C# as a bare <see cref="IntPtr"/> - nothing in the generated
		/// signature suggests you are meant to pass <c>Class.GetHandle(typeof(T))</c>, and passing
		/// <see cref="IntPtr.Zero"/> is accepted silently and then instruments nothing at all.
		/// </remarks>
		internal static IntPtr ClassHandleOf (Type delegateType)
		{
			if (delegateType is null)
				throw new ArgumentNullException (nameof (delegateType));

			var handle = Class.GetHandle (delegateType);
			if (handle == IntPtr.Zero) {
				throw new ArgumentException (
					$"'{delegateType.FullName}' is not registered with the Objective-C runtime. " +
					"A URLSession delegate must derive from NSObject, implement " +
					"INSUrlSessionDataDelegate, and carry a [Register] attribute.",
					nameof (delegateType));
			}

			return handle;
		}
	}

	public partial class DDURLSessionInstrumentation
	{
		/// <summary>Instruments <typeparamref name="TDelegate"/> for RUM resources and tracing.</summary>
		/// <remarks>
		/// This is the API Datadog documents for automatic network tracking from 3.0 onwards; the
		/// <c>DatadogURLSessionDelegate</c> and <c>DDNSURLSessionDelegate</c> types it replaced are
		/// gone. Attach the delegate to your <c>NSUrlSession</c> as usual - instrumentation is
		/// installed on the class, not on an instance.
		/// </remarks>
		public static void Enable<TDelegate> ()
			where TDelegate : NSObject, INSUrlSessionDataDelegate
			=> EnableWithConfiguration (DDURLSessionInstrumentationConfiguration.Create<TDelegate> ());

		/// <inheritdoc cref="Enable{TDelegate}"/>
		public static void Enable (Type delegateType)
			=> EnableWithConfiguration (DDURLSessionInstrumentationConfiguration.Create (delegateType));

		/// <summary>Stops instrumenting <typeparamref name="TDelegate"/>.</summary>
		public static void Disable<TDelegate> ()
			where TDelegate : NSObject, INSUrlSessionDataDelegate
			=> DisableWithDelegateClass (
				DDURLSessionInstrumentationConfiguration.ClassHandleOf (typeof (TDelegate)));

		/// <inheritdoc cref="Disable{TDelegate}"/>
		public static void Disable (Type delegateType)
			=> DisableWithDelegateClass (DDURLSessionInstrumentationConfiguration.ClassHandleOf (delegateType));
	}
}
