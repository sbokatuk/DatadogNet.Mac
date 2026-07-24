using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;

namespace DatadogLogs
{
	// @interface DDLogEvent
	/// <summary>One log entry, as the event mapper sees it before upload.</summary>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEvent
	{
		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		[Export ("status")]
		DDLogEventStatus Status { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		DDLogEventError Error { get; }

		[Export ("serviceName", ArgumentSemantic.Copy)]
		string ServiceName { get; }

		[Export ("environment", ArgumentSemantic.Copy)]
		string Environment { get; }

		[Export ("loggerName", ArgumentSemantic.Copy)]
		string LoggerName { get; }

		[Export ("loggerVersion", ArgumentSemantic.Copy)]
		string LoggerVersion { get; }

		[NullAllowed, Export ("threadName", ArgumentSemantic.Copy)]
		string ThreadName { get; }

		[Export ("applicationVersion", ArgumentSemantic.Copy)]
		string ApplicationVersion { get; }

		[Export ("applicationBuildNumber", ArgumentSemantic.Copy)]
		string ApplicationBuildNumber { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("variant", ArgumentSemantic.Copy)]
		string Variant { get; }

		[Export ("dd", ArgumentSemantic.Strong)]
		DDLogEventDd Dd { get; }

		[Export ("device", ArgumentSemantic.Strong)]
		DDLogEventDevice Device { get; }

		[Export ("os", ArgumentSemantic.Strong)]
		DDLogEventOperatingSystem Os { get; }

		[Export ("userInfo", ArgumentSemantic.Strong)]
		DDLogEventUserInfo UserInfo { get; }

		[NullAllowed, Export ("accountInfo", ArgumentSemantic.Strong)]
		DDLogEventAccountInfo AccountInfo { get; }

		[NullAllowed, Export ("networkConnectionInfo", ArgumentSemantic.Strong)]
		DDLogEventNetworkConnectionInfo NetworkConnectionInfo { get; }

		[NullAllowed, Export ("mobileCarrierInfo", ArgumentSemantic.Strong)]
		DDLogEventCarrierInfo MobileCarrierInfo { get; }

		[Export ("attributes", ArgumentSemantic.Strong)]
		DDLogEventAttributes Attributes { get; }

		[NullAllowed, Export ("tags", ArgumentSemantic.Copy)]
		string[] Tags { get; set; }
	}

	// @interface DDLogEventAccountInfo
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventAccountInfo
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("extraInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ExtraInfo { get; set; }
	}

	// @interface DDLogEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventAttributes
	{
		[Export ("userAttributes", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UserAttributes { get; set; }
	}

	// @interface DDLogEventBinaryImage
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventBinaryImage
	{
		[NullAllowed, Export ("arch", ArgumentSemantic.Copy)]
		string Arch { get; }

		[Export ("isSystem")]
		bool IsSystem { get; }

		[NullAllowed, Export ("loadAddress", ArgumentSemantic.Copy)]
		string LoadAddress { get; }

		[NullAllowed, Export ("maxAddress", ArgumentSemantic.Copy)]
		string MaxAddress { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("uuid", ArgumentSemantic.Copy)]
		string Uuid { get; }
	}

	// @interface DDLogEventCarrierInfo
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventCarrierInfo
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("carrierISOCountryCode", ArgumentSemantic.Copy)]
		string CarrierISOCountryCode { get; }

		[Export ("carrierAllowsVOIP")]
		bool CarrierAllowsVOIP { get; }

		[Export ("radioAccessTechnology")]
		DDLogEventRadioAccessTechnology RadioAccessTechnology { get; }
	}

	// @interface DDLogEventDDDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventDDDevice
	{
		[Export ("architecture", ArgumentSemantic.Copy)]
		string Architecture { get; }
	}

	// @interface DDLogEventDd
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventDd
	{
		[Export ("device", ArgumentSemantic.Strong)]
		DDLogEventDDDevice Device { get; }
	}

	// @interface DDLogEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventDevice
	{
		[NullAllowed, Export ("architecture", ArgumentSemantic.Copy)]
		string Architecture { get; }

		[NullAllowed, Export ("batteryLevel", ArgumentSemantic.Strong)]
		NSNumber BatteryLevel { get; }

		[NullAllowed, Export ("brand", ArgumentSemantic.Copy)]
		string Brand { get; }

		[NullAllowed, Export ("brightnessLevel", ArgumentSemantic.Strong)]
		NSNumber BrightnessLevel { get; }

		[NullAllowed, Export ("isLowRam", ArgumentSemantic.Strong)]
		NSNumber IsLowRam { get; }

		[NullAllowed, Export ("locale", ArgumentSemantic.Copy)]
		string Locale { get; }

		[NullAllowed, Export ("locales", ArgumentSemantic.Copy)]
		string[] Locales { get; }

		[NullAllowed, Export ("logicalCpuCount", ArgumentSemantic.Strong)]
		NSNumber LogicalCpuCount { get; }

		[NullAllowed, Export ("model", ArgumentSemantic.Copy)]
		string Model { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("powerSavingMode", ArgumentSemantic.Strong)]
		NSNumber PowerSavingMode { get; }

		[NullAllowed, Export ("timeZone", ArgumentSemantic.Copy)]
		string TimeZone { get; }

		[NullAllowed, Export ("totalRam", ArgumentSemantic.Strong)]
		NSNumber TotalRam { get; }

		[Export ("type")]
		DDLogEventDeviceDeviceType Type { get; }
	}

	// @interface DDLogEventError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventError
	{
		[NullAllowed, Export ("kind", ArgumentSemantic.Copy)]
		string Kind { get; set; }

		[NullAllowed, Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		[NullAllowed, Export ("stack", ArgumentSemantic.Copy)]
		string Stack { get; set; }

		[Export ("sourceType", ArgumentSemantic.Copy)]
		string SourceType { get; set; }

		[NullAllowed, Export ("fingerprint", ArgumentSemantic.Copy)]
		string Fingerprint { get; set; }

		[NullAllowed, Export ("binaryImages", ArgumentSemantic.Copy)]
		DDLogEventBinaryImage[] BinaryImages { get; set; }
	}

	// @interface DDLogEventNetworkConnectionInfo
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventNetworkConnectionInfo
	{
		[Export ("reachability")]
		DDLogEventReachability Reachability { get; }

		[NullAllowed, Export ("availableInterfaces", ArgumentSemantic.Copy)]
		NSNumber[] AvailableInterfaces { get; }

		[NullAllowed, Export ("supportsIPv4", ArgumentSemantic.Strong)]
		NSNumber SupportsIPv4 { get; }

		[NullAllowed, Export ("supportsIPv6", ArgumentSemantic.Strong)]
		NSNumber SupportsIPv6 { get; }

		[NullAllowed, Export ("isExpensive", ArgumentSemantic.Strong)]
		NSNumber IsExpensive { get; }

		[NullAllowed, Export ("isConstrained", ArgumentSemantic.Strong)]
		NSNumber IsConstrained { get; }

		[Export ("linkQuality")]
		DDLogEventLinkQuality LinkQuality { get; }
	}

	// @interface DDLogEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventOperatingSystem
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDLogEventUserInfo
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogEventUserInfo
	{
		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[Export ("extraInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ExtraInfo { get; set; }
	}

	// @interface DDLogger
	/// <summary>Sends logs to Datadog, one method group per level. The Additions layer's <c>Create(...)</c> is the ergonomic factory, and its <c>Log(level, message, exception, attributes)</c> accepts <see langword="null"/> where these raw methods, faithful to upstream, do not.</summary>
	//
	// The error/attributes parameters on the per-level methods below carry no [NullAllowed], and
	// that is deliberate, not an omission: upstream declares them _Nonnull, and the Swift
	// implementation takes a non-optional [String: Any] (Logs+objc.swift), so a nil smuggled
	// through a [NullAllowed] binding would trap in the bridging thunk - a native crash instead of
	// today's managed ArgumentNullException. A caller with no attributes uses the message-only
	// overload, or the Additions layer's Log(level, message, exception?, attributes?), which
	// accepts null for both and converts.
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDLogger
	{
		[Export ("debug:")]
		void Debug (string message);

		[Export ("debug:attributes:")]
		void Debug (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("debug:error:attributes:")]
		void Debug (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("info:")]
		void Info (string message);

		[Export ("info:attributes:")]
		void Info (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("info:error:attributes:")]
		void Info (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("notice:")]
		void Notice (string message);

		[Export ("notice:attributes:")]
		void Notice (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("notice:error:attributes:")]
		void Notice (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("warn:")]
		void Warn (string message);

		[Export ("warn:attributes:")]
		void Warn (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("warn:error:attributes:")]
		void Warn (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("error:")]
		void Error (string message);

		[Export ("error:attributes:")]
		void Error (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("error:error:attributes:")]
		void Error (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("critical:")]
		void Critical (string message);

		[Export ("critical:attributes:")]
		void Critical (string message, NSDictionary<NSString, NSObject> attributes);

		[Export ("critical:error:attributes:")]
		void Critical (string message, NSError error, NSDictionary<NSString, NSObject> attributes);

		[Export ("addAttributeForKey:value:")]
		void AddAttributeForKey (string key, NSObject value);

		[Export ("removeAttributeForKey:")]
		void RemoveAttributeForKey (string key);

		[Export ("addTagWithKey:value:")]
		void AddTagWithKey (string key, string value);

		[Export ("removeTagWithKey:")]
		void RemoveTagWithKey (string key);

		[Export ("addWithTag:")]
		void AddWithTag (string tag);

		[Export ("removeWithTag:")]
		void RemoveWithTag (string tag);

		[Static]
		[Export ("createWith:")]
		DDLogger CreateWith (DDLoggerConfiguration configuration);

		[Static]
		[Export ("createWith:instanceName:")]
		DDLogger CreateWith (DDLoggerConfiguration configuration, [NullAllowed] string instanceName);
	}

	// @interface DDLoggerConfiguration
	/// <summary>How one logger reports: service, name, network info, RUM and Trace bundling, remote sampling and threshold, console echo.</summary>
	[BaseType (typeof(NSObject))]
	interface DDLoggerConfiguration
	{
		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; set; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[Export ("networkInfoEnabled")]
		bool NetworkInfoEnabled { get; set; }

		[Export ("bundleWithRumEnabled")]
		bool BundleWithRumEnabled { get; set; }

		[Export ("bundleWithTraceEnabled")]
		bool BundleWithTraceEnabled { get; set; }

		[Export ("remoteSampleRate")]
		float RemoteSampleRate { get; set; }

		[Export ("printLogsToConsole")]
		bool PrintLogsToConsole { get; set; }

		[Export ("remoteLogThreshold")]
		DDLogLevel RemoteLogThreshold { get; set; }

		[Export ("initWithService:name:networkInfoEnabled:bundleWithRumEnabled:bundleWithTraceEnabled:remoteSampleRate:remoteLogThreshold:printLogsToConsole:")]
		NativeHandle Constructor ([NullAllowed] string service, [NullAllowed] string name, bool networkInfoEnabled, bool bundleWithRumEnabled, bool bundleWithTraceEnabled, float remoteSampleRate, DDLogLevel remoteLogThreshold, bool printLogsToConsole);
	}

	// @interface DDLogs
	/// <summary>Enables log collection. <c>EnableWith</c> once, after core initialisation and before creating loggers; the static attribute methods apply to every logger.</summary>
	[BaseType (typeof(NSObject))]
	interface DDLogs
	{
		[Static]
		[Export ("enableWith:")]
		void EnableWith (DDLogsConfiguration configuration);

		[Static]
		[Export ("enableWith:instanceName:")]
		void EnableWith (DDLogsConfiguration configuration, [NullAllowed] string instanceName);

		[Static]
		[Export ("addAttributeForKey:value:")]
		void AddAttributeForKey (string key, NSObject value);

		[Static]
		[Export ("addAttributeForKey:value:instanceName:")]
		void AddAttributeForKey (string key, NSObject value, [NullAllowed] string instanceName);

		[Static]
		[Export ("removeAttributeForKey:")]
		void RemoveAttributeForKey (string key);

		[Static]
		[Export ("removeAttributeForKey:instanceName:")]
		void RemoveAttributeForKey (string key, [NullAllowed] string instanceName);
	}

	// @interface DDLogsConfiguration
	/// <summary>Options for <c>DDLogs.EnableWith</c>: a custom endpoint, and <c>SetEventMapper</c> - the on-device hook that can redact a log or return <see langword="null"/> to drop it.</summary>
	[BaseType (typeof(NSObject))]
	interface DDLogsConfiguration
	{
		[NullAllowed, Export ("customEndpoint", ArgumentSemantic.Copy)]
		NSUrl CustomEndpoint { get; set; }

		[Export ("initWithCustomEndpoint:")]
		NativeHandle Constructor ([NullAllowed] NSUrl customEndpoint);

		[Export ("setEventMapper:")]
		void SetEventMapper (Func<DDLogEvent, DDLogEvent> mapper);
	}
}
