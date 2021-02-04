using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
namespace Interface
{
    public interface IUserInformationRepository
    {
        UserInformation GetUserInformationByLogin(string loginId);
        UserInformation GetUserInformationById(int Id);
        UserInformation GetUserInformationByFriendlyLogin(string friendlyLogin);
        void CreateUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation dbUserInfo, UserInformation userInfo);
        
    }
}
