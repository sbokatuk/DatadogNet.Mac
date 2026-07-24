using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;

namespace DatadogCrashReporting
{
	// @interface DDCrashReporter
	/// <summary>KSCrash-based crash capture. <c>Enable()</c> after core initialisation; crashes are reported through RUM on the next launch, so RUM must be enabled too.</summary>
	[BaseType (typeof(NSObject))]
	interface DDCrashReporter
	{
		[Static]
		[Export ("enable")]
		void Enable ();
	}
}
