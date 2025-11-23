using FitLife.ViewModels;

namespace FitLife.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealEditPage : ContentPage
    {
        public MealEditPage()
        {
            InitializeComponent(); // Load the XAML UI

            // Attach ViewModel to this page
            BindingContext = new MealEditViewModel();
        }

        // Runs when Back button is clicked
        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Navigate back to previous page
            await Shell.Current.GoToAsync("..");
        }
    }
}
