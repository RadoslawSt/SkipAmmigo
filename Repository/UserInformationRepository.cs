using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Models;
using Interface;
using System.Linq;
using Entities.Extensions;
namespace Repository
{
   public class UserInformationRepository : RepositoryBase<UserInformation>, IUserInformationRepository
    {
       public UserInformationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public UserInformation GetUserInformation(string loginID)
        {
            return FindByCondition(owner => owner.LoginId.Equals(loginID))
            .DefaultIfEmpty(new UserInformation())
            .FirstOrDefault();
        }
        public void CreateUserInformation(UserInformation userInformation)
        {
            Create(userInformation);
            Save();
        }
        public void UpdateUserInformation(UserInformation dbUser, UserInformation user)
        {
            dbUser.MapUserInfo(user);
            Update(dbUser);
            Save();
        }
    }
}
