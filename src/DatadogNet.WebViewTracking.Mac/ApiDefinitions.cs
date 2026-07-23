using System;
using DatadogInternal;
using Foundation;
using ObjCRuntime;
using WebKit;

namespace DatadogWebViewTracking
{
	// @interface DDWebViewTracking
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
