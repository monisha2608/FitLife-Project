using System.Windows.Input;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for service detail screen
    public class ServiceDetailViewModel : BaseViewModel
    {
        // API client for services
        private readonly ServicesApiClient _apiClient = new();

        private ServiceApiModel? _service;
        public ServiceApiModel? Service
        {
            get => _service;
            set => Set(ref _service, value);
        }

        // Command for register button
        public ICommand RegisterCommand { get; }

        public ServiceDetailViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterAsync(), () => Service != null);
        }

        // Load service details
        public async Task LoadAsync(int serviceId)
        {
            IsBusy = true;
            try
            {
                // Get service from API
                Service = await _apiClient.GetServiceAsync(serviceId);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Register for selected service
        private async Task RegisterAsync()
        {
            if (Service == null) return;

            RegistrationService.Register(Service.Id);

            await Shell.Current.DisplayAlert(
                "Registered",
                $"You are registered for {Service.Name}.",
                "OK");
        }
    }
}
