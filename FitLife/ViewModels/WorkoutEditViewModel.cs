using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for adding and editing workouts
    public class WorkoutEditViewModel : BaseViewModel
    {
        // API clients
        private readonly WorkoutsApiClient _workoutsApi = new();
        private readonly ServicesApiClient _servicesApi = new();

        // List of services for dropdown
        public ObservableCollection<ServiceApiModel> Services { get; } = new();

        // Selected service
        private ServiceApiModel? _selectedService;
        public ServiceApiModel? SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        // Workout fields
        private int _id;
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string? _category;
        public string? Category
        {
            get => _category;
            set => Set(ref _category, value);
        }

        private int _durationMins = 30;
        public int DurationMins
        {
            get => _durationMins;
            set => Set(ref _durationMins, value);
        }

        private int _calories = 100;
        public int Calories
        {
            get => _calories;
            set => Set(ref _calories, value);
        }

        private string? _level;
        public string? Level
        {
            get => _level;
            set => Set(ref _level, value);
        }

        // Page title
        private string _pageTitle = "Add Workout";
        public string PageTitle
        {
            get => _pageTitle;
            set => Set(ref _pageTitle, value);
        }

        // Controls delete button
        private bool _canDelete;
        public bool CanDelete
        {
            get => _canDelete;
            set => Set(ref _canDelete, value);
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public WorkoutEditViewModel()
        {
            // Setup commands
            SaveCommand = new Command(async () => await SaveAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());

            // Load services list
            _ = LoadServicesAsync();
        }

        // Load workout for editing
        public async Task LoadAsync(int workoutId)
        {
            if (workoutId <= 0)
            {
                PageTitle = "Add Workout";
                CanDelete = false;
                return;
            }

            try
            {
                var w = await _workoutsApi.GetWorkoutAsync(workoutId);
                if (w == null) return;

                Id = w.Id;
                Title = w.Title;
                Description = w.Description;
                Category = w.Category;
                DurationMins = w.DurationMins;
                Calories = w.Calories;
                Level = w.Level;

                PageTitle = "Edit Workout";
                CanDelete = true;

                await LoadServicesAsync(w.ServiceId);
            }
            catch (Exception ex)
            {
                var page = Application.Current.MainPage;
                if (page != null)
                {
                    await page.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        // Load services for picker
        private async Task LoadServicesAsync(int? preselectServiceId = null)
        {
            try
            {
                if (Services.Count == 0)
                {
                    var list = await _servicesApi.GetServicesAsync();
                    Services.Clear();
                    foreach (var s in list)
                        Services.Add(s);
                }

                // Preselect service if available
                if (preselectServiceId.HasValue)
                {
                    SelectedService = Services.FirstOrDefault(s => s.Id == preselectServiceId.Value);
                }
                else if (AppState.RegisteredServiceId.HasValue)
                {
                    SelectedService = Services.FirstOrDefault(s => s.Id == AppState.RegisteredServiceId.Value);
                }
            }
            catch
            {
                // Ignore errors, list can stay empty
            }
        }

        // Save workout
        private async Task SaveAsync()
        {
            if (IsBusy)
                return;

            // Make sure a service is selected
            if (SelectedService == null && !AppState.RegisteredServiceId.HasValue)
            {
                var page = Application.Current.MainPage;
                if (page != null)
                {
                    await page.DisplayAlert(
                        "Missing service",
                        "Please choose a service for this workout.",
                        "OK");
                }
                return;
            }

            IsBusy = true;
            try
            {
                int serviceId = SelectedService?.Id ?? AppState.RegisteredServiceId!.Value;

                var model = new WorkoutApiModel
                {
                    Id = this.Id,
                    ServiceId = serviceId,
                    Title = this.Title,
                    Description = this.Description,
                    Category = this.Category,
                    DurationMins = this.DurationMins,
                    Calories = this.Calories,
                    Level = this.Level
                };

                if (model.Id == 0)
                {
                    // Create new workout
                    var created = await _workoutsApi.CreateWorkoutAsync(model);
                    if (created != null)
                        Id = created.Id;
                }
                else
                {
                    // Update existing workout
                    await _workoutsApi.UpdateWorkoutAsync(model);
                }

                // Go back after save
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Delete workout
        private async Task DeleteAsync()
        {
            if (IsBusy || Id <= 0)
                return;

            IsBusy = true;
            try
            {
                // Ask for confirmation
                var page = Application.Current.MainPage;
                if (page != null)
                {
                    bool ok = await page.DisplayAlert(
                        "Delete",
                        "Are you sure you want to delete this workout?",
                        "Yes", "No");

                    if (!ok)
                    {
                        IsBusy = false;
                        return;
                    }
                }

                await _workoutsApi.DeleteWorkoutAsync(Id);

                // Go back after delete
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
