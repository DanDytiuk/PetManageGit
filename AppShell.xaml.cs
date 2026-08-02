using PetManage.View;

namespace PetManage
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(FoodPage), typeof(FoodPage));
            Routing.RegisterRoute(nameof(GamesPage), typeof(GamesPage));
            Routing.RegisterRoute(nameof(FinancePage), typeof(FinancePage));
            Routing.RegisterRoute(nameof(HealthPage), typeof(HealthPage));
            Routing.RegisterRoute(nameof(AddInfoFoodPage), typeof(AddInfoFoodPage));
        }
    }
}
