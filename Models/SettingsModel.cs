using PetManage.Infrastructure;
using SQLite;

namespace PetManage.Models
{
    [Table("Settings")]
    public class SettingsModel
    {
        [PrimaryKey]
        public int Id { get; set; } = 1;
        public string Language { get; set; } = "en";
        public Themes Theme { get; set; } = Themes.System;
        public bool PushEat { get; set; } = false;
        public bool PushWalk { get; set; } = false;
        public bool PushVaccination { get; set; } = false;
        public bool PushGivePill { get; set; } = false;
    }
}
