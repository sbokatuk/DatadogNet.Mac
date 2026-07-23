using DatadogLogs;
using DatadogRUM;
using DatadogTrace;

namespace DatadogNetExample;

/// <summary>
/// Each button drives one part of the Datadog API. The <see cref="ActivityLabel"/> echoes what was
/// reported, so the sample is useful even without a Datadog account to send it to.
/// </summary>
public partial class MainPage : ContentPage
{
    private const string ViewKey = "main";

    private readonly List<string> activity = [];

    public MainPage()
    {
        InitializeComponent();

        StatusLabel.Text = Datadog.IsConfigured
            ? "Reporting to Datadog."
            : "No client token set, so events are collected but never delivered. "
              + "Put your own values in Datadog.cs to send them for real.";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Manual view tracking. The UIKit predicate configured in Datadog.cs already reports views
        // automatically; this is here to show the explicit API, which is what you need for a
        // screen the predicate cannot name usefully - a MAUI Shell route, say.
        Datadog.Rum.StartView(ViewKey, "Main");
    }

    protected override void OnDisappearing()
    {
        Datadog.Rum.StopView(ViewKey);
        base.OnDisappearing();
    }

    private void OnRecordAction(object? sender, EventArgs e)
    {
        // Attributes are plain C# values - the convenience overload converts them. The raw binding
        // takes an NSDictionary<NSString, NSObject> and requires one even when it is empty.
        Datadog.Rum.AddAction(DDRUMActionType.Tap, "record-action", new Dictionary<string, object?>
        {
            ["source"] = "sample",
            ["count"] = activity.Count,
        });

        Record("RUM action recorded");
    }

    private void OnRecordError(object? sender, EventArgs e)
    {
        try
        {
            throw new InvalidOperationException("Something went wrong, but the app handled it.");
        }
        catch (Exception exception)
        {
            // The Exception overload reports the type, message and stack trace, so errors group by
            // where they were thrown rather than by message alone.
            Datadog.Rum.AddError(exception);
            Record($"RUM error recorded: {exception.Message}");
        }
    }

    private async void OnTrackWork(object? sender, EventArgs e)
    {
        // A view scope stops the view however the block is left, including on an exception. The
        // raw API is a StartViewWithKey/StopViewWithKey pair matched by key, and a view left open
        // by an early return captures every later action in the session.
        using (Datadog.Rum.StartView("work", "Background Work"))
        {
            Record("started tracking a unit of work");
            await Task.Delay(750);
        }

        Record("work finished, view stopped");
    }

    private async void OnShowSessionId(object? sender, EventArgs e)
    {
        // Worth attaching to a support ticket: it is what turns "the app was slow" into a session
        // you can watch. The raw binding answers through a completion block on the SDK's own queue.
        var sessionId = await Datadog.Rum.GetCurrentSessionIdAsync();

        Record($"session {sessionId ?? "(none - RUM is off, or this session was sampled out)"}");
    }

    private async void OnTraceRequest(object? sender, EventArgs e)
    {
        var tracer = DDTracer.Shared();

        // The operation name is what APM groups by, so it has to be low cardinality - never a URL
        // and never anything with an id in it.
        var span = tracer.StartSpan("http.request");

        try
        {
            span.SetTag("http.method", "GET");

            using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api/items");

            // This is the whole point of distributed tracing: the receiving service reads these
            // headers and continues the same trace, so one flame graph spans both sides.
            //
            // The raw API needs a dance that is not obvious from the signatures - construct a
            // writer, hand it to Inject as though it were the carrier, then read the headers back
            // off the writer, once per format.
            foreach (var header in span.InjectHeaders(tracer))
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Record($"trace {span.GetTraceId(tracer)}");
            Record($"propagating {request.Headers.Count()} headers");

            using var client = new HttpClient();
            using var response = await client.SendAsync(request);

            span.SetTag("http.status_code", (double)(int)response.StatusCode);
            Record($"request finished: {(int)response.StatusCode}");
        }
        catch (Exception exception)
        {
            // Sets error.type, error.message and error.stack together. Setting only the message
            // leaves a span marked as an error with nothing in the APM error panel to act on.
            span.SetError(exception);
            Record($"request failed: {exception.GetType().Name}");
        }
        finally
        {
            span.Finish();
        }
    }

    private void OnFailedSpan(object? sender, EventArgs e)
    {
        var tracer = DDTracer.Shared();
        var span = tracer.StartSpan("checkout.submit");

        span.Log(new Dictionary<string, object?>
        {
            ["event"] = "validation",
            ["fields"] = 3,
        });

        try
        {
            throw new InvalidOperationException("The cart expired before checkout completed.");
        }
        catch (Exception exception)
        {
            span.SetError(exception);
            Record($"span {span.GetSpanId(tracer)} marked as failed");
        }
        finally
        {
            span.Finish();
        }
    }

    private void OnWriteLogs(object? sender, EventArgs e)
    {
        Datadog.Logger.Log(DDLogLevel.Debug, "a debug message");
        Datadog.Logger.Log(DDLogLevel.Info, "an info message");
        Datadog.Logger.Log(DDLogLevel.Notice, "a notice");
        Datadog.Logger.Log(DDLogLevel.Warn, "a warning");
        Datadog.Logger.Log(DDLogLevel.Error, "an error");
        Datadog.Logger.Log(DDLogLevel.Critical, "something critical");

        Record("wrote six log levels");
    }

    private void OnLogException(object? sender, EventArgs e)
    {
        try
        {
            _ = int.Parse("not a number");
        }
        catch (Exception exception)
        {
            // error.kind, error.message and error.stack are set from the exception, which is what
            // makes it render as an error in the Logs UI rather than as a plain message.
            Datadog.Logger.Log(DDLogLevel.Error, "parsing failed", exception, new Dictionary<string, object?>
            {
                ["input"] = "not a number",
            });

            Record($"logged exception: {exception.GetType().Name}");
        }
    }

    private void Record(string message)
    {
        activity.Insert(0, $"{DateTime.Now:HH:mm:ss}  {message}");

        // Keep the list short enough to stay on screen.
        if (activity.Count > 12)
        {
            activity.RemoveAt(activity.Count - 1);
        }

        ActivityLabel.Text = string.Join(Environment.NewLine, activity);
    }
}
