using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace DatadogRUM
{
	partial interface IDDNetworkSettledResourcePredicate {}

	// @protocol DDNetworkSettledResourcePredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDNetworkSettledResourcePredicate
	{
		[Abstract]
		[Export ("isInitialResourceFrom:")]
		bool IsInitialResourceFrom (DDTNSResourceParams resourceParams);
	}

	partial interface IDDNextViewActionPredicate {}

	// @protocol DDNextViewActionPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDNextViewActionPredicate
	{
		[Abstract]
		[Export ("isLastActionFrom:")]
		bool IsLastActionFrom (DDINVActionParams actionParams);
	}

	partial interface IDDSwiftUIRUMActionsPredicate {}

	// @protocol DDSwiftUIRUMActionsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDSwiftUIRUMActionsPredicate
	{
		[Abstract]
		[Export ("rumActionWith:")]
		DDRUMAction RumActionWith (string componentName);
	}

	partial interface IDDSwiftUIRUMViewsPredicate {}

	// @protocol DDSwiftUIRUMViewsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDSwiftUIRUMViewsPredicate
	{
		[Abstract]
		[Export ("rumViewFor:")]
		DDRUMView RumViewFor (string extractedViewName);
	}

	partial interface IDDUIKitRUMActionsPredicate {}

	// @protocol DDUIKitRUMActionsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDUIKitRUMActionsPredicate
	{
	}

	partial interface IDDUIKitRUMViewsPredicate {}

	// @protocol DDUIKitRUMViewsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDUIKitRUMViewsPredicate
	{
		[Abstract]
		[Export ("rumViewFor:")]
		DDRUMView RumViewFor (UIViewController viewController);
	}

	partial interface IDDUIPressRUMActionsPredicate {}

	// @protocol DDUIPressRUMActionsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDUIPressRUMActionsPredicate
	{
		[Abstract]
		[Export ("rumActionWithPress:targetView:")]
		DDRUMAction RumActionWithPress (UIPressType type, UIView targetView);
	}

	partial interface IDDUITouchRUMActionsPredicate {}

	// @protocol DDUITouchRUMActionsPredicate
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDUITouchRUMActionsPredicate
	{
		[Abstract]
		[Export ("rumActionWithTargetView:")]
		DDRUMAction RumActionWithTargetView (UIView targetView);
	}

	// @interface DDDefaultSwiftUIRUMActionsPredicate
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDDefaultSwiftUIRUMActionsPredicate : DDSwiftUIRUMActionsPredicate
	{
		[Export ("initWithIsLegacyDetectionEnabled:")]
		NativeHandle Constructor (bool isLegacyDetectionEnabled);

		[Export ("rumActionWith:")]
		DDRUMAction RumActionWith (string componentName);
	}

	// @interface DDDefaultSwiftUIRUMViewsPredicate
	[BaseType (typeof(NSObject))]
	interface DDDefaultSwiftUIRUMViewsPredicate : DDSwiftUIRUMViewsPredicate
	{
		[Export ("rumViewFor:")]
		DDRUMView RumViewFor (string extractedViewName);
	}

	// @interface DDDefaultUIKitRUMActionsPredicate
	[BaseType (typeof(NSObject))]
	interface DDDefaultUIKitRUMActionsPredicate : DDUIKitRUMActionsPredicate
	{
		[Export ("rumActionWithTargetView:")]
		DDRUMAction RumActionWithTargetView (UIView targetView);
	}

	// @interface DDDefaultUIKitRUMViewsPredicate
	[BaseType (typeof(NSObject))]
	interface DDDefaultUIKitRUMViewsPredicate : DDUIKitRUMViewsPredicate
	{
		[Export ("rumViewFor:")]
		DDRUMView RumViewFor (UIViewController viewController);
	}

	// @interface DDINVActionParams
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDINVActionParams
	{
		[Export ("type")]
		DDRUMActionType Type { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("timeToNextView")]
		double TimeToNextView { get; }

		[Export ("nextViewName", ArgumentSemantic.Copy)]
		string NextViewName { get; }
	}

	// @interface DDOperationOptions
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDOperationOptions
	{
	}

	// @interface DDProfilingOptions
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDProfilingOptions
	{
		[Export ("initWithSampleRate:")]
		NativeHandle Constructor (float sampleRate);
	}

	// @interface DDRUM
	[BaseType (typeof(NSObject))]
	interface DDRUM
	{
		[Static]
		[Export ("enableWith:")]
		void EnableWith (DDRUMConfiguration configuration);

		[Static]
		[Export ("enableWith:instanceName:")]
		void EnableWith (DDRUMConfiguration configuration, [NullAllowed] string instanceName);
	}

	// @interface DDRUMAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMAction
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("attributes", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Attributes { get; }

		[Export ("initWithName:attributes:")]
		NativeHandle Constructor (string name, NSDictionary<NSString, NSObject> attributes);
	}

	// @interface DDRUMActionEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMActionEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMAccount Account { get; }

		[Export ("action", ArgumentSemantic.Strong)]
		DDRUMActionEventAction Action { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMActionEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMActionEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMActionEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMActionEventDisplay Display { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMActionEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMActionEventSession Session { get; }

		[Export ("source")]
		DDRUMActionEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMActionEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMActionEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMActionEventView View { get; }
	}

	// @interface DDRUMActionEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventAction
	{
		[NullAllowed, Export ("crash", ArgumentSemantic.Strong)]
		DDRUMActionEventActionCrash Crash { get; }

		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		DDRUMActionEventActionError Error { get; }

		[NullAllowed, Export ("frustration", ArgumentSemantic.Strong)]
		DDRUMActionEventActionFrustration Frustration { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("loadingTime", ArgumentSemantic.Strong)]
		NSNumber LoadingTime { get; }

		[NullAllowed, Export ("longTask", ArgumentSemantic.Strong)]
		DDRUMActionEventActionLongTask LongTask { get; }

		[NullAllowed, Export ("resource", ArgumentSemantic.Strong)]
		DDRUMActionEventActionResource Resource { get; }

		[NullAllowed, Export ("target", ArgumentSemantic.Strong)]
		DDRUMActionEventActionTarget Target { get; }

		[Export ("type")]
		DDRUMActionEventActionActionType Type { get; }
	}

	// @interface DDRUMActionEventActionCrash
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionCrash
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMActionEventActionError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionError
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMActionEventActionFrustration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionFrustration
	{
		[Export ("type", ArgumentSemantic.Copy)]
		NSNumber[] Type { get; }
	}

	// @interface DDRUMActionEventActionLongTask
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionLongTask
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMActionEventActionResource
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionResource
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMActionEventActionTarget
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventActionTarget
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }
	}

	// @interface DDRUMActionEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMActionEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventContainer
	{
		[Export ("source")]
		DDRUMActionEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMActionEventContainerView View { get; }
	}

	// @interface DDRUMActionEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMActionEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDD
	{
		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDRUMActionEventDDAction Action { get; }

		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMActionEventDDConfiguration Configuration { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMActionEventDDSession Session { get; }
	}

	// @interface DDRUMActionEventDDAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDDAction
	{
		[Export ("nameSource")]
		DDRUMActionEventDDActionNameSource NameSource { get; set; }

		[NullAllowed, Export ("position", ArgumentSemantic.Strong)]
		DDRUMActionEventDDActionPosition Position { get; }

		[NullAllowed, Export ("target", ArgumentSemantic.Strong)]
		DDRUMActionEventDDActionTarget Target { get; }
	}

	// @interface DDRUMActionEventDDActionPosition
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDDActionPosition
	{
		[Export ("x", ArgumentSemantic.Strong)]
		NSNumber X { get; }

		[Export ("y", ArgumentSemantic.Strong)]
		NSNumber Y { get; }
	}

	// @interface DDRUMActionEventDDActionTarget
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDDActionTarget
	{
		[NullAllowed, Export ("composedPathSelector", ArgumentSemantic.Copy)]
		string ComposedPathSelector { get; }

		[NullAllowed, Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[NullAllowed, Export ("permanentId", ArgumentSemantic.Copy)]
		string PermanentId { get; }

		[NullAllowed, Export ("selector", ArgumentSemantic.Copy)]
		string SelectorName { get; }

		[NullAllowed, Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMActionEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMActionEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDDSession
	{
		[Export ("plan")]
		DDRUMActionEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMActionEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMActionEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDevice
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
		DDRUMActionEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMActionEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMActionEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMActionEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMActionEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMActionEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMActionEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMActionEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMActionEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMActionEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMActionEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMActionEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMActionEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMActionEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMActionEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMActionEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMActionEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMActionEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMActionEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMActionEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMActionEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("inForeground", ArgumentSemantic.Strong)]
		NSNumber InForeground { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMConfiguration
	{
		[Export ("applicationID", ArgumentSemantic.Copy)]
		string ApplicationID { get; }

		[Export ("sessionSampleRate")]
		float SessionSampleRate { get; set; }

		[Export ("telemetrySampleRate")]
		float TelemetrySampleRate { get; set; }

		[NullAllowed, Export ("uiKitViewsPredicate", ArgumentSemantic.Strong)]
		IDDUIKitRUMViewsPredicate UiKitViewsPredicate { get; set; }

		[NullAllowed, Export ("uiKitActionsPredicate", ArgumentSemantic.Strong)]
		IDDUIKitRUMActionsPredicate UiKitActionsPredicate { get; set; }

		[NullAllowed, Export ("swiftUIViewsPredicate", ArgumentSemantic.Strong)]
		IDDSwiftUIRUMViewsPredicate SwiftUIViewsPredicate { get; set; }

		[NullAllowed, Export ("swiftUIActionsPredicate", ArgumentSemantic.Strong)]
		IDDSwiftUIRUMActionsPredicate SwiftUIActionsPredicate { get; set; }

		[Export ("trackMemoryWarnings")]
		bool TrackMemoryWarnings { get; set; }

		[Export ("collectAccessibility")]
		bool CollectAccessibility { get; set; }

		[Export ("networkSettledResourcePredicate", ArgumentSemantic.Strong)]
		IDDNetworkSettledResourcePredicate NetworkSettledResourcePredicate { get; set; }

		[NullAllowed, Export ("nextViewActionPredicate", ArgumentSemantic.Strong)]
		IDDNextViewActionPredicate NextViewActionPredicate { get; set; }

		[Export ("trackFrustrations")]
		bool TrackFrustrations { get; set; }

		[Export ("trackBackgroundEvents")]
		bool TrackBackgroundEvents { get; set; }

		[Export ("trackWatchdogTerminations")]
		bool TrackWatchdogTerminations { get; set; }

		[Export ("longTaskThreshold")]
		double LongTaskThreshold { get; set; }

		[Export ("appHangThreshold")]
		double AppHangThreshold { get; set; }

		[Export ("vitalsUpdateFrequency")]
		DDRUMVitalsFrequency VitalsUpdateFrequency { get; set; }

		[NullAllowed, Export ("customEndpoint", ArgumentSemantic.Copy)]
		NSUrl CustomEndpoint { get; set; }

		[Export ("trackAnonymousUser")]
		bool TrackAnonymousUser { get; set; }

		[Export ("initWithApplicationID:")]
		NativeHandle Constructor (string applicationID);

		[Export ("setURLSessionTracking:")]
		void SetURLSessionTracking (DDRUMURLSessionTracking tracking);

		[Export ("setViewEventMapper:")]
		void SetViewEventMapper (Func<DDRUMViewEvent, DDRUMViewEvent> mapper);

		[Export ("setResourceEventMapper:")]
		void SetResourceEventMapper (Func<DDRUMResourceEvent, DDRUMResourceEvent> mapper);

		[Export ("setActionEventMapper:")]
		void SetActionEventMapper (Func<DDRUMActionEvent, DDRUMActionEvent> mapper);

		[Export ("setErrorEventMapper:")]
		void SetErrorEventMapper (Func<DDRUMErrorEvent, DDRUMErrorEvent> mapper);

		[Export ("setLongTaskEventMapper:")]
		void SetLongTaskEventMapper (Func<DDRUMLongTaskEvent, DDRUMLongTaskEvent> mapper);
	}

	// @interface DDRUMErrorEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMErrorEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMAccount Account { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDRUMErrorEventAction Action { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMErrorEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMErrorEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMErrorEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMErrorEventDisplay Display { get; }

		[Export ("error", ArgumentSemantic.Strong)]
		DDRUMErrorEventError Error { get; }

		[NullAllowed, Export ("featureFlags", ArgumentSemantic.Strong)]
		DDRUMErrorEventFeatureFlags FeatureFlags { get; }

		[NullAllowed, Export ("freeze", ArgumentSemantic.Strong)]
		DDRUMErrorEventFreeze Freeze { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMErrorEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMErrorEventSession Session { get; }

		[Export ("source")]
		DDRUMErrorEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMErrorEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMErrorEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMErrorEventView View { get; }
	}

	// @interface DDRUMErrorEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventAction
	{
		[Export ("id", ArgumentSemantic.Strong)]
		DDRUMErrorEventActionRUMActionID Id { get; }
	}

	// @interface DDRUMErrorEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDRUMErrorEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMErrorEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventContainer
	{
		[Export ("source")]
		DDRUMErrorEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMErrorEventContainerView View { get; }
	}

	// @interface DDRUMErrorEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMErrorEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMErrorEventDDConfiguration Configuration { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("parentSpanId", ArgumentSemantic.Copy)]
		string ParentSpanId { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMErrorEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("rulePsr", ArgumentSemantic.Strong)]
		NSNumber RulePsr { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMErrorEventDDSession Session { get; }

		[NullAllowed, Export ("spanId", ArgumentSemantic.Copy)]
		string SpanId { get; }

		[NullAllowed, Export ("traceId", ArgumentSemantic.Copy)]
		string TraceId { get; }
	}

	// @interface DDRUMErrorEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMErrorEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMErrorEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMErrorEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMErrorEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMErrorEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDDSession
	{
		[Export ("plan")]
		DDRUMErrorEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMErrorEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMErrorEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDevice
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
		DDRUMErrorEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMErrorEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMErrorEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMErrorEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMErrorEventError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventError
	{
		[NullAllowed, Export ("binaryImages", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorBinaryImages[] BinaryImages { get; }

		[Export ("category")]
		DDRUMErrorEventErrorCategory Category { get; }

		[NullAllowed, Export ("causes", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorCauses[] Causes { get; set; }

		[NullAllowed, Export ("csp", ArgumentSemantic.Strong)]
		DDRUMErrorEventErrorCSP Csp { get; }

		[NullAllowed, Export ("fingerprint", ArgumentSemantic.Copy)]
		string Fingerprint { get; set; }

		[Export ("handling")]
		DDRUMErrorEventErrorHandling Handling { get; }

		[NullAllowed, Export ("handlingStack", ArgumentSemantic.Copy)]
		string HandlingStack { get; set; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("isCrash", ArgumentSemantic.Strong)]
		NSNumber IsCrash { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		[NullAllowed, Export ("meta", ArgumentSemantic.Strong)]
		DDRUMErrorEventErrorMetaInfo Meta { get; }

		[NullAllowed, Export ("resource", ArgumentSemantic.Strong)]
		DDRUMErrorEventErrorResource Resource { get; }

		[Export ("source")]
		DDRUMErrorEventErrorSource Source { get; }

		[Export ("sourceType")]
		DDRUMErrorEventErrorSourceType SourceType { get; }

		[NullAllowed, Export ("stack", ArgumentSemantic.Copy)]
		string Stack { get; set; }

		[NullAllowed, Export ("threads", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorThreads[] Threads { get; }

		[NullAllowed, Export ("timeSinceAppStart", ArgumentSemantic.Strong)]
		NSNumber TimeSinceAppStart { get; }

		[NullAllowed, Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("wasTruncated", ArgumentSemantic.Strong)]
		NSNumber WasTruncated { get; }
	}

	// @interface DDRUMErrorEventErrorBinaryImages
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorBinaryImages
	{
		[NullAllowed, Export ("arch", ArgumentSemantic.Copy)]
		string Arch { get; }

		[Export ("isSystem", ArgumentSemantic.Strong)]
		NSNumber IsSystem { get; }

		[NullAllowed, Export ("loadAddress", ArgumentSemantic.Copy)]
		string LoadAddress { get; }

		[NullAllowed, Export ("maxAddress", ArgumentSemantic.Copy)]
		string MaxAddress { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("uuid", ArgumentSemantic.Copy)]
		string Uuid { get; }
	}

	// @interface DDRUMErrorEventErrorCSP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorCSP
	{
		[Export ("disposition")]
		DDRUMErrorEventErrorCSPDisposition Disposition { get; }
	}

	// @interface DDRUMErrorEventErrorCauses
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorCauses
	{
		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; set; }

		[Export ("source")]
		DDRUMErrorEventErrorCausesSource Source { get; }

		[NullAllowed, Export ("stack", ArgumentSemantic.Copy)]
		string Stack { get; set; }

		[NullAllowed, Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }
	}

	// @interface DDRUMErrorEventErrorMetaInfo
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorMetaInfo
	{
		[NullAllowed, Export ("codeType", ArgumentSemantic.Copy)]
		string CodeType { get; }

		[NullAllowed, Export ("exceptionCodes", ArgumentSemantic.Copy)]
		string ExceptionCodes { get; }

		[NullAllowed, Export ("exceptionType", ArgumentSemantic.Copy)]
		string ExceptionType { get; }

		[NullAllowed, Export ("incidentIdentifier", ArgumentSemantic.Copy)]
		string IncidentIdentifier { get; }

		[NullAllowed, Export ("parentProcess", ArgumentSemantic.Copy)]
		string ParentProcess { get; }

		[NullAllowed, Export ("path", ArgumentSemantic.Copy)]
		string Path { get; }

		[NullAllowed, Export ("process", ArgumentSemantic.Copy)]
		string Process { get; }
	}

	// @interface DDRUMErrorEventErrorResource
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResource
	{
		[NullAllowed, Export ("graphql", ArgumentSemantic.Strong)]
		DDRUMErrorEventErrorResourceRUMGraphql Graphql { get; }

		[Export ("method")]
		DDRUMErrorEventErrorResourceRUMMethod Method { get; }

		[NullAllowed, Export ("provider", ArgumentSemantic.Strong)]
		DDRUMErrorEventErrorResourceProvider Provider { get; }

		[Export ("statusCode", ArgumentSemantic.Strong)]
		NSNumber StatusCode { get; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMErrorEventErrorResourceProvider
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResourceProvider
	{
		[NullAllowed, Export ("domain", ArgumentSemantic.Copy)]
		string Domain { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("type")]
		DDRUMErrorEventErrorResourceProviderProviderType Type { get; }
	}

	// @interface DDRUMErrorEventErrorResourceRUMGraphql
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResourceRUMGraphql
	{
		[NullAllowed, Export ("errorCount", ArgumentSemantic.Strong)]
		NSNumber ErrorCount { get; }

		[NullAllowed, Export ("errors", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorResourceRUMGraphqlErrors[] Errors { get; }

		[NullAllowed, Export ("operationName", ArgumentSemantic.Copy)]
		string OperationName { get; }

		[Export ("operationType")]
		DDRUMErrorEventErrorResourceRUMGraphqlOperationType OperationType { get; }

		[NullAllowed, Export ("payload", ArgumentSemantic.Copy)]
		string Payload { get; set; }

		[NullAllowed, Export ("variables", ArgumentSemantic.Copy)]
		string Variables { get; set; }
	}

	// @interface DDRUMErrorEventErrorResourceRUMGraphqlErrors
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResourceRUMGraphqlErrors
	{
		[NullAllowed, Export ("code", ArgumentSemantic.Copy)]
		string Code { get; }

		[NullAllowed, Export ("locations", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorResourceRUMGraphqlErrorsLocations[] Locations { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; }

		[NullAllowed, Export ("path", ArgumentSemantic.Copy)]
		DDRUMErrorEventErrorResourceRUMGraphqlErrorsPath[] Path { get; }
	}

	// @interface DDRUMErrorEventErrorResourceRUMGraphqlErrorsLocations
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResourceRUMGraphqlErrorsLocations
	{
		[Export ("column", ArgumentSemantic.Strong)]
		NSNumber Column { get; }

		[Export ("line", ArgumentSemantic.Strong)]
		NSNumber Line { get; }
	}

	// @interface DDRUMErrorEventErrorResourceRUMGraphqlErrorsPath
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorResourceRUMGraphqlErrorsPath
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("integer", ArgumentSemantic.Strong)]
		NSNumber Integer { get; }
	}

	// @interface DDRUMErrorEventErrorThreads
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventErrorThreads
	{
		[Export ("crashed", ArgumentSemantic.Strong)]
		NSNumber Crashed { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("stack", ArgumentSemantic.Copy)]
		string Stack { get; }

		[NullAllowed, Export ("state", ArgumentSemantic.Copy)]
		string State { get; }
	}

	// @interface DDRUMErrorEventFeatureFlags
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventFeatureFlags
	{
		[Export ("featureFlagsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> FeatureFlagsInfo { get; set; }
	}

	// @interface DDRUMErrorEventFreeze
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventFreeze
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }
	}

	// @interface DDRUMErrorEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMErrorEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMErrorEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMErrorEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMErrorEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMErrorEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMErrorEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMErrorEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMErrorEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMErrorEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMErrorEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMErrorEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMErrorEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMErrorEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMErrorEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMErrorEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMErrorEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("inForeground", ArgumentSemantic.Strong)]
		NSNumber InForeground { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMFirstPartyHostsTracing
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMFirstPartyHostsTracing
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

	// @interface DDRUMHeaderCaptureRule
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMHeaderCaptureRule
	{
		[Static]
		[Export ("defaults", ArgumentSemantic.Strong)]
		DDRUMHeaderCaptureRule Defaults { get; }

		[Static]
		[Export ("matchHeaders:")]
		DDRUMHeaderCaptureRule MatchHeaders (string[] names);
	}

	// @interface DDRUMLongTaskEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMAccount Account { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventAction Action { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDisplay Display { get; }

		[Export ("longTask", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventLongTask LongTask { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventSession Session { get; }

		[Export ("source")]
		DDRUMLongTaskEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventView View { get; }
	}

	// @interface DDRUMLongTaskEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventAction
	{
		[Export ("id", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventActionRUMActionID Id { get; }
	}

	// @interface DDRUMLongTaskEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDRUMLongTaskEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMLongTaskEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventContainer
	{
		[Export ("source")]
		DDRUMLongTaskEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventContainerView View { get; }
	}

	// @interface DDRUMLongTaskEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMLongTaskEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDDConfiguration Configuration { get; }

		[NullAllowed, Export ("discarded", ArgumentSemantic.Strong)]
		NSNumber Discarded { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDDSession Session { get; }
	}

	// @interface DDRUMLongTaskEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMLongTaskEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMLongTaskEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMLongTaskEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMLongTaskEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMLongTaskEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDDSession
	{
		[Export ("plan")]
		DDRUMLongTaskEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMLongTaskEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMLongTaskEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDevice
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
		DDRUMLongTaskEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMLongTaskEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMLongTaskEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMLongTaskEventLongTask
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventLongTask
	{
		[NullAllowed, Export ("blockingDuration", ArgumentSemantic.Strong)]
		NSNumber BlockingDuration { get; }

		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("entryType")]
		DDRUMLongTaskEventLongTaskEntryType EntryType { get; }

		[NullAllowed, Export ("firstUiEventTimestamp", ArgumentSemantic.Strong)]
		NSNumber FirstUiEventTimestamp { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("isFrozenFrame", ArgumentSemantic.Strong)]
		NSNumber IsFrozenFrame { get; }

		[NullAllowed, Export ("renderStart", ArgumentSemantic.Strong)]
		NSNumber RenderStart { get; }

		[NullAllowed, Export ("scripts", ArgumentSemantic.Copy)]
		DDRUMLongTaskEventLongTaskScripts[] Scripts { get; }

		[NullAllowed, Export ("startTime", ArgumentSemantic.Strong)]
		NSNumber StartTime { get; }

		[NullAllowed, Export ("styleAndLayoutStart", ArgumentSemantic.Strong)]
		NSNumber StyleAndLayoutStart { get; }
	}

	// @interface DDRUMLongTaskEventLongTaskScripts
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventLongTaskScripts
	{
		[NullAllowed, Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("executionStart", ArgumentSemantic.Strong)]
		NSNumber ExecutionStart { get; }

		[NullAllowed, Export ("forcedStyleAndLayoutDuration", ArgumentSemantic.Strong)]
		NSNumber ForcedStyleAndLayoutDuration { get; }

		[NullAllowed, Export ("invoker", ArgumentSemantic.Copy)]
		string Invoker { get; }

		[Export ("invokerType")]
		DDRUMLongTaskEventLongTaskScriptsInvokerType InvokerType { get; }

		[NullAllowed, Export ("pauseDuration", ArgumentSemantic.Strong)]
		NSNumber PauseDuration { get; }

		[NullAllowed, Export ("sourceCharPosition", ArgumentSemantic.Strong)]
		NSNumber SourceCharPosition { get; }

		[NullAllowed, Export ("sourceFunctionName", ArgumentSemantic.Copy)]
		string SourceFunctionName { get; }

		[NullAllowed, Export ("sourceUrl", ArgumentSemantic.Copy)]
		string SourceUrl { get; }

		[NullAllowed, Export ("startTime", ArgumentSemantic.Strong)]
		NSNumber StartTime { get; }

		[NullAllowed, Export ("windowAttribution", ArgumentSemantic.Copy)]
		string WindowAttribution { get; }
	}

	// @interface DDRUMLongTaskEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMLongTaskEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMLongTaskEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMLongTaskEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMLongTaskEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMLongTaskEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMLongTaskEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMLongTaskEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMLongTaskEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMLongTaskEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMLongTaskEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMLongTaskEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMLongTaskEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMLongTaskEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMLongTaskEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMLongTaskEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMLongTaskEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMMonitor
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMMonitor
	{
		[Export ("debug")]
		bool Debug { get; set; }

		[Static]
		[Export ("shared")]
		DDRUMMonitor Shared ();

		[Static]
		[Export ("sharedWithInstanceName:")]
		DDRUMMonitor SharedWithInstanceName ([NullAllowed] string instanceName);

		[Export ("currentSessionIDWithCompletion:")]
		void CurrentSessionIDWithCompletion (Action<string> completion);

		[Export ("stopSession")]
		void StopSession ();

		[Export ("reportAppFullyDisplayed")]
		void ReportAppFullyDisplayed ();

		[Export ("addViewAttributeForKey:value:")]
		void AddViewAttributeForKey (string key, NSObject value);

		[Export ("addViewAttributes:")]
		void AddViewAttributes (NSDictionary<NSString, NSObject> attributes);

		[Export ("removeViewAttributeForKey:")]
		void RemoveViewAttributeForKey (string key);

		[Export ("removeViewAttributesForKeys:")]
		void RemoveViewAttributesForKeys (string[] keys);

		[Export ("startViewWithViewController:name:attributes:")]
		void StartViewWithViewController (UIViewController viewController, [NullAllowed] string name, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopViewWithViewController:attributes:")]
		void StopViewWithViewController (UIViewController viewController, NSDictionary<NSString, NSObject> attributes);

		[Export ("startViewWithKey:name:attributes:")]
		void StartViewWithKey (string key, [NullAllowed] string name, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopViewWithKey:attributes:")]
		void StopViewWithKey (string key, NSDictionary<NSString, NSObject> attributes);

		[Export ("addViewLoadingTimeWithOverwrite:")]
		void AddViewLoadingTimeWithOverwrite (bool overwrite);

		[Export ("addTimingWithName:")]
		void AddTimingWithName (string name);

		[Export ("addErrorWithMessage:stack:source:attributes:")]
		void AddErrorWithMessage (string message, [NullAllowed] string stack, DDRUMErrorSource source, NSDictionary<NSString, NSObject> attributes);

		[Export ("addErrorWithError:source:attributes:")]
		void AddErrorWithError (NSError error, DDRUMErrorSource source, NSDictionary<NSString, NSObject> attributes);

		[Export ("startResourceWithResourceKey:request:attributes:")]
		void StartResourceWithResourceKey (string resourceKey, NSUrlRequest request, NSDictionary<NSString, NSObject> attributes);

		[Export ("startResourceWithResourceKey:url:attributes:")]
		void StartResourceWithResourceKey (string resourceKey, NSUrl url, NSDictionary<NSString, NSObject> attributes);

		[Export ("startResourceWithResourceKey:httpMethod:urlString:attributes:")]
		void StartResourceWithResourceKey (string resourceKey, DDRUMMethod httpMethod, string urlString, NSDictionary<NSString, NSObject> attributes);

		[Export ("addResourceMetricsWithResourceKey:metrics:attributes:")]
		void AddResourceMetricsWithResourceKey (string resourceKey, NSUrlSessionTaskMetrics metrics, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopResourceWithResourceKey:response:size:attributes:")]
		void StopResourceWithResourceKey (string resourceKey, NSUrlResponse response, [NullAllowed] NSNumber size, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopResourceWithResourceKey:statusCode:kind:size:attributes:")]
		void StopResourceWithResourceKey (string resourceKey, [NullAllowed] NSNumber statusCode, DDRUMResourceType kind, [NullAllowed] NSNumber size, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopResourceWithErrorWithResourceKey:error:response:attributes:")]
		void StopResourceWithErrorWithResourceKey (string resourceKey, NSError error, [NullAllowed] NSUrlResponse response, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopResourceWithErrorWithResourceKey:message:response:attributes:")]
		void StopResourceWithErrorWithResourceKey (string resourceKey, string message, [NullAllowed] NSUrlResponse response, NSDictionary<NSString, NSObject> attributes);

		[Export ("startActionWithType:name:attributes:")]
		void StartActionWithType (DDRUMActionType type, string name, NSDictionary<NSString, NSObject> attributes);

		[Export ("stopActionWithType:name:attributes:")]
		void StopActionWithType (DDRUMActionType type, [NullAllowed] string name, NSDictionary<NSString, NSObject> attributes);

		[Export ("addActionWithType:name:attributes:")]
		void AddActionWithType (DDRUMActionType type, string name, NSDictionary<NSString, NSObject> attributes);

		[Export ("addAttributeForKey:value:")]
		void AddAttributeForKey (string key, NSObject value);

		[Export ("addAttributes:")]
		void AddAttributes (NSDictionary<NSString, NSObject> attributes);

		[Export ("removeAttributeForKey:")]
		void RemoveAttributeForKey (string key);

		[Export ("removeAttributesForKeys:")]
		void RemoveAttributesForKeys (string[] keys);

		[Export ("addFeatureFlagEvaluationWithName:value:")]
		void AddFeatureFlagEvaluationWithName (string name, NSObject value);

		[Export ("startOperationWithName:operationKey:attributes:options:")]
		void StartOperationWithName (string name, [NullAllowed] string operationKey, NSDictionary<NSString, NSObject> attributes, [NullAllowed] DDOperationOptions options);

		[Export ("startFeatureOperationWithName:operationKey:attributes:")]
		void StartFeatureOperationWithName (string name, [NullAllowed] string operationKey, NSDictionary<NSString, NSObject> attributes);

		[Export ("succeedOperationWithName:operationKey:attributes:")]
		void SucceedOperationWithName (string name, [NullAllowed] string operationKey, NSDictionary<NSString, NSObject> attributes);

		[Export ("succeedFeatureOperationWithName:operationKey:attributes:")]
		void SucceedFeatureOperationWithName (string name, [NullAllowed] string operationKey, NSDictionary<NSString, NSObject> attributes);

		[Export ("failOperationWithName:operationKey:reason:attributes:")]
		void FailOperationWithName (string name, [NullAllowed] string operationKey, DDRUMFeatureOperationFailureReason reason, NSDictionary<NSString, NSObject> attributes);

		[Export ("failFeatureOperationWithName:operationKey:reason:attributes:")]
		void FailFeatureOperationWithName (string name, [NullAllowed] string operationKey, DDRUMFeatureOperationFailureReason reason, NSDictionary<NSString, NSObject> attributes);
	}

	// @interface DDRUMResourceEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMResourceEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMAccount Account { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDRUMResourceEventAction Action { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMResourceEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMResourceEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMResourceEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMResourceEventDisplay Display { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMResourceEventOperatingSystem Os { get; }

		[Export ("resource", ArgumentSemantic.Strong)]
		DDRUMResourceEventResource Resource { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMResourceEventSession Session { get; }

		[Export ("source")]
		DDRUMResourceEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMResourceEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMResourceEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMResourceEventView View { get; }
	}

	// @interface DDRUMResourceEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventAction
	{
		[Export ("id", ArgumentSemantic.Strong)]
		DDRUMResourceEventActionRUMActionID Id { get; }
	}

	// @interface DDRUMResourceEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDRUMResourceEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMResourceEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventContainer
	{
		[Export ("source")]
		DDRUMResourceEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMResourceEventContainerView View { get; }
	}

	// @interface DDRUMResourceEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMResourceEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMResourceEventDDConfiguration Configuration { get; }

		[NullAllowed, Export ("discarded", ArgumentSemantic.Strong)]
		NSNumber Discarded { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("parentSpanId", ArgumentSemantic.Copy)]
		string ParentSpanId { get; }

		[NullAllowed, Export ("rulePsr", ArgumentSemantic.Strong)]
		NSNumber RulePsr { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMResourceEventDDSession Session { get; }

		[NullAllowed, Export ("spanId", ArgumentSemantic.Copy)]
		string SpanId { get; }

		[NullAllowed, Export ("traceId", ArgumentSemantic.Copy)]
		string TraceId { get; }
	}

	// @interface DDRUMResourceEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMResourceEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDDSession
	{
		[Export ("plan")]
		DDRUMResourceEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMResourceEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMResourceEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDevice
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
		DDRUMResourceEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMResourceEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMResourceEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMResourceEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMResourceEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMResourceEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMResourceEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMResourceEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMResourceEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMResourceEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMResourceEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMResourceEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMResourceEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMResourceEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMResourceEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMResourceEventResource
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResource
	{
		[NullAllowed, Export ("connect", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceConnect Connect { get; }

		[NullAllowed, Export ("decodedBodySize", ArgumentSemantic.Strong)]
		NSNumber DecodedBodySize { get; }

		[Export ("deliveryType")]
		DDRUMResourceEventResourceDeliveryType DeliveryType { get; }

		[NullAllowed, Export ("dns", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceDNS Dns { get; }

		[NullAllowed, Export ("download", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceDownload Download { get; }

		[NullAllowed, Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("encodedBodySize", ArgumentSemantic.Strong)]
		NSNumber EncodedBodySize { get; }

		[NullAllowed, Export ("firstByte", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceFirstByte FirstByte { get; }

		[NullAllowed, Export ("graphql", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceRUMGraphql Graphql { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("method")]
		DDRUMResourceEventResourceRUMMethod Method { get; }

		[NullAllowed, Export ("protocol", ArgumentSemantic.Copy)]
		string Protocol { get; }

		[NullAllowed, Export ("provider", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceProvider Provider { get; }

		[NullAllowed, Export ("redirect", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceRedirect Redirect { get; }

		[Export ("renderBlockingStatus")]
		DDRUMResourceEventResourceRenderBlockingStatus RenderBlockingStatus { get; }

		[NullAllowed, Export ("request", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceRequest Request { get; }

		[NullAllowed, Export ("response", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceResponse Response { get; }

		[NullAllowed, Export ("size", ArgumentSemantic.Strong)]
		NSNumber Size { get; }

		[NullAllowed, Export ("ssl", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceSSL Ssl { get; }

		[NullAllowed, Export ("statusCode", ArgumentSemantic.Strong)]
		NSNumber StatusCode { get; }

		[NullAllowed, Export ("transferSize", ArgumentSemantic.Strong)]
		NSNumber TransferSize { get; }

		[Export ("type")]
		DDRUMResourceEventResourceResourceType Type { get; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }

		[NullAllowed, Export ("worker", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceWorker Worker { get; }
	}

	// @interface DDRUMResourceEventResourceConnect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceConnect
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceDNS
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceDNS
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceDownload
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceDownload
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceFirstByte
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceFirstByte
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceProvider
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceProvider
	{
		[NullAllowed, Export ("domain", ArgumentSemantic.Copy)]
		string Domain { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("type")]
		DDRUMResourceEventResourceProviderProviderType Type { get; }
	}

	// @interface DDRUMResourceEventResourceRUMGraphql
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRUMGraphql
	{
		[NullAllowed, Export ("errorCount", ArgumentSemantic.Strong)]
		NSNumber ErrorCount { get; }

		[NullAllowed, Export ("errors", ArgumentSemantic.Copy)]
		DDRUMResourceEventResourceRUMGraphqlErrors[] Errors { get; }

		[NullAllowed, Export ("operationName", ArgumentSemantic.Copy)]
		string OperationName { get; }

		[Export ("operationType")]
		DDRUMResourceEventResourceRUMGraphqlOperationType OperationType { get; }

		[NullAllowed, Export ("payload", ArgumentSemantic.Copy)]
		string Payload { get; set; }

		[NullAllowed, Export ("variables", ArgumentSemantic.Copy)]
		string Variables { get; set; }
	}

	// @interface DDRUMResourceEventResourceRUMGraphqlErrors
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRUMGraphqlErrors
	{
		[NullAllowed, Export ("code", ArgumentSemantic.Copy)]
		string Code { get; }

		[NullAllowed, Export ("locations", ArgumentSemantic.Copy)]
		DDRUMResourceEventResourceRUMGraphqlErrorsLocations[] Locations { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; }

		[NullAllowed, Export ("path", ArgumentSemantic.Copy)]
		DDRUMResourceEventResourceRUMGraphqlErrorsPath[] Path { get; }
	}

	// @interface DDRUMResourceEventResourceRUMGraphqlErrorsLocations
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRUMGraphqlErrorsLocations
	{
		[Export ("column", ArgumentSemantic.Strong)]
		NSNumber Column { get; }

		[Export ("line", ArgumentSemantic.Strong)]
		NSNumber Line { get; }
	}

	// @interface DDRUMResourceEventResourceRUMGraphqlErrorsPath
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRUMGraphqlErrorsPath
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("integer", ArgumentSemantic.Strong)]
		NSNumber Integer { get; }
	}

	// @interface DDRUMResourceEventResourceRedirect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRedirect
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceRequest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRequest
	{
		[NullAllowed, Export ("decodedBodySize", ArgumentSemantic.Strong)]
		NSNumber DecodedBodySize { get; }

		[NullAllowed, Export ("encodedBodySize", ArgumentSemantic.Strong)]
		NSNumber EncodedBodySize { get; }

		[NullAllowed, Export ("headers", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceRequestHeaders Headers { get; }
	}

	// @interface DDRUMResourceEventResourceRequestHeaders
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceRequestHeaders
	{
		[Export ("headersInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> HeadersInfo { get; set; }
	}

	// @interface DDRUMResourceEventResourceResponse
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceResponse
	{
		[NullAllowed, Export ("headers", ArgumentSemantic.Strong)]
		DDRUMResourceEventResourceResponseHeaders Headers { get; }
	}

	// @interface DDRUMResourceEventResourceResponseHeaders
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceResponseHeaders
	{
		[Export ("headersInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> HeadersInfo { get; set; }
	}

	// @interface DDRUMResourceEventResourceSSL
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceSSL
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventResourceWorker
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventResourceWorker
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMResourceEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMResourceEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMResourceEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMResourceEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMResourceEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMResourceEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMTrackResourceHeaders
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMTrackResourceHeaders
	{
		[Static]
		[Export ("disabled", ArgumentSemantic.Strong)]
		DDRUMTrackResourceHeaders Disabled { get; }

		[Static]
		[Export ("defaults", ArgumentSemantic.Strong)]
		DDRUMTrackResourceHeaders Defaults { get; }

		[Static]
		[Export ("custom:")]
		DDRUMTrackResourceHeaders Custom (DDRUMHeaderCaptureRule[] rules);
	}

	// @interface DDRUMURLSessionTracking
	[BaseType (typeof(NSObject))]
	interface DDRUMURLSessionTracking
	{
		[Export ("setFirstPartyHostsTracing:")]
		void SetFirstPartyHostsTracing (DDRUMFirstPartyHostsTracing firstPartyHostsTracing);

		[Export ("setResourceAttributesProvider:")]
		void SetResourceAttributesProvider (Func<NSUrlRequest, NSUrlResponse, NSData, NSError, NSDictionary<NSString, NSObject>> provider);

		[Export ("setTrackResourceHeaders:")]
		void SetTrackResourceHeaders (DDRUMTrackResourceHeaders trackResourceHeaders);
	}

	// @interface DDRUMView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMView
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("attributes", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Attributes { get; }

		[Export ("initWithName:attributes:")]
		NativeHandle Constructor (string name, NSDictionary<NSString, NSObject> attributes);
	}

	// @interface DDRUMViewEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMViewEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMAccount Account { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMViewEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMViewEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMViewEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMViewEventDisplay Display { get; }

		[NullAllowed, Export ("featureFlags", ArgumentSemantic.Strong)]
		DDRUMViewEventFeatureFlags FeatureFlags { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMViewEventOperatingSystem Os { get; }

		[NullAllowed, Export ("privacy", ArgumentSemantic.Strong)]
		DDRUMViewEventPrivacy Privacy { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMViewEventSession Session { get; }

		[Export ("source")]
		DDRUMViewEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMViewEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMViewEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMViewEventView View { get; }
	}

	// @interface DDRUMViewEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventContainer
	{
		[Export ("source")]
		DDRUMViewEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMViewEventContainerView View { get; }
	}

	// @interface DDRUMViewEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("cls", ArgumentSemantic.Strong)]
		DDRUMViewEventDDCLS Cls { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMViewEventDDConfiguration Configuration { get; }

		[Export ("documentVersion", ArgumentSemantic.Strong)]
		NSNumber DocumentVersion { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("pageStates", ArgumentSemantic.Copy)]
		DDRUMViewEventDDPageStates[] PageStates { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMViewEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("replayStats", ArgumentSemantic.Strong)]
		DDRUMViewEventDDReplayStats ReplayStats { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMViewEventDDSession Session { get; }
	}

	// @interface DDRUMViewEventDDCLS
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDCLS
	{
		[NullAllowed, Export ("devicePixelRatio", ArgumentSemantic.Strong)]
		NSNumber DevicePixelRatio { get; }
	}

	// @interface DDRUMViewEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("startSessionReplayRecordingManually", ArgumentSemantic.Strong)]
		NSNumber StartSessionReplayRecordingManually { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMViewEventDDPageStates
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDPageStates
	{
		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }

		[Export ("state")]
		DDRUMViewEventDDPageStatesState State { get; }
	}

	// @interface DDRUMViewEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMViewEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMViewEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMViewEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMViewEventDDReplayStats
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDReplayStats
	{
		[NullAllowed, Export ("recordsCount", ArgumentSemantic.Strong)]
		NSNumber RecordsCount { get; }

		[NullAllowed, Export ("segmentsCount", ArgumentSemantic.Strong)]
		NSNumber SegmentsCount { get; }

		[NullAllowed, Export ("segmentsTotalRawSize", ArgumentSemantic.Strong)]
		NSNumber SegmentsTotalRawSize { get; }
	}

	// @interface DDRUMViewEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDDSession
	{
		[Export ("plan")]
		DDRUMViewEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMViewEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMViewEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDevice
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
		DDRUMViewEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMViewEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDisplay
	{
		[NullAllowed, Export ("scroll", ArgumentSemantic.Strong)]
		DDRUMViewEventDisplayScroll Scroll { get; }

		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMViewEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMViewEventDisplayScroll
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDisplayScroll
	{
		[Export ("maxDepth", ArgumentSemantic.Strong)]
		NSNumber MaxDepth { get; }

		[Export ("maxDepthScrollTop", ArgumentSemantic.Strong)]
		NSNumber MaxDepthScrollTop { get; }

		[Export ("maxScrollHeight", ArgumentSemantic.Strong)]
		NSNumber MaxScrollHeight { get; }

		[Export ("maxScrollHeightTime", ArgumentSemantic.Strong)]
		NSNumber MaxScrollHeightTime { get; }
	}

	// @interface DDRUMViewEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMViewEventFeatureFlags
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventFeatureFlags
	{
		[Export ("featureFlagsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> FeatureFlagsInfo { get; set; }
	}

	// @interface DDRUMViewEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMViewEventPrivacy
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventPrivacy
	{
		[Export ("replayLevel")]
		DDRUMViewEventPrivacyReplayLevel ReplayLevel { get; }
	}

	// @interface DDRUMViewEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMViewEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMViewEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMViewEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMViewEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMViewEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMViewEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMViewEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMViewEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMViewEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMViewEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("isActive", ArgumentSemantic.Strong)]
		NSNumber IsActive { get; }

		[NullAllowed, Export ("sampledForReplay", ArgumentSemantic.Strong)]
		NSNumber SampledForReplay { get; }

		[Export ("type")]
		DDRUMViewEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMViewEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventStream
	{
		[NullAllowed, Export ("bitrate", ArgumentSemantic.Strong)]
		NSNumber Bitrate { get; }

		[NullAllowed, Export ("completionPercent", ArgumentSemantic.Strong)]
		NSNumber CompletionPercent { get; }

		[NullAllowed, Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("format", ArgumentSemantic.Copy)]
		string Format { get; }

		[NullAllowed, Export ("fps", ArgumentSemantic.Strong)]
		NSNumber Fps { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("resolution", ArgumentSemantic.Copy)]
		string Resolution { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }

		[NullAllowed, Export ("watchTime", ArgumentSemantic.Strong)]
		NSNumber WatchTime { get; }
	}

	// @interface DDRUMViewEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventView
	{
		[NullAllowed, Export ("accessibility", ArgumentSemantic.Strong)]
		DDRUMViewEventViewAccessibility Accessibility { get; }

		[Export ("action", ArgumentSemantic.Strong)]
		DDRUMViewEventViewAction Action { get; }

		[NullAllowed, Export ("cpuTicksCount", ArgumentSemantic.Strong)]
		NSNumber CpuTicksCount { get; }

		[NullAllowed, Export ("cpuTicksPerSecond", ArgumentSemantic.Strong)]
		NSNumber CpuTicksPerSecond { get; }

		[NullAllowed, Export ("crash", ArgumentSemantic.Strong)]
		DDRUMViewEventViewCrash Crash { get; }

		[NullAllowed, Export ("cumulativeLayoutShift", ArgumentSemantic.Strong)]
		NSNumber CumulativeLayoutShift { get; }

		[NullAllowed, Export ("cumulativeLayoutShiftTargetSelector", ArgumentSemantic.Copy)]
		string CumulativeLayoutShiftTargetSelector { get; }

		[NullAllowed, Export ("cumulativeLayoutShiftTime", ArgumentSemantic.Strong)]
		NSNumber CumulativeLayoutShiftTime { get; }

		[NullAllowed, Export ("customTimings", ArgumentSemantic.Strong)]
		DDRUMViewEventViewCustomTimings CustomTimings { get; }

		[NullAllowed, Export ("domComplete", ArgumentSemantic.Strong)]
		NSNumber DomComplete { get; }

		[NullAllowed, Export ("domContentLoaded", ArgumentSemantic.Strong)]
		NSNumber DomContentLoaded { get; }

		[NullAllowed, Export ("domInteractive", ArgumentSemantic.Strong)]
		NSNumber DomInteractive { get; }

		[Export ("error", ArgumentSemantic.Strong)]
		DDRUMViewEventViewError Error { get; }

		[NullAllowed, Export ("firstByte", ArgumentSemantic.Strong)]
		NSNumber FirstByte { get; }

		[NullAllowed, Export ("firstContentfulPaint", ArgumentSemantic.Strong)]
		NSNumber FirstContentfulPaint { get; }

		[NullAllowed, Export ("firstInputDelay", ArgumentSemantic.Strong)]
		NSNumber FirstInputDelay { get; }

		[NullAllowed, Export ("firstInputTargetSelector", ArgumentSemantic.Copy)]
		string FirstInputTargetSelector { get; }

		[NullAllowed, Export ("firstInputTime", ArgumentSemantic.Strong)]
		NSNumber FirstInputTime { get; }

		[NullAllowed, Export ("flutterBuildTime", ArgumentSemantic.Strong)]
		DDRUMViewEventViewFlutterBuildTime FlutterBuildTime { get; }

		[NullAllowed, Export ("flutterRasterTime", ArgumentSemantic.Strong)]
		DDRUMViewEventViewFlutterRasterTime FlutterRasterTime { get; }

		[NullAllowed, Export ("freezeRate", ArgumentSemantic.Strong)]
		NSNumber FreezeRate { get; }

		[NullAllowed, Export ("frozenFrame", ArgumentSemantic.Strong)]
		DDRUMViewEventViewFrozenFrame FrozenFrame { get; }

		[NullAllowed, Export ("frustration", ArgumentSemantic.Strong)]
		DDRUMViewEventViewFrustration Frustration { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("inForegroundPeriods", ArgumentSemantic.Copy)]
		DDRUMViewEventViewInForegroundPeriods[] InForegroundPeriods { get; }

		[NullAllowed, Export ("interactionToNextPaint", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextPaint { get; }

		[NullAllowed, Export ("interactionToNextPaintTargetSelector", ArgumentSemantic.Copy)]
		string InteractionToNextPaintTargetSelector { get; }

		[NullAllowed, Export ("interactionToNextPaintTime", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextPaintTime { get; }

		[NullAllowed, Export ("interactionToNextViewTime", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextViewTime { get; }

		[NullAllowed, Export ("isActive", ArgumentSemantic.Strong)]
		NSNumber IsActive { get; }

		[NullAllowed, Export ("isSlowRendered", ArgumentSemantic.Strong)]
		NSNumber IsSlowRendered { get; }

		[NullAllowed, Export ("jsRefreshRate", ArgumentSemantic.Strong)]
		DDRUMViewEventViewJsRefreshRate JsRefreshRate { get; }

		[NullAllowed, Export ("largestContentfulPaint", ArgumentSemantic.Strong)]
		NSNumber LargestContentfulPaint { get; }

		[NullAllowed, Export ("largestContentfulPaintTargetSelector", ArgumentSemantic.Copy)]
		string LargestContentfulPaintTargetSelector { get; }

		[NullAllowed, Export ("loadEvent", ArgumentSemantic.Strong)]
		NSNumber LoadEvent { get; }

		[NullAllowed, Export ("loadingTime", ArgumentSemantic.Strong)]
		NSNumber LoadingTime { get; }

		[Export ("loadingType")]
		DDRUMViewEventViewLoadingType LoadingType { get; }

		[NullAllowed, Export ("longTask", ArgumentSemantic.Strong)]
		DDRUMViewEventViewLongTask LongTask { get; }

		[NullAllowed, Export ("memoryAverage", ArgumentSemantic.Strong)]
		NSNumber MemoryAverage { get; }

		[NullAllowed, Export ("memoryMax", ArgumentSemantic.Strong)]
		NSNumber MemoryMax { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("networkSettledTime", ArgumentSemantic.Strong)]
		NSNumber NetworkSettledTime { get; }

		[NullAllowed, Export ("performance", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformance Performance { get; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[NullAllowed, Export ("refreshRateAverage", ArgumentSemantic.Strong)]
		NSNumber RefreshRateAverage { get; }

		[NullAllowed, Export ("refreshRateMin", ArgumentSemantic.Strong)]
		NSNumber RefreshRateMin { get; }

		[Export ("resource", ArgumentSemantic.Strong)]
		DDRUMViewEventViewResource Resource { get; }

		[NullAllowed, Export ("slowFrames", ArgumentSemantic.Copy)]
		DDRUMViewEventViewSlowFrames[] SlowFrames { get; }

		[NullAllowed, Export ("slowFramesRate", ArgumentSemantic.Strong)]
		NSNumber SlowFramesRate { get; }

		[Export ("timeSpent", ArgumentSemantic.Strong)]
		NSNumber TimeSpent { get; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMViewEventViewAccessibility
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewAccessibility
	{
		[NullAllowed, Export ("assistiveSwitchEnabled", ArgumentSemantic.Strong)]
		NSNumber AssistiveSwitchEnabled { get; }

		[NullAllowed, Export ("assistiveTouchEnabled", ArgumentSemantic.Strong)]
		NSNumber AssistiveTouchEnabled { get; }

		[NullAllowed, Export ("boldTextEnabled", ArgumentSemantic.Strong)]
		NSNumber BoldTextEnabled { get; }

		[NullAllowed, Export ("buttonShapesEnabled", ArgumentSemantic.Strong)]
		NSNumber ButtonShapesEnabled { get; }

		[NullAllowed, Export ("closedCaptioningEnabled", ArgumentSemantic.Strong)]
		NSNumber ClosedCaptioningEnabled { get; }

		[NullAllowed, Export ("grayscaleEnabled", ArgumentSemantic.Strong)]
		NSNumber GrayscaleEnabled { get; }

		[NullAllowed, Export ("increaseContrastEnabled", ArgumentSemantic.Strong)]
		NSNumber IncreaseContrastEnabled { get; }

		[NullAllowed, Export ("invertColorsEnabled", ArgumentSemantic.Strong)]
		NSNumber InvertColorsEnabled { get; }

		[NullAllowed, Export ("monoAudioEnabled", ArgumentSemantic.Strong)]
		NSNumber MonoAudioEnabled { get; }

		[NullAllowed, Export ("onOffSwitchLabelsEnabled", ArgumentSemantic.Strong)]
		NSNumber OnOffSwitchLabelsEnabled { get; }

		[NullAllowed, Export ("reduceMotionEnabled", ArgumentSemantic.Strong)]
		NSNumber ReduceMotionEnabled { get; }

		[NullAllowed, Export ("reduceTransparencyEnabled", ArgumentSemantic.Strong)]
		NSNumber ReduceTransparencyEnabled { get; }

		[NullAllowed, Export ("reducedAnimationsEnabled", ArgumentSemantic.Strong)]
		NSNumber ReducedAnimationsEnabled { get; }

		[NullAllowed, Export ("rtlEnabled", ArgumentSemantic.Strong)]
		NSNumber RtlEnabled { get; }

		[NullAllowed, Export ("screenReaderEnabled", ArgumentSemantic.Strong)]
		NSNumber ScreenReaderEnabled { get; }

		[NullAllowed, Export ("shakeToUndoEnabled", ArgumentSemantic.Strong)]
		NSNumber ShakeToUndoEnabled { get; }

		[NullAllowed, Export ("shouldDifferentiateWithoutColor", ArgumentSemantic.Strong)]
		NSNumber ShouldDifferentiateWithoutColor { get; }

		[NullAllowed, Export ("singleAppModeEnabled", ArgumentSemantic.Strong)]
		NSNumber SingleAppModeEnabled { get; }

		[NullAllowed, Export ("speakScreenEnabled", ArgumentSemantic.Strong)]
		NSNumber SpeakScreenEnabled { get; }

		[NullAllowed, Export ("speakSelectionEnabled", ArgumentSemantic.Strong)]
		NSNumber SpeakSelectionEnabled { get; }

		[NullAllowed, Export ("textSize", ArgumentSemantic.Copy)]
		string TextSize { get; }

		[NullAllowed, Export ("videoAutoplayEnabled", ArgumentSemantic.Strong)]
		NSNumber VideoAutoplayEnabled { get; }
	}

	// @interface DDRUMViewEventViewAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewAction
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewCrash
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewCrash
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewCustomTimings
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewCustomTimings
	{
		[Export ("customTimingsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSNumber> CustomTimingsInfo { get; set; }
	}

	// @interface DDRUMViewEventViewError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewError
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewFlutterBuildTime
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewFlutterBuildTime
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewEventViewFlutterRasterTime
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewFlutterRasterTime
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewEventViewFrozenFrame
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewFrozenFrame
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewFrustration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewFrustration
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewInForegroundPeriods
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewInForegroundPeriods
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMViewEventViewJsRefreshRate
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewJsRefreshRate
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewEventViewLongTask
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewLongTask
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewPerformance
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformance
	{
		[NullAllowed, Export ("cls", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceCLS Cls { get; }

		[NullAllowed, Export ("fbc", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceFBC Fbc { get; }

		[NullAllowed, Export ("fcp", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceFCP Fcp { get; }

		[NullAllowed, Export ("fid", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceFID Fid { get; }

		[NullAllowed, Export ("inp", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceINP Inp { get; }

		[NullAllowed, Export ("lcp", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceLCP Lcp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceCLS
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceCLS
	{
		[NullAllowed, Export ("currentRect", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceCLSCurrentRect CurrentRect { get; }

		[NullAllowed, Export ("previousRect", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceCLSPreviousRect PreviousRect { get; }

		[Export ("score", ArgumentSemantic.Strong)]
		NSNumber Score { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceCLSCurrentRect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceCLSCurrentRect
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }

		[Export ("x", ArgumentSemantic.Strong)]
		NSNumber X { get; }

		[Export ("y", ArgumentSemantic.Strong)]
		NSNumber Y { get; }
	}

	// @interface DDRUMViewEventViewPerformanceCLSPreviousRect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceCLSPreviousRect
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }

		[Export ("x", ArgumentSemantic.Strong)]
		NSNumber X { get; }

		[Export ("y", ArgumentSemantic.Strong)]
		NSNumber Y { get; }
	}

	// @interface DDRUMViewEventViewPerformanceFBC
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceFBC
	{
		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceFCP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceFCP
	{
		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceFID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceFID
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceINP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceINP
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("subParts", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceINPSubParts SubParts { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceINPSubParts
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceINPSubParts
	{
		[Export ("inputDelay", ArgumentSemantic.Strong)]
		NSNumber InputDelay { get; }

		[Export ("presentationDelay", ArgumentSemantic.Strong)]
		NSNumber PresentationDelay { get; }

		[Export ("processingDuration", ArgumentSemantic.Strong)]
		NSNumber ProcessingDuration { get; }
	}

	// @interface DDRUMViewEventViewPerformanceLCP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceLCP
	{
		[NullAllowed, Export ("resourceUrl", ArgumentSemantic.Copy)]
		string ResourceUrl { get; set; }

		[NullAllowed, Export ("subParts", ArgumentSemantic.Strong)]
		DDRUMViewEventViewPerformanceLCPSubParts SubParts { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewEventViewPerformanceLCPSubParts
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewPerformanceLCPSubParts
	{
		[Export ("loadDelay", ArgumentSemantic.Strong)]
		NSNumber LoadDelay { get; }

		[Export ("loadTime", ArgumentSemantic.Strong)]
		NSNumber LoadTime { get; }

		[Export ("renderDelay", ArgumentSemantic.Strong)]
		NSNumber RenderDelay { get; }
	}

	// @interface DDRUMViewEventViewResource
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewResource
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewEventViewSlowFrames
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewEventViewSlowFrames
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMViewUpdateEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMAccount Account { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDisplay Display { get; }

		[NullAllowed, Export ("featureFlags", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventFeatureFlags FeatureFlags { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventOperatingSystem Os { get; }

		[NullAllowed, Export ("privacy", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventPrivacy Privacy { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventSession Session { get; }

		[Export ("source")]
		DDRUMViewUpdateEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventView View { get; }
	}

	// @interface DDRUMViewUpdateEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewUpdateEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventContainer
	{
		[Export ("source")]
		DDRUMViewUpdateEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventContainerView View { get; }
	}

	// @interface DDRUMViewUpdateEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewUpdateEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDDConfiguration Configuration { get; }

		[Export ("documentVersion", ArgumentSemantic.Strong)]
		NSNumber DocumentVersion { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDDSession Session { get; }
	}

	// @interface DDRUMViewUpdateEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMViewUpdateEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDDSession
	{
		[Export ("plan")]
		DDRUMViewUpdateEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMViewUpdateEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMViewUpdateEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDevice
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
		DDRUMViewUpdateEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMViewUpdateEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDisplay
	{
		[NullAllowed, Export ("scroll", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDisplayScroll Scroll { get; }

		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMViewUpdateEventDisplayScroll
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDisplayScroll
	{
		[Export ("maxDepth", ArgumentSemantic.Strong)]
		NSNumber MaxDepth { get; }

		[Export ("maxDepthScrollTop", ArgumentSemantic.Strong)]
		NSNumber MaxDepthScrollTop { get; }

		[Export ("maxScrollHeight", ArgumentSemantic.Strong)]
		NSNumber MaxScrollHeight { get; }

		[Export ("maxScrollHeightTime", ArgumentSemantic.Strong)]
		NSNumber MaxScrollHeightTime { get; }
	}

	// @interface DDRUMViewUpdateEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMViewUpdateEventFeatureFlags
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventFeatureFlags
	{
		[Export ("featureFlagsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> FeatureFlagsInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMViewUpdateEventPrivacy
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventPrivacy
	{
		[Export ("replayLevel")]
		DDRUMViewUpdateEventPrivacyReplayLevel ReplayLevel { get; }
	}

	// @interface DDRUMViewUpdateEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMViewUpdateEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMViewUpdateEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMViewUpdateEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMViewUpdateEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMViewUpdateEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("isActive", ArgumentSemantic.Strong)]
		NSNumber IsActive { get; }

		[NullAllowed, Export ("sampledForReplay", ArgumentSemantic.Strong)]
		NSNumber SampledForReplay { get; }

		[Export ("type")]
		DDRUMViewUpdateEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMViewUpdateEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventStream
	{
		[NullAllowed, Export ("bitrate", ArgumentSemantic.Strong)]
		NSNumber Bitrate { get; }

		[NullAllowed, Export ("completionPercent", ArgumentSemantic.Strong)]
		NSNumber CompletionPercent { get; }

		[NullAllowed, Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("format", ArgumentSemantic.Copy)]
		string Format { get; }

		[NullAllowed, Export ("fps", ArgumentSemantic.Strong)]
		NSNumber Fps { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("resolution", ArgumentSemantic.Copy)]
		string Resolution { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }

		[NullAllowed, Export ("watchTime", ArgumentSemantic.Strong)]
		NSNumber WatchTime { get; }
	}

	// @interface DDRUMViewUpdateEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMViewUpdateEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventView
	{
		[NullAllowed, Export ("accessibility", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewAccessibility Accessibility { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewAction Action { get; }

		[NullAllowed, Export ("cpuTicksCount", ArgumentSemantic.Strong)]
		NSNumber CpuTicksCount { get; }

		[NullAllowed, Export ("cpuTicksPerSecond", ArgumentSemantic.Strong)]
		NSNumber CpuTicksPerSecond { get; }

		[NullAllowed, Export ("crash", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewCrash Crash { get; }

		[NullAllowed, Export ("cumulativeLayoutShift", ArgumentSemantic.Strong)]
		NSNumber CumulativeLayoutShift { get; }

		[NullAllowed, Export ("cumulativeLayoutShiftTargetSelector", ArgumentSemantic.Copy)]
		string CumulativeLayoutShiftTargetSelector { get; }

		[NullAllowed, Export ("cumulativeLayoutShiftTime", ArgumentSemantic.Strong)]
		NSNumber CumulativeLayoutShiftTime { get; }

		[NullAllowed, Export ("customTimings", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewCustomTimings CustomTimings { get; }

		[NullAllowed, Export ("domComplete", ArgumentSemantic.Strong)]
		NSNumber DomComplete { get; }

		[NullAllowed, Export ("domContentLoaded", ArgumentSemantic.Strong)]
		NSNumber DomContentLoaded { get; }

		[NullAllowed, Export ("domInteractive", ArgumentSemantic.Strong)]
		NSNumber DomInteractive { get; }

		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewError Error { get; }

		[NullAllowed, Export ("firstByte", ArgumentSemantic.Strong)]
		NSNumber FirstByte { get; }

		[NullAllowed, Export ("firstContentfulPaint", ArgumentSemantic.Strong)]
		NSNumber FirstContentfulPaint { get; }

		[NullAllowed, Export ("firstInputDelay", ArgumentSemantic.Strong)]
		NSNumber FirstInputDelay { get; }

		[NullAllowed, Export ("firstInputTargetSelector", ArgumentSemantic.Copy)]
		string FirstInputTargetSelector { get; }

		[NullAllowed, Export ("firstInputTime", ArgumentSemantic.Strong)]
		NSNumber FirstInputTime { get; }

		[NullAllowed, Export ("flutterBuildTime", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewFlutterBuildTime FlutterBuildTime { get; }

		[NullAllowed, Export ("flutterRasterTime", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewFlutterRasterTime FlutterRasterTime { get; }

		[NullAllowed, Export ("freezeRate", ArgumentSemantic.Strong)]
		NSNumber FreezeRate { get; }

		[NullAllowed, Export ("frozenFrame", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewFrozenFrame FrozenFrame { get; }

		[NullAllowed, Export ("frustration", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewFrustration Frustration { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("inForegroundPeriods", ArgumentSemantic.Copy)]
		DDRUMViewUpdateEventViewInForegroundPeriods[] InForegroundPeriods { get; }

		[NullAllowed, Export ("interactionToNextPaint", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextPaint { get; }

		[NullAllowed, Export ("interactionToNextPaintTargetSelector", ArgumentSemantic.Copy)]
		string InteractionToNextPaintTargetSelector { get; }

		[NullAllowed, Export ("interactionToNextPaintTime", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextPaintTime { get; }

		[NullAllowed, Export ("interactionToNextViewTime", ArgumentSemantic.Strong)]
		NSNumber InteractionToNextViewTime { get; }

		[NullAllowed, Export ("isActive", ArgumentSemantic.Strong)]
		NSNumber IsActive { get; }

		[NullAllowed, Export ("isSlowRendered", ArgumentSemantic.Strong)]
		NSNumber IsSlowRendered { get; }

		[NullAllowed, Export ("jsRefreshRate", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewJsRefreshRate JsRefreshRate { get; }

		[NullAllowed, Export ("largestContentfulPaint", ArgumentSemantic.Strong)]
		NSNumber LargestContentfulPaint { get; }

		[NullAllowed, Export ("largestContentfulPaintTargetSelector", ArgumentSemantic.Copy)]
		string LargestContentfulPaintTargetSelector { get; }

		[NullAllowed, Export ("loadEvent", ArgumentSemantic.Strong)]
		NSNumber LoadEvent { get; }

		[NullAllowed, Export ("loadingTime", ArgumentSemantic.Strong)]
		NSNumber LoadingTime { get; }

		[Export ("loadingType")]
		DDRUMViewUpdateEventViewLoadingType LoadingType { get; }

		[NullAllowed, Export ("longTask", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewLongTask LongTask { get; }

		[NullAllowed, Export ("memoryAverage", ArgumentSemantic.Strong)]
		NSNumber MemoryAverage { get; }

		[NullAllowed, Export ("memoryMax", ArgumentSemantic.Strong)]
		NSNumber MemoryMax { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("networkSettledTime", ArgumentSemantic.Strong)]
		NSNumber NetworkSettledTime { get; }

		[NullAllowed, Export ("performance", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformance Performance { get; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[NullAllowed, Export ("refreshRateAverage", ArgumentSemantic.Strong)]
		NSNumber RefreshRateAverage { get; }

		[NullAllowed, Export ("refreshRateMin", ArgumentSemantic.Strong)]
		NSNumber RefreshRateMin { get; }

		[NullAllowed, Export ("resource", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewResource Resource { get; }

		[NullAllowed, Export ("slowFrames", ArgumentSemantic.Copy)]
		DDRUMViewUpdateEventViewSlowFrames[] SlowFrames { get; }

		[NullAllowed, Export ("slowFramesRate", ArgumentSemantic.Strong)]
		NSNumber SlowFramesRate { get; }

		[NullAllowed, Export ("timeSpent", ArgumentSemantic.Strong)]
		NSNumber TimeSpent { get; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMViewUpdateEventViewAccessibility
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewAccessibility
	{
		[NullAllowed, Export ("assistiveSwitchEnabled", ArgumentSemantic.Strong)]
		NSNumber AssistiveSwitchEnabled { get; }

		[NullAllowed, Export ("assistiveTouchEnabled", ArgumentSemantic.Strong)]
		NSNumber AssistiveTouchEnabled { get; }

		[NullAllowed, Export ("boldTextEnabled", ArgumentSemantic.Strong)]
		NSNumber BoldTextEnabled { get; }

		[NullAllowed, Export ("buttonShapesEnabled", ArgumentSemantic.Strong)]
		NSNumber ButtonShapesEnabled { get; }

		[NullAllowed, Export ("closedCaptioningEnabled", ArgumentSemantic.Strong)]
		NSNumber ClosedCaptioningEnabled { get; }

		[NullAllowed, Export ("grayscaleEnabled", ArgumentSemantic.Strong)]
		NSNumber GrayscaleEnabled { get; }

		[NullAllowed, Export ("increaseContrastEnabled", ArgumentSemantic.Strong)]
		NSNumber IncreaseContrastEnabled { get; }

		[NullAllowed, Export ("invertColorsEnabled", ArgumentSemantic.Strong)]
		NSNumber InvertColorsEnabled { get; }

		[NullAllowed, Export ("monoAudioEnabled", ArgumentSemantic.Strong)]
		NSNumber MonoAudioEnabled { get; }

		[NullAllowed, Export ("onOffSwitchLabelsEnabled", ArgumentSemantic.Strong)]
		NSNumber OnOffSwitchLabelsEnabled { get; }

		[NullAllowed, Export ("reduceMotionEnabled", ArgumentSemantic.Strong)]
		NSNumber ReduceMotionEnabled { get; }

		[NullAllowed, Export ("reduceTransparencyEnabled", ArgumentSemantic.Strong)]
		NSNumber ReduceTransparencyEnabled { get; }

		[NullAllowed, Export ("reducedAnimationsEnabled", ArgumentSemantic.Strong)]
		NSNumber ReducedAnimationsEnabled { get; }

		[NullAllowed, Export ("rtlEnabled", ArgumentSemantic.Strong)]
		NSNumber RtlEnabled { get; }

		[NullAllowed, Export ("screenReaderEnabled", ArgumentSemantic.Strong)]
		NSNumber ScreenReaderEnabled { get; }

		[NullAllowed, Export ("shakeToUndoEnabled", ArgumentSemantic.Strong)]
		NSNumber ShakeToUndoEnabled { get; }

		[NullAllowed, Export ("shouldDifferentiateWithoutColor", ArgumentSemantic.Strong)]
		NSNumber ShouldDifferentiateWithoutColor { get; }

		[NullAllowed, Export ("singleAppModeEnabled", ArgumentSemantic.Strong)]
		NSNumber SingleAppModeEnabled { get; }

		[NullAllowed, Export ("speakScreenEnabled", ArgumentSemantic.Strong)]
		NSNumber SpeakScreenEnabled { get; }

		[NullAllowed, Export ("speakSelectionEnabled", ArgumentSemantic.Strong)]
		NSNumber SpeakSelectionEnabled { get; }

		[NullAllowed, Export ("textSize", ArgumentSemantic.Copy)]
		string TextSize { get; }

		[NullAllowed, Export ("videoAutoplayEnabled", ArgumentSemantic.Strong)]
		NSNumber VideoAutoplayEnabled { get; }
	}

	// @interface DDRUMViewUpdateEventViewAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewAction
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewCrash
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewCrash
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewCustomTimings
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewCustomTimings
	{
		[Export ("customTimingsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSNumber> CustomTimingsInfo { get; set; }
	}

	// @interface DDRUMViewUpdateEventViewError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewError
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewFlutterBuildTime
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewFlutterBuildTime
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewUpdateEventViewFlutterRasterTime
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewFlutterRasterTime
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewUpdateEventViewFrozenFrame
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewFrozenFrame
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewFrustration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewFrustration
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewInForegroundPeriods
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewInForegroundPeriods
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMViewUpdateEventViewJsRefreshRate
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewJsRefreshRate
	{
		[Export ("average", ArgumentSemantic.Strong)]
		NSNumber Average { get; }

		[Export ("max", ArgumentSemantic.Strong)]
		NSNumber Max { get; }

		[NullAllowed, Export ("metricMax", ArgumentSemantic.Strong)]
		NSNumber MetricMax { get; }

		[Export ("min", ArgumentSemantic.Strong)]
		NSNumber Min { get; }
	}

	// @interface DDRUMViewUpdateEventViewLongTask
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewLongTask
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformance
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformance
	{
		[NullAllowed, Export ("cls", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceCLS Cls { get; }

		[NullAllowed, Export ("fbc", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceFBC Fbc { get; }

		[NullAllowed, Export ("fcp", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceFCP Fcp { get; }

		[NullAllowed, Export ("fid", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceFID Fid { get; }

		[NullAllowed, Export ("inp", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceINP Inp { get; }

		[NullAllowed, Export ("lcp", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceLCP Lcp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceCLS
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceCLS
	{
		[NullAllowed, Export ("currentRect", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceCLSCurrentRect CurrentRect { get; }

		[NullAllowed, Export ("previousRect", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceCLSPreviousRect PreviousRect { get; }

		[Export ("score", ArgumentSemantic.Strong)]
		NSNumber Score { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceCLSCurrentRect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceCLSCurrentRect
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }

		[Export ("x", ArgumentSemantic.Strong)]
		NSNumber X { get; }

		[Export ("y", ArgumentSemantic.Strong)]
		NSNumber Y { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceCLSPreviousRect
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceCLSPreviousRect
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }

		[Export ("x", ArgumentSemantic.Strong)]
		NSNumber X { get; }

		[Export ("y", ArgumentSemantic.Strong)]
		NSNumber Y { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceFBC
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceFBC
	{
		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceFCP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceFCP
	{
		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceFID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceFID
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceINP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceINP
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("subParts", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceINPSubParts SubParts { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[NullAllowed, Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceINPSubParts
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceINPSubParts
	{
		[Export ("inputDelay", ArgumentSemantic.Strong)]
		NSNumber InputDelay { get; }

		[Export ("presentationDelay", ArgumentSemantic.Strong)]
		NSNumber PresentationDelay { get; }

		[Export ("processingDuration", ArgumentSemantic.Strong)]
		NSNumber ProcessingDuration { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceLCP
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceLCP
	{
		[NullAllowed, Export ("resourceUrl", ArgumentSemantic.Copy)]
		string ResourceUrl { get; set; }

		[NullAllowed, Export ("subParts", ArgumentSemantic.Strong)]
		DDRUMViewUpdateEventViewPerformanceLCPSubParts SubParts { get; }

		[NullAllowed, Export ("targetSelector", ArgumentSemantic.Copy)]
		string TargetSelector { get; }

		[Export ("timestamp", ArgumentSemantic.Strong)]
		NSNumber Timestamp { get; }
	}

	// @interface DDRUMViewUpdateEventViewPerformanceLCPSubParts
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewPerformanceLCPSubParts
	{
		[Export ("loadDelay", ArgumentSemantic.Strong)]
		NSNumber LoadDelay { get; }

		[Export ("loadTime", ArgumentSemantic.Strong)]
		NSNumber LoadTime { get; }

		[Export ("renderDelay", ArgumentSemantic.Strong)]
		NSNumber RenderDelay { get; }
	}

	// @interface DDRUMViewUpdateEventViewResource
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewResource
	{
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber Count { get; }
	}

	// @interface DDRUMViewUpdateEventViewSlowFrames
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMViewUpdateEventViewSlowFrames
	{
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("start", ArgumentSemantic.Strong)]
		NSNumber Start { get; }
	}

	// @interface DDRUMVitalAppLaunchEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMAccount Account { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDisplay Display { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventSession Session { get; }

		[Export ("source")]
		DDRUMVitalAppLaunchEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventView View { get; }

		[Export ("vital", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventVital Vital { get; }
	}

	// @interface DDRUMVitalAppLaunchEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalAppLaunchEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventContainer
	{
		[Export ("source")]
		DDRUMVitalAppLaunchEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventContainerView View { get; }
	}

	// @interface DDRUMVitalAppLaunchEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDDConfiguration Configuration { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDDSession Session { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMVitalAppLaunchEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMVitalAppLaunchEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMVitalAppLaunchEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDDSession
	{
		[Export ("plan")]
		DDRUMVitalAppLaunchEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMVitalAppLaunchEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDevice
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
		DDRUMVitalAppLaunchEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMVitalAppLaunchEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMVitalAppLaunchEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMVitalAppLaunchEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMVitalAppLaunchEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMVitalAppLaunchEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMVitalAppLaunchEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMVitalAppLaunchEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMVitalAppLaunchEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMVitalAppLaunchEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalAppLaunchEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalAppLaunchEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMVitalAppLaunchEventVital
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalAppLaunchEventVital
	{
		[Export ("appLaunchMetric")]
		DDRUMVitalAppLaunchEventVitalAppLaunchMetric AppLaunchMetric { get; }

		[NullAllowed, Export ("vitalDescription", ArgumentSemantic.Copy)]
		string VitalDescription { get; }

		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[NullAllowed, Export ("hasSavedInstanceStateBundle", ArgumentSemantic.Strong)]
		NSNumber HasSavedInstanceStateBundle { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("isPrewarmed", ArgumentSemantic.Strong)]
		NSNumber IsPrewarmed { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("startupType")]
		DDRUMVitalAppLaunchEventVitalStartupType StartupType { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }
	}

	// @interface DDRUMVitalDurationEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMAccount Account { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDisplay Display { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventSession Session { get; }

		[Export ("source")]
		DDRUMVitalDurationEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventView View { get; }

		[Export ("vital", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventVital Vital { get; }
	}

	// @interface DDRUMVitalDurationEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalDurationEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventContainer
	{
		[Export ("source")]
		DDRUMVitalDurationEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventContainerView View { get; }
	}

	// @interface DDRUMVitalDurationEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalDurationEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDDConfiguration Configuration { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDDSession Session { get; }
	}

	// @interface DDRUMVitalDurationEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMVitalDurationEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMVitalDurationEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMVitalDurationEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMVitalDurationEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMVitalDurationEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDDSession
	{
		[Export ("plan")]
		DDRUMVitalDurationEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMVitalDurationEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMVitalDurationEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDevice
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
		DDRUMVitalDurationEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMVitalDurationEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMVitalDurationEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMVitalDurationEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMVitalDurationEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMVitalDurationEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMVitalDurationEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMVitalDurationEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMVitalDurationEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMVitalDurationEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMVitalDurationEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMVitalDurationEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMVitalDurationEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMVitalDurationEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMVitalDurationEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMVitalDurationEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMVitalDurationEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalDurationEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalDurationEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMVitalDurationEventVital
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalDurationEventVital
	{
		[NullAllowed, Export ("vitalDescription", ArgumentSemantic.Copy)]
		string VitalDescription { get; }

		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber Duration { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }
	}

	// @interface DDRUMVitalOperationStepEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDD Dd { get; }

		[NullAllowed, Export ("account", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMAccount Account { get; }

		[Export ("application", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventApplication Application { get; }

		[NullAllowed, Export ("buildId", ArgumentSemantic.Copy)]
		string BuildId { get; }

		[NullAllowed, Export ("buildVersion", ArgumentSemantic.Copy)]
		string BuildVersion { get; }

		[NullAllowed, Export ("ciTest", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMCITest CiTest { get; }

		[NullAllowed, Export ("connectivity", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMConnectivity Connectivity { get; }

		[NullAllowed, Export ("container", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventContainer Container { get; }

		[NullAllowed, Export ("context", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMEventAttributes Context { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("ddtags", ArgumentSemantic.Copy)]
		string Ddtags { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDevice Device { get; }

		[NullAllowed, Export ("display", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDisplay Display { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventOperatingSystem Os { get; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventSession Session { get; }

		[Export ("source")]
		DDRUMVitalOperationStepEventSource Source { get; }

		[NullAllowed, Export ("stream", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventStream Stream { get; }

		[NullAllowed, Export ("synthetics", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMSyntheticsTest Synthetics { get; }

		[NullAllowed, Export ("tab", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventTAB Tab { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[NullAllowed, Export ("usr", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMUser Usr { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventView View { get; }

		[Export ("vital", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventVital Vital { get; }
	}

	// @interface DDRUMVitalOperationStepEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventApplication
	{
		[NullAllowed, Export ("currentLocale", ArgumentSemantic.Copy)]
		string CurrentLocale { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalOperationStepEventContainer
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventContainer
	{
		[Export ("source")]
		DDRUMVitalOperationStepEventContainerSource Source { get; }

		[Export ("view", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventContainerView View { get; }
	}

	// @interface DDRUMVitalOperationStepEventContainerView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventContainerView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalOperationStepEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDD
	{
		[NullAllowed, Export ("browserSdkVersion", ArgumentSemantic.Copy)]
		string BrowserSdkVersion { get; }

		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDDConfiguration Configuration { get; }

		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }

		[NullAllowed, Export ("profiling", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDDProfiling Profiling { get; }

		[NullAllowed, Export ("sdkName", ArgumentSemantic.Copy)]
		string SdkName { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDDSession Session { get; }
	}

	// @interface DDRUMVitalOperationStepEventDDConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDDConfiguration
	{
		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; }

		[Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }
	}

	// @interface DDRUMVitalOperationStepEventDDProfiling
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDDProfiling
	{
		[Export ("errorReason")]
		DDRUMVitalOperationStepEventDDProfilingErrorReason ErrorReason { get; }

		[Export ("quotaReason")]
		DDRUMVitalOperationStepEventDDProfilingQuotaReason QuotaReason { get; }

		[Export ("status")]
		DDRUMVitalOperationStepEventDDProfilingStatus Status { get; }
	}

	// @interface DDRUMVitalOperationStepEventDDSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDDSession
	{
		[Export ("plan")]
		DDRUMVitalOperationStepEventDDSessionPlan Plan { get; }

		[Export ("sessionPrecondition")]
		DDRUMVitalOperationStepEventDDSessionRUMSessionPrecondition SessionPrecondition { get; }
	}

	// @interface DDRUMVitalOperationStepEventDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDevice
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
		DDRUMVitalOperationStepEventDeviceDeviceType Type { get; }
	}

	// @interface DDRUMVitalOperationStepEventDisplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDisplay
	{
		[NullAllowed, Export ("viewport", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventDisplayViewport Viewport { get; }
	}

	// @interface DDRUMVitalOperationStepEventDisplayViewport
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventDisplayViewport
	{
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber Height { get; }

		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; }
	}

	// @interface DDRUMVitalOperationStepEventOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[Export ("versionMajor", ArgumentSemantic.Copy)]
		string VersionMajor { get; }
	}

	// @interface DDRUMVitalOperationStepEventRUMAccount
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMAccount
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("accountInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AccountInfo { get; set; }
	}

	// @interface DDRUMVitalOperationStepEventRUMCITest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMCITest
	{
		[Export ("testExecutionId", ArgumentSemantic.Copy)]
		string TestExecutionId { get; }
	}

	// @interface DDRUMVitalOperationStepEventRUMConnectivity
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMConnectivity
	{
		[NullAllowed, Export ("cellular", ArgumentSemantic.Strong)]
		DDRUMVitalOperationStepEventRUMConnectivityCellular Cellular { get; }

		[Export ("effectiveType")]
		DDRUMVitalOperationStepEventRUMConnectivityEffectiveType EffectiveType { get; }

		[NullAllowed, Export ("interfaces", ArgumentSemantic.Copy)]
		NSNumber[] Interfaces { get; }

		[Export ("status")]
		DDRUMVitalOperationStepEventRUMConnectivityStatus Status { get; }
	}

	// @interface DDRUMVitalOperationStepEventRUMConnectivityCellular
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMConnectivityCellular
	{
		[NullAllowed, Export ("carrierName", ArgumentSemantic.Copy)]
		string CarrierName { get; }

		[NullAllowed, Export ("technology", ArgumentSemantic.Copy)]
		string Technology { get; }
	}

	// @interface DDRUMVitalOperationStepEventRUMEventAttributes
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMEventAttributes
	{
		[Export ("contextInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> ContextInfo { get; set; }
	}

	// @interface DDRUMVitalOperationStepEventRUMSyntheticsTest
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMSyntheticsTest
	{
		[NullAllowed, Export ("injected", ArgumentSemantic.Strong)]
		NSNumber Injected { get; }

		[Export ("resultId", ArgumentSemantic.Copy)]
		string ResultId { get; }

		[Export ("testId", ArgumentSemantic.Copy)]
		string TestId { get; }

		[Export ("syntheticsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> SyntheticsInfo { get; set; }
	}

	// @interface DDRUMVitalOperationStepEventRUMUser
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventRUMUser
	{
		[NullAllowed, Export ("anonymousId", ArgumentSemantic.Copy)]
		string AnonymousId { get; }

		[NullAllowed, Export ("email", ArgumentSemantic.Copy)]
		string Email { get; }

		[NullAllowed, Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("usrInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> UsrInfo { get; set; }
	}

	// @interface DDRUMVitalOperationStepEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventSession
	{
		[NullAllowed, Export ("hasReplay", ArgumentSemantic.Strong)]
		NSNumber HasReplay { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("type")]
		DDRUMVitalOperationStepEventSessionRUMSessionType Type { get; }
	}

	// @interface DDRUMVitalOperationStepEventStream
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventStream
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalOperationStepEventTAB
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventTAB
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDRUMVitalOperationStepEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[NullAllowed, Export ("referrer", ArgumentSemantic.Copy)]
		string Referrer { get; set; }

		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; set; }
	}

	// @interface DDRUMVitalOperationStepEventVital
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDRUMVitalOperationStepEventVital
	{
		[NullAllowed, Export ("vitalDescription", ArgumentSemantic.Copy)]
		string VitalDescription { get; }

		[Export ("failureReason")]
		DDRUMVitalOperationStepEventVitalFailureReason FailureReason { get; }

		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("operationKey", ArgumentSemantic.Copy)]
		string OperationKey { get; }

		[Export ("stepType")]
		DDRUMVitalOperationStepEventVitalStepType StepType { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }
	}

	// @interface DDTNSResourceParams
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTNSResourceParams
	{
		[Export ("url", ArgumentSemantic.Copy)]
		string Url { get; }

		[Export ("timeSinceViewStart")]
		double TimeSinceViewStart { get; }

		[Export ("viewName", ArgumentSemantic.Copy)]
		string ViewName { get; }
	}

	// @interface DDTelemetryConfigurationEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventDD Dd { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventAction Action { get; }

		[NullAllowed, Export ("application", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventApplication Application { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("effectiveSampleRate", ArgumentSemantic.Strong)]
		NSNumber EffectiveSampleRate { get; }

		[NullAllowed, Export ("experimentalFeatures", ArgumentSemantic.Copy)]
		string[] ExperimentalFeatures { get; }

		[Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventSession Session { get; }

		[Export ("source")]
		DDTelemetryConfigurationEventSource Source { get; }

		[Export ("telemetry", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetry Telemetry { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[NullAllowed, Export ("view", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventView View { get; }
	}

	// @interface DDTelemetryConfigurationEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventAction
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("idValue", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventActionRUMActionID IdValue { get; }
	}

	// @interface DDTelemetryConfigurationEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDTelemetryConfigurationEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventApplication
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryConfigurationEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventDD
	{
		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }
	}

	// @interface DDTelemetryConfigurationEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventSession
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryConfigurationEventTelemetry
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetry
	{
		[Export ("configuration", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetryConfiguration Configuration { get; }

		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetryRUMTelemetryDevice Device { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetryRUMTelemetryOperatingSystem Os { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("telemetryInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> TelemetryInfo { get; set; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryConfiguration
	{
		[NullAllowed, Export ("actionNameAttribute", ArgumentSemantic.Copy)]
		string ActionNameAttribute { get; }

		[NullAllowed, Export ("allowFallbackToLocalStorage", ArgumentSemantic.Strong)]
		NSNumber AllowFallbackToLocalStorage { get; }

		[NullAllowed, Export ("allowUntrustedEvents", ArgumentSemantic.Strong)]
		NSNumber AllowUntrustedEvents { get; }

		[NullAllowed, Export ("appHangThreshold", ArgumentSemantic.Strong)]
		NSNumber AppHangThreshold { get; }

		[NullAllowed, Export ("backgroundTasksEnabled", ArgumentSemantic.Strong)]
		NSNumber BackgroundTasksEnabled { get; }

		[NullAllowed, Export ("batchProcessingLevel", ArgumentSemantic.Strong)]
		NSNumber BatchProcessingLevel { get; }

		[NullAllowed, Export ("batchSize", ArgumentSemantic.Strong)]
		NSNumber BatchSize { get; }

		[NullAllowed, Export ("batchUploadFrequency", ArgumentSemantic.Strong)]
		NSNumber BatchUploadFrequency { get; }

		[NullAllowed, Export ("betaEncodeCookieOptions", ArgumentSemantic.Strong)]
		NSNumber BetaEncodeCookieOptions { get; set; }

		[NullAllowed, Export ("compressIntakeRequests", ArgumentSemantic.Strong)]
		NSNumber CompressIntakeRequests { get; }

		[NullAllowed, Export ("dartVersion", ArgumentSemantic.Copy)]
		string DartVersion { get; set; }

		[NullAllowed, Export ("defaultPrivacyLevel", ArgumentSemantic.Copy)]
		string DefaultPrivacyLevel { get; set; }

		[NullAllowed, Export ("enablePrivacyForActionName", ArgumentSemantic.Strong)]
		NSNumber EnablePrivacyForActionName { get; set; }

		[NullAllowed, Export ("forwardConsoleLogs", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetryConfigurationForwardConsoleLogs ForwardConsoleLogs { get; }

		[NullAllowed, Export ("forwardErrorsToLogs", ArgumentSemantic.Strong)]
		NSNumber ForwardErrorsToLogs { get; }

		[NullAllowed, Export ("forwardReports", ArgumentSemantic.Strong)]
		DDTelemetryConfigurationEventTelemetryConfigurationForwardReports ForwardReports { get; }

		[NullAllowed, Export ("imagePrivacyLevel", ArgumentSemantic.Copy)]
		string ImagePrivacyLevel { get; set; }

		[NullAllowed, Export ("initializationType", ArgumentSemantic.Copy)]
		string InitializationType { get; set; }

		[NullAllowed, Export ("invTimeThresholdMs", ArgumentSemantic.Strong)]
		NSNumber InvTimeThresholdMs { get; }

		[NullAllowed, Export ("isMainProcess", ArgumentSemantic.Strong)]
		NSNumber IsMainProcess { get; }

		[NullAllowed, Export ("mauiVersion", ArgumentSemantic.Copy)]
		string MauiVersion { get; set; }

		[NullAllowed, Export ("mobileVitalsUpdatePeriod", ArgumentSemantic.Strong)]
		NSNumber MobileVitalsUpdatePeriod { get; set; }

		[NullAllowed, Export ("numberOfDisplays", ArgumentSemantic.Strong)]
		NSNumber NumberOfDisplays { get; }

		[NullAllowed, Export ("plugins", ArgumentSemantic.Copy)]
		DDTelemetryConfigurationEventTelemetryConfigurationPlugins[] Plugins { get; set; }

		[NullAllowed, Export ("premiumSampleRate", ArgumentSemantic.Strong)]
		NSNumber PremiumSampleRate { get; }

		[NullAllowed, Export ("profilingSampleRate", ArgumentSemantic.Strong)]
		NSNumber ProfilingSampleRate { get; set; }

		[NullAllowed, Export ("propagateTraceBaggage", ArgumentSemantic.Strong)]
		NSNumber PropagateTraceBaggage { get; set; }

		[NullAllowed, Export ("reactNativeVersion", ArgumentSemantic.Copy)]
		string ReactNativeVersion { get; set; }

		[NullAllowed, Export ("reactVersion", ArgumentSemantic.Copy)]
		string ReactVersion { get; set; }

		[NullAllowed, Export ("remoteConfigurationId", ArgumentSemantic.Copy)]
		string RemoteConfigurationId { get; set; }

		[NullAllowed, Export ("replaySampleRate", ArgumentSemantic.Strong)]
		NSNumber ReplaySampleRate { get; }

		[NullAllowed, Export ("sdkVersion", ArgumentSemantic.Copy)]
		string SdkVersion { get; set; }

		[NullAllowed, Export ("selectedTracingPropagators", ArgumentSemantic.Copy)]
		NSNumber[] SelectedTracingPropagators { get; }

		[NullAllowed, Export ("sendLogsAfterSessionExpiration", ArgumentSemantic.Strong)]
		NSNumber SendLogsAfterSessionExpiration { get; set; }

		[Export ("sessionPersistence")]
		DDTelemetryConfigurationEventTelemetryConfigurationSessionPersistence SessionPersistence { get; }

		[NullAllowed, Export ("sessionReplaySampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionReplaySampleRate { get; set; }

		[NullAllowed, Export ("sessionSampleRate", ArgumentSemantic.Strong)]
		NSNumber SessionSampleRate { get; }

		[NullAllowed, Export ("silentMultipleInit", ArgumentSemantic.Strong)]
		NSNumber SilentMultipleInit { get; }

		[NullAllowed, Export ("source", ArgumentSemantic.Copy)]
		string Source { get; set; }

		[NullAllowed, Export ("startRecordingImmediately", ArgumentSemantic.Strong)]
		NSNumber StartRecordingImmediately { get; set; }

		[NullAllowed, Export ("startSessionReplayRecordingManually", ArgumentSemantic.Strong)]
		NSNumber StartSessionReplayRecordingManually { get; set; }

		[NullAllowed, Export ("storeContextsAcrossPages", ArgumentSemantic.Strong)]
		NSNumber StoreContextsAcrossPages { get; }

		[NullAllowed, Export ("swiftuiActionTrackingEnabled", ArgumentSemantic.Strong)]
		NSNumber SwiftuiActionTrackingEnabled { get; set; }

		[NullAllowed, Export ("swiftuiViewTrackingEnabled", ArgumentSemantic.Strong)]
		NSNumber SwiftuiViewTrackingEnabled { get; set; }

		[NullAllowed, Export ("telemetryConfigurationSampleRate", ArgumentSemantic.Strong)]
		NSNumber TelemetryConfigurationSampleRate { get; }

		[NullAllowed, Export ("telemetrySampleRate", ArgumentSemantic.Strong)]
		NSNumber TelemetrySampleRate { get; }

		[NullAllowed, Export ("telemetryUsageSampleRate", ArgumentSemantic.Strong)]
		NSNumber TelemetryUsageSampleRate { get; }

		[NullAllowed, Export ("textAndInputPrivacyLevel", ArgumentSemantic.Copy)]
		string TextAndInputPrivacyLevel { get; set; }

		[NullAllowed, Export ("tnsTimeThresholdMs", ArgumentSemantic.Strong)]
		NSNumber TnsTimeThresholdMs { get; }

		[NullAllowed, Export ("touchPrivacyLevel", ArgumentSemantic.Copy)]
		string TouchPrivacyLevel { get; set; }

		[Export ("traceContextInjection")]
		DDTelemetryConfigurationEventTelemetryConfigurationTraceContextInjection TraceContextInjection { get; set; }

		[NullAllowed, Export ("traceSampleRate", ArgumentSemantic.Strong)]
		NSNumber TraceSampleRate { get; }

		[NullAllowed, Export ("tracerApi", ArgumentSemantic.Copy)]
		string TracerApi { get; set; }

		[NullAllowed, Export ("tracerApiVersion", ArgumentSemantic.Copy)]
		string TracerApiVersion { get; set; }

		[NullAllowed, Export ("trackAnonymousUser", ArgumentSemantic.Strong)]
		NSNumber TrackAnonymousUser { get; set; }

		[NullAllowed, Export ("trackBackgroundEvents", ArgumentSemantic.Strong)]
		NSNumber TrackBackgroundEvents { get; set; }

		[NullAllowed, Export ("trackBfcacheViews", ArgumentSemantic.Strong)]
		NSNumber TrackBfcacheViews { get; set; }

		[NullAllowed, Export ("trackCrossPlatformLongTasks", ArgumentSemantic.Strong)]
		NSNumber TrackCrossPlatformLongTasks { get; set; }

		[NullAllowed, Export ("trackEarlyRequests", ArgumentSemantic.Strong)]
		NSNumber TrackEarlyRequests { get; set; }

		[NullAllowed, Export ("trackErrors", ArgumentSemantic.Strong)]
		NSNumber TrackErrors { get; set; }

		[NullAllowed, Export ("trackFeatureFlagsForEvents", ArgumentSemantic.Copy)]
		NSNumber[] TrackFeatureFlagsForEvents { get; }

		[NullAllowed, Export ("trackFlutterPerformance", ArgumentSemantic.Strong)]
		NSNumber TrackFlutterPerformance { get; set; }

		[NullAllowed, Export ("trackFrustrations", ArgumentSemantic.Strong)]
		NSNumber TrackFrustrations { get; set; }

		[NullAllowed, Export ("trackInteractions", ArgumentSemantic.Strong)]
		NSNumber TrackInteractions { get; set; }

		[NullAllowed, Export ("trackLongTask", ArgumentSemantic.Strong)]
		NSNumber TrackLongTask { get; set; }

		[NullAllowed, Export ("trackNativeErrors", ArgumentSemantic.Strong)]
		NSNumber TrackNativeErrors { get; set; }

		[NullAllowed, Export ("trackNativeLongTasks", ArgumentSemantic.Strong)]
		NSNumber TrackNativeLongTasks { get; set; }

		[NullAllowed, Export ("trackNativeViews", ArgumentSemantic.Strong)]
		NSNumber TrackNativeViews { get; set; }

		[NullAllowed, Export ("trackNetworkRequests", ArgumentSemantic.Strong)]
		NSNumber TrackNetworkRequests { get; set; }

		[Export ("trackResourceHeaders")]
		DDTelemetryConfigurationEventTelemetryConfigurationTrackResourceHeaders TrackResourceHeaders { get; set; }

		[NullAllowed, Export ("trackResources", ArgumentSemantic.Strong)]
		NSNumber TrackResources { get; set; }

		[NullAllowed, Export ("trackSessionAcrossSubdomains", ArgumentSemantic.Strong)]
		NSNumber TrackSessionAcrossSubdomains { get; }

		[NullAllowed, Export ("trackUserInteractions", ArgumentSemantic.Strong)]
		NSNumber TrackUserInteractions { get; set; }

		[NullAllowed, Export ("trackViewsManually", ArgumentSemantic.Strong)]
		NSNumber TrackViewsManually { get; set; }

		[Export ("trackingConsent")]
		DDTelemetryConfigurationEventTelemetryConfigurationTrackingConsent TrackingConsent { get; }

		[NullAllowed, Export ("unityVersion", ArgumentSemantic.Copy)]
		string UnityVersion { get; set; }

		[NullAllowed, Export ("useAllowedGraphQlUrls", ArgumentSemantic.Strong)]
		NSNumber UseAllowedGraphQlUrls { get; }

		[NullAllowed, Export ("useAllowedTracingOrigins", ArgumentSemantic.Strong)]
		NSNumber UseAllowedTracingOrigins { get; }

		[NullAllowed, Export ("useAllowedTracingUrls", ArgumentSemantic.Strong)]
		NSNumber UseAllowedTracingUrls { get; }

		[NullAllowed, Export ("useAllowedTrackingOrigins", ArgumentSemantic.Strong)]
		NSNumber UseAllowedTrackingOrigins { get; set; }

		[NullAllowed, Export ("useBeforeSend", ArgumentSemantic.Strong)]
		NSNumber UseBeforeSend { get; }

		[NullAllowed, Export ("useCrossSiteSessionCookie", ArgumentSemantic.Strong)]
		NSNumber UseCrossSiteSessionCookie { get; }

		[NullAllowed, Export ("useExcludedActivityUrls", ArgumentSemantic.Strong)]
		NSNumber UseExcludedActivityUrls { get; }

		[NullAllowed, Export ("useFirstPartyHosts", ArgumentSemantic.Strong)]
		NSNumber UseFirstPartyHosts { get; set; }

		[NullAllowed, Export ("useLocalEncryption", ArgumentSemantic.Strong)]
		NSNumber UseLocalEncryption { get; }

		[NullAllowed, Export ("usePartitionedCrossSiteSessionCookie", ArgumentSemantic.Strong)]
		NSNumber UsePartitionedCrossSiteSessionCookie { get; }

		[NullAllowed, Export ("usePciIntake", ArgumentSemantic.Strong)]
		NSNumber UsePciIntake { get; set; }

		[NullAllowed, Export ("useProxy", ArgumentSemantic.Strong)]
		NSNumber UseProxy { get; set; }

		[NullAllowed, Export ("useRemoteConfigurationProxy", ArgumentSemantic.Strong)]
		NSNumber UseRemoteConfigurationProxy { get; set; }

		[NullAllowed, Export ("useSecureSessionCookie", ArgumentSemantic.Strong)]
		NSNumber UseSecureSessionCookie { get; }

		[NullAllowed, Export ("useTracing", ArgumentSemantic.Strong)]
		NSNumber UseTracing { get; }

		[NullAllowed, Export ("useTrackGraphQlPayload", ArgumentSemantic.Strong)]
		NSNumber UseTrackGraphQlPayload { get; }

		[NullAllowed, Export ("useTrackGraphQlResponseErrors", ArgumentSemantic.Strong)]
		NSNumber UseTrackGraphQlResponseErrors { get; }

		[NullAllowed, Export ("useWorkerUrl", ArgumentSemantic.Strong)]
		NSNumber UseWorkerUrl { get; }

		[NullAllowed, Export ("variant", ArgumentSemantic.Copy)]
		string Variant { get; set; }

		[Export ("viewTrackingStrategy")]
		DDTelemetryConfigurationEventTelemetryConfigurationViewTrackingStrategy ViewTrackingStrategy { get; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryConfigurationForwardConsoleLogs
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryConfigurationForwardConsoleLogs
	{
		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }

		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryConfigurationForwardReports
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryConfigurationForwardReports
	{
		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }

		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryConfigurationPlugins
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryConfigurationPlugins
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("pluginsInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> PluginsInfo { get; set; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryRUMTelemetryDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryRUMTelemetryDevice
	{
		[NullAllowed, Export ("architecture", ArgumentSemantic.Copy)]
		string Architecture { get; }

		[NullAllowed, Export ("brand", ArgumentSemantic.Copy)]
		string Brand { get; }

		[NullAllowed, Export ("isLowRam", ArgumentSemantic.Strong)]
		NSNumber IsLowRam { get; }

		[NullAllowed, Export ("logicalCpuCount", ArgumentSemantic.Strong)]
		NSNumber LogicalCpuCount { get; }

		[NullAllowed, Export ("model", ArgumentSemantic.Copy)]
		string Model { get; }

		[NullAllowed, Export ("totalRam", ArgumentSemantic.Strong)]
		NSNumber TotalRam { get; }
	}

	// @interface DDTelemetryConfigurationEventTelemetryRUMTelemetryOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventTelemetryRUMTelemetryOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }
	}

	// @interface DDTelemetryConfigurationEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryConfigurationEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryDebugEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventDD Dd { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventAction Action { get; }

		[NullAllowed, Export ("application", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventApplication Application { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("effectiveSampleRate", ArgumentSemantic.Strong)]
		NSNumber EffectiveSampleRate { get; }

		[NullAllowed, Export ("experimentalFeatures", ArgumentSemantic.Copy)]
		string[] ExperimentalFeatures { get; }

		[Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventSession Session { get; }

		[Export ("source")]
		DDTelemetryDebugEventSource Source { get; }

		[Export ("telemetry", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventTelemetry Telemetry { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[NullAllowed, Export ("view", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventView View { get; }
	}

	// @interface DDTelemetryDebugEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventAction
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("idValue", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventActionRUMActionID IdValue { get; }
	}

	// @interface DDTelemetryDebugEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDTelemetryDebugEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventApplication
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryDebugEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventDD
	{
		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }
	}

	// @interface DDTelemetryDebugEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventSession
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryDebugEventTelemetry
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventTelemetry
	{
		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventTelemetryRUMTelemetryDevice Device { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDTelemetryDebugEventTelemetryRUMTelemetryOperatingSystem Os { get; }

		[Export ("status", ArgumentSemantic.Copy)]
		string Status { get; }

		[NullAllowed, Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("telemetryInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> TelemetryInfo { get; set; }
	}

	// @interface DDTelemetryDebugEventTelemetryRUMTelemetryDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventTelemetryRUMTelemetryDevice
	{
		[NullAllowed, Export ("architecture", ArgumentSemantic.Copy)]
		string Architecture { get; }

		[NullAllowed, Export ("brand", ArgumentSemantic.Copy)]
		string Brand { get; }

		[NullAllowed, Export ("isLowRam", ArgumentSemantic.Strong)]
		NSNumber IsLowRam { get; }

		[NullAllowed, Export ("logicalCpuCount", ArgumentSemantic.Strong)]
		NSNumber LogicalCpuCount { get; }

		[NullAllowed, Export ("model", ArgumentSemantic.Copy)]
		string Model { get; }

		[NullAllowed, Export ("totalRam", ArgumentSemantic.Strong)]
		NSNumber TotalRam { get; }
	}

	// @interface DDTelemetryDebugEventTelemetryRUMTelemetryOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventTelemetryRUMTelemetryOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }
	}

	// @interface DDTelemetryDebugEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryDebugEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryErrorEvent
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEvent
	{
		[Export ("dd", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventDD Dd { get; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventAction Action { get; }

		[NullAllowed, Export ("application", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventApplication Application { get; }

		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber Date { get; }

		[NullAllowed, Export ("effectiveSampleRate", ArgumentSemantic.Strong)]
		NSNumber EffectiveSampleRate { get; }

		[NullAllowed, Export ("experimentalFeatures", ArgumentSemantic.Copy)]
		string[] ExperimentalFeatures { get; }

		[Export ("service", ArgumentSemantic.Copy)]
		string Service { get; }

		[NullAllowed, Export ("session", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventSession Session { get; }

		[Export ("source")]
		DDTelemetryErrorEventSource Source { get; }

		[Export ("telemetry", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventTelemetry Telemetry { get; }

		[Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }

		[NullAllowed, Export ("view", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventView View { get; }
	}

	// @interface DDTelemetryErrorEventAction
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventAction
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }

		[Export ("idValue", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventActionRUMActionID IdValue { get; }
	}

	// @interface DDTelemetryErrorEventActionRUMActionID
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventActionRUMActionID
	{
		[NullAllowed, Export ("string", ArgumentSemantic.Copy)]
		string String { get; }

		[NullAllowed, Export ("stringsArray", ArgumentSemantic.Copy)]
		string[] StringsArray { get; }
	}

	// @interface DDTelemetryErrorEventApplication
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventApplication
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryErrorEventDD
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventDD
	{
		[Export ("formatVersion", ArgumentSemantic.Strong)]
		NSNumber FormatVersion { get; }
	}

	// @interface DDTelemetryErrorEventSession
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventSession
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTelemetryErrorEventTelemetry
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventTelemetry
	{
		[NullAllowed, Export ("device", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventTelemetryRUMTelemetryDevice Device { get; }

		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventTelemetryError Error { get; }

		[Export ("message", ArgumentSemantic.Copy)]
		string Message { get; }

		[NullAllowed, Export ("os", ArgumentSemantic.Strong)]
		DDTelemetryErrorEventTelemetryRUMTelemetryOperatingSystem Os { get; }

		[Export ("status", ArgumentSemantic.Copy)]
		string Status { get; }

		[NullAllowed, Export ("type", ArgumentSemantic.Copy)]
		string Type { get; }

		[Export ("telemetryInfo", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> TelemetryInfo { get; set; }
	}

	// @interface DDTelemetryErrorEventTelemetryError
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventTelemetryError
	{
		[NullAllowed, Export ("kind", ArgumentSemantic.Copy)]
		string Kind { get; }

		[NullAllowed, Export ("stack", ArgumentSemantic.Copy)]
		string Stack { get; }
	}

	// @interface DDTelemetryErrorEventTelemetryRUMTelemetryDevice
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventTelemetryRUMTelemetryDevice
	{
		[NullAllowed, Export ("architecture", ArgumentSemantic.Copy)]
		string Architecture { get; }

		[NullAllowed, Export ("brand", ArgumentSemantic.Copy)]
		string Brand { get; }

		[NullAllowed, Export ("isLowRam", ArgumentSemantic.Strong)]
		NSNumber IsLowRam { get; }

		[NullAllowed, Export ("logicalCpuCount", ArgumentSemantic.Strong)]
		NSNumber LogicalCpuCount { get; }

		[NullAllowed, Export ("model", ArgumentSemantic.Copy)]
		string Model { get; }

		[NullAllowed, Export ("totalRam", ArgumentSemantic.Strong)]
		NSNumber TotalRam { get; }
	}

	// @interface DDTelemetryErrorEventTelemetryRUMTelemetryOperatingSystem
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventTelemetryRUMTelemetryOperatingSystem
	{
		[NullAllowed, Export ("build", ArgumentSemantic.Copy)]
		string Build { get; }

		[NullAllowed, Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; }
	}

	// @interface DDTelemetryErrorEventView
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTelemetryErrorEventView
	{
		[Export ("id", ArgumentSemantic.Copy)]
		string Id { get; }
	}

	// @interface DDTimeBasedINVActionPredicate
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTimeBasedINVActionPredicate : DDNextViewActionPredicate
	{
		[Static]
		[Export ("defaultMaxTimeToNextView")]
		double DefaultMaxTimeToNextView { get; }

		[Export ("initWithMaxTimeToNextView:")]
		NativeHandle Constructor (double maxTimeToNextView);

		[Export ("isLastActionFrom:")]
		bool IsLastActionFrom (DDINVActionParams actionParams);
	}

	// @interface DDTimeBasedTNSResourcePredicate
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTimeBasedTNSResourcePredicate : DDNetworkSettledResourcePredicate
	{
		[Static]
		[Export ("defaultThreshold")]
		double DefaultThreshold { get; }

		[Export ("initWithThreshold:")]
		NativeHandle Constructor (double threshold);

		[Export ("isInitialResourceFrom:")]
		bool IsInitialResourceFrom (DDTNSResourceParams resourceParams);
	}

	// @interface __dd_private_DDForwardingProxyBase
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface __dd_private_DDForwardingProxyBase
	{
		[Export ("forwardingTargetOrNil")]
		NSObject ForwardingTargetOrNil ();
	}
}
