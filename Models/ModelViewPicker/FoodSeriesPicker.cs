using PetManage.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Models.ModelViewPicker
{
    public class FoodSeriesPicker
    {
        public int ID { get; set; }
        public int FoodNameID { get; set; }
        public string LocalizationKey { get; set; } = string.Empty;
        public string DisplayName => LocalizationManager.Get(LocalizationKey);
    }
}
