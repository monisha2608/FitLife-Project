using FitLife.Models;

namespace FitLife.Services;

public static class MockDataService
{
    public static List<Workout> GetWorkouts() => new()
    {
        new Workout { Title="HIIT Blast", Category="Cardio", DurationMins=20, Calories=180, Image="workout1.png" },
        new Workout { Title="Leg Day", Category="Strength", DurationMins=35, Calories=260, Image="workout2.png" },
        new Workout { Title="Yoga Flow", Category="Flexibility", DurationMins=25, Calories=120, Image="workout3.png" },
        new Workout { Title="Core Crush", Category="Strength", DurationMins=30, Calories=220, Image="workout4.png" }
    };

    public static List<Meal> GetMeals() => new()
    {
        new Meal { Name="Oats & Berries", Type="Breakfast", Calories=320, Image="meal1.png" },
        new Meal { Name="Chicken Salad",  Type="Lunch",     Calories=450, Image="meal2.png" },
        new Meal { Name="Grilled Salmon", Type="Dinner",    Calories=520, Image="meal3.png" },
        new Meal { Name="Greek Yogurt",   Type="Snack",     Calories=180, Image="meal4.png" }
    };

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
