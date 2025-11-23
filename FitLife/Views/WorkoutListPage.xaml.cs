using FitLife.Helpers;
using FitLife.Models;
using FitLife.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace FitLife.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutListPage : ContentPage
    {
        private readonly WorkoutsViewModel _viewModel;

        public WorkoutListPage()
        {
            InitializeComponent();
            _viewModel = new WorkoutsViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // show / hide admin button when page loads
            _viewModel.ShowAdminButton = AppState.IsAdmin;

            // load workouts for current user/service
            _viewModel.LoadWorkoutsCommand.Execute(null);
        }

        // admin clicks add workout
        private async void OnAddWorkoutClicked(object sender, EventArgs e)
        {
            // block non-admin users
            if (!AppState.IsAdmin)
            {
                await DisplayAlert("Not allowed", "Only admin can add workouts.", "OK");
                return;
            }

            // go to add workout page
            await Shell.Current.GoToAsync("workoutEdit");
        }

        // when workout is tapped
        private async void OnWorkoutSelected(object sender, SelectionChangedEventArgs e)
        {
            // no item selected
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            // wrong item type
            if (e.CurrentSelection[0] is not WorkoutApiModel selected)
                return;

            // clear highlight
            ((CollectionView)sender).SelectedItem = null;

            // go to edit page with selected workout
            var route = $"workoutEdit?workoutId={selected.Id}&serviceId={selected.ServiceId}";
            await Shell.Current.GoToAsync(route);
        }
    }

    // helper for async command support
    public static class CommandExtensions
    {
        public static Task ExecuteAsync(this Command command, object? parameter)
        {
            command.Execute(parameter);
            return Task.CompletedTask;
        }
    }
}
