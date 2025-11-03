namespace FitLife.Models
{
    // Represents a single day's workout or activity data for a bar chart visualization
    public class DailyBar
    {
        // The name or label of the day (e.g., "Mon", "Tue")
        public string Day { get; set; } = "";

        // Total minutes spent on workouts or activity for this day
        public int Minutes { get; set; }

        // Converts the number of minutes into a visual height for UI bars,
        // clamping the value so that every bar stays visible (min 10) and not too tall (max 120)
        public int BarHeight => Math.Clamp(Minutes * 2, 10, 120);
    }
}
