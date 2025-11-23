using FitLife.Models;
using FitLife.ViewModels;

namespace FitLife.Views;

public partial class ServiceEditPage : ContentPage
{
    private readonly ServiceEditViewModel _viewModel; // view model for this page
    private readonly Service? _initialService; // service passed for editing

    public ServiceEditPage(Service? service = null)
    {
        InitializeComponent(); // load XAML UI
        _viewModel = new ServiceEditViewModel(); // create view model
        BindingContext = _viewModel; // bind data to UI
        _initialService = service; // store service for later use
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing(); // call base page logic
        await _viewModel.LoadFromService(_initialService); // load existing data if editing
    }
}
