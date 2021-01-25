using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public interface IRepositoryWrapper
    {
        IItemRepository Item { get; }
        IUserCoreInformationRepository CoreUserInfo { get; }
        IActivityLogRepository ActivityLog { get; }
        IUserInformationRepository UserInfo { get; }
        IWorkoutSessionRepository WorkoutSession { get; }
        //IRepositoryBase<T> repositoryBase { get; }
    }
}
