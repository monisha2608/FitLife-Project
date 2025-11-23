using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FitLife.Models;

namespace FitLife.Services
{
    // Handles API calls for services
    public class ServicesApiClient
    {
        private readonly HttpClient _httpClient;

        // JSON settings
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ServicesApiClient()
        {
            _httpClient = new HttpClient
            {
#if ANDROID
                // Android emulator talks to the host machine
                BaseAddress = new Uri("http://10.0.2.2:7232/")
#else
                // Windows local API
                BaseAddress = new Uri("http://localhost:7232/")
#endif
            };
        }

        // ✅ Get all services
        public async Task<List<ServiceApiModel>> GetServicesAsync()
        {
            var response = await _httpClient.GetAsync("api/services");
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<ServiceApiModel>>(_jsonOptions);
            return items ?? new List<ServiceApiModel>();
        }

        // ✅ Get one service
        public async Task<ServiceApiModel?> GetServiceAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/services/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ServiceApiModel>(_jsonOptions);
        }

        // ✅ Create service
        public async Task<ServiceApiModel?> CreateServiceAsync(ServiceApiModel model)
        {
            var json = JsonSerializer.Serialize(model, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/services", content);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ServiceApiModel>(_jsonOptions);
        }

        // ✅ Update service
        public async Task<bool> UpdateServiceAsync(ServiceApiModel model)
        {
            if (model.Id <= 0)
                throw new ArgumentException("Service Id must be > 0");

            var json = JsonSerializer.Serialize(model, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/services/{model.Id}", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete service
        public async Task<bool> DeleteServiceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/services/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
