using FitLife.Helpers;
using FitLife.Models;
using FitLife.Services;
using FitLife.ViewModels;

namespace FitLife.Views;

[QueryProperty(nameof(ServiceId), "serviceId")]
public partial class ServiceDetailPage : ContentPage
{
    // view model for this page
    private readonly ServiceDetailViewModel _viewModel;

    // api client for delete
    private readonly ServicesApiClient _apiClient = new();

    // gets service id from navigation
    public int ServiceId
    {
        set
        {
            // load selected service details
            _ = _viewModel.LoadAsync(value);
        }
    }

    public ServiceDetailPage()
    {
        InitializeComponent();

        // create and attach view model
        _viewModel = new ServiceDetailViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // show admin buttons only if admin
        bool isAdmin = AppState.IsAdmin;
        EditButton.IsVisible = isAdmin;
        DeleteButton.IsVisible = isAdmin;
    }

    // back navigation
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
        else
            await Shell.Current.GoToAsync("..");
    }

    // register user to service
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        if (_viewModel.Service is ServiceApiModel s)
        {
            // save registration to app state
            AppState.IsRegisteredForService = true;
            AppState.RegisteredServiceId = s.Id;
            AppState.RegisteredServiceName = s.Name;

            // enable tabs after registration
            if (Shell.Current is AppShell shell)
            {
                shell.EnableRegisteredTabs();
            }

            // show confirmation
            await DisplayAlert(
                "Registered",
                $"You are registered for {s.Name}. Dashboard and other tabs are now available.",
                "OK");

            // go to dashboard
            await Shell.Current.GoToAsync("//dashboard");
        }
    }

    // go to edit page
    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (_viewModel.Service is ServiceApiModel s)
        {
            await Shell.Current.GoToAsync($"serviceEdit?serviceId={s.Id}");
        }
    }

    // delete service
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_viewModel.Service is not ServiceApiModel s)
            return;

        // confirm delete
        bool confirm = await DisplayAlert(
            "Delete Service",
            $"Are you sure you want to delete {s.Name}?",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            // call api to delete
            await _apiClient.DeleteServiceAsync(s.Id);

            // success message
            await DisplayAlert("Deleted", "Service deleted successfully.", "OK");

            // go back to services list
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            // show error
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
