namespace FitLife.Models
{
    // Represents a single entry of a user's workout progress for a specific day
    // Used to track how many minutes they exercised versus their daily goal
    public class ProgressEntry
    {
        // The name or label of the day (e.g., "Monday", "Tue")
        public string Day { get; set; } = "";

        // The actual number of workout minutes completed on that day
        public double Minutes { get; set; }

        // The target goal for daily exercise (default is 30 minutes)
        // This helps compare actual progress against a consistent goal
        public double GoalMinutes { get; set; } = 30;
    }
}
