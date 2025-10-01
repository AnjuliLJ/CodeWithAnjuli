using System.Globalization;

namespace FinanceBuddy.Converters;

public class PeriodButtonColorConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is string selectedPeriod && parameter is string buttonPeriod)
		{
			return selectedPeriod == buttonPeriod
				? Color.FromArgb("#5B9BED") 
				: Color.FromArgb("#E0F2FE");
		}
		return Color.FromArgb("#E0F2FE");
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
