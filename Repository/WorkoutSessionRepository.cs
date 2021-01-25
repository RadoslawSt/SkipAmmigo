using Entities;
using Entities.Models;
using Interface;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    public class WorkoutSessionRepository : RepositoryBase<WorkoutSession>, IWorkoutSessionRepository
    {
       public WorkoutSessionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public IEnumerable<WorkoutSession> GetWorkoutSessions(string loginId)
        {
            return FindByCondition(owner => owner.LoginId.Equals(loginId))
            .DefaultIfEmpty(new WorkoutSession())
            .ToList()
            .OrderBy(x=> x.DateTime);
        }
        public WorkoutSession GetWorkoutSessionById(int workoutSessionId)
        {
            return FindByCondition(owner => owner.WorkoutSessionId.Equals(workoutSessionId))
                .DefaultIfEmpty(new WorkoutSession())
                .FirstOrDefault();
        }
        public void CreateWorkoutSession(WorkoutSession workoutSession)
        {
            Create(workoutSession);
            Save();
        }
        public void CreateMultipleWorkoutSessions(IEnumerable<WorkoutSession> workoutSessions)
        {
            foreach(var workoutSession in workoutSessions)
            {
                Create(workoutSession);
                Save();
            }
        }
    }
}
