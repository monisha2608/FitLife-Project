using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Api.Models
{
    // Represents a workout exercise in the system
    public class WorkoutEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Links this workout to a service
        public int ServiceId { get; set; }

        // Title of the workout
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        // Short description of the workout
        [MaxLength(500)]
        public string? Description { get; set; }

        // Category like Cardio or Strength
        [MaxLength(50)]
        public string? Category { get; set; }

        // Duration in minutes
        public int DurationMins { get; set; }

        // Calories burned
        public int Calories { get; set; }

        // Difficulty level
        [MaxLength(50)]
        public string? Level { get; set; }
    }
}
