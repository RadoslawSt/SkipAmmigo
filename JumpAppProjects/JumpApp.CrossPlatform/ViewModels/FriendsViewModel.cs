
using JumpApp.Models;
using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
   public class FriendsViewModel : BaseUserViewModel
    {
        private IAzureRestService azureRestServ = new AzureRestService();
        public ICommand ConfirmPendingFriendCommand { get; private set; }
        public ICommand CancelPendingFriendCommand { get; private set; }
      
        public string PFName { get; set; }
        public DateTime PFLastActive { get; set; }
        public UserInfo PFTopUser { get; set; }
        public string PFLoginID { get; set; }
        public string PFProfileImageExtension { get; set; }

        public FriendsViewModel()
        {
            publicUserInfo = new UserInfo();
            azureRestServ = DependencyService.Get<IAzureRestService>();
            publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
            ConfirmPendingFriendCommand = new Command(async () => await ConfirmFriend());
            CancelPendingFriendCommand = new Command(async () => await CancelFriend());
            //var ok = publicUserInfo.PendingID.Split(',');

            
            SetupPendingFriend();
            SetupFriend();
            //PendingFriendList = new ObservableCollection<string[]>();
            
            //Task.Run(async () => {await SetUpLabels(); });

            //foreach (var i in PendingFriendList)
            //{
            //    string stop = "";
            //}
        }
        private void SetupPendingFriend()
        {
            foreach (var i in publicUserInfo.PendingID.Split(','))
            {
                var pendingFriend = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(Convert.ToInt32(i)); }).Result;
                PendingFriendList.Add(pendingFriend);
            }

            PFTopUser = PendingFriendList.FirstOrDefault();
            PFName = PFTopUser.UserName;
            PFLastActive = PFTopUser.LastActiveDateTime;
            PFProfileImageExtension = 15.ToString() + "|" + PFTopUser.HasProfileImage;
            PFLoginID = "2f18d8a0-9a56-42c9-a87d-5481e4ed3e28";
            //pendingLoginID = topUser.LoginId;

            //pendingProfileImageExtension = topUser.ProfileImageExtension.ToString();

            //imageUri = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + topUser.LoginId + topUser.ProfileImageExtension));
        }
        private void SetupFriend()
        {
            foreach(var i in publicUserInfo.FriendsID.Split(','))
            {
                var friend = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(Convert.ToInt32(i)); }).Result;
                FriendList.Add(friend);
            }
        }
        private async Task ConfirmFriend()
        {

        }
        private async Task CancelFriend()
        {

        }
        }
}
