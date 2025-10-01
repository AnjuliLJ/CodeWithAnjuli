using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceBuddy.Models;
using FinanceBuddy.Services;
using System.Collections.ObjectModel;

namespace FinanceBuddy.Charts;

public partial class ChartsViewModel : ObservableObject
{
	private readonly ExpenseService _expenseService;

	[ObservableProperty]
	private ObservableCollection<ChartDataPoint> chartData = new();

	[ObservableProperty]
	private ObservableCollection<CategorySummary> categorySummaries = new();

	[ObservableProperty]
	private string selectedPeriodType = "Week";

	[ObservableProperty]
	private DateTime currentDate = DateTime.Now;

	[ObservableProperty]
	private string currentPeriodLabel = string.Empty;

	[ObservableProperty]
	private bool isAnyCategorySelected;

	[ObservableProperty]
	private CategorySummary? selectedCategory;

	public ChartsViewModel(ExpenseService expenseService)
	{
		_expenseService = expenseService;
		UpdatePeriodLabel();
		LoadChartData();
		LoadCategorySummaries();
	}

	[RelayCommand]
	private void SelectWeek()
	{
		SelectedPeriodType = "Week";
	}

	[RelayCommand]
	private void SelectMonth()
	{
		SelectedPeriodType = "Month";
	}

	[RelayCommand]
	private void SelectYear()
	{
		SelectedPeriodType = "Year";
	}

	[RelayCommand]
	private void NavigatePrevious()
	{
		CurrentDate = SelectedPeriodType switch
		{
			"Week" => CurrentDate.AddDays(-7),
			"Month" => CurrentDate.AddMonths(-1),
			"Year" => CurrentDate.AddYears(-1),
			_ => CurrentDate
		};
	}

	[RelayCommand]
	private void NavigateNext()
	{
		CurrentDate = SelectedPeriodType switch
		{
			"Week" => CurrentDate.AddDays(7),
			"Month" => CurrentDate.AddMonths(1),
			"Year" => CurrentDate.AddYears(1),
			_ => CurrentDate
		};
	}

	[RelayCommand]
	private void SelectCategory(CategorySummary category)
	{
		if (SelectedCategory == category)
		{
			RemoveSelection();
			return;
		}

		// Deselect previous category
		if (SelectedCategory != null)
		{
			SelectedCategory.IsSelected = false;
		}

		SelectedCategory = category;
		category.IsSelected = true;
		IsAnyCategorySelected = true;
		LoadChartData();
	}

	[RelayCommand]
	private void RemoveSelection()
	{
		if (SelectedCategory != null)
		{
			SelectedCategory.IsSelected = false;
			SelectedCategory = null;
		}
		IsAnyCategorySelected = false;
		LoadChartData();
	}

	partial void OnSelectedPeriodTypeChanged(string value)
	{
		UpdatePeriodLabel();
		LoadChartData();
		LoadCategorySummaries();
	}

	partial void OnCurrentDateChanged(DateTime value)
	{
		UpdatePeriodLabel();
		LoadChartData();
		LoadCategorySummaries();
	}

	private void LoadChartData()
	{
		var expenses = SelectedPeriodType switch
		{
			"Week" => _expenseService.GetExpensesByWeek(CurrentDate),
			"Month" => _expenseService.GetExpensesByMonth(CurrentDate),
			"Year" => _expenseService.GetExpensesByYear(CurrentDate),
			_ => new List<Expense>()
		};

		// Apply category filter if selected
		if (SelectedCategory != null)
		{
			expenses = expenses.Where(e => e.Category.Name == SelectedCategory.Category.Name).ToList();
		}

		var dataPoints = new List<ChartDataPoint>();

		if (SelectedPeriodType == "Week")
		{
			// Group by day
			var grouped = expenses.GroupBy(e => e.Date.Date)
				.OrderBy(g => g.Key);

			foreach (var group in grouped)
			{
				dataPoints.Add(new ChartDataPoint
				{
					Label = group.Key.ToString("ddd"),
					Amount = group.Sum(e => e.Amount),
					Date = group.Key
				});
			}
		}
		else if (SelectedPeriodType == "Month")
		{
			// Group by week
			var grouped = expenses.GroupBy(e => _expenseService.GetWeekNumber(e.Date))
				.OrderBy(g => g.Key);

			foreach (var group in grouped)
			{
				dataPoints.Add(new ChartDataPoint
				{
					Label = $"Week {group.Key}",
					Amount = group.Sum(e => e.Amount),
					Date = group.First().Date
				});
			}
		}
		else if (SelectedPeriodType == "Year")
		{
			// Group by month
			var grouped = expenses.GroupBy(e => e.Date.Month)
				.OrderBy(g => g.Key);

			foreach (var group in grouped)
			{
				dataPoints.Add(new ChartDataPoint
				{
					Label = new DateTime(CurrentDate.Year, group.Key, 1).ToString("MMM"),
					Amount = group.Sum(e => e.Amount),
					Date = new DateTime(CurrentDate.Year, group.Key, 1)
				});
			}
		}

		ChartData = new ObservableCollection<ChartDataPoint>(dataPoints);
	}

	private void LoadCategorySummaries()
	{
		var expenses = SelectedPeriodType switch
		{
			"Week" => _expenseService.GetExpensesByWeek(CurrentDate),
			"Month" => _expenseService.GetExpensesByMonth(CurrentDate),
			"Year" => _expenseService.GetExpensesByYear(CurrentDate),
			_ => new List<Expense>()
		};

		var summaries = expenses
			.GroupBy(e => e.Category.Name)
			.Select(g =>
			{
				var summary = new CategorySummary
				{
					Category = g.First().Category,
					TotalAmount = g.Sum(e => e.Amount),
					IsSelected = SelectedCategory?.Category.Name == g.Key
				};
				return summary;
			})
			.OrderByDescending(s => s.TotalAmount)
			.ToList();

		CategorySummaries = new ObservableCollection<CategorySummary>(summaries);
	}

	private void UpdatePeriodLabel()
	{
		CurrentPeriodLabel = SelectedPeriodType switch
		{
			"Week" => $"Week {_expenseService.GetWeekNumber(CurrentDate)}",
			"Month" => CurrentDate.ToString("MMMM yyyy"),
			"Year" => CurrentDate.ToString("yyyy"),
			_ => string.Empty
		};
	}
}
