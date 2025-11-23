using FitLife.Views;

namespace FitLife;

public partial class AppShell : Shell
{
    // Tabs that will be added after user registers
    private ShellContent _dashboardTab;
    private ShellContent _workoutsTab;
    private ShellContent _mealsTab;
    private ShellContent _progressTab;

    public AppShell()
    {
        InitializeComponent();

        // Register routes for pages that are not part of the tabs
        Routing.RegisterRoute("serviceDetail", typeof(ServiceDetailPage));
        Routing.RegisterRoute("serviceEdit", typeof(ServiceEditPage));
        Routing.RegisterRoute("workoutEdit", typeof(WorkoutEditPage));
        Routing.RegisterRoute("mealEdit", typeof(MealEditPage));

        // Prepare hidden tabs that will be shown later
        _dashboardTab = new ShellContent
        {
            Title = "Dashboard",
            Route = "dashboard",
            ContentTemplate = new DataTemplate(typeof(DashboardPage))
        };

        _workoutsTab = new ShellContent
        {
            Title = "Workouts",
            Route = "workouts",
            ContentTemplate = new DataTemplate(typeof(WorkoutListPage))
        };

        _mealsTab = new ShellContent
        {
            Title = "Meals",
            Route = "meals",
            ContentTemplate = new DataTemplate(typeof(MealPlanPage))
        };

        _progressTab = new ShellContent
        {
            Title = "Progress",
            Route = "progress",
            ContentTemplate = new DataTemplate(typeof(ProgressChartPage))
        };
    }

    // Call this after a user registers to enable extra tabs
    public void EnableRegisteredTabs()
    {
        // Prevent adding the same tabs twice
        if (!MainTabBar.Items.Contains(_dashboardTab))
        {
            // Insert new tabs after Services and before Login
            MainTabBar.Items.Insert(1, _dashboardTab);
            MainTabBar.Items.Insert(2, _workoutsTab);
            MainTabBar.Items.Insert(3, _mealsTab);
            MainTabBar.Items.Insert(4, _progressTab);
        }
    }
}
