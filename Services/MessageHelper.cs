using PetManage.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Services
{
    public static class MessageHelper
    {
        public static async Task ShowError(
            string messageKey,
            string titleKey)
        {
            await Shell.Current.DisplayAlert(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task ShowMessage(
            string messageKey,
            string titleKey)
        {
            await Shell.Current.DisplayAlert(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task ShowAttention(
            string messageKey,
            string titleKey)
        {
            await Shell.Current.DisplayAlert(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                "OK");
        }

        public static async Task<bool> ShowQuestion(
            string messageKey,
            string titleKey)
        {
            return await Shell.Current.DisplayAlert(
                AppResources.ResourceManager.GetString(titleKey),
                AppResources.ResourceManager.GetString(messageKey),
                AppResources.Yes,
                AppResources.No);
        }
    }
}
