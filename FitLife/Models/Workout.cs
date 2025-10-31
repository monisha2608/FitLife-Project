namespace FitLife.Models;

public class Workout
{
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string Image { get; set; } = "";
    public int DurationMins { get; set; }
    public int Calories { get; set; }
}
