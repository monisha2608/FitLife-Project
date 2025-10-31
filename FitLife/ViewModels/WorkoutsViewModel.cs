using System.Collections.ObjectModel;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels;

public class WorkoutsViewModel : BaseViewModel
{
    public ObservableCollection<Workout> Workouts { get; } =
        new ObservableCollection<Workout>(MockDataService.GetWorkouts());

    public Command AddWorkoutCmd { get; }

    public WorkoutsViewModel()
    {
        AddWorkoutCmd = new Command(async () => await AddWorkout());
    }

    private async Task AddWorkout()
    {
        string title = await Application.Current!.MainPage!.DisplayPromptAsync("Add workout", "Workout title:");
        if (string.IsNullOrWhiteSpace(title)) return;
        var w = new Workout { Title = title, Category = "Custom", DurationMins = 20, Calories = 150, Image = "workout1.png" };
        Workouts.Add(w);
    }
}
