using System.Collections.ObjectModel;
using System.Linq;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels;

public class MealsViewModel : BaseViewModel
{
    public ObservableCollection<Meal> Meals { get; } =
        new ObservableCollection<Meal>(MockDataService.GetMeals());

    public IEnumerable<Meal> Breakfast => Meals.Where(m => m.Type == "Breakfast");
    public IEnumerable<Meal> Lunch => Meals.Where(m => m.Type == "Lunch");
    public IEnumerable<Meal> Dinner => Meals.Where(m => m.Type == "Dinner");
    public IEnumerable<Meal> Snack => Meals.Where(m => m.Type == "Snack");

    public int TotalCalories => Meals.Sum(m => m.Calories);

    public Command AddMealCmd { get; }

    public MealsViewModel()
    {
        AddMealCmd = new Command(async () => await AddMeal());
    }

    private async Task AddMeal()
    {
        string name = await Application.Current!.MainPage!.DisplayPromptAsync("Add meal", "Meal name:");
        if (string.IsNullOrWhiteSpace(name)) return;
        var m = new Meal { Name = name, Type = "Snack", Calories = 200, Image = "meal1.png" };
        Meals.Add(m);
        OnPropertyChanged(nameof(TotalCalories));
    }
}
