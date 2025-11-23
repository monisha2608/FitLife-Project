using System.Collections.ObjectModel;
using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for the dashboard screen
    public class DashboardViewModel : BaseViewModel
    {
        // API client for workouts
        private readonly WorkoutsApiClient _workoutsApi = new();

        // List of today's workouts
        public ObservableCollection<WorkoutApiModel> TodayWorkouts { get; } = new();

        // Total calories burned today
        private int _caloriesToday;
        public int CaloriesToday
        {
            get => _caloriesToday;
            set => Set(ref _caloriesToday, value);
        }

        // Total workout minutes for the week
        private int _minutesThisWeek;
        public int MinutesThisWeek
        {
            get => _minutesThisWeek;
            set => Set(ref _minutesThisWeek, value);
        }

        // Navigation commands
        public Command GoToWorkoutsCommand { get; }
        public Command GoToProgressCommand { get; }

        public DashboardViewModel()
        {
            // Navigate to workouts page
            GoToWorkoutsCommand = new Command(async () => await Shell.Current.GoToAsync("//workouts"));

            // Navigate to progress page
            GoToProgressCommand = new Command(async () => await Shell.Current.GoToAsync("//progress"));
        }

        // Load dashboard data
        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Clear old data
                TodayWorkouts.Clear();
                CaloriesToday = 0;
                MinutesThisWeek = 0;

                // Check if user registered for a service
                if (!AppState.IsRegisteredForService || !AppState.RegisteredServiceId.HasValue)
                    return;

                int serviceId = AppState.RegisteredServiceId.Value;

                // Get workouts from API
                var allWorkouts = await _workoutsApi.GetWorkoutsAsync();

                // Filter workouts by service
                var serviceWorkouts = allWorkouts
                    .Where(w => w.ServiceId == serviceId)
                    .ToList();

                // Show only top 3 workouts
                foreach (var w in serviceWorkouts.Take(3))
                    TodayWorkouts.Add(w);

                // Calculate totals
                MinutesThisWeek = serviceWorkouts.Sum(w => w.DurationMins);
                CaloriesToday = serviceWorkouts.Sum(w => w.Calories);
            }
            catch (Exception ex)
            {
                // Show error message
                var page = Application.Current.MainPage;
                if (page != null)
                    await page.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                // Reset loading state
                IsBusy = false;
            }
        }
    }
}
