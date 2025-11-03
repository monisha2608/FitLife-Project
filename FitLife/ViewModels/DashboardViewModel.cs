using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for the dashboard screen
    public class DashboardViewModel : BaseViewModel
    {
        // Workouts to display on the dashboard (e.g., today’s list)
        public ObservableCollection<Workout> TodayWorkouts { get; }

        private int _caloriesToday;
        public int CaloriesToday
        {
            get => _caloriesToday;
            set => Set(ref _caloriesToday, value);
        }

        private int _minutesThisWeek;
        public int MinutesThisWeek
        {
            get => _minutesThisWeek;
            set => Set(ref _minutesThisWeek, value);
        }

        // Commands for navigation (linked with buttons in XAML)
        public ICommand GoToWorkoutsCommand { get; }
        public ICommand GoToMealsCommand { get; }
        public ICommand GoToProgressCommand { get; }

        public DashboardViewModel()
        {
            // Load sample data
            TodayWorkouts = new ObservableCollection<Workout>(MockDataService.GetWorkouts().Take(3));

            // Calculate totals
            CaloriesToday = TodayWorkouts.Sum(w => w.Calories);
            MinutesThisWeek = MockDataService.GetWeeklyProgress().Sum(p => (int)p.Minutes);

            // Page navigation commands
            GoToWorkoutsCommand = new Command(async () => await Shell.Current.GoToAsync("//workouts"));
            GoToMealsCommand = new Command(async () => await Shell.Current.GoToAsync("//meals"));
            GoToProgressCommand = new Command(async () => await Shell.Current.GoToAsync("//progress"));
        }
    }
}
