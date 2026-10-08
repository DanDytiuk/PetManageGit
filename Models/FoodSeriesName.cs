using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Models
{
    [Table("FoodSeriesName")]
    public class FoodSeriesName
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int FoodID { get; set; }
        public string TypeOfFood { get; set; } = string.Empty;
        public string ForAnimalType { get; set; } = string.Empty;
        public string LocalizationCode { get; set; } = string.Empty;
    }
}
