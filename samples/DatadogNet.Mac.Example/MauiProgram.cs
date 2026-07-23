using Microsoft.Extensions.Logging;

namespace DatadogNetExample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Initialized here rather than in a page or a platform AppDelegate, so the SDK is running
        // before any of the app's own code - crash reporting in particular only covers what
        // happens after it is enabled, and startup crashes are the ones worth catching.
        Datadog.Initialize();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
