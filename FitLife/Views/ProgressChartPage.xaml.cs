using FitLife.ViewModels;

namespace FitLife.Views;

public partial class ProgressChartPage : ContentPage
{
    public ProgressChartPage()
    {
        InitializeComponent();
        BindingContext = new ProgressViewModel();
    }
}
