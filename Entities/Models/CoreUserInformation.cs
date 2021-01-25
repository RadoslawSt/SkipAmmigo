using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class CoreUserInformation
    {
        public int CoreUserId { get; set; }
        public DateTime Dob { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Gender { get; set; }
        public int Sensetivity { get; set; }
        public string LoginId { get; set; }
    }
}
