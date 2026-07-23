using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace DatadogSessionReplay
{
	// @interface DDSessionReplay
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDSessionReplay
	{
		[Static]
		[Export ("enableWith:")]
		void EnableWith (DDSessionReplayConfiguration configuration);

		[Static]
		[Export ("enableWith:instanceName:")]
		void EnableWith (DDSessionReplayConfiguration configuration, [NullAllowed] string instanceName);

		[Static]
		[Export ("startRecording")]
		void StartRecording ();

		[Static]
		[Export ("startRecordingWithInstanceName:")]
		void StartRecordingWithInstanceName ([NullAllowed] string instanceName);

		[Static]
		[Export ("stopRecording")]
		void StopRecording ();

		[Static]
		[Export ("stopRecordingWithInstanceName:")]
		void StopRecordingWithInstanceName ([NullAllowed] string instanceName);
	}

	// @interface DDSessionReplayConfiguration
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDSessionReplayConfiguration
	{
		[Export ("replaySampleRate")]
		float ReplaySampleRate { get; set; }

		[Export ("textAndInputPrivacyLevel")]
		DDTextAndInputPrivacyLevel TextAndInputPrivacyLevel { get; set; }

		[Export ("imagePrivacyLevel")]
		DDImagePrivacyLevel ImagePrivacyLevel { get; set; }

		[Export ("touchPrivacyLevel")]
		DDTouchPrivacyLevel TouchPrivacyLevel { get; set; }

		[Export ("startRecordingImmediately")]
		bool StartRecordingImmediately { get; set; }

		[NullAllowed, Export ("customEndpoint", ArgumentSemantic.Copy)]
		NSUrl CustomEndpoint { get; set; }

		[Export ("featureFlags", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSNumber> FeatureFlags { get; set; }

		[Export ("initWithReplaySampleRate:textAndInputPrivacyLevel:imagePrivacyLevel:touchPrivacyLevel:featureFlags:")]
		NativeHandle Constructor (float replaySampleRate, DDTextAndInputPrivacyLevel textAndInputPrivacyLevel, DDImagePrivacyLevel imagePrivacyLevel, DDTouchPrivacyLevel touchPrivacyLevel, [NullAllowed] NSDictionary<NSString, NSNumber> featureFlags);

		[Export ("initWithReplaySampleRate:textAndInputPrivacyLevel:imagePrivacyLevel:touchPrivacyLevel:")]
		NativeHandle Constructor (float replaySampleRate, DDTextAndInputPrivacyLevel textAndInputPrivacyLevel, DDImagePrivacyLevel imagePrivacyLevel, DDTouchPrivacyLevel touchPrivacyLevel);
	}

	// @interface DDSessionReplayPrivacyOverrides
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDSessionReplayPrivacyOverrides
	{
		[Export ("textAndInputPrivacy")]
		DDTextAndInputPrivacyLevelOverride TextAndInputPrivacy { get; set; }

		[Export ("imagePrivacy")]
		DDImagePrivacyLevelOverride ImagePrivacy { get; set; }

		[Export ("touchPrivacy")]
		DDTouchPrivacyLevelOverride TouchPrivacy { get; set; }

		[NullAllowed, Export ("hide", ArgumentSemantic.Strong)]
		NSNumber Hide { get; set; }

		[Export ("initWithView:")]
		NativeHandle Constructor (UIView view);
	}

	// @interface UIView (SWIFT_EXTENSION(DatadogSessionReplay))
	[Category]
	[BaseType (typeof(UIView))]
	interface UIView_DatadogSessionReplay
	{
		[Export ("ddSessionReplayPrivacyOverrides")]
		DDSessionReplayPrivacyOverrides GetDdSessionReplayPrivacyOverrides ();
	}
}
