using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.Models
{
    [Table("UserInformation")]
    public class UserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string LoginId { get; set; }
        public bool HasProfileImage { get; set; }
        public int Experience { get; set; }
        public string Rank { get; set; }
        public string Achievements { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public string UserName { get; set; }
        public int ProfileImageExtension { get; set; }
        public string FriendsID { get; set; }
        public string PendingID { get; set; }
    }
}
