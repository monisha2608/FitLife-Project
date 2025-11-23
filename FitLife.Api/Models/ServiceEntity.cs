using System.ComponentModel.DataAnnotations;

namespace FitLife.Api.Models
{
    // Represents a fitness service like Yoga or HIIT
    public class ServiceEntity
    {
        [Key]
        public int Id { get; set; }

        // Name of the service
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        // Type of service
        [MaxLength(40)]
        public string? Type { get; set; }

        // Description of the service
        [MaxLength(200)]
        public string? Description { get; set; }

        // Name of the trainer
        [MaxLength(60)]
        public string? Trainer { get; set; }

        // Day when the service happens
        [MaxLength(20)]
        public string? DayOfWeek { get; set; }

        // Time when the service starts
        [MaxLength(20)]
        public string? StartTime { get; set; }

        // Duration of the service in minutes
        public int DurationMins { get; set; }

        // Price of the service
        public decimal Price { get; set; }

        // Skill level of the service
        [MaxLength(30)]
        public string? Level { get; set; }
    }
}
