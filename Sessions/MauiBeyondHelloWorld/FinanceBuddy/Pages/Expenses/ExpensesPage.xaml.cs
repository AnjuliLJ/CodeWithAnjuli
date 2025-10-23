namespace FinanceBuddy.Expenses;

public partial class ExpensesPage : ContentPage
{
	private readonly ExpensesViewModel _viewModel;

	public ExpensesPage(ExpensesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.RefreshExpenses();
	}
}
