namespace FitLife.Models
{
    public class DailyBar
    {
        public string Day { get; set; } = "";
        public int Minutes { get; set; }

        // scale minutes -> pixels (so bars are visible)
        public int BarHeight => Math.Clamp(Minutes * 2, 10, 120);
    }
}
