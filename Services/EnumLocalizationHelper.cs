using PetManage.Resources.Languages;

namespace PetManage.Services
{
    public static class EnumLocalizationHelper
    {
        public static string GetLocalized(Enum value)
        {
            string key = $"{value.GetType().Name}_{value}";

            return AppResources.ResourceManager.GetString(key, AppResources.Culture) ?? value.ToString();
        }
    }
}
