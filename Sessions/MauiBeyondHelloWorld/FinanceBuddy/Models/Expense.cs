namespace FinanceBuddy.Models;

public class Expense
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; } = string.Empty;
	public string Store { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
	public Category Category { get; set; } = new Category();
}
