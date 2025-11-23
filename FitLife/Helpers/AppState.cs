namespace FitLife.Helpers
{
    // Stores app-wide user and registration state
    public static class AppState
    {
        // Set during login to control admin access
        public static bool IsAdmin { get; set; }

        // Stores the current user's name
        public static string CurrentUserName { get; set; } = "Guest";

        // Tracks service registration status
        public static bool IsRegisteredForService { get; set; }

        // Stores the registered service ID
        public static int? RegisteredServiceId { get; set; }

        // Stores the registered service name
        public static string? RegisteredServiceName { get; set; }
    }
}
