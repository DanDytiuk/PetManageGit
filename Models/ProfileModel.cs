using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PetManage.Models
{
    [Table("Profile")]
    public class ProfileModel
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string TypeOfPet { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public float Weight { get; set; } = 0;
        public float Age { get; set; } = 0;

    }
}
