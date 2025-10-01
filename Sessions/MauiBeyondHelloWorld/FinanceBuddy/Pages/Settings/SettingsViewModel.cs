using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FinanceBuddy.Settings;

public partial class SettingsViewModel : ObservableObject
{
	[ObservableProperty]
	private bool keepMeLoggedIn;

	[ObservableProperty]
	private string selectedFirstDayOfWeek = "Monday";

	public List<string> DaysOfWeek { get; set; }

	public SettingsViewModel()
	{
		DaysOfWeek = new List<string>
		{
			"Monday",
			"Tuesday",
			"Wednesday",
			"Thursday",
			"Friday",
			"Saturday",
			"Sunday"
		};
	}

	[RelayCommand]
	public async Task NavigateToCategories()
	{
		await Shell.Current.GoToAsync("CategoriesPage");
	}
}
