using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.View;

namespace PetManage.ViewModels
{
    public partial class MainVM : ObservableObject
    {

        #region ObservableProperty

        [ObservableProperty]
        private object currentView;

        #endregion

        #region RelayCommand

        [RelayCommand]
        private async Task OpenSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        [RelayCommand]
        private async Task OpenFood()
        {
            await Shell.Current.GoToAsync(nameof(FoodPage));
        }

        [RelayCommand]
        private async Task OpenGames()
        {
            await Shell.Current.GoToAsync(nameof(GamesPage));
        }

        [RelayCommand]
        private async Task OpenFinance()
        {
            await Shell.Current.GoToAsync(nameof(FinancePage));
        }

        [RelayCommand]
        private async Task OpenHealth()
        {
            await Shell.Current.GoToAsync(nameof(HealthPage));
        }

        #endregion

        #region Constructor

        public MainVM()
        {
            
        }

        #endregion
    }
}
