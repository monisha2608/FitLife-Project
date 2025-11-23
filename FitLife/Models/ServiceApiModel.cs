namespace FitLife.Models
{
    // Model for mapping service data from the API
    public class ServiceApiModel
    {
        public int Id { get; set; }

        // Service name
        public string Name { get; set; } = string.Empty;

        // Type of service
        public string? Type { get; set; }

        // Description text
        public string? Description { get; set; }

        // Trainer name
        public string? Trainer { get; set; }

        // Day of the service
        public string? DayOfWeek { get; set; }

        // Start time of the service
        public string? StartTime { get; set; }

        // Duration in minutes
        public int DurationMins { get; set; }

        // Price of the service
        public decimal Price { get; set; }

        // Difficulty level
        public string? Level { get; set; }
    }
}
