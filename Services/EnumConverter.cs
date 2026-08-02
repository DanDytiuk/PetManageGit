using Microsoft.Maui.Controls;
using System.Globalization;

namespace PetManage.Services
{
    public class EnumConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType,object? parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return EnumLocalizationHelper.GetLocalized(enumValue);
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
