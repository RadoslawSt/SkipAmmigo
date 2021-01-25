using System;
using System.Collections.Generic;
using System.Text;


namespace JumpApp.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IUserInfoRepository _coreUserInfo;
        private IWorkoutSessionRepository _workoutSession;
        private IWorkoutSessionPerMinRepository _workoutSessionPerMin;
        private IPublicUserInfoRepository _publicUserInfo;

        public IUserInfoRepository CoreUserInfo
        {
            get
            {
                _coreUserInfo = new UserInfoRepository();
                return _coreUserInfo;
            }
        }

        public IPublicUserInfoRepository PublicUserInfo
        {
            get
            {
                _publicUserInfo = new PublicUserInfoRepository();
                return _publicUserInfo;
            }
        }

        public IWorkoutSessionRepository WorkoutSession
        {
            get
            {
                _workoutSession = new WorkoutSessionRepository();
                return _workoutSession;
            }
        }
        public IWorkoutSessionPerMinRepository WorkoutSessionPerMin
        {
            get
            {
                _workoutSessionPerMin = new WorkoutSessionPerMinRepository();
                return _workoutSessionPerMin;
            }
        }
    }
}
