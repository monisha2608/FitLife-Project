using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for workouts list screen
    public class WorkoutsViewModel : BaseViewModel
    {
        // API client for workouts
        private readonly WorkoutsApiClient _apiClient = new();

        // List of workouts
        public ObservableCollection<WorkoutApiModel> Workouts { get; } = new();

        // Controls admin button visibility
        private bool _showAdminButton;
        public bool ShowAdminButton
        {
            get => _showAdminButton;
            set => Set(ref _showAdminButton, value);
        }

        // Selected service id
        private int _serviceId;
        public int ServiceId
        {
            get => _serviceId;
            set => Set(ref _serviceId, value);
        }

        // Commands
        public ICommand LoadWorkoutsCommand { get; }
        public ICommand AddWorkoutCommand { get; }

        public WorkoutsViewModel()
        {
            Title = "Workouts";

            LoadWorkoutsCommand = new Command(async () =>
                await LoadWorkoutsForServiceAsync(ServiceId));

            AddWorkoutCommand = new Command(async () =>
                await GoToAddWorkoutAsync(), () => AppState.IsAdmin);
        }

        // Load workouts for selected service
        public async Task LoadWorkoutsForServiceAsync(int serviceId)
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Workouts.Clear();

                var items = await _apiClient.GetWorkoutsAsync();

                // Filter workouts by registered service
                if (AppState.RegisteredServiceId != null)
                {
                    int sid = AppState.RegisteredServiceId.Value;
                    items = items.Where(w => w.ServiceId == sid).ToList();
                }

                foreach (var w in items)
                    Workouts.Add(w);

                // Update admin button visibility
                ShowAdminButton = AppState.IsAdmin;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Navigate to add workout page
        private async Task GoToAddWorkoutAsync()
        {
            if (!AppState.IsAdmin)
                return;

            await Shell.Current.GoToAsync(
                $"workoutEdit?workoutId=0&serviceId={ServiceId}");
        }
    }
}
