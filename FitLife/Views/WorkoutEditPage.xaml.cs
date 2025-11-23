using FitLife.ViewModels;
namespace FitLife.Views;

[QueryProperty(nameof(WorkoutId), "workoutId")]
public partial class WorkoutEditPage : ContentPage
{
    private readonly WorkoutEditViewModel _viewModel; // view model for this page

    public int WorkoutId
    {
        set
        {
            // load workout when id is passed
            _ = _viewModel.LoadAsync(value);
        }
    }

    public WorkoutEditPage()
    {
        InitializeComponent(); // load XAML UI
        _viewModel = new WorkoutEditViewModel(); // create view model
        BindingContext = _viewModel; // bind data to UI
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    

}
