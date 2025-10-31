using System.Collections.ObjectModel;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels;

public class ProgressViewModel : BaseViewModel
{
    public ObservableCollection<ProgressEntry> Weekly { get; } =
        new ObservableCollection<ProgressEntry>(MockDataService.GetWeeklyProgress());

    public double WeeklyGoalMinutes => 7 * 30; // 30 min/day

    public double MinutesSum => Weekly.Sum(p => p.Minutes);

    public double ProgressRatio => WeeklyGoalMinutes > 0 ? Math.Min(1.0, MinutesSum / WeeklyGoalMinutes) : 0.0;
}
