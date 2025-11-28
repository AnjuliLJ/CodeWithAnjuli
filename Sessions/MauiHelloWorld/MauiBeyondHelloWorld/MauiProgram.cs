using Microsoft.Extensions.Logging;
using MauiBeyondHelloWorld.Views;
using MauiBeyondHelloWorld.Views.Layouts;

namespace MauiBeyondHelloWorld;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Register pages and ViewModels
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddTransient<AbsoluteLayoutPage>();
		builder.Services.AddTransient<AbsoluteLayoutViewModel>();


		builder.ConfigureMauiHandlers(handlers =>
        {
#if IOS
            handlers.AddHandler(typeof(Entry), typeof(MauiBeyondHelloWorld.Platforms.iOS.Handlers.EntryCustomHandler));
#endif
        });

		return builder.Build();
	}
}
