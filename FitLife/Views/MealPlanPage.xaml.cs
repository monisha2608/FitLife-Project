using FitLife.ViewModels;

namespace FitLife.Views
{
    // Code-behind for the Meal Plan page
    public partial class MealPlanPage : ContentPage
    {
        public MealPlanPage()
        {
            InitializeComponent();

            // Connect the page to its ViewModel
            BindingContext = new MealsViewModel();
        }
    }
}
