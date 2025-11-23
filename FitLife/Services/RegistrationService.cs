namespace FitLife.Services
{
    // Handles saving and reading registered services locally
    public static class RegistrationService
    {
        // Key used to store service IDs in preferences
        private const string RegisteredKey = "RegisteredServiceIds";

        // Get all registered service IDs
        public static IReadOnlyList<int> GetRegisteredServices()
        {
            var raw = Preferences.Get(RegisteredKey, string.Empty);
            if (string.IsNullOrWhiteSpace(raw))
                return Array.Empty<int>();

            return raw
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s, out var id) ? id : 0)
                .Where(id => id > 0)
                .ToList();
        }

        // Check if any service is registered
        public static bool HasAnyRegistered => GetRegisteredServices().Any();

        // Register a new service
        public static void Register(int serviceId)
        {
            var list = GetRegisteredServices().ToList();
            if (!list.Contains(serviceId))
            {
                list.Add(serviceId);
                Save(list);
            }
        }

        // Save the updated list to preferences
        private static void Save(List<int> ids)
        {
            var raw = string.Join(',', ids);
            Preferences.Set(RegisteredKey, raw);
        }
    }
}
