using FitLife.ViewModels;

namespace FitLife.Views
{
    public partial class DashboardPage : ContentPage
    {
        // ViewModel for this page
        private readonly DashboardViewModel _viewModel;

        public DashboardPage()
        {
            InitializeComponent();

            // Create and attach the ViewModel
            _viewModel = new DashboardViewModel();
            BindingContext = _viewModel;
        }

        // Runs when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Load dashboard data
            await _viewModel.LoadAsync();
        }
    }
}
