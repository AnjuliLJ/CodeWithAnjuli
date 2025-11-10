using Microsoft.Extensions.Logging;
using FinanceBuddy.Expenses;
using FinanceBuddy.Charts;
using FinanceBuddy.Settings;
using FinanceBuddy.Categories;
using FinanceBuddy.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FinanceBuddy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbolsOutlined");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		builder.Services.AddTransient<ExpenseService>();

		// ViewModels
		builder.Services.AddTransient<ExpensesViewModel>();
		builder.Services.AddTransient<ExpenseDetailsViewModel>();
		builder.Services.AddTransient<AddExpenseViewModel>();
		builder.Services.AddTransient<ChartsViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();
		builder.Services.AddTransient<CategoriesViewModel>();

		// Pages
		builder.Services.AddTransient<ExpensesPage>();
		builder.Services.AddTransient<ExpenseDetailsPage>();
		builder.Services.AddTransient<AddExpensePage>();
		builder.Services.AddTransient<ChartsPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<CategoriesPage>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
