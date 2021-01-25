using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Interface
{
    public interface IUserCoreInformationRepository
    {
        CoreUserInformation GetCoreUserById(int coreUserId);
        CoreUserInformation GetCoreUserByLoginId(string coreUserLoginId);
        void CreateCoreUser(CoreUserInformation coreUser);
        void UpdateCoreUser(CoreUserInformation dbCoreUser, CoreUserInformation coreUser);
        void DeleteCoreUser(CoreUserInformation coreUser);
    }
}
