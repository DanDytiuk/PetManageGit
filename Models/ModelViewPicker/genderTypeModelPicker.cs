using PetManage.Services;

namespace PetManage.Models.ModelViewPicker
{
    public class GenderTypeModelPicker
    {
        public int ID { get; set;}
        public string LocalizationKey { get; set; } = string.Empty;
        public string DisplayName => LocalizationManager.Get(LocalizationKey);

    }
}
