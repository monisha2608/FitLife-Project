using FitLife.ViewModels;

namespace FitLife.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent(); // Load the XAML UI

        // Attach the ViewModel to this page
        BindingContext = new LoginViewModel();
    }
}
