using Microsoft.Extensions.Logging;
using FitLife.Services;
using FitLife.ViewModels;
using FitLife.Views;


namespace FitLife
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Dependency Injection registrations
            builder.Services.AddSingleton<DatabaseService>();

            // ViewModels
            builder.Services.AddTransient<ServiceListViewModel>();

            // Views
            builder.Services.AddTransient<ServicesPage>();

            return builder.Build();
        }
    }
}
