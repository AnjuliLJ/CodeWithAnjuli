using System.Globalization;

namespace FinanceBuddy.Converters;

public class BoolToStrokeThicknessConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is bool isSelected)
		{
			return isSelected ? 2 : 0;
		}
		return 0;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
