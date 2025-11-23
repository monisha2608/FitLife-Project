using System.Net.Http.Json;
using FitLife.Models;

namespace FitLife.Services
{
    // Handles API calls for meals
    public class MealsApiClient
    {
        private readonly HttpClient _httpClient;

        public MealsApiClient()
        {
            // Base URL of the API
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7232/")
            };
        }

        // Get all meals
        public async Task<List<MealApiModel>> GetMealsAsync()
        {
            var result = await _httpClient
                .GetFromJsonAsync<List<MealApiModel>>("api/meals");

            return result ?? new List<MealApiModel>();
        }

        // Get meals for a specific service
        public async Task<List<MealApiModel>> GetMealsForServiceAsync(int serviceId)
        {
            var result = await _httpClient
                .GetFromJsonAsync<List<MealApiModel>>($"api/meals/by-service/{serviceId}");

            return result ?? new List<MealApiModel>();
        }

        // Get a single meal by ID
        public async Task<MealApiModel?> GetMealAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<MealApiModel>($"api/meals/{id}");
        }

        // Create a new meal
        public async Task<MealApiModel?> CreateMealAsync(MealApiModel meal)
        {
            var response = await _httpClient.PostAsJsonAsync("api/meals", meal);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MealApiModel>();
        }

        // Update an existing meal
        public async Task UpdateMealAsync(MealApiModel meal)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/meals/{meal.Id}", meal);
            response.EnsureSuccessStatusCode();
        }

        // Delete a meal
        public async Task DeleteMealAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/meals/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
