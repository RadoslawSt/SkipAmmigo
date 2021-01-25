using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
namespace Interface
{
    public interface IWorkoutSessionRepository
    {
        IEnumerable<WorkoutSession> GetWorkoutSessions(string LoginId);
        WorkoutSession GetWorkoutSessionById(int Id);
        void CreateWorkoutSession(WorkoutSession workoutSession);
        void CreateMultipleWorkoutSessions(IEnumerable<WorkoutSession> workoutSessions);

    }
}
