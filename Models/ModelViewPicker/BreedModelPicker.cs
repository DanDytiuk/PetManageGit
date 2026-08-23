using PetManage.Services;

namespace PetManage.Models.ModelViewPicker
{
    public class BreedModelPicker
    {
        public int Id { get; set; }
        public int AnimalTypeId { get; set; }
        public string LocalizationKey { get; set; } = string.Empty;
        public string DisplayName => LocalizationManager.Get(LocalizationKey);
    }
}
