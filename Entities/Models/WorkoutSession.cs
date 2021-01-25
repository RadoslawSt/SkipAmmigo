using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class WorkoutSession
    {
        public int WorkoutSessionId { get; set; }
        public string Duration { get; set; }
        public int TotalJumps { get; set; }
        public double Calories { get; set; }
        public int AveragePace { get; set; }
        public int Experience { get; set; }
        public DateTime DateTime { get; set; }
        public string LoginId { get; set; }
    }
}
