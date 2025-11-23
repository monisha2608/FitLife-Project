using System.Globalization;

namespace FitLife.Converters
{
    // Converts minutes into progress value for UI
    public class ProgressConverter : IValueConverter
    {
        // Converts minutes to a progress ratio
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Get completed minutes
                double minutes = System.Convert.ToDouble(value);

                // Use goal if provided, otherwise use default
                double goal = parameter is double d ? d : 210.0;

                // Prevent division errors
                if (goal <= 0) return 0.0;

                // Return progress between 0 and 1
                return Math.Min(1.0, minutes / goal);
            }
            catch
            {
                // Return 0 if conversion fails
                return 0.0;
            }
        }

        // One-way only, not converting back
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
