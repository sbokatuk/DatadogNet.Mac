using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;

namespace DatadogCrashReporting
{
	// @interface DDCrashReporter
	[BaseType (typeof(NSObject))]
	interface DDCrashReporter
	{
		[Static]
		[Export ("enable")]
		void Enable ();
	}
}
