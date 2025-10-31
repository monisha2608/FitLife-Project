using FitLife.ViewModels;

namespace FitLife.Views;

public partial class MealPlanPage : ContentPage
{
    public MealPlanPage()
    {
        InitializeComponent();
        BindingContext = new MealsViewModel();
    }
}
