using System.Collections.ObjectModel;
using System.Windows.Input;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for service list screen
    public class ServiceListViewModel : BaseViewModel
    {
        // API client for services
        private readonly ServicesApiClient _apiClient = new();

        // List of services
        public ObservableCollection<ServiceApiModel> Services { get; } = new();

        // Commands
        public ICommand LoadServicesCommand { get; }
        public ICommand AddServiceCommand { get; }

        // Used to control admin-only UI
        public bool IsAdmin => AuthState.IsAdmin;

        public ServiceListViewModel()
        {
            LoadServicesCommand = new Command(async () => await LoadAsync(), () => !IsBusy);
            AddServiceCommand = new Command(async () => await GoToAddAsync(), () => IsAdmin);
        }

        // Load services from API
        private async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Services.Clear();

                var items = await _apiClient.GetServicesAsync();
                if (items != null)
                {
                    foreach (var s in items)
                        Services.Add(s);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Navigate to add service page
        private Task GoToAddAsync()
        {
            if (!IsAdmin)
                return Task.CompletedTask;

            return Shell.Current.GoToAsync("serviceEdit");
        }
    }
}
