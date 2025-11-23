using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FitLife.Models;

namespace FitLife.Services
{
    // Handles API calls for services
    public class ServicesApiClient
    {
        // Base URL for the API
        private const string BaseUrl = "https://localhost:7232";

        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public ServicesApiClient()
        {
            // Create HTTP client
            _http = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            // JSON settings
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        // Get all services
        public async Task<List<ServiceApiModel>> GetServicesAsync()
        {
            var response = await _http.GetAsync("api/services");
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<ServiceApiModel>>(_jsonOptions);
            return items ?? new List<ServiceApiModel>();
        }

        // Get a single service by ID
        public async Task<ServiceApiModel?> GetServiceAsync(int id)
        {
            var response = await _http.GetAsync($"api/services/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ServiceApiModel>(_jsonOptions);
        }

        // Create a new service
        public async Task<ServiceApiModel?> CreateServiceAsync(ServiceApiModel model)
        {
            var json = JsonSerializer.Serialize(model, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/services", content);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var created = await response.Content.ReadFromJsonAsync<ServiceApiModel>(_jsonOptions);
            return created;
        }

        // Update an existing service
        public async Task<bool> UpdateServiceAsync(ServiceApiModel model)
        {
            if (model.Id <= 0)
                throw new ArgumentException("Service Id must be > 0 for update.");

            var json = JsonSerializer.Serialize(model, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PutAsync($"api/services/{model.Id}", content);

            return response.IsSuccessStatusCode;
        }

        // Delete a service
        public async Task<bool> DeleteServiceAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/services/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
