using System;
using System.Collections.Generic;
using System.Text;
using JumpApp.Helpers;
using JumpApp.Models;
namespace JumpApp.Services
{
    public class WorkoutSessionPerMinRepository : IWorkoutSessionPerMinRepository
    {
        DatabaseHelper databaseHelper;
        public WorkoutSessionPerMinRepository()
        {
            databaseHelper = new DatabaseHelper();
        }
        public List<WorkoutSessionPerMin> GetWorkoutSessionDetails(int workoutID)
        {
            return databaseHelper.GetWorkoutSessionDetails(workoutID);
        }
        public void InsertWorkoutSessionDetails(WorkoutSessionPerMin session)
        {
            databaseHelper.InsertWorkoutSessionDetails(session);
        }
        public List<int> GetAllAvailableWorkoutSessionsPerMin()
        {
            return databaseHelper.GetAllAvailableWorkoutSessionsPerMin();
        }
    }
}
