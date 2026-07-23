using System;
using Foundation;
using ObjCRuntime;

namespace DatadogInternal
{
	// @interface DDInternalLogger
	[BaseType (typeof(NSObject))]
	interface DDInternalLogger
	{
		[Static]
		[Export ("consolePrint:")]
		void ConsolePrint (string message);

		[Static]
		[Export ("telemetryDebugWithId:message:")]
		void TelemetryDebugWithId (string id, string message);

		[Static]
		[Export ("telemetryErrorWithId:message:kind:stack:")]
		void TelemetryErrorWithId (string id, string message, [NullAllowed] string kind, [NullAllowed] string stack);
	}

	// @interface DDTracingHeaderType
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTracingHeaderType
	{
		[Static]
		[Export ("datadog", ArgumentSemantic.Strong)]
		DDTracingHeaderType Datadog { get; }

		[Static]
		[Export ("b3multi", ArgumentSemantic.Strong)]
		DDTracingHeaderType B3multi { get; }

		[Static]
		[Export ("b3", ArgumentSemantic.Strong)]
		DDTracingHeaderType B3 { get; }

		[Static]
		[Export ("tracecontext", ArgumentSemantic.Strong)]
		DDTracingHeaderType Tracecontext { get; }
	}
}
