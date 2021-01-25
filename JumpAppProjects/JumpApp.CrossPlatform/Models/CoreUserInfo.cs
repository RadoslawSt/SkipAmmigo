using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace JumpApp.Models
{
    [Table("CoreUserInfo")]
    public class CoreUserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int CoreUserId { get; set; }
        public DateTime Dob { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Gender { get; set; }
        public int Sensetivity { get; set; }
        public string LoginId { get; set; }
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }             
        //public string Gender { get; set; }
        //public DateTime DOB { get; set; }
        //public int Weight { get; set; }
        //public int Height { get; set; }
        //public int Sensetivity { get; set; }
    }
    //public partial class CoreUserInformation
    //{
    //    public int CoreUserId { get; set; }
    //    public DateTime Dob { get; set; }
    //    public int Weight { get; set; }
    //    public int Height { get; set; }
    //    public string Gender { get; set; }
    //    public int Sensetivity { get; set; }
    //    public string LoginId { get; set; }
    //}
}
