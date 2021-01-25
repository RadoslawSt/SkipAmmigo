using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Models;
using Interface;

namespace Repository
{
    public class ActivityLogRepository : RepositoryBase<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public IEnumerable<ActivityLog> GetActivityLogsById(string loginId)
        {
            return FindByCondition(owner => owner.LoginId.Equals(loginId))
                .DefaultIfEmpty(new ActivityLog())
                    .ToList();

        }
        public void CreateActivityLog(ActivityLog activityLog)
        {
            Create(activityLog);
            Save();
        }
    }
}
