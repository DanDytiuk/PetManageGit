using SQLite;

namespace PetManage.Models
{
    [Table("FoodNames")]
    public class FoodNames
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int FoodID { get; set; }
        public string LocalizationCode { get; set; } = string.Empty;
    }
}
