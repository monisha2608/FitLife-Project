using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FitLife.Models;
using Microsoft.Maui.Storage;
using SQLite;

namespace FitLife.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _db;

        private async Task InitAsync()
        {
            if (_db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "FitLife.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<Service>();
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            await InitAsync();
            return await _db!
                .Table<Service>()
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<Service?> GetServiceAsync(int id)
        {
            await InitAsync();
            return await _db!.Table<Service>()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddServiceAsync(Service service)
        {
            await InitAsync();
            return await _db!.InsertAsync(service);
        }

        public async Task<int> UpdateServiceAsync(Service service)
        {
            await InitAsync();
            return await _db!.UpdateAsync(service);
        }

        public async Task<int> DeleteServiceAsync(Service service)
        {
            await InitAsync();
            return await _db!.DeleteAsync(service);
        }

        // Seed initial demo services – called first time
        public async Task SeedSampleDataAsync()
        {
            await InitAsync();

            var count = await _db!.Table<Service>().CountAsync();
            if (count > 0)
                return;

            var sample = new List<Service>
            {
                new Service
                {
                    Name = "Morning Yoga Flow",
                    Type = "Yoga",
                    Description = "Gentle full-body stretch and breathing to start your day.",
                    Trainer = "Aditi Sharma",
                    DayOfWeek = "Monday",
                    StartTime = "07:00 AM",
                    DurationMins = 60,
                    Price = 15m,
                    Level = "Beginner"
                },
                new Service
                {
                    Name = "HIIT Power Burn",
                    Type = "HIIT",
                    Description = "High-intensity intervals for max calorie burn.",
                    Trainer = "Rohan Singh",
                    DayOfWeek = "Wednesday",
                    StartTime = "06:00 PM",
                    DurationMins = 45,
                    Price = 18m,
                    Level = "Intermediate"
                },
                new Service
                {
                    Name = "Evening Relax & Restore",
                    Type = "Yoga",
                    Description = "Slow restorative yoga to unwind after a busy day.",
                    Trainer = "Meera Patel",
                    DayOfWeek = "Friday",
                    StartTime = "08:00 PM",
                    DurationMins = 50,
                    Price = 16m,
                    Level = "All Levels"
                }
            };

            await _db.InsertAllAsync(sample);
        }
    }
}
