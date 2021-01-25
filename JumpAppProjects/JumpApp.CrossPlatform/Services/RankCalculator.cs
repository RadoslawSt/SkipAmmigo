using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.Services
{
   public static class RankCalculator
    {
        public static string CalculateRank(int experience)
        {
            string Rank = "";

            if(experience <= 1000)
            {
                Rank = "Newbie";
            }
            if(experience > 1000 && experience <= 2000)
            {
                Rank = "Rookie";
            }
            if (experience > 2000 && experience <= 4000)
            {
                Rank = "Beginner";
            }
            if (experience > 4000 && experience <= 8000)
            {
                Rank = "Talented";
            }
            if (experience > 8000 && experience <= 16000)
            {
                Rank = "Skilled";
            }
            if (experience > 16000 && experience <= 25000)
            {
                Rank = "Proficient";
            }
            if (experience > 25000 && experience <= 50000)
            {
                Rank = "Experienced";
            }
            if (experience > 50000 && experience <= 75000)
            {
                Rank = "Advanced";
            }
            if (experience > 75000 && experience <= 100000)
            {
                Rank = "Expert";
            }
            if (experience > 100000 && experience <= 200000)
            {
                Rank = "Professional";
            }
            if (experience > 200000)
            {
                Rank = "Legend";
            }

            return Rank;
        }
        public static double RankProgress(int experience)
        {
            double progress = 0.0;

            var values = RankExperienceBand(CalculateRank(experience));
            int low = values.Low;
            int high = values.High;

            progress = (double)(experience - low) / (high-low);
            
            return progress;
        }
       
        public static (int Low, int High) RankExperienceBand(string Rank)
        {         
            switch(Rank)
            {
                case "Newbie":
                    return (Low: 0, High: 1000);
                   

                case "Rookie":
                    return (Low: 1001, High: 2000);
                   

                case "Beginner":
                    return (Low: 2001, High: 4000);
                   

                case "Talented":
                    return (Low: 4001, High: 8000);
                   
                
                case "Skilled":
                    return (Low: 8001, High: 16000);
                  

                case "Proficient":
                    return (Low: 16001, High: 25000);
                   

                case "Experienced":
                    return (Low: 25001, High: 50000);
                    

                case "Advanced":
                    return (Low: 50001, High: 75000);
                   

                case "Expert":
                    return (Low: 75001, High: 100000);
                  

                case "Professional":
                    return (Low: 100001, High: 200000);
                  

                case "Legend":
                    return (Low: 200001, High: 200001);
                   
                   

                default:
                    break;
            }
            return (Low: 0, High: 0);
        }
    }
}
