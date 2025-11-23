using FitLife.ViewModels;

namespace FitLife.Views
{
    public partial class ProgressChartPage : ContentPage
    {
        private readonly ProgressViewModel _viewModel;

        public ProgressChartPage()
        {
            InitializeComponent(); // load XAML
            _viewModel = new ProgressViewModel(); // create view model
            BindingContext = _viewModel; // bind data
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); // call base method
            _viewModel.LoadProgressCommand.Execute(null); // load progress data
        }
    }
}
