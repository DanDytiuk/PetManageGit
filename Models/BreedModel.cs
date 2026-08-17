using SQLite;

namespace PetManage.Models
{
    [Table("Breeds")]
    public class BreedModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AnimalID { get; set; }
        public string LocalizationCode { get; set; } = string.Empty;
    }
}
