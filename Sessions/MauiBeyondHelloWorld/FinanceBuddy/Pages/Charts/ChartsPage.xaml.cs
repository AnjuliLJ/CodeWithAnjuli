namespace FinanceBuddy.Charts;

public partial class ChartsPage : ContentPage
{
	public ChartsPage(ChartsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
