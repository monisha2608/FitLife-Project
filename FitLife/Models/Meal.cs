namespace FitLife.Models
{
    // Represents a single meal item in the FitLife app
    // Used to display meal info such as type, calories, and image
    public class Meal
    {
        // The name of the meal (e.g., "Grilled Chicken Salad", "Oatmeal Bowl")
        public string Name { get; set; } = "";

        // The category or type of meal (e.g., "Breakfast", "Lunch", "Snack")
        public string Type { get; set; } = "";

        // Total calories for this meal — used for tracking daily intake
        public int Calories { get; set; }

        // Image path or URL for displaying the meal visually in the app
        public string Image { get; set; } = "";
    }
}
