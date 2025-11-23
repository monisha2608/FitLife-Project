using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // Gets query values from navigation
    [QueryProperty(nameof(MealId), "mealId")]
    [QueryProperty(nameof(ServiceId), "serviceId")]
    public class MealEditViewModel : BaseViewModel
    {
        // API clients
        private readonly MealsApiClient _mealsApi = new();
        private readonly ServicesApiClient _servicesApi = new();

        private int _mealId;
        public int MealId
        {
            get => _mealId;
            set
            {
                if (Set(ref _mealId, value))
                {
                    // Load meal when id is set
                    _ = LoadMealAsync(value);
                }
            }
        }

        private int _serviceId;
        public int ServiceId
        {
            get => _serviceId;
            set => Set(ref _serviceId, value);
        }

        // List of services for dropdown
        public ObservableCollection<ServiceApiModel> Services { get; } = new();

        private ServiceApiModel? _selectedService;
        public ServiceApiModel? SelectedService
        {
            get => _selectedService;
            set
            {
                if (Set(ref _selectedService, value) && value != null)
                    ServiceId = value.Id;
            }
        }

        // Form fields
        private string _name = string.Empty;
        public string Name { get => _name; set => Set(ref _name, value); }

        private string _type = "Breakfast";
        public string Type { get => _type; set => Set(ref _type, value); }

        private int _calories;
        public int Calories { get => _calories; set => Set(ref _calories, value); }

        private string _description = string.Empty;
        public string Description { get => _description; set => Set(ref _description, value); }

        private string _dayOfWeek = "Monday";
        public string DayOfWeek { get => _dayOfWeek; set => Set(ref _dayOfWeek, value); }

        private string _timeOfDay = "Any";
        public string TimeOfDay { get => _timeOfDay; set => Set(ref _timeOfDay, value); }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public MealEditViewModel()
        {
            Title = "Add Meal";

            // Save and delete actions
            SaveCommand = new Command(async () => await SaveAsync(), () => !IsBusy);
            DeleteCommand = new Command(async () => await DeleteAsync(), () => MealId > 0 && !IsBusy);

            // Load services list
            _ = LoadServicesAsync();
        }

        // Load list of services
        private async Task LoadServicesAsync()
        {
            var items = await _servicesApi.GetServicesAsync();
            Services.Clear();

            foreach (var s in items)
                Services.Add(s);

            if (ServiceId > 0)
                SelectedService = Services.FirstOrDefault(s => s.Id == ServiceId);
        }

        // Load meal data for editing
        private async Task LoadMealAsync(int mealId)
        {
            if (mealId <= 0) return;

            var meal = await _mealsApi.GetMealAsync(mealId);
            if (meal == null) return;

            Title = "Edit Meal";

            MealId = meal.Id;
            ServiceId = meal.ServiceId;

            Name = meal.Name;
            Type = meal.Type;
            Calories = meal.Calories;
            Description = meal.Description;
            DayOfWeek = meal.DayOfWeek;
            TimeOfDay = meal.TimeOfDay;

            if (Services.Count > 0)
                SelectedService = Services.FirstOrDefault(s => s.Id == ServiceId);
        }

        // Convert form to API model
        private MealApiModel ToApiModel() => new()
        {
            Id = MealId,
            ServiceId = ServiceId,
            Name = Name,
            Type = Type,
            Calories = Calories,
            Description = Description,
            DayOfWeek = DayOfWeek,
            TimeOfDay = TimeOfDay
        };

        // Save meal to API
        private async Task SaveAsync()
        {
            if (IsBusy) return;
            if (ServiceId == 0) return;

            IsBusy = true;
            try
            {
                var model = ToApiModel();

                if (MealId == 0)
                {
                    var created = await _mealsApi.CreateMealAsync(model);
                    if (created != null)
                        MealId = created.Id;
                }
                else
                {
                    await _mealsApi.UpdateMealAsync(model);
                }

                // Go back after save
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Delete meal
        private async Task DeleteAsync()
        {
            if (IsBusy || MealId <= 0) return;

            IsBusy = true;
            try
            {
                await _mealsApi.DeleteMealAsync(MealId);

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
