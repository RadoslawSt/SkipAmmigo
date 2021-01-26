
using JumpApp.Models;
using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
   public class FriendsViewModel : BaseUserViewModel
    {
        private IAzureRestService azureRestServ = new AzureRestService();
        public ImageSource imageUri { get; set; }
        public string lblName { get; set; }
        
        public FriendsViewModel()
        {
            publicUserInfo = new UserInfo();
            azureRestServ = DependencyService.Get<IAzureRestService>();
            publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
            var ok = publicUserInfo.PendingID.Split(',');
            foreach (var i in ok)
            {
                var pendingFriend = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(Convert.ToInt32(i)); }).Result;
                PendingFriendList.Add(pendingFriend);
            }

            //PendingFriendList = new ObservableCollection<string[]>();


            foreach (var i in PendingFriendList)
            {
                string stop = "";
            }
        }
    }
}
