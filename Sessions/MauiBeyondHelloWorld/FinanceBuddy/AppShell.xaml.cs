using FinanceBuddy.Categories;
using FinanceBuddy.Expenses;

namespace FinanceBuddy;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute("CategoriesPage", typeof(CategoriesPage));
		Routing.RegisterRoute("ExpenseDetailsPage", typeof(ExpenseDetailsPage));
		Routing.RegisterRoute("AddExpensePage", typeof(AddExpensePage));
	
		//  Shell.Current.GoToAsync("/CategoriesPage");		Relative URI navigation
		// Shell.Current.GoToAsync("//ExpensesPage");	Absolute URI navigation
		// Shell.Current.GoToAsync("///ExpenseDetailsPage");	Navigate to root and then to ExpenseDetailsPage (Global root)
	}
}
