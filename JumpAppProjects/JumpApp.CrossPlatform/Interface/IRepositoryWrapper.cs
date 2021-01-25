using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.Services
{
    public interface IRepositoryWrapper
    {
        IUserInfoRepository CoreUserInfo { get; }
        IPublicUserInfoRepository PublicUserInfo { get; }
        IWorkoutSessionRepository WorkoutSession { get; }
        IWorkoutSessionPerMinRepository WorkoutSessionPerMin { get; }
    }
}
