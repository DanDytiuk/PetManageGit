using SQLite;

namespace PetManage.Models
{
    [Table("Profile")]
    public class ProfileModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = 1;

        [AutoIncrement]
        public int PetID { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string TypeOfPet { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public double Weight { get; set; } = 0;
        public double Age { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;

    }
}
