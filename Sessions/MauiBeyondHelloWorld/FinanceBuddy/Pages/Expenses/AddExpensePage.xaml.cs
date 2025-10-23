namespace FinanceBuddy.Expenses;

public partial class AddExpensePage : ContentPage
{
	public AddExpensePage(AddExpenseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
