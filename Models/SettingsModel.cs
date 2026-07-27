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

        #region Food

        public bool PushEat { get; set; } = false;
        public DateTime FromTimeEat { get; set; }
        public DateTime ToTimeEat { get; set; }
        public DateTime StepTimeEat { get; set; }

        #endregion

        #region Walk

        public bool PushWalk { get; set; } = false;
        public DateTime FromTimeWalk { get; set; }
        public DateTime ToTimeWalk { get; set; }
        public DateTime StepTimeWalk { get; set; }

        #endregion

        #region Vaccination

        public bool PushVaccination { get; set; } = false;
        public DateTime DateVaccination { get; set; }

        #endregion

        #region Pill

        public bool PushGivePill { get; set; } = false;
        public DateTime FromTimeGivePill { get; set; }
        public DateTime ToTimeGivePill { get; set; }
        public DateTime StepTimeGivePill { get; set; }

        #endregion

        public bool Vibration { get; set; } = false;
        
        #region Disturb

        public bool DonutDisturb { get; set; }
        public DateTime FromDonutDisturb { get; set; }
        public DateTime ToDonutDisturb { get; set; }

        #endregion

        public string ValueOfCurrency { get; set; } = "USD";
        public double VersionOfApp { get; set; } = 1.0;

    }
}
