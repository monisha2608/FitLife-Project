using FitLife.Helpers;
using FitLife.Models;
using FitLife.ViewModels;

namespace FitLife.Views;

public partial class ServicesPage : ContentPage
{
    private readonly ServiceListViewModel _viewModel; // view model for this page

    public ServicesPage()
    {
        InitializeComponent(); // load XAML UI
        _viewModel = new ServiceListViewModel(); // create view model
        BindingContext = _viewModel; // bind data
    }

    protected override void OnAppearing()
    {
        base.OnAppearing(); // call base method

        // reload services from API
        _viewModel.LoadServicesCommand.Execute(null);

        // show add button only for admin
        AddServiceButton.IsVisible = AppState.IsAdmin;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // go back if possible
        if (Navigation.NavigationStack.Count > 1)
        {
            await Navigation.PopAsync();
        }
    }

    private async void OnAddServiceClicked(object sender, EventArgs e)
    {
        // go to add service page
        await Shell.Current.GoToAsync("serviceEdit");
    }

    private async void OnServiceTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is ServiceApiModel service && service.Id > 0)
        {
            // open service details page
            await Shell.Current.GoToAsync($"serviceDetail?serviceId={service.Id}");
        }
    }
}
