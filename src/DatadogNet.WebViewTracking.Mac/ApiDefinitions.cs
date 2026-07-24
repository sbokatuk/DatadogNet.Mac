using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;
using WebKit;

namespace DatadogWebViewTracking
{
	// @interface DDWebViewTracking
	/// <summary>Bridges RUM events and logs out of a <c>WKWebView</c> whose page runs the Datadog Browser SDK, into the surrounding native session.</summary>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface DDWebViewTracking
	{
		[Static]
		[Export ("enableWithWebView:hosts:logsSampleRate:")]
		void EnableWithWebView (WKWebView webView, NSSet<NSString> hosts, float logsSampleRate);

		[Static]
		[Export ("enableWithWebView:instanceName:hosts:logsSampleRate:")]
		void EnableWithWebView (WKWebView webView, [NullAllowed] string instanceName, NSSet<NSString> hosts, float logsSampleRate);

		[Static]
		[Export ("disableWithWebView:")]
		void DisableWithWebView (WKWebView webView);
	}
}
