using DatadogWebViewTracking;
using Foundation;
using WebKit;

namespace DatadogNetExample;

/// <summary>
/// A page hosting a <c>WKWebView</c> with the Datadog bridge installed, through the raw
/// <c>DatadogNet.WebViewTracking.iOS</c> binding.
/// </summary>
/// <remarks>
/// For anything to actually cross the bridge, the page inside must run the Datadog Browser SDK
/// and its host must be on the allowlist — example.com does not, so what this page demonstrates
/// is the wiring: enable when the platform view exists, disable when it goes away (the bridge
/// holds a reference to the web view, so leaving it installed keeps the page alive).
/// </remarks>
public sealed class WebViewPage : ContentPage
{
    private readonly WebView webView;

    public WebViewPage()
    {
        Title = "Web view";

        webView = new WebView { Source = "https://example.com/" };
        webView.Loaded += OnWebViewLoaded;
        webView.Unloaded += OnWebViewUnloaded;

        var layout = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star),
            ],
        };

        layout.Add(
            new Label
            {
                Padding = 12,
                FontSize = 13,
                Text = "DDWebViewTracking is enabled on this WKWebView. A page running the "
                       + "Datadog Browser SDK on an allowlisted host would report into the "
                       + "surrounding native session; example.com does not, so the point here "
                       + "is the wiring.",
            },
            0,
            0);
        layout.Add(webView, 0, 1);

        Content = layout;
    }

    private void OnWebViewLoaded(object? sender, EventArgs e)
    {
        // MAUI's iOS handler exposes the platform view, which is a WKWebView subclass. The hosts
        // allowlist is an NSSet in the raw binding, matched by suffix — and it is an allowlist
        // because the bridge lets page JavaScript write into your RUM session.
        if (webView.Handler?.PlatformView is WKWebView platform)
        {
            DDWebViewTracking.EnableWithWebView(
                platform,
                new NSSet<NSString>(new NSString("example.com")),
                logsSampleRate: 100);
        }
    }

    private void OnWebViewUnloaded(object? sender, EventArgs e)
    {
        if (webView.Handler?.PlatformView is WKWebView platform)
        {
            DDWebViewTracking.DisableWithWebView(platform);
        }
    }
}
