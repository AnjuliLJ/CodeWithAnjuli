namespace FinanceBuddy.Expenses;

public partial class ExpensesPage : ContentPage
{
	public ExpensesPage(ExpensesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
