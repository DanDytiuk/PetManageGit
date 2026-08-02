using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Services
{
    public class LocalizedEnum<T> where T : Enum
    {
        public T Value { get; set; }

        public string Name => EnumLocalizationHelper.GetLocalized(Value);
    }
}
