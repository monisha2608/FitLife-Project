namespace FitLife.Api.Models
{
    // Represents a meal in the system
    public class MealEntity
    {
        public int Id { get; set; }

        // Links this meal to a service like Yoga or HIIT
        public int ServiceId { get; set; }

        // Name of the meal
        public string Name { get; set; } = string.Empty;

        // Type of meal like Breakfast or Lunch
        public string Type { get; set; } = string.Empty;

        // Total calories in the meal
        public int Calories { get; set; }

        // Description of the meal
        public string Description { get; set; } = string.Empty;

        // Day when this meal is suggested
        public string DayOfWeek { get; set; } = "Monday";

        // Time of day for the meal
        public string TimeOfDay { get; set; } = "Any";
    }
}
