using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
namespace Interface
{
    public interface IActivityLogRepository
    {
        //IEnumerable<ActivityLog> GetAl();
        IEnumerable<ActivityLog> GetActivityLogsById(string UserId);
        //Extended GetOwnerWithDetails(Guid ownerId);
        void CreateActivityLog(ActivityLog item);
        //void UpdateItem(ActivityLog dbItem, ActivityLog item);
        //void DeleteItem(ActivityLog item);
    }
}
