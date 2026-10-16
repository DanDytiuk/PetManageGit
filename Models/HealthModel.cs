using PetManage.Services;
using SQLite;

namespace PetManage.Models
{
    [Table("Health")]
    public class HealthModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PetID { get; set; }
        public string TypeHealthOperation { get; set; } = string.Empty;
        public string NameOfHealthOperation { get; set; } = string.Empty;
        public DateTime HealthOperationDate { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
