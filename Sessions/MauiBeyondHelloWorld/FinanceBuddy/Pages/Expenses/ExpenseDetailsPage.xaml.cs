using FinanceBuddy.Expenses;

namespace FinanceBuddy.Expenses;

public partial class ExpenseDetailsPage : ContentPage
{
    public ExpenseDetailsPage(ExpenseDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
	}
}
