using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for meals screen
    public class MealsViewModel : BaseViewModel
    {
        // API clients
        private readonly MealsApiClient _mealsApi = new();
        private readonly ServicesApiClient _servicesApi = new();

        // List of meals
        public ObservableCollection<MealApiModel> Meals { get; } = new();

        // Currently selected service id
        public int CurrentServiceId { get; private set; }

        // Refresh command
        public ICommand RefreshCommand { get; }

        public MealsViewModel()
        {
            Title = "Meals";
            RefreshCommand = new Command(async () => await LoadMealsForServiceAsync(CurrentServiceId));
        }

        // Load meals for specific service, or all if id is 0
        public async Task LoadMealsForServiceAsync(int serviceId)
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                CurrentServiceId = serviceId;
                Meals.Clear();

                List<MealApiModel> items;

                // Get meals based on filter
                if (serviceId > 0)
                    items = await _mealsApi.GetMealsForServiceAsync(serviceId);
                else
                    items = await _mealsApi.GetMealsAsync();

                // Load services to match service names
                var services = await _servicesApi.GetServicesAsync();

                foreach (var m in items)
                {
                    var serviceName = services
                        .FirstOrDefault(s => s.Id == m.ServiceId)?.Name ?? string.Empty;

                    // Add meal to list
                    Meals.Add(new MealApiModel
                    {
                        Id = m.Id,
                        ServiceId = m.ServiceId,
                        Name = m.Name,
                        Description = m.Description,
                        Type = m.Type,
                        Calories = m.Calories
                    });
                }
            }
            catch (Exception ex)
            {
                // Show error if loading fails
                await Application.Current.MainPage.DisplayAlert(
                    "Error loading meals",
                    ex.Message,
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
