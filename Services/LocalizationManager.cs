using System.ComponentModel;
using System.Globalization;
using PetManage.Resources.Languages;

namespace PetManage.Services;

public class LocalizationManager : INotifyPropertyChanged
{
    private static LocalizationManager? _instance;

    public static LocalizationManager Instance =>
        _instance ??= new LocalizationManager();

    public event PropertyChangedEventHandler? PropertyChanged;

    public string this[string key]
    {
        get => Get(key);
    }

    public static string Get(string key)
    {
        return AppResources.ResourceManager.GetString(key,AppResources.Culture) ?? key;
    }

    public void ChangeLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);

        AppResources.Culture = culture;

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs("Item[]"));
    }
}