using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceBuddy.Models;

public partial class CategorySummary : ObservableObject
{
	public Category Category { get; set; } = new Category();
	
	[ObservableProperty]
	private decimal totalAmount;
	
	[ObservableProperty]
	private bool isSelected;
}
