using DatadogCore;
using DatadogCrashReporting;
// DDCoreLoggerLevel is declared in DatadogInternal, which every module depends on.
using DatadogInternal;
using DatadogLogs;
using DatadogRUM;
using DatadogTrace;

namespace DatadogNetExample;

/// <summary>
/// All the Datadog setup for this app, in one place.
/// </summary>
/// <remarks>
/// This is the shape most apps end up with: one method called once at startup that configures the
/// SDK and turns on the features the app uses. Everything after that goes through
/// <see cref="Rum"/> and <see cref="Logger"/>.
/// <para>
/// Note the <c>using</c> list. Up to dd-sdk-ios 2.x every DD* type lived in one <c>DatadogObjc</c>
/// namespace; 3.0 moved them into the module each belongs to, so a per-feature import is now the
/// norm.
/// </para>
/// </remarks>
public static class Datadog
{
    /// <summary>
    /// Replace these with your own values from the Datadog UI. The placeholders let the sample run
    /// and report exactly the same way as a configured app; the events simply never arrive.
    /// </summary>
    private const string ClientToken = "<CLIENT_TOKEN>";

    private const string RumApplicationId = "<RUM_APPLICATION_ID>";

    private static DDLogger? logger;

    /// <summary>The RUM monitor, for reporting views, actions and errors.</summary>
    public static DDRUMMonitor Rum => DDRUMMonitor.Shared();

    /// <summary>The app's logger.</summary>
    public static DDLogger Logger =>
        logger ?? throw new InvalidOperationException($"Call {nameof(Initialize)} first.");

    /// <summary>Whether real credentials were supplied, so events actually reach Datadog.</summary>
    public static bool IsConfigured => !ClientToken.StartsWith('<');

    /// <summary>Configures the SDK and enables RUM, Logs, Trace and crash reporting.</summary>
    public static void Initialize()
    {
        var configuration = new DDConfiguration(clientToken: ClientToken, env: "sample")
        {
            Service = "datadognet-mac-example",
            // Pick the site your Datadog organisation is on - Us1, Us3, Us5, Eu1, Ap1, Ap2 or
            // Us1_fed. Sending to the wrong one is the most common reason events never show up.
            Site = DDSite.Us1(),
        };

        // Consent must be set before, or as part of, initialization. Pending collects events but
        // holds them on the device until consent is granted or refused, which is what a GDPR
        // prompt-on-first-launch flow wants.
        DDDatadog.InitializeWithConfiguration(configuration, DDTrackingConsent.Granted());

        // Logs SDK problems to the Xcode console. Worth leaving on while integrating - it is how
        // you find out that, say, the client token is wrong.
        DDDatadog.SetVerbosityLevel(DDCoreLoggerLevel.Warn);

        EnableRum();
        EnableLogs();
        EnableTrace();

        // No EnableSessionReplay, unlike the iOS sample: Datadog supports Session Replay on iOS
        // and iPadOS only, so on Mac Catalyst it would initialize and then record nothing.

        // Crash reporting is enabled after the SDK is initialized, never before: it attaches to
        // RUM to report the crash on the next launch. From 3.0 the engine is KSCrash, and crashes
        // are reported through RUM error tracking rather than Logs.
        DDCrashReporter.Enable();
    }

    private static void EnableRum()
    {
        var rum = new DDRUMConfiguration(applicationID: RumApplicationId)
        {
            // Percentage of sessions kept. 100 while developing; a real app usually samples down.
            SessionSampleRate = 100,
            // Detects rage taps, dead clicks and error taps.
            TrackFrustrations = true,
            TrackBackgroundEvents = true,
            // New in 3.0: reports memory warnings as RUM errors.
            TrackMemoryWarnings = true,

            // Automatic UIKit instrumentation. MAUI renders through UIKit, so these report views
            // and taps without any per-page code - the manual StartView calls in MainPage are
            // there to show the explicit API, not because they are required.
            UiKitViewsPredicate = new DDDefaultUIKitRUMViewsPredicate(),
            UiKitActionsPredicate = new DDDefaultUIKitRUMActionsPredicate(),
        };

        DDRUM.EnableWith(rum);
    }

    private static void EnableLogs()
    {
        // The convenience form from this repository's additions. The raw binding equivalent is
        // DDLogs.EnableWith (new DDLogsConfiguration (null)).
        DDLogs.Enable();

        logger = DDLogger.Create(
            name: "example",
            networkInfoEnabled: true,
            // Correlates each log with the RUM view and span active when it was written, so a log
            // line can be opened straight from the session replay.
            bundleWithRumEnabled: true,
            bundleWithTraceEnabled: true,
            printLogsToConsole: true);
    }

    private static void EnableTrace()
    {
        DDTrace.EnableWith(new DDTraceConfiguration
        {
            SampleRate = 100,
            NetworkInfoEnabled = true,
            BundleWithRumEnabled = true,
        });
    }
}
