using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceBuddy.Models;

namespace FinanceBuddy.Expenses;

[QueryProperty(nameof(Expense), "Expense")]
public partial class ExpenseDetailsViewModel : ObservableObject
{
	[ObservableProperty]
	private Expense? expense;

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.GoToAsync("..");
		
	}
}
