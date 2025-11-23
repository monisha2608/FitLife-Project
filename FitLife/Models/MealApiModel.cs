namespace FitLife.Models
{
    // Model used to map meal data from the API
    public class MealApiModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }

        // Name of the meal
        public string Name { get; set; } = string.Empty;

        // Type of meal
        public string Type { get; set; } = string.Empty;

        // Calories value
        public int Calories { get; set; }

        // Description of the meal
        public string Description { get; set; } = string.Empty;

        // Day for the meal plan
        public string DayOfWeek { get; set; } = "Monday";

        // Time of day for the meal
        public string TimeOfDay { get; set; } = "Any";
    }
}
