using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Models
{
    [Table("FoodInfo")]
    public class FoodModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public string TypeOfFood { get; set; } = string.Empty;
        public double Weight { get; set; } = 0;
    }
}
