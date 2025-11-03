using FitLife.ViewModels;

namespace FitLife.Views
{
    // Code-behind for the Workouts page
    public partial class WorkoutListPage : ContentPage
    {
        public WorkoutListPage()
        {
            InitializeComponent();

            // Set the ViewModel for data binding
            BindingContext = new WorkoutsViewModel();
        }
    }
}
