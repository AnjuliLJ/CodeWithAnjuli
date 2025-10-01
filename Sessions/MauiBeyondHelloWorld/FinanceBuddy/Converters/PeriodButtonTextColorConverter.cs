using System.Globalization;

namespace FinanceBuddy.Converters;

public class PeriodButtonTextColorConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is string selectedPeriod && parameter is string buttonPeriod)
		{
			return selectedPeriod == buttonPeriod
				? Colors.White
				: Color.FromArgb("#333333");
		}
		return Color.FromArgb("#333333");
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
