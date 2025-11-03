namespace FitLife.Models
{
    // Represents a workout or exercise session shown in the FitLife app
    // Each workout includes details like name, type, image, and tracking data
    public class Workout
    {
        // The title or name of the workout (e.g., "Morning Yoga", "Cardio Blast")
        public string Title { get; set; } = "";

        // The workout category (e.g., "Strength", "Cardio", "Flexibility")
        public string Category { get; set; } = "";

        // Path or URL to the workout image, used for visual display in the app
        public string Image { get; set; } = "";

        // Total duration of the workout in minutes
        public int DurationMins { get; set; }

        // Estimated calories burned during the workout
        public int Calories { get; set; }
    }
}
