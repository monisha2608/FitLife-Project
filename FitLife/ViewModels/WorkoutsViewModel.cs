using System.Collections.ObjectModel;
using FitLife.Models;
using FitLife.Services;

namespace FitLife.ViewModels
{
    // ViewModel for the workouts screen
    public class WorkoutsViewModel : BaseViewModel
    {
        // List of workouts (loaded from mock data)
        public ObservableCollection<Workout> Workouts { get; } =
            new ObservableCollection<Workout>(MockDataService.GetWorkouts());

        // Command for adding a new workout
        public Command AddWorkoutCmd { get; }

        public WorkoutsViewModel()
        {
            AddWorkoutCmd = new Command(async () => await AddWorkout());
        }

        // Adds a new custom workout through a prompt
        private async Task AddWorkout()
        {
            string title = await Application.Current!.MainPage!.DisplayPromptAsync("Add workout", "Workout title:");
            if (string.IsNullOrWhiteSpace(title)) return;

            var w = new Workout
            {
                Title = title,
                Category = "Custom",
                DurationMins = 20,
                Calories = 150,
                Image = "workout1.png"
            };

            Workouts.Add(w);
        }
    }
}
