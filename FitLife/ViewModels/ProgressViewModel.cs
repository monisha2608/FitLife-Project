using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for progress screen
    public class ProgressViewModel : BaseViewModel
    {
        // API client for workouts
        private readonly WorkoutsApiClient _workoutsApi = new();

        // Weekly progress list
        public ObservableCollection<ProgressDay> Weekly { get; } = new();

        // Total minutes completed
        private double _minutesSum;
        public double MinutesSum
        {
            get => _minutesSum;
            set => Set(ref _minutesSum, value);
        }

        // Weekly goal in minutes
        private double _weeklyGoalMinutes = 210;
        public double WeeklyGoalMinutes
        {
            get => _weeklyGoalMinutes;
            set => Set(ref _weeklyGoalMinutes, value);
        }

        // Progress ratio for UI
        public double ProgressRatio =>
            WeeklyGoalMinutes <= 0 ? 0 : MinutesSum / WeeklyGoalMinutes;

        // Command to load progress
        public ICommand LoadProgressCommand { get; }

        public ProgressViewModel()
        {
            Title = "Progress";
            LoadProgressCommand = new Command(async () => await LoadAsync(), () => !IsBusy);
        }

        // Load weekly progress data
        private async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Clear old values
                Weekly.Clear();
                MinutesSum = 0;

                // Check if user is registered to a service
                if (!AppState.IsRegisteredForService || !AppState.RegisteredServiceId.HasValue)
                    return;

                int serviceId = AppState.RegisteredServiceId.Value;

                // Get workouts from API
                var workouts = await _workoutsApi.GetWorkoutsAsync(serviceId);

                // Prepare week structure
                var dayNames = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
                var dayTotals = dayNames.ToDictionary(d => d, d => 0.0);

                int index = 0;
                foreach (var w in workouts)
                {
                    var day = dayNames[index % dayNames.Length];
                    dayTotals[day] += w.DurationMins;
                    index++;
                }

                // Fill weekly list
                foreach (var day in dayNames)
                {
                    var minutes = dayTotals[day];
                    Weekly.Add(new ProgressDay
                    {
                        Day = day,
                        Minutes = minutes
                    });
                }

                // Calculate total minutes
                MinutesSum = Weekly.Sum(d => d.Minutes);

                // Update progress ratio
                OnPropertyChanged(nameof(ProgressRatio));
            }
            catch (System.Exception ex)
            {
                // Show error if something fails
                var page = Application.Current.MainPage;
                if (page != null)
                    await page.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
