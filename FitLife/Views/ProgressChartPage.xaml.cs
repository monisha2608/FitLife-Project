using FitLife.ViewModels;

namespace FitLife.Views
{
    // Code-behind for the Progress Chart page
    public partial class ProgressChartPage : ContentPage
    {
        public ProgressChartPage()
        {
            InitializeComponent();

            // Set the ViewModel for data binding
            BindingContext = new ProgressViewModel();
        }
    }
}
