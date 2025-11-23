using FitLife.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Api.Data
{
    // Database context for the FitLife application
    public class FitLifeContext : DbContext
    {
        // Constructor to pass database configuration
        public FitLifeContext(DbContextOptions<FitLifeContext> options)
            : base(options)
        {
        }

        // Table for services
        public DbSet<ServiceEntity> Services { get; set; } = null!;

        // Table for workouts
        public DbSet<WorkoutEntity> Workouts { get; set; } = null!;

        // Table for meals
        public DbSet<MealEntity> Meals { get; set; } = null!;
    }
}
