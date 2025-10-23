using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using FinanceBuddy.Models;
using FinanceBuddy.Services;

namespace FinanceBuddy.Expenses;

public partial class AddExpenseViewModel : ObservableObject
{
	private readonly ExpenseService _expenseService;

	[ObservableProperty]
	private DateTime _selectedDate = DateTime.Now;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsValid))]
	[NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
	private string _name = string.Empty;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsValid))]
	[NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
	private string _store = string.Empty;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsValid))]
	[NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
	private string _amount = string.Empty;

	[ObservableProperty]
	private ObservableCollection<Category> _categories = new();

	[ObservableProperty]
	private Category? _selectedCategory;

	public bool IsValid =>
		!string.IsNullOrWhiteSpace(Name) &&
		!string.IsNullOrWhiteSpace(Store) &&
		!string.IsNullOrWhiteSpace(Amount) &&
		decimal.TryParse(Amount, out _);

	public AddExpenseViewModel(ExpenseService expenseService)
	{
		_expenseService = expenseService;
		LoadCategories();
	}

	private void LoadCategories()
	{
		var categories = _expenseService.GetAllCategories();
		Categories = new ObservableCollection<Category>(categories);

		// Set first category as default
		if (Categories.Count > 0)
		{
			SelectedCategory = Categories[0];
		}
	}

	[RelayCommand(CanExecute = nameof(IsValid))]
	private async Task SaveExpense()
	{
		if (!decimal.TryParse(Amount, out decimal amountValue))
		{
			return;
		}

		var expense = new Expense
		{
			Name = Name,
			Store = Store,
			Amount = amountValue,
			Date = SelectedDate,
			Category = SelectedCategory ?? Categories[0]
		};

		_expenseService.AddExpense(expense);

		await Shell.Current.GoToAsync("..");
	}
}
