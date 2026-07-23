using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;

namespace DatadogTrace
{
	partial interface IOTSpan {}

	// @protocol OTSpan
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface OTSpan
	{
		[Abstract]
		[Export ("context", ArgumentSemantic.Strong)]
		IOTSpanContext Context { get; }

		[Abstract]
		[Export ("tracer", ArgumentSemantic.Strong)]
		IOTTracer Tracer { get; }

		[Abstract]
		[Export ("setOperationName:")]
		void SetOperationName (string operationName);

		[Abstract]
		[Export ("setTag:value:")]
		void SetTag (string key, string value);

		[Abstract]
		[Export ("setTag:numberValue:")]
		void SetTag (string key, NSNumber numberValue);

		[Abstract]
		[Export ("setTag:boolValue:")]
		void SetTag (string key, bool boolValue);

		[Abstract]
		[Export ("log:")]
		void Log (NSDictionary<NSString, NSObject> fields);

		[Abstract]
		[Export ("log:timestamp:")]
		void Log (NSDictionary<NSString, NSObject> fields, [NullAllowed] NSDate timestamp);

		[Abstract]
		[Export ("setBaggageItem:value:")]
		IOTSpan SetBaggageItem (string key, string value);

		[Abstract]
		[Export ("getBaggageItem:")]
		string GetBaggageItem (string key);

		[Abstract]
		[Export ("setError:")]
		void SetError (NSError error);

		[Abstract]
		[Export ("setErrorWithKind:message:stack:")]
		void SetErrorWithKind (string kind, string message, [NullAllowed] string stack);

		[Abstract]
		[Export ("keepTrace")]
		void KeepTrace ();

		[Abstract]
		[Export ("dropTrace")]
		void DropTrace ();

		[Abstract]
		[Export ("finish")]
		void Finish ();

		[Abstract]
		[Export ("finishWithTime:")]
		void FinishWithTime ([NullAllowed] NSDate finishTime);

		[Abstract]
		[Export ("setActive")]
		IOTSpan SetActive ();
	}

	partial interface IOTSpanContext {}

	// @protocol OTSpanContext
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface OTSpanContext
	{
		[Abstract]
		[Export ("forEachBaggageItem:")]
		void ForEachBaggageItem (Func<string, string, bool> callback);
	}

	partial interface IOTTracer {}

	// @protocol OTTracer
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface OTTracer
	{
		[Abstract]
		[Export ("startSpan:")]
		IOTSpan StartSpan (string operationName);

		[Abstract]
		[Export ("startSpan:tags:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] NSDictionary tags);

		[Abstract]
		[Export ("startSpan:childOf:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent);

		[Abstract]
		[Export ("startSpan:childOf:tags:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent, [NullAllowed] NSDictionary tags);

		[Abstract]
		[Export ("startSpan:childOf:tags:startTime:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent, [NullAllowed] NSDictionary tags, [NullAllowed] NSDate startTime);

		[Abstract]
		[Export ("startRootSpan:tags:startTime:customSampleRate:")]
		IOTSpan StartRootSpan (string operationName, [NullAllowed] NSDictionary tags, [NullAllowed] NSDate startTime, [NullAllowed] NSNumber customSampleRate);

		[Abstract]
		[Export ("inject:format:carrier:error:")]
		bool Inject (IOTSpanContext spanContext, string format, NSObject carrier, [NullAllowed] out NSError error);

		[Abstract]
		[Export ("extractWithFormat:carrier:error:")]
		bool ExtractWithFormat (string format, NSObject carrier, [NullAllowed] out NSError error);
	}

	// @interface DDB3HTTPHeadersWriter
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDB3HTTPHeadersWriter
	{
		[Export ("traceHeaderFields", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> TraceHeaderFields { get; }

		[Export ("initWithInjectEncoding:traceContextInjection:")]
		NativeHandle Constructor (DDInjectEncoding injectEncoding, DDTraceContextInjection traceContextInjection);
	}

	// @interface DDHTTPHeadersWriter
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDHTTPHeadersWriter
	{
		[Export ("traceHeaderFields", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> TraceHeaderFields { get; }

		[Export ("initWithTraceContextInjection:")]
		NativeHandle Constructor (DDTraceContextInjection traceContextInjection);
	}

	// @interface DDTrace
	[BaseType (typeof(NSObject))]
	interface DDTrace
	{
		[Static]
		[Export ("enableWith:")]
		void EnableWith (DDTraceConfiguration configuration);

		[Static]
		[Export ("enableWith:instanceName:")]
		void EnableWith (DDTraceConfiguration configuration, [NullAllowed] string instanceName);
	}

	// @interface DDTraceConfiguration
	[BaseType (typeof(NSObject))]
	interface DDTraceConfiguration
	{
		[Export ("sampleRate")]
		float SampleRate { get; set; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; set; }

		[NullAllowed, Export ("tags", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Tags { get; set; }

		[Export ("bundleWithRumEnabled")]
		bool BundleWithRumEnabled { get; set; }

		[Export ("networkInfoEnabled")]
		bool NetworkInfoEnabled { get; set; }

		[NullAllowed, Export ("customEndpoint", ArgumentSemantic.Copy)]
		NSUrl CustomEndpoint { get; set; }

		[Export ("setURLSessionTracking:")]
		void SetURLSessionTracking (DDTraceURLSessionTracking tracking);
	}

	// @interface DDTraceFirstPartyHostsTracing
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTraceFirstPartyHostsTracing
	{
		[Export ("initWithHostsWithHeaderTypes:")]
		NativeHandle Constructor (NSDictionary<NSString, NSSet<DDTracingHeaderType>> hostsWithHeaderTypes);

		[Export ("initWithHostsWithHeaderTypes:sampleRate:")]
		NativeHandle Constructor (NSDictionary<NSString, NSSet<DDTracingHeaderType>> hostsWithHeaderTypes, float sampleRate);

		[Export ("initWithHosts:")]
		NativeHandle Constructor (NSSet<NSString> hosts);

		[Export ("initWithHosts:sampleRate:")]
		NativeHandle Constructor (NSSet<NSString> hosts, float sampleRate);
	}

	// @interface DDTraceURLSessionTracking
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTraceURLSessionTracking
	{
		[Export ("initWithFirstPartyHostsTracing:")]
		NativeHandle Constructor (DDTraceFirstPartyHostsTracing firstPartyHostsTracing);

		[Export ("setFirstPartyHostsTracing:")]
		void SetFirstPartyHostsTracing (DDTraceFirstPartyHostsTracing firstPartyHostsTracing);

		[Export ("setRedactedStatusCodes:")]
		void SetRedactedStatusCodes (NSNumber[] codes);
	}

	// @interface DDTracer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTracer
	{
		[Static]
		[Export ("shared")]
		IOTTracer Shared ();

		[Static]
		[Export ("sharedWithInstanceName:")]
		IOTTracer SharedWithInstanceName ([NullAllowed] string instanceName);

		[Export ("startSpan:")]
		IOTSpan StartSpan (string operationName);

		[Export ("startSpan:tags:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] NSDictionary tags);

		[Export ("startSpan:childOf:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent);

		[Export ("startSpan:childOf:tags:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent, [NullAllowed] NSDictionary tags);

		[Export ("startSpan:childOf:tags:startTime:")]
		IOTSpan StartSpan (string operationName, [NullAllowed] IOTSpanContext parent, [NullAllowed] NSDictionary tags, [NullAllowed] NSDate startTime);

		[Export ("startRootSpan:tags:startTime:customSampleRate:")]
		IOTSpan StartRootSpan (string operationName, [NullAllowed] NSDictionary tags, [NullAllowed] NSDate startTime, [NullAllowed] NSNumber customSampleRate);

		[Export ("inject:format:carrier:error:")]
		bool Inject (IOTSpanContext spanContext, string format, NSObject carrier, [NullAllowed] out NSError error);

		[Export ("extractWithFormat:carrier:error:")]
		bool ExtractWithFormat (string format, NSObject carrier, [NullAllowed] out NSError error);
	}

	// @interface DDW3CHTTPHeadersWriter
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDW3CHTTPHeadersWriter
	{
		[Export ("traceHeaderFields", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> TraceHeaderFields { get; }

		[Export ("initWithTraceContextInjection:")]
		NativeHandle Constructor (DDTraceContextInjection traceContextInjection);
	}

	// @interface OT
	[BaseType (typeof(NSObject), Name = "_TtC12DatadogTrace2OT")]
	interface OT
	{
		[Static]
		[Export ("formatTextMap", ArgumentSemantic.Copy)]
		string FormatTextMap { get; }
	}
}
