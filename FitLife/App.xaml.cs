namespace FitLife
{
    // Main class that starts the application
    public partial class App : Application
    {
        public App()
        {
            // Loads XAML resources and styles
            InitializeComponent();
        }

        // Creates and returns the main window of the app
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
