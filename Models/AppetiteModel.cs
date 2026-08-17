using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetManage.Models
{
    [Table("AppetiteTypes")]
    public class AppetiteModel
    {
        [PrimaryKey, AutoIncrement]
        public int AppetiteID { get; set; }
        public string LocalizationCode { get; set; } = string.Empty;
    }
}
