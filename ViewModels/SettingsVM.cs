using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Services;
using SQLite;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PetManage.ViewModels
{
    public partial class SettingsVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property
        [ObservableProperty]
        private LanguageModel selectedLanguage;

        [ObservableProperty]
        private Themes selectedTheme;

        #region Bool

        [ObservableProperty]
        private bool isEventFoodEnabled;

        [ObservableProperty]
        private bool isEventWalkEnabled;

        [ObservableProperty]
        private bool isEventVacEnabled;

        [ObservableProperty]
        private bool isEventHealthCareEnabled;

        [ObservableProperty]
        private bool isVibroEnabled;

        [ObservableProperty]
        private bool isDisturbEnabled; 

        #endregion

        #endregion

        #region Observable Collection
        public ObservableCollection<LanguageModel> Languages { get; }
        public ObservableCollection<Themes> ThemesPicker { get; }
        public ObservableCollection<TypesOfCurrency> CurrencyTypes { get; } 
        #endregion

        private SettingsModel Settings;
        
        private LanguageModel CreateLanguage(string code)
        {
            var culture = new CultureInfo(code);

            return new LanguageModel
            {
                DisplayName = culture.NativeName,
                LanguageCode = code
            };
        }

        private async Task LoadAsync()
        {
            Settings = await _database.GetSettingsAsync() ?? new SettingsModel
            {
                // Подставьте реальные значения по умолчанию для вашей модели
                Language = Languages.First().LanguageCode,
                Theme = ThemesPicker.FirstOrDefault()
            };

            SelectedLanguage =
                Languages.FirstOrDefault(l => l.LanguageCode == Settings.Language) ?? Languages.First();

            SelectedTheme = Settings.Theme;
        }

        partial void OnIsEventFoodEnabledChanged(bool value)
        {
            if (value)
            {
                _ = ToogleEventFood();
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task ToogleEventFood()
        {
            await MessageHelper.ShowMessageFood();
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedLanguage != null)
            {
                Settings.Language = SelectedLanguage.LanguageCode;
            }

            Settings.Theme = SelectedTheme;

            await _database.SaveSettingsAsync(Settings);

            // безопасное получение локализатора из ресурсов
            LocalizationManager loc = null;
            var resources = Application.Current?.Resources;
            if (resources != null && resources.TryGetValue("Loc", out var locObj))
            {
                loc = locObj as LocalizationManager;
            }

            loc?.ChangeLanguage(Settings.Language);

            Application.Current.UserAppTheme = SelectedTheme switch
            {
                Themes.Light => AppTheme.Light,
                Themes.Dark => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }

        public SettingsVM(DatabaseService database)
        {
            _database = database;

            Languages = new ObservableCollection<LanguageModel>
            {
                CreateLanguage("en"),
                CreateLanguage("ru"),
                CreateLanguage("uk"),
                CreateLanguage("es"),
                CreateLanguage("fr"),
                CreateLanguage("de")
            };



            ThemesPicker = new ObservableCollection<Themes>(Enum.GetValues<Themes>());

            CurrencyTypes = new ObservableCollection<TypesOfCurrency>(Enum.GetValues<TypesOfCurrency>());

            _ = LoadAsync();
        }

    }
}
