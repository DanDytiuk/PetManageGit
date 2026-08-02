using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PetManage.Services
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return EnumLocalizationHelper.GetLocalized(enumValue);
                
            }

            return string.Empty;
        }

        public object Convertback(object value, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
