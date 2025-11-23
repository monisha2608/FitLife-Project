using System.Windows.Input;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for creating and editing services
    public class ServiceEditViewModel : BaseViewModel
    {
        // API client for services
        private readonly ServicesApiClient _apiClient = new();

        // Id field
        private int _id;
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        // Form fields
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _type = string.Empty;
        public string Type
        {
            get => _type;
            set => Set(ref _type, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _trainer = string.Empty;
        public string Trainer
        {
            get => _trainer;
            set => Set(ref _trainer, value);
        }

        private string _dayOfWeek = string.Empty;
        public string DayOfWeek
        {
            get => _dayOfWeek;
            set => Set(ref _dayOfWeek, value);
        }

        private string _startTime = string.Empty;
        public string StartTime
        {
            get => _startTime;
            set => Set(ref _startTime, value);
        }

        private int _durationMins;
        public int DurationMins
        {
            get => _durationMins;
            set => Set(ref _durationMins, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        private string _level = string.Empty;
        public string Level
        {
            get => _level;
            set => Set(ref _level, value);
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ServiceEditViewModel()
        {
            // Bind commands
            SaveCommand = new Command(async () => await SaveAsync(), () => !IsBusy);
            DeleteCommand = new Command(async () => await DeleteAsync(), () => Id > 0 && !IsBusy);
        }

        // Load data from existing service or prepare new form
        public Task LoadFromService(ServiceApiModel? service)
        {
            if (service == null)
            {
                Title = "Add Service";
                Id = 0;
                Name = string.Empty;
                Type = string.Empty;
                Description = string.Empty;
                Trainer = string.Empty;
                DayOfWeek = "Monday";
                StartTime = string.Empty;
                DurationMins = 60;
                Price = 0m;
                Level = "All Levels";
                return Task.CompletedTask;
            }

            Title = "Edit Service";
            Id = service.Id;
            Name = service.Name;
            Type = service.Type;
            Description = service.Description;
            Trainer = service.Trainer;
            DayOfWeek = service.DayOfWeek;
            StartTime = service.StartTime;
            DurationMins = service.DurationMins;
            Price = service.Price;
            Level = service.Level;

            return Task.CompletedTask;
        }

        // Convert data to API model
        private ServiceApiModel ToApiModel()
        {
            return new ServiceApiModel
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Description = this.Description,
                Trainer = this.Trainer,
                DayOfWeek = this.DayOfWeek,
                StartTime = this.StartTime,
                DurationMins = this.DurationMins,
                Price = this.Price,
                Level = this.Level
            };
        }

        // Save service to API
        private async Task SaveAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var apiModel = ToApiModel();
                bool ok = false;

                if (apiModel.Id == 0)
                {
                    // Create new service
                    var created = await _apiClient.CreateServiceAsync(apiModel);
                    ok = created != null;

                    if (created != null)
                        Id = created.Id;
                }
                else
                {
                    // Update existing service
                    ok = await _apiClient.UpdateServiceAsync(apiModel);
                }

                if (!ok)
                {
                    // Show error if save fails
                    await Application.Current.MainPage.DisplayAlert(
                        "Save failed",
                        "The server could not save this service.",
                        "OK");
                    return;
                }

                // Go back after success
                await Shell.Current.GoToAsync("..");
            }
            catch (HttpRequestException ex)
            {
                // Handle network errors
                await Application.Current.MainPage.DisplayAlert(
                    "Network error",
                    ex.Message,
                    "OK");
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                await Application.Current.MainPage.DisplayAlert(
                    "Unexpected error",
                    ex.Message,
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Delete service
        private async Task DeleteAsync()
        {
            if (IsBusy || Id <= 0)
                return;

            IsBusy = true;
            try
            {
                await _apiClient.DeleteServiceAsync(Id);

                // Go back after delete
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
