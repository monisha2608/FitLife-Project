using FitLife.Helpers;
using FitLife.ViewModels;
using FitLife.Models;

namespace FitLife.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ServiceId), "serviceId")]
    public partial class MealPlanPage : ContentPage
    {
        // ViewModel for this page
        private readonly MealsViewModel _viewModel;

        // Holds service id from navigation
        public int ServiceId { get; set; }

        public MealPlanPage()
        {
            InitializeComponent();

            // Create and bind ViewModel
            _viewModel = new MealsViewModel();
            BindingContext = _viewModel;

            // Set initial button visibility
            AddMealButton.IsVisible = AppState.IsAdmin;
        }

        // Runs when page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // prefer explicit service id; fallback to registered one
            int? idToUse = ServiceId > 0
                ? ServiceId
                : AppState.RegisteredServiceId;

            await _viewModel.LoadMealsForServiceAsync(idToUse);

            // Show Add button only for admin
            AddMealButton.IsVisible = AppState.IsAdmin;
        }

        // Navigate to add meal page
        private async void OnAddMealClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"mealEdit?mealId=0&serviceId={ServiceId}");
        }

        // Handle meal selection
        private async void OnMealSelected(object sender, SelectionChangedEventArgs e)
        {
            // Block edit if not admin
            if (!AppState.IsAdmin)
            {
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // Get selected meal
            if (e.CurrentSelection.FirstOrDefault() is not MealApiModel meal)
                return;

            ((CollectionView)sender).SelectedItem = null;

            // Navigate to edit page
            await Shell.Current.GoToAsync(
                $"mealEdit?mealId={meal.Id}&serviceId={meal.ServiceId}");
        }
    }
}
