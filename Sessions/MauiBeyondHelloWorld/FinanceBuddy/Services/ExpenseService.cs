using FinanceBuddy.Models;

namespace FinanceBuddy.Services;

public class ExpenseService
{
	private readonly List<Category> _categories;
	private readonly List<Expense> _userExpenses;

	public ExpenseService()
	{
		_categories = new List<Category>
		{
			new Category { Name = "Food", Icon = "\ue56c", IconColor = Color.FromRgba("#FFE5E5") },
			new Category { Name = "Transport", Icon = "\ue530", IconColor = Color.FromRgba("#E5F0FF") },
			new Category { Name = "Groceries", Icon = "\ue8cc", IconColor = Color.FromRgba("#E5FFE5") },
			new Category { Name = "Entertainment", Icon = "\ue02c", IconColor = Color.FromRgba("#F0E5FF") },
			new Category { Name = "Coffee", Icon = "\uef45", IconColor = Color.FromRgba("#FFF5E5") }
		};
		_userExpenses = new List<Expense>();
	}

	public List<Expense> GetExpensesByMonth(DateTime date)
	{
		var startOfMonth = new DateTime(date.Year, date.Month, 1);
		var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
		var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
		var expenses = new List<Expense>();

		var random = new Random(date.Year * 10000 + date.Month * 100);

		for (int day = 1; day <= daysInMonth; day++)
		{
			var currentDate = startOfMonth.AddDays(day - 1);
			var numberOfExpensesForDay = random.Next(1, 4); // 1-3 expenses per day

			for (int i = 0; i < numberOfExpensesForDay; i++)
			{
				var category = _categories[random.Next(_categories.Count)];
				var amount = random.Next(10, 150);

				expenses.Add(new Expense
				{
					Name = $"{category.Name} expense",
					Store = $"Store {random.Next(1, 15)}",
					Amount = amount,
					Date = currentDate.AddHours(random.Next(8, 20)),
					Category = category
				});
			}
		}

		// Add user expenses for this month
		var userExpensesForMonth = _userExpenses
			.Where(e => e.Date >= startOfMonth && e.Date <= endOfMonth)
			.ToList();
		expenses.AddRange(userExpensesForMonth);

		// Sort by date descending
		return expenses.OrderByDescending(e => e.Date).ToList();
	}

	public void AddExpense(Expense expense)
	{
		_userExpenses.Add(expense);
	}

	public List<Expense> GetExpensesByWeek(DateTime date)
	{
		var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

		var expenses = new List<Expense>();

		var random = new Random(startOfWeek.Year * 10000 + startOfWeek.DayOfYear);

		for (int i = 0; i < 7; i++)
		{
			var day = startOfWeek.AddDays(i);
			var numberOfExpensesForDay = random.Next(2, 5); // 2-4 expenses per day

			for (int j = 0; j < numberOfExpensesForDay; j++)
			{
				var category = _categories[random.Next(_categories.Count)];
				var amount = random.Next(10, 150);

				expenses.Add(new Expense
				{
					Name = $"{category.Name} expense",
					Store = $"Store {random.Next(1, 10)}",
					Amount = amount,
					Date = day.AddHours(random.Next(8, 20)),
					Category = category
				});
			}
		}

		return expenses;
	}

	public List<Expense> GetExpensesByYear(DateTime date)
	{
		var startOfYear = new DateTime(date.Year, 1, 1);
		var expenses = new List<Expense>();

		var random = new Random(date.Year * 10000);

		for (int month = 0; month < 12; month++)
		{
			var monthDate = startOfYear.AddMonths(month);
			var numberOfExpensesForMonth = random.Next(15, 30);

			for (int i = 0; i < numberOfExpensesForMonth; i++)
			{
				var category = _categories[random.Next(_categories.Count)];
				var amount = random.Next(20, 200);

				expenses.Add(new Expense
				{
					Name = $"{category.Name} expense",
					Store = $"Store {random.Next(1, 20)}",
					Amount = amount,
					Date = monthDate.AddDays(random.Next(0, 28)).AddHours(random.Next(8, 20)),
					Category = category
				});
			}
		}

		return expenses;
	}

	public List<Category> GetAllCategories()
	{
		return _categories;
	}

	public int GetWeekNumber(DateTime date)
	{
		var day = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
		if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
		{
			date = date.AddDays(3);
		}

		return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
			date,
			System.Globalization.CalendarWeekRule.FirstFourDayWeek,
			DayOfWeek.Monday);
	}
}
