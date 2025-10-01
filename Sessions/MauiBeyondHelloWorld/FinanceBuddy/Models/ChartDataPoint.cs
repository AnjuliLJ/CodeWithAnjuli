namespace FinanceBuddy.Models;

public class ChartDataPoint
{
	public string Label { get; set; } = string.Empty;
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
