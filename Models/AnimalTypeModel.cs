using SQLite;

namespace PetManage.Models
{   
    [Table("AnimalTypes")]
    public class AnimalTypeModel
    {
        [PrimaryKey,  AutoIncrement]
        public int TypeID { get; set; }
        public string LocalizationCode { get; set; } = string.Empty;
    }
}
