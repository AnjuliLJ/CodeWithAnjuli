using System.Threading.Tasks;

namespace MauiBeyondHelloWorld.Pages.Layouts;

public partial class LayoutsPage : ContentPage
{
	public LayoutsPage()
	{
		InitializeComponent();
	}

	private async void AbsoluteButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(AbsoluteLayoutPage));
	}

	private async void GridButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(GridLayoutPage));
	}

	private async void HorizontalStackButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(HorizontalStackLayoutPage));
	}

	private async void FlexButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FlexLayoutPage));
	}
}