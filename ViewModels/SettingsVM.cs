using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PetManage.ViewModels
{
    public partial class SettingsVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        [ObservableProperty]
        public partial LanguageModel? SelectedLanguage { get; set; }

        [ObservableProperty]
        public partial Themes SelectedTheme { get; set; }

        #region Food

        [ObservableProperty]
        public partial bool IsEventFoodEnabled { get; set; }

        [ObservableProperty]
        public partial TimeSpan FromTimeEat { get; set; }

        [ObservableProperty]
        public partial TimeSpan ToTimeEat { get; set; }

        [ObservableProperty]
        public partial TimeSpan StepTimeEat { get; set; }

        #endregion

        #region Walk

        [ObservableProperty]
        public partial bool IsEventWalkEnabled { get; set; }

        [ObservableProperty]
        public partial TimeSpan FromTimeWalk { get; set; }

        [ObservableProperty]
        public partial TimeSpan ToTimeWalk { get; set; }

        [ObservableProperty]
        public partial TimeSpan StepTimeWalk { get; set; }

        #endregion

        #region Vaccination

        [ObservableProperty]
        public partial bool IsEventVacEnabled { get; set; }

        [ObservableProperty]
        public partial DateTime DateVaccination { get; set; }

        #endregion

        #region Pill

        [ObservableProperty]
        public partial bool IsEventHealthCareEnabled { get; set; }

        [ObservableProperty]
        public partial TimeSpan FromTimePill { get; set; }

        [ObservableProperty]
        public partial TimeSpan ToTimePill { get; set; }

        [ObservableProperty]
        public partial TimeSpan StepTimePill { get; set; }

        #endregion

        [ObservableProperty]
        public partial bool IsVibroEnabled { get; set; }

        #region DonutDisturb

        [ObservableProperty]
        public partial bool IsDisturbEnabled { get; set; }

        [ObservableProperty]
        public partial TimeSpan FromDonutDisturb { get; set; }

        [ObservableProperty]
        public partial TimeSpan ToDonutDisturb { get; set; }

        #endregion

        [ObservableProperty]
        public partial string ValueOfCurrency { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ValueOfVersion { get; set; } = string.Empty;

        #endregion

        #region Observable Collection

        public ObservableCollection<LanguageModel> Languages { get; }
        public ObservableCollection<Themes> ThemesPicker { get; }
        public ObservableCollection<TypesOfCurrency> CurrencyTypes { get; }

        #endregion

        private SettingsModel Settings = new();

        public DateTime MinDate { get; } = new DateTime(2020, 1, 1);
        public DateTime MaxDate { get; } = new DateTime(2099, 12, 31);

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
                Language = Languages.First().LanguageCode,
                Theme = ThemesPicker.FirstOrDefault(),

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
