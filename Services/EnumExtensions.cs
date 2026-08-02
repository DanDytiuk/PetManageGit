namespace PetManage.Services
{
    public static class EnumExtensions
    {
        public static List<LocalizedEnum<T>> ToLocalizedList<T>() where T : struct, Enum
        { 
            return Enum.GetValues<T>().Select(x => new LocalizedEnum<T>
            {
                Value = x
            }).ToList();
        }
    }
}
