using SQLite;

namespace FitLife.Models
{
    // Local model for storing service data
    public class Service
    {
        // Primary key for local database
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Name of the service
        public string Name { get; set; } = string.Empty;

        // Type of workout or class
        public string Type { get; set; } = string.Empty;

        // Description of the service
        public string Description { get; set; } = string.Empty;

        // Trainer name
        public string Trainer { get; set; } = string.Empty;

        // Day of the service
        public string DayOfWeek { get; set; } = string.Empty;

        // Start time of the service
        public string StartTime { get; set; } = string.Empty;

        // Duration in minutes
        public int DurationMins { get; set; }

        // Price of the service
        public decimal Price { get; set; }

        // Level of the service
        public string Level { get; set; } = string.Empty;
    }
}
