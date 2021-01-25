using Interface;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IItemRepository _item;
        private IUserCoreInformationRepository _coreUserInfo;
        private IActivityLogRepository _activityLog;
        private IUserInformationRepository _userInfo;
        private IWorkoutSessionRepository _workoutSession;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IWorkoutSessionRepository WorkoutSession
        {
            get
            {
                if(_workoutSession == null)
                {
                    _workoutSession = new WorkoutSessionRepository(_repoContext);
                }
                return _workoutSession;
            }
        }
        public IUserInformationRepository UserInfo
        {
            get
            {
                if(_userInfo == null)
                {
                    _userInfo = new UserInformationRepository(_repoContext);
                }
                return _userInfo;
            }
        }
        public IItemRepository Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new ItemRepository(_repoContext);
                }

                return _item;
            }
        }
        public IUserCoreInformationRepository CoreUserInfo
        {
            get
            {
                if(_coreUserInfo == null)
                {
                    _coreUserInfo = new UserCoreInformationRepository(_repoContext);
                }
                return _coreUserInfo;
            }
        }
       
        public IActivityLogRepository ActivityLog
        {
            get
            {
                if(_activityLog == null)
                {
                    _activityLog = new ActivityLogRepository(_repoContext);
                }
                return _activityLog;
            }
        }

        
    }
}
