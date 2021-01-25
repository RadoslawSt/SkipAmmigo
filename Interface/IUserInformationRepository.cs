using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
namespace Interface
{
    public interface IUserInformationRepository
    {
        UserInformation GetUserInformation(string loginId);
        void CreateUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation dbUserInfo, UserInformation userInfo);
        
    }
}
