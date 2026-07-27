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

        #region Food

        [ObservableProperty]
        private bool isEventFoodEnabled;

        [ObservableProperty]
        private DateTime fromTimeEat;

        [ObservableProperty]
        private DateTime toTimeEat;

        [ObservableProperty]
        private DateTime stepTimeEat;
        
        #endregion

        #region Walk

        [ObservableProperty]
        private bool isEventWalkEnabled;

        [ObservableProperty]
        private DateTime fromTimeWalk;

        [ObservableProperty]
        private DateTime toTimeWalk;

        [ObservableProperty]
        private DateTime stepTimeWalk;

        #endregion

        #region Vaccination

        [ObservableProperty]
        private bool isEventVacEnabled;

        [ObservableProperty]
        private DateTime dateVaccination;

        #endregion

        #region Pill
        [ObservableProperty]
        private bool isEventHealthCareEnabled;

        [ObservableProperty]
        private DateTime fromTimePill;

        [ObservableProperty]
        private DateTime toTimePill;

        [ObservableProperty]
        private DateTime stepTimePill; 
        #endregion

        [ObservableProperty]
        private bool isVibroEnabled;

        #region DonutDisturb
        [ObservableProperty]
        private bool isDisturbEnabled;

        [ObservableProperty]
        private DateTime fromDonutDisturb;

        [ObservableProperty]
        private DateTime toDonutDisturb;
        #endregion

        [ObservableProperty]
        private string valueOfCurrency;

        [ObservableProperty]
        private string valueOfVersion;

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
                Theme = ThemesPicker.FirstOrDefault(),

                PushEat = false,
                FromTimeEat = DateTime.Now,
                ToTimeEat = DateTime.Now.AddHours(1),
                StepTimeEat = DateTime.Now.AddMinutes(30),

                PushWalk = false,
                FromTimeWalk = DateTime.Now,
                ToTimeWalk = DateTime.Now.AddHours(1),
                StepTimeWalk = DateTime.Now.AddMinutes(30),

                PushVaccination = false,
                DateVaccination = DateTime.Now,

                DonutDisturb = false,
                FromDonutDisturb = DateTime.Now,
                ToDonutDisturb = DateTime.Now.AddHours(1),

                PushGivePill = false,
                FromTimeGivePill = DateTime.Now,
                ToTimeGivePill = DateTime.Now.AddHours(1),
                StepTimeGivePill = DateTime.Now.AddMinutes(30),

                Vibration = false,

                ValueOfCurrency = CurrencyTypes.FirstOrDefault().ToString() ?? "USD",
                VersionOfApp = 1.0
            };

            SelectedLanguage =
                Languages.FirstOrDefault(l => l.LanguageCode == Settings.Language) ?? Languages.First();

            SelectedTheme = Settings.Theme;

            IsEventFoodEnabled = Settings.PushEat;
            FromTimeEat = Settings.FromTimeEat;
            ToTimeEat = Settings.ToTimeEat;
            StepTimeEat = Settings.StepTimeEat;

            IsEventWalkEnabled = Settings.PushWalk;
            FromTimeWalk = Settings.FromTimeWalk;
            ToTimeWalk = Settings.ToTimeWalk;
            StepTimeWalk = Settings.StepTimeWalk;

            IsEventVacEnabled = Settings.PushVaccination;
            DateVaccination = Settings.DateVaccination;

            IsDisturbEnabled = Settings.DonutDisturb;
            FromDonutDisturb = Settings.FromDonutDisturb;
            ToDonutDisturb = Settings.ToDonutDisturb;

            IsEventHealthCareEnabled = Settings.PushGivePill;
            FromTimePill = Settings.FromTimeGivePill;
            ToTimePill = Settings.ToTimeGivePill;
            StepTimePill = Settings.StepTimeGivePill;

            IsVibroEnabled = Settings.Vibration;

            ValueOfCurrency = Settings.ValueOfCurrency;
            ValueOfVersion = Settings.VersionOfApp.ToString();

        }

        partial void OnIsEventFoodEnabledChanged(bool value)
        {
            if (value)
            {
                _ = ToogleEventFood();
                Settings.PushEat = true;
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

            Settings.PushEat = IsEventFoodEnabled;
            Settings.FromTimeEat = FromTimeEat;
            Settings.ToTimeEat = ToTimeEat;
            Settings.StepTimeEat = StepTimeEat;

            Settings.PushWalk = IsEventWalkEnabled;
            Settings.FromTimeWalk = FromTimeWalk;
            Settings.ToTimeWalk = ToTimeWalk;
            Settings.StepTimeWalk = StepTimeWalk;

            Settings.PushVaccination = IsEventVacEnabled;
            Settings.DateVaccination = DateVaccination;

            Settings.DonutDisturb = IsDisturbEnabled;
            Settings.FromDonutDisturb = FromDonutDisturb;
            Settings.ToDonutDisturb = ToDonutDisturb;

            Settings.PushGivePill = IsEventHealthCareEnabled;
            Settings.FromTimeGivePill = FromTimePill;
            Settings.ToTimeGivePill = ToTimePill;
            Settings.StepTimeGivePill = StepTimePill;

            Settings.Vibration = IsVibroEnabled;

            await _database.SaveSettingsAsync(Settings);

            LocalizationManager? loc = null;

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
