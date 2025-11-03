using FitLife.Models;

namespace FitLife.Services
{
    // This class provides sample (mock) data for the FitLife app.
    // It's mainly used during development to test the UI without needing a real database or API.
    public static class MockDataService
    {
        // Returns a list of example workouts that can be shown on the dashboard or workouts page.
        // Each workout includes its title, category, duration, calories burned, and an image.
        public static List<Workout> GetWorkouts() => new()
        {
            new Workout { Title="HIIT Blast", Category="Cardio",      DurationMins=20, Calories=180, Image="workout1.png" },
            new Workout { Title="Leg Day",    Category="Strength",    DurationMins=35, Calories=260, Image="workout2.png" },
            new Workout { Title="Yoga Flow",  Category="Flexibility", DurationMins=25, Calories=120, Image="workout3.png" },
            new Workout { Title="Core Crush", Category="Strength",    DurationMins=30, Calories=220, Image="workout4.png" }
        };

        // Returns a list of sample meals to populate the meals screen.
        // Useful for showing how meal cards look with names, types, calories, and images.
        public static List<Meal> GetMeals() => new()
        {
            new Meal { Name="Oats & Berries", Type="Breakfast", Calories=320, Image="meal1.png" },
            new Meal { Name="Chicken Salad",  Type="Lunch",     Calories=450, Image="meal2.png" },
            new Meal { Name="Grilled Salmon", Type="Dinner",    Calories=520, Image="meal3.png" },
            new Meal { Name="Greek Yogurt",   Type="Snack",     Calories=180, Image="meal4.png" }
        };

        // Returns example weekly progress data.
        // Each entry represents total workout minutes logged for that day.
        // This data is typically used for charts or progress bars in the dashboard.
        public static List<ProgressEntry> GetWeeklyProgress() => new()
        {
            new ProgressEntry { Day="Mon", Minutes=20 },
            new ProgressEntry { Day="Tue", Minutes=35 },
            new ProgressEntry { Day="Wed", Minutes=0  },
            new ProgressEntry { Day="Thu", Minutes=25 },
            new ProgressEntry { Day="Fri", Minutes=40 },
            new ProgressEntry { Day="Sat", Minutes=15 },
            new ProgressEntry { Day="Sun", Minutes=30 }
        };
    }
}
