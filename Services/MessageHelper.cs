using PetManage.Resources.Languages;
using PetManage.ViewModels;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace PetManage.Services
{
    public class MessageHelper
    {
        private readonly SettingsVM _settings;

        public static async Task ShowError(
            string messageKey,
            string titleKey)
        {
            await Shell.Current.DisplayAlertAsync(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task ShowMessage(
            string messageKey,
            string titleKey)
        {
            await Shell.Current.DisplayAlertAsync(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task ShowAttention(string messageKey, 
                                               string titleKey)
        {
            await Shell.Current.DisplayAlertAsync(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task<bool> ShowQuestion(
            string messageKey,
            string titleKey)
        {
            return await Shell.Current.DisplayAlertAsync(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                AppResources.Yes,
                AppResources.No);
        }

        public static async Task ShowMessageFood()
        {
            var request = new NotificationRequest
            {
                NotificationId = 1,
                Title = "PetManage",
                Description = "Покорми котейку епта"
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }
}
