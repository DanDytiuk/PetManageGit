namespace PetManage.Services
{
    [ContentProperty(nameof(Key))]
    public class Translate : IMarkupExtension
    {
        public string Key { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return LocalizationManager.Instance[Key];
        }
    }
}
