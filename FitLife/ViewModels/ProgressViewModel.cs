using System.Collections.ObjectModel;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for the progress tracking screen
    public class ProgressViewModel : BaseViewModel
    {
        // Weekly progress data (minutes exercised each day)
        public ObservableCollection<ProgressEntry> Weekly { get; } =
            new ObservableCollection<ProgressEntry>(MockDataService.GetWeeklyProgress());

        // Weekly goal in minutes (7 days × 30 min/day)
        public double WeeklyGoalMinutes => 7 * 30;

        // Total minutes completed this week
        public double MinutesSum => Weekly.Sum(p => p.Minutes);

        // Progress ratio (used for progress bars or charts)
        public double ProgressRatio => WeeklyGoalMinutes > 0
            ? Math.Min(1.0, MinutesSum / WeeklyGoalMinutes)
            : 0.0;
    }
}
