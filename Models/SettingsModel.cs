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
        public TimeSpan FromTimeEat { get; set; }
        public TimeSpan ToTimeEat { get; set; }
        public TimeSpan StepTimeEat { get; set; }

        #endregion

        #region Walk

        public bool PushWalk { get; set; } = false;
        public TimeSpan FromTimeWalk { get; set; }
        public TimeSpan ToTimeWalk { get; set; }
        public TimeSpan StepTimeWalk { get; set; }

        #endregion

        #region Vaccination

        public bool PushVaccination { get; set; } = false;
        public DateTime DateVaccination { get; set; }

        #endregion

        #region Pill

        public bool PushGivePill { get; set; } = false;
        public TimeSpan FromTimeGivePill { get; set; }
        public TimeSpan ToTimeGivePill { get; set; }
        public TimeSpan StepTimeGivePill { get; set; }

        #endregion

        public bool Vibration { get; set; } = false;
        
        #region Disturb

        public bool DonutDisturb { get; set; }
        public TimeSpan FromDonutDisturb { get; set; }
        public TimeSpan ToDonutDisturb { get; set; }

        #endregion

        public string ValueOfCurrency { get; set; } = "USD";
        public double VersionOfApp { get; set; } = 1.0;

    }
}
