using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class UserInformation
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public bool? HasProfileImage { get; set; }
        public int? Experience { get; set; }
        public string Rank { get; set; }
        public bool? IsActive { get; set; }
        public string Achievements { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public string UserName { get; set; }
        public int ProfileImageExtension { get; set; }
    }
}
