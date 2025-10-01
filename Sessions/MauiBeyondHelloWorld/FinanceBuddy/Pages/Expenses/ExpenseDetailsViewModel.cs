using CommunityToolkit.Mvvm.ComponentModel;
using FinanceBuddy.Models;

namespace FinanceBuddy.Expenses;

[QueryProperty(nameof(Expense), "Expense")]
public partial class ExpenseDetailsViewModel : ObservableObject
{
	[ObservableProperty]
	private Expense? expense;

}
