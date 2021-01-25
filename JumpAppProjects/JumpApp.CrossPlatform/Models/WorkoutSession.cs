using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace JumpApp.Models
{
    [Table("WorkoutSession")]
    public class WorkoutSession
    {
        [PrimaryKey, AutoIncrement]
        public int WorkoutSessionId { get; set; }
        public TimeSpan Duration { get; set; }
        public int TotalJumps { get; set; }
        public double Calories { get; set; }
        public int AveragePace { get; set; }
        public int Experience { get; set; }
        public DateTime DateTime { get; set; }
        public string LoginID { get; set; }
    }
}
