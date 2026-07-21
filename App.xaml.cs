using Microsoft.Extensions.DependencyInjection;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Services;

namespace PetManage
{
    public partial class App : Application
    {
        private readonly DatabaseService _database;

        public App(DatabaseService database)
        {
            InitializeComponent();

            _database = database;

            LoadSettings();
        }

        private async void LoadSettings()
        {
            var settings = await _database.GetSettingsAsync();

            LocalizationManager.Instance
                .ChangeLanguage(settings.Language);

            Current.UserAppTheme =
                settings.Theme switch
                {
                    Themes.Light => AppTheme.Light,
                    Themes.Dark => AppTheme.Dark,
                    _ => AppTheme.Unspecified
                };
        }

        protected override Window CreateWindow(
            IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}