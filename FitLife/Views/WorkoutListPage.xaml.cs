using FitLife.ViewModels;

namespace FitLife.Views;

public partial class WorkoutListPage : ContentPage
{
    public WorkoutListPage()
    {
        InitializeComponent();
        BindingContext = new WorkoutsViewModel();
    }
}
