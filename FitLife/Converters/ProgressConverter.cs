using System.Globalization;

namespace FitLife.Converters;

public class ProgressConverter : IValueConverter
{
    // value = minutes done, parameter = weekly goal (double)
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            double minutes = System.Convert.ToDouble(value);
            double goal = parameter is double d ? d : 210.0; // default 7*30
            if (goal <= 0) return 0.0;
            return Math.Min(1.0, minutes / goal);
        }
        catch
        {
            return 0.0;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
