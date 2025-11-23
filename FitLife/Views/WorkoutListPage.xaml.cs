using FitLife.Helpers;
using FitLife.Models;
using FitLife.ViewModels;


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
        // when workout is tapped
        private async void OnWorkoutSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            // clear highlight right away
            ((CollectionView)sender).SelectedItem = null;

            // if not admin, just show a message and stop
            if (!AppState.IsAdmin)
            {
                await DisplayAlert("View only", "Only admin can edit workouts.", "OK");
                return;
            }

            if (e.CurrentSelection[0] is not WorkoutApiModel selected)
                return;

            var route = $"workoutEdit?workoutId={selected.Id}&serviceId={selected.ServiceId}";
            await Shell.Current.GoToAsync(route);
        }


        // helper for async command support
        private async void OnEditWorkoutClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is WorkoutApiModel workout)
            {
                var route = $"workoutEdit?workoutId={workout.Id}&serviceId={workout.ServiceId}";
                await Shell.Current.GoToAsync(route);
            }
        }


    }

}
