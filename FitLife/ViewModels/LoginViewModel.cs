using System.Windows.Input;
using FitLife.Helpers;

namespace FitLife.ViewModels
{
    // ViewModel for the login screen
    public class LoginViewModel : BaseViewModel
    {
        // Stores username input
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        // Stores password input
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        // Command for login button
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            Title = "Login";

            // Bind login command
            LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
        }

        // Main login logic
        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Check for hard-coded admin user
                if (Username == "admin" && Password == "admin123")
                {
                    AppState.IsAdmin = true;
                    AppState.CurrentUserName = "Admin";
                }
                else
                {
                    AppState.IsAdmin = false;
                    AppState.CurrentUserName = string.IsNullOrWhiteSpace(Username)
                        ? "Guest"
                        : Username;
                }

                var mainPage = Application.Current.MainPage;

                // Build message based on role
                var message = AppState.IsAdmin
                    ? "Logged in as Admin. You can manage services."
                    : $"Logged in as {AppState.CurrentUserName} (view-only).";

                if (mainPage != null)
                {
                    // Show login result
                    await mainPage.DisplayAlert("Login", message, "OK");

                    // Go back to previous page
                    if (mainPage.Navigation?.NavigationStack?.Count > 1)
                    {
                        await mainPage.Navigation.PopAsync();
                    }
                }
            }
            catch (System.Exception ex)
            {
                var mainPage = Application.Current.MainPage;
                if (mainPage != null)
                {
                    // Show error if something fails
                    await mainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            finally
            {
                // Reset loading state
                IsBusy = false;
            }
        }
    }
}
