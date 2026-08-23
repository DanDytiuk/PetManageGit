using PetManage.Services;

namespace PetManage.Models.ModelViewPicker
{
    public class AnimalModelPicker
    {
        public int Id { get; set; }
        public string LocalizationKey { get; set; } = string.Empty;
        public string DisplayName => LocalizationManager.Get(LocalizationKey);
    }
}
