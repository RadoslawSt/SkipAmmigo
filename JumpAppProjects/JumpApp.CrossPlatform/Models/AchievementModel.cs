using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.Models
{
    public class AchievementModel
    {
        public int AchievementID { get; set; }
        public string AchievementTitle { get; set; }
        public string AchievementDescription { get; set; }
        public string AchievementSource { get; set; }
        public DateTime AchievementDateAchieved { get; set; }
        public bool AchievementAchieved { get; set; }
    }
}
