using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ActivityLog
    {
        public int ActivityId { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan ActiveDuration { get; set; }
        public string LoginId { get; set; }
    }
}
