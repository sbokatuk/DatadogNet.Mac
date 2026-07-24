using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace DatadogCore
{
	// typedef void (^UIApplicationNotificationCallback)(NSDate * _Nullable didFinishLaunchingTimeInterval,
	delegate void UIApplicationNotificationCallback ([NullAllowed] NSDate didFinishLaunchingTimeInterval, [NullAllowed] NSDate didBecomeActiveTimeInterval);

	partial interface IDDDataEncryption {}

	// @protocol DDDataEncryption
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDDataEncryption
	{
		[Abstract]
		[Export ("encryptWithData:error:")]
		NSData EncryptWithData (NSData data, [NullAllowed] out NSError error);

		[Abstract]
		[Export ("decryptWithData:error:")]
		NSData DecryptWithData (NSData data, [NullAllowed] out NSError error);
	}

	partial interface IDDServerDateProvider {}

	// @protocol DDServerDateProvider
	[Model, Protocol]
	[BaseType (typeof(NSObject))]
	interface DDServerDateProvider
	{
		[Abstract]
		[Export ("synchronizeWithUpdate:")]
		void SynchronizeWithUpdate (Action<double> update);
	}

	// @interface DDConfiguration
	/// <summary>What <c>DDDatadog.InitializeWithConfiguration</c> starts: client token, environment, site, service, and batching/upload behaviour.</summary>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDConfiguration
	{
		[Export ("clientToken", ArgumentSemantic.Copy)]
		string ClientToken { get; set; }

		[Export ("env", ArgumentSemantic.Copy)]
		string Env { get; set; }

		[Export ("site", ArgumentSemantic.Strong)]
		DDSite Site { get; set; }

		[NullAllowed, Export ("service", ArgumentSemantic.Copy)]
		string Service { get; set; }

		[NullAllowed, Export ("version", ArgumentSemantic.Copy)]
		string Version { get; set; }

		[Export ("batchSize")]
		DDBatchSize BatchSize { get; set; }

		[Export ("uploadFrequency")]
		DDUploadFrequency UploadFrequency { get; set; }

		[Export ("batchProcessingLevel")]
		DDBatchProcessingLevel BatchProcessingLevel { get; set; }

		[NullAllowed, Export ("proxyConfiguration", ArgumentSemantic.Copy)]
		NSDictionary ProxyConfiguration { get; set; }

		[Export ("bundle", ArgumentSemantic.Strong)]
		NSBundle Bundle { get; set; }

		[Export ("additionalConfiguration", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> AdditionalConfiguration { get; set; }

		[Export ("backgroundTasksEnabled")]
		bool BackgroundTasksEnabled { get; set; }

		[Export ("setEncryption:")]
		void SetEncryption (IDDDataEncryption encryption);

		[Export ("setServerDateProvider:")]
		void SetServerDateProvider (IDDServerDateProvider serverDateProvider);

		[Export ("initWithClientToken:env:")]
		NativeHandle Constructor (string clientToken, string env);
	}

	// @interface DDCrossPlatformExtension
	[BaseType (typeof(NSObject))]
	interface DDCrossPlatformExtension
	{
		[Static]
		[Export ("subscribeToSharedContext:")]
		void SubscribeToSharedContext (Action<DDSharedContext> toSharedContext);

		[Static]
		[Export ("unsubscribeFromSharedContext")]
		void UnsubscribeFromSharedContext ();
	}

	// @interface DDDatadog
	/// <summary>The Datadog SDK entry point: initialisation, user and account info, tracking consent, verbosity and data clearing.</summary>
	/// <remarks>Initialise once, as early as possible, with <c>InitializeWithConfiguration</c>. Docs: https://docs.datadoghq.com/real_user_monitoring/mobile_and_tv_monitoring/setup/ios/</remarks>
	[BaseType (typeof(NSObject))]
	interface DDDatadog
	{
		[Static]
		[Export ("initializeWithConfiguration:trackingConsent:")]
		void InitializeWithConfiguration (DDConfiguration configuration, DDTrackingConsent trackingConsent);

		[Static]
		[Export ("initializeWithConfiguration:trackingConsent:instanceName:")]
		void InitializeWithConfiguration (DDConfiguration configuration, DDTrackingConsent trackingConsent, string instanceName);

		[Static]
		[Export ("setVerbosityLevel:")]
		void SetVerbosityLevel (DDCoreLoggerLevel verbosityLevel);

		[Static]
		[Export ("verbosityLevel")]
		DDCoreLoggerLevel VerbosityLevel ();

		[Static]
		[Export ("setUserInfoWithUserId:name:email:extraInfo:")]
		void SetUserInfoWithUserId (string userId, [NullAllowed] string name, [NullAllowed] string email, NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("setUserInfoWithUserId:instanceName:name:email:extraInfo:")]
		void SetUserInfoWithUserId (string userId, [NullAllowed] string instanceName, [NullAllowed] string name, [NullAllowed] string email, NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("clearUserInfo")]
		void ClearUserInfo ();

		[Static]
		[Export ("clearUserInfoWithInstanceName:")]
		void ClearUserInfoWithInstanceName ([NullAllowed] string instanceName);

		[Static]
		[Export ("addUserExtraInfo:")]
		void AddUserExtraInfo (NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("addUserExtraInfo:instanceName:")]
		void AddUserExtraInfo (NSDictionary<NSString, NSObject> extraInfo, [NullAllowed] string instanceName);

		[Static]
		[Export ("setAccountInfoWithAccountId:name:extraInfo:")]
		void SetAccountInfoWithAccountId (string accountId, [NullAllowed] string name, NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("setAccountInfoWithAccountId:instanceName:name:extraInfo:")]
		void SetAccountInfoWithAccountId (string accountId, [NullAllowed] string instanceName, [NullAllowed] string name, NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("addAccountExtraInfo:")]
		void AddAccountExtraInfo (NSDictionary<NSString, NSObject> extraInfo);

		[Static]
		[Export ("addAccountExtraInfo:instanceName:")]
		void AddAccountExtraInfo (NSDictionary<NSString, NSObject> extraInfo, [NullAllowed] string instanceName);

		[Static]
		[Export ("clearAccountInfo")]
		void ClearAccountInfo ();

		[Static]
		[Export ("clearAccountInfoWithInstanceName:")]
		void ClearAccountInfoWithInstanceName ([NullAllowed] string instanceName);

		[Static]
		[Export ("setTrackingConsentWithConsent:")]
		void SetTrackingConsentWithConsent (DDTrackingConsent consent);

		[Static]
		[Export ("setTrackingConsentWithConsent:instanceName:")]
		void SetTrackingConsentWithConsent (DDTrackingConsent consent, [NullAllowed] string instanceName);

		[Static]
		[Export ("isInitialized")]
		bool IsInitialized ();

		[Static]
		[Export ("isInitializedWithInstanceName:")]
		bool IsInitializedWithInstanceName ([NullAllowed] string instanceName);

		[Static]
		[Export ("stopInstance")]
		void StopInstance ();

		[Static]
		[Export ("stopInstanceWithInstanceName:")]
		void StopInstanceWithInstanceName ([NullAllowed] string instanceName);

		[Static]
		[Export ("clearAllData")]
		void ClearAllData ();

		[Static]
		[Export ("clearAllDataWithInstanceName:")]
		void ClearAllDataWithInstanceName ([NullAllowed] string instanceName);
	}

	// @interface DDSharedContext
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDSharedContext
	{
		[NullAllowed, Export ("userId", ArgumentSemantic.Copy)]
		string UserId { get; }

		[NullAllowed, Export ("accountId", ArgumentSemantic.Copy)]
		string AccountId { get; }
	}

	// @interface DDSite
	/// <summary>The Datadog site the SDK uploads to, as static factories: <c>Us1()</c>, <c>Us3()</c>, <c>Us5()</c>, <c>Eu1()</c>, <c>Ap1()</c>, <c>Ap2()</c>, <c>Us1_fed()</c>. Match your organisation's region - the wrong site is the most common reason nothing appears in Datadog.</summary>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDSite
	{
		[Static]
		[Export ("us1")]
		DDSite Us1 ();

		[Static]
		[Export ("us3")]
		DDSite Us3 ();

		[Static]
		[Export ("us5")]
		DDSite Us5 ();

		[Static]
		[Export ("eu1")]
		DDSite Eu1 ();

		[Static]
		[Export ("ap1")]
		DDSite Ap1 ();

		[Static]
		[Export ("ap2")]
		DDSite Ap2 ();

		[Static]
		[Export ("us1_fed")]
		DDSite Us1_fed ();

		[Static]
		[Export ("us2_fed")]
		DDSite Us2_fed ();
	}

	// @interface DDTrackingConsent
	/// <summary>Tracking consent, as static factories: <c>Granted()</c>, <c>NotGranted()</c>, and <c>Pending()</c> - which collects and holds data on the device until consent is decided.</summary>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDTrackingConsent
	{
		[Static]
		[Export ("granted")]
		DDTrackingConsent Granted ();

		[Static]
		[Export ("notGranted")]
		DDTrackingConsent NotGranted ();

		[Static]
		[Export ("pending")]
		DDTrackingConsent Pending ();
	}

	// @interface DDURLSessionInstrumentation
	/// <summary>Automatic RUM resources and distributed tracing for <c>NSUrlSession</c>. Prefer the generic <c>Enable&lt;TDelegate&gt;()</c> from the Additions layer over passing a raw class handle.</summary>
	[BaseType (typeof(NSObject))]
	interface DDURLSessionInstrumentation
	{
		[Static]
		[Export ("enableDurationBreakdownWith:")]
		void EnableDurationBreakdownWith (DDURLSessionInstrumentationConfiguration configuration);

		[Static]
		[Export ("enableDurationBreakdownWith:instanceName:")]
		void EnableDurationBreakdownWith (DDURLSessionInstrumentationConfiguration configuration, [NullAllowed] string instanceName);

		[Static]
		[Export ("enableWithConfiguration:")]
		void EnableWithConfiguration (DDURLSessionInstrumentationConfiguration configuration);

		[Static]
		[Export ("enableWithConfiguration:instanceName:")]
		void EnableWithConfiguration (DDURLSessionInstrumentationConfiguration configuration, [NullAllowed] string instanceName);

		[Static]
		[Export ("disableWithDelegateClass:")]
		void DisableWithDelegateClass (IntPtr delegateClass);

		[Static]
		[Export ("disableWithDelegateClass:instanceName:")]
		void DisableWithDelegateClass (IntPtr delegateClass, [NullAllowed] string instanceName);
	}

	// @interface DDURLSessionInstrumentationConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDURLSessionInstrumentationConfiguration
	{
		[Export ("delegateClass")]
		IntPtr DelegateClass { get; set; }

		[Export ("initWithDelegateClass:")]
		NativeHandle Constructor (IntPtr delegateClass);

		[Export ("setFirstPartyHostsTracing:")]
		void SetFirstPartyHostsTracing (DDURLSessionInstrumentationFirstPartyHostsTracing firstPartyHostsTracing);
	}

	// @interface DDURLSessionInstrumentationFirstPartyHostsTracing
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDURLSessionInstrumentationFirstPartyHostsTracing
	{
		[Export ("initWithHostsWithHeaderTypes:")]
		NativeHandle Constructor (NSDictionary<NSString, NSSet<DDTracingHeaderType>> hostsWithHeaderTypes);

		[Export ("initWithHosts:")]
		NativeHandle Constructor (NSSet<NSString> hosts);
	}

	// @interface __dd_private_AppLaunchHandler
	[BaseType (typeof(NSObject))]
	interface __dd_private_AppLaunchHandler
	{
		[Static]
		[Export ("shared")]
		__dd_private_AppLaunchHandler Shared { get; }

		[Export ("taskPolicyRole")]
		nint TaskPolicyRole { get; }

		[Export ("processLaunchDate")]
		NSDate ProcessLaunchDate { get; }

		[Export ("runtimeLoadDate")]
		NSDate RuntimeLoadDate { get; }

		[Export ("runtimePreMainDate")]
		NSDate RuntimePreMainDate { get; }

		[Export ("observeNotificationCenter:")]
		void ObserveNotificationCenter (NSNotificationCenter notificationCenter);

		[Export ("setApplicationNotificationCallback:")]
		void SetApplicationNotificationCallback (UIApplicationNotificationCallback callback);
	}

	// @interface __dd_private_ObjcExceptionHandler
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface __dd_private_ObjcExceptionHandler
	{
		[Static]
		[Export ("catchException:error:")]
		bool CatchException (Action tryBlock, [NullAllowed] out NSError error);
	}
}
