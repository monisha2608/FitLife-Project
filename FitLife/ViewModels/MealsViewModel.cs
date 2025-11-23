using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;
using Microsoft.Maui.Controls;

namespace FitLife.ViewModels
{
    public class MealsViewModel : BaseViewModel
    {
        private readonly MealsApiClient _mealsApi = new();

        public ObservableCollection<MealApiModel> Meals { get; } = new();

        public ICommand LoadMealsCommand { get; }

        public MealsViewModel()
        {
            Title = "Meals";
            LoadMealsCommand = new Command<int?>(async serviceId => await LoadMealsForServiceAsync(serviceId));
        }

        // load meals only for one service
        public async Task LoadMealsForServiceAsync(int? serviceId)
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Meals.Clear();

                // pick service id – prefer param, then registered service
                int? targetServiceId = serviceId;

                if (!targetServiceId.HasValue || targetServiceId.Value == 0)
                {
                    if (AppState.RegisteredServiceId.HasValue)
                        targetServiceId = AppState.RegisteredServiceId.Value;
                }

                // if still null, user not registered – nothing to show
                if (!targetServiceId.HasValue)
                    return;

                var allMeals = await _mealsApi.GetMealsAsync();

                var filtered = allMeals
                    .Where(m => m.ServiceId == targetServiceId.Value)
                    .ToList();

                foreach (var meal in filtered)
                    Meals.Add(meal);
            }
            catch (Exception ex)
            {
                var page = Application.Current.MainPage;
                if (page != null)
                    await page.DisplayAlert("Error loading meals", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
