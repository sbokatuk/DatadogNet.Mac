// Nullable annotations are enabled per file rather than for the project: the generated binding
// sources are not written against a nullable context, and switching the whole project over would
// bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using System.Collections.Generic;
using DatadogCore;
using Foundation;

namespace DatadogLogs
{
	public partial class DDLoggerConfiguration
	{
		/// <summary>Creates a logger configuration, with Datadog's own defaults for anything unset.</summary>
		/// <param name="name">The logger name, reported as the event's logger.</param>
		/// <param name="service">The service the logs belong to. Defaults to the SDK's service.</param>
		/// <param name="networkInfoEnabled">Attach network connectivity information to each log.</param>
		/// <param name="bundleWithRumEnabled">Correlate logs with the current RUM view.</param>
		/// <param name="bundleWithTraceEnabled">Correlate logs with the active span.</param>
		/// <param name="remoteSampleRate">Percentage of logs sent to Datadog, 0 to 100.</param>
		/// <param name="remoteLogThreshold">The lowest level sent to Datadog.</param>
		/// <param name="printLogsToConsole">Also write logs to the Xcode console.</param>
		/// <remarks>
		/// The generated type only has the designated initializer, which takes all eight settings
		/// positionally with no defaults - so configuring just a name means spelling out seven more
		/// arguments and knowing what Datadog's defaults are.
		/// </remarks>
		public static DDLoggerConfiguration Create (
			string? name = null,
			string? service = null,
			bool networkInfoEnabled = false,
			bool bundleWithRumEnabled = true,
			bool bundleWithTraceEnabled = true,
			float remoteSampleRate = 100,
			DDLogLevel remoteLogThreshold = DDLogLevel.Debug,
			bool printLogsToConsole = false)
		{
			return new DDLoggerConfiguration (
				service,
				name,
				networkInfoEnabled,
				bundleWithRumEnabled,
				bundleWithTraceEnabled,
				remoteSampleRate,
				remoteLogThreshold,
				printLogsToConsole);
		}
	}

	public partial class DDLogs
	{
		/// <summary>Enables log collection.</summary>
		/// <param name="customEndpoint">
		/// Where logs are sent. Leave null for the intake of the site the SDK was configured with.
		/// </param>
		public static void Enable (NSUrl? customEndpoint = null) =>
			EnableWith (new DDLogsConfiguration (customEndpoint));
	}

	public partial class DDLogger
	{
		/// <summary>Creates a logger, with Datadog's own defaults for anything unset.</summary>
		public static DDLogger Create (
			string? name = null,
			string? service = null,
			bool networkInfoEnabled = false,
			bool bundleWithRumEnabled = true,
			bool bundleWithTraceEnabled = true,
			float remoteSampleRate = 100,
			DDLogLevel remoteLogThreshold = DDLogLevel.Debug,
			bool printLogsToConsole = false)
		{
			return CreateWith (DDLoggerConfiguration.Create (
				name,
				service,
				networkInfoEnabled,
				bundleWithRumEnabled,
				bundleWithTraceEnabled,
				remoteSampleRate,
				remoteLogThreshold,
				printLogsToConsole));
		}

		/// <summary>Logs at the given level, with attributes and an optional exception.</summary>
		/// <remarks>
		/// The bound API is six methods per level and an <see cref="NSError"/> parameter that a
		/// managed exception is not. This takes the level as an argument, so a level chosen at
		/// runtime does not need a switch over six method names, and folds an
		/// <see cref="Exception"/> into the log's attributes.
		/// </remarks>
		public void Log (
			DDLogLevel level,
			string message,
			Exception? exception = null,
			IReadOnlyDictionary<string, object?>? attributes = null)
		{
			if (message is null)
				throw new ArgumentNullException (nameof (message));

			var payload = attributes;

			if (exception is not null) {
				// Copied key by key rather than through the Dictionary(IDictionary) constructor:
				// the parameter is an IReadOnlyDictionary, and plenty of those - anything from
				// ToFrozenDictionary to an ImmutableDictionary - do not also implement IDictionary,
				// so casting would throw for exactly the callers being helped here.
				var merged = new Dictionary<string, object?> ();
				if (attributes is not null) {
					foreach (var pair in attributes)
						merged[pair.Key] = pair.Value;
				}

				// Datadog's own reserved attribute names for an error, so these render as an error
				// in the Logs UI rather than as three unrelated custom attributes.
				merged["error.kind"] = exception.GetType ().FullName;
				merged["error.message"] = exception.Message;
				merged["error.stack"] = exception.StackTrace;
				payload = merged;
			}

			var native = DatadogAttributes.From (payload);

			// The (message, attributes) overload, not (message, error, attributes): the native
			// error parameter is _Nonnull, so the generated binding throws ArgumentNullException
			// rather than passing nil, and the exception has already been folded in above.
			switch (level) {
			case DDLogLevel.Debug:
				Debug (message, native);
				break;
			case DDLogLevel.Info:
				Info (message, native);
				break;
			case DDLogLevel.Notice:
				Notice (message, native);
				break;
			case DDLogLevel.Warn:
				Warn (message, native);
				break;
			case DDLogLevel.Error:
				Error (message, native);
				break;
			case DDLogLevel.Critical:
				Critical (message, native);
				break;
			default:
				throw new ArgumentOutOfRangeException (nameof (level), level, "Unknown log level.");
			}
		}

		/// <summary>Adds an attribute to every subsequent entry from this logger.</summary>
		/// <remarks>
		/// The generated overload takes an <see cref="NSObject"/>, so a logger-wide attribute has to
		/// be hand-wrapped — which is what <see cref="DatadogAttributes"/> exists to avoid for the
		/// dictionary-taking members.
		/// </remarks>
		public void AddAttribute (string key, object? value)
		{
			if (key is null)
				throw new ArgumentNullException (nameof (key));

			AddAttributeForKey (key, DatadogAttributes.ToNSObject (value, key));
		}
	}
}
