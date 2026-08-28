using SQLite;

namespace PetManage.Models
{
    [Table("Gender")]
    public class GenderModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string LocalizationName { get; set; } = string.Empty;
    }
}
