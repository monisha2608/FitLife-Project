using System.Collections.ObjectModel;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public ObservableCollection<Workout> TodayWorkouts { get; }
    private int _totalCaloriesToday;
    public int TotalCaloriesToday { get => _totalCaloriesToday; set => Set(ref _totalCaloriesToday, value); }

    private int _minutesThisWeek;
    public int MinutesThisWeek { get => _minutesThisWeek; set => Set(ref _minutesThisWeek, value); }

    public Command NavigateWorkoutsCmd { get; }
    public Command NavigateMealsCmd { get; }
    public Command SeeProgressCmd { get; }

    public DashboardViewModel()
    {
        TodayWorkouts = new ObservableCollection<Workout>(MockDataService.GetWorkouts().Take(3));
        TotalCaloriesToday = TodayWorkouts.Sum(w => w.Calories);
        MinutesThisWeek = MockDataService.GetWeeklyProgress().Sum(p => (int)p.Minutes);

        NavigateWorkoutsCmd = new Command(async () => await Shell.Current.GoToAsync("//Workouts"));
        NavigateMealsCmd = new Command(async () => await Shell.Current.GoToAsync("//Meals"));
        SeeProgressCmd = new Command(async () => await Shell.Current.GoToAsync("//Progress"));
    }
}
