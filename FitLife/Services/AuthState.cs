namespace FitLife.Services
{
    // Stores login information for the current user
    public static class AuthState
    {
        // Tells if the user is logged in
        public static bool IsAuthenticated { get; set; }

        // Tells if the user is an admin
        public static bool IsAdmin { get; set; }

        // Stores the current user's name
        public static string CurrentUserName { get; set; } = string.Empty;
    }
}
