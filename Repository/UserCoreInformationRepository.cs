using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Interface;
using Entities;
using Entities.Extensions;
using System.Linq;

namespace Repository
{
   public class UserCoreInformationRepository : RepositoryBase<CoreUserInformation>, IUserCoreInformationRepository
    {
        public UserCoreInformationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public CoreUserInformation GetCoreUserById(int coreUserId)
        {
            return FindByCondition(owner => owner.CoreUserId.Equals(coreUserId))
                    .DefaultIfEmpty(new CoreUserInformation())
                    .FirstOrDefault();
        }
        public CoreUserInformation GetCoreUserByLoginId(string coreUserLoginId)
        {
            return FindByCondition(owner => owner.LoginId.Equals(coreUserLoginId))
                    .DefaultIfEmpty(new CoreUserInformation())
                    .FirstOrDefault();
        }


        public void CreateCoreUser(CoreUserInformation coreUser)
        {
            //item.CoreUserId = Guid.NewGuid().ToString();
            Create(coreUser);
            Save();
        }

        public void UpdateCoreUser(CoreUserInformation dbCoreUser, CoreUserInformation coreUser)
        {
            dbCoreUser.MapUserCoreInfo(coreUser);
            Update(dbCoreUser);
            Save();
        }

        public void DeleteCoreUser(CoreUserInformation coreUser)
        {
            Delete(coreUser);
            Save();
        }
    }
}
