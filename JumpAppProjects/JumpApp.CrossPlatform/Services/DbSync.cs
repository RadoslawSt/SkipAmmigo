using JumpApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JumpApp.Services
{
    public class DbSync : IDbSync
    {
        private IUserInfoRepository userRepository = new UserInfoRepository();
        private IAzureRestService dataStore = new AzureRestService();

        public DbSync()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coreUserInfo"></param>
        /// <param name="db">
        /// Possilbe values are From Azure or From Local</param>
        /// <returns></returns>
        //public async Task<bool> SyncCoreUserInfo(CoreUserInfo coreUserInfo,string db)
       // {
            //if(db == "From Azure")
            //{
            //    await dataStore.UpdateCoreUserInfo(coreUserInfo);
            //}
            //else if(db == "From Local")
            //{
            //    userRepository.UpdateUserInfo(coreUserInfo);
            //}
            //else
            //{
            //   return false;
            //}
            //return true;
       // }
    }
}
