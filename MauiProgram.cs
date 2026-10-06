using Microsoft.Extensions.Logging;
using PetManage.ViewModels;
using PetManage.Services;
using PetManage.View;
using Plugin.LocalNotification;

namespace PetManage
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddTransient<SettingsVM>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddSingleton<LocalizationManager>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainVM>();
            builder.Services.AddTransient<FoodVM>();
            builder.Services.AddTransient<FoodPage>();
            builder.Services.AddTransient<AddInfoFoodVM>();
            builder.Services.AddTransient<AddInfoFoodPage>();
            builder.Services.AddTransient<ProfileVM>();
            builder.Services.AddTransient<ProfilePage>();

            builder.Services.AddTransient<AddNewProfilePage>();
            builder.Services.AddTransient<AddNewProfilePageVM>();

            return builder.Build();
        }
    }
}
