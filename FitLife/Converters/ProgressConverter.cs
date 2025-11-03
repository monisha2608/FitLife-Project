using System.Globalization;

namespace FitLife.Converters
{
    // This converter is used in XAML bindings to turn workout progress (in minutes)
    // into a normalized value (0 to 1) that can visually fill a progress bar or ring.
    public class ProgressConverter : IValueConverter
    {
        // Converts total minutes into progress ratio based on the weekly goal.
        // Example: if a user did 90 mins out of a 210-min goal, it returns 0.43 (43% progress).
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Convert input to double (minutes completed)
                double minutes = System.Convert.ToDouble(value);

                // If a goal is passed as a parameter, use it; otherwise default to 210 (7 days × 30 mins/day)
                double goal = parameter is double d ? d : 210.0;

                // Avoid dividing by zero or negative numbers
                if (goal <= 0) return 0.0;

                // Calculate the ratio and clamp between 0 and 1 (so it never exceeds 100%)
                return Math.Min(1.0, minutes / goal);
            }
            catch
            {
                // If anything goes wrong (like invalid input), return 0 as a safe fallback
                return 0.0;
            }
        }

        // Not used — one-way conversion only (we don't convert progress back to minutes)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
