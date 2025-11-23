using System.Net.Http.Json;
using FitLife.Models;

namespace FitLife.Services
{
    // Handles API calls for workouts
    public class WorkoutsApiClient
    {
        private readonly HttpClient _httpClient;

        public WorkoutsApiClient()
        {
            _httpClient = new HttpClient
            {
#if ANDROID
                // Android emulator -> talks to host machine
                BaseAddress = new Uri("http://10.0.2.2:7232/")
#else
            
            BaseAddress = new Uri("http://localhost:7232/")
#endif
            };
        }


        // Get workouts, optionally filtered by service
        public async Task<List<WorkoutApiModel>> GetWorkoutsAsync(int? serviceId = null)
        {
            string url = "/api/workouts";
            if (serviceId.HasValue)
            {
                url += $"?serviceId={serviceId.Value}";
            }

            var result = await _httpClient.GetFromJsonAsync<List<WorkoutApiModel>>(url);
            return result ?? new List<WorkoutApiModel>();
        }

        // Get workout by ID
        public async Task<WorkoutApiModel?> GetWorkoutAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<WorkoutApiModel>($"/api/workouts/{id}");
        }

        // Create a new workout
        public async Task<WorkoutApiModel?> CreateWorkoutAsync(WorkoutApiModel workout)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/workouts", workout);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<WorkoutApiModel>();
        }

        // Update an existing workout
        public async Task<bool> UpdateWorkoutAsync(WorkoutApiModel workout)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/workouts/{workout.Id}", workout);
            return response.IsSuccessStatusCode;
        }

        // Delete a workout
        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/workouts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
