using System;
using System.Collections.Generic;
using System.Text;
using JumpApp.Models;
namespace JumpApp.Services
{
    public interface IWorkoutSessionPerMinRepository
    {
        List<WorkoutSessionPerMin> GetWorkoutSessionDetails(int workoutID);
        void InsertWorkoutSessionDetails(WorkoutSessionPerMin session);
        List<int> GetAllAvailableWorkoutSessionsPerMin();
    }
}
