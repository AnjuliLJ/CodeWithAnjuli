using Microsoft.Extensions.Logging;
using PassXYZ.Vault.Models;
using PassXYZ.Vault.Services;
using PassXYZ.Vault.ViewModels;
using PassXYZ.Vault.Views;

namespace PassXYZ.Vault;

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

		// Views
		builder.Services.AddScoped<ItemsPage>();
        builder.Services.AddScoped<ItemDetailPage>();

        // ViewModels
        builder.Services.AddScoped<ItemDetailViewModel>();


		//Models
		builder.Services.AddSingleton<IDataStore<Item>, MockDataStore>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
