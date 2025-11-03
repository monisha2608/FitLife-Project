using FitLife.ViewModels;

namespace FitLife.Views
{
    // Code-behind for the Dashboard page
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();

            // Set the ViewModel for data binding
            BindingContext ??= new DashboardViewModel();
        }
    }
}
