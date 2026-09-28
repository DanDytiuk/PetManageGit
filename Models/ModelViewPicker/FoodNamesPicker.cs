using PetManage.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Models.ModelViewPicker
{
    internal class FoodNamesPicker
    {
        public int ID { get; set; }
        public string LocalizationKey { get; set; } = string.Empty;
        public string DisplayName => LocalizationManager.Get(LocalizationKey);
    }
}
