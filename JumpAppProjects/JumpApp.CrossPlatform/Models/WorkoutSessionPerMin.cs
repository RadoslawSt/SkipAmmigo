using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace JumpApp.Models
{
    [Table ("WorkoutSessionPerMin")]
    public class WorkoutSessionPerMin
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public TimeSpan Minute { get; set; }
        public int Jumps { get; set; }
        public Double Pace { get; set; }
        public string PaceChangeIcon { get; set; }
        public int TotalJumps { get; set; }
        //public double AveragePace { get; set; }
        
    }
}
