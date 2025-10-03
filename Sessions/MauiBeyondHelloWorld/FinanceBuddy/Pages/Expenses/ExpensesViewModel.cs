using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using FinanceBuddy.Models;
using FinanceBuddy.Services;

namespace FinanceBuddy.Expenses;

public partial class ExpensesViewModel : ObservableObject
{
	private readonly ExpenseService _expenseService;

	[ObservableProperty]
	private string _currentMonth = string.Empty;

	[ObservableProperty]
	private decimal _totalSpent;

	[ObservableProperty]
	private ObservableCollection<Expense> _expenses = new();

	public ExpensesViewModel(ExpenseService expenseService)
	{
		_expenseService = expenseService;
		LoadExpenses(DateTime.Now);
	}

	private void LoadExpenses(DateTime date)
	{
		// Set current month display
		CurrentMonth = date.ToString("MMMM yyyy");

		// Get expenses for the month
		var expensesList = _expenseService.GetExpensesByMonth(date);
		
		// Update expenses collection
		Expenses = new ObservableCollection<Expense>(expensesList);

		// Calculate total spent
		TotalSpent = Expenses.Sum(e => e.Amount);
	}

	[RelayCommand]
	private async Task ViewExpenseDetails(Expense expense)
	{
		var navigationParameter = new Dictionary<string, object>
		{
			{ "Expense", expense }
		};

		await Shell.Current.GoToAsync("ExpenseDetailsPage", navigationParameter);
	}
}