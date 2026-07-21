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
            Settings = await _database.GetSettingsAsync();

            SelectedLanguage =
                Languages.FirstOrDefault(l => l.LanguageCode == Settings.Language) ?? Languages.First();

                SelectedTheme = Settings.Theme;
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Save()
        {
            Settings.Language = SelectedLanguage.LanguageCode;

            Settings.Theme = SelectedTheme;

            await _database.SaveSettingsAsync(Settings);

            var loc = Application.Current.Resources["Loc"] as LocalizationManager;

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
