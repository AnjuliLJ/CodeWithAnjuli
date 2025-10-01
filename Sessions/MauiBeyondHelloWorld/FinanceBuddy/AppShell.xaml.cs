using FinanceBuddy.Categories;
using FinanceBuddy.Expenses;

namespace FinanceBuddy;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes for navigation
		Routing.RegisterRoute("CategoriesPage", typeof(CategoriesPage));
		Routing.RegisterRoute("ExpenseDetailsPage", typeof(ExpenseDetailsPage));
	}
}
