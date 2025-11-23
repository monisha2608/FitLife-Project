namespace FitLife.Models
{
    // Model for mapping workout data from the API
    public class WorkoutApiModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }

        // Title of the workout
        public string Title { get; set; } = string.Empty;

        // Description of the workout
        public string? Description { get; set; }

        // Category like cardio or strength
        public string? Category { get; set; }

        // Duration in minutes
        public int DurationMins { get; set; }

        // Calories burned
        public int Calories { get; set; }

        // Difficulty level
        public string? Level { get; set; }
    }
}
