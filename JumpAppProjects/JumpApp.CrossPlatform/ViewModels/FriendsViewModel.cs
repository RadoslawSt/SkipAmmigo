
using JumpApp.Models;
using JumpApp.Services;
using JumpApp.Views;
using JumpApp.Views.Settings;
using Rg.Plugins.Popup.Extensions;
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
        public ICommand ProcessFriendRequestCommand { get; private set; }
        public ICommand DeleteFriendCommand { get; private set; }
        public ICommand AddFriendCommand { get; private set; }
        public ICommand ViewFriendCommand { get; private set; }

        public FriendsViewModel(INavigation Navigation)
        {
            publicUserInfo = new UserInfo();
            FriendList = new ObservableCollection<UserInfo>();
            navigation = Navigation;
            azureRestServ = DependencyService.Get<IAzureRestService>();
            publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
            ProcessFriendRequestCommand = new Command(async (object obj) => await ProcessFriendRequest(obj));
            DeleteFriendCommand = new Command(async (object obj) => await DeleteFriend(obj));
            ViewFriendCommand = new Command(async (object obj) => await ViewFriend(obj));
            AddFriendCommand = new Command(async () => await AddFriend());

            SetupPendingFriend();
            SetupFriend();

        }
        private void SetupPendingFriend()
        {
            PendingFriendList = new ObservableCollection<UserInfo>();
            if (publicUserInfo.PendingID != "")
            {
                GridVisibility = "True";
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
            }
            else
            {
                GridVisibility = "False";
                PFName = "";
                PFLastActive = DateTime.Now;
                //PFProfileImageExtension = "";
                PFLoginID = "";
            }

        }
        private void SetupFriend()
        {
            FriendList = new ObservableCollection<UserInfo>();
            if(publicUserInfo.FriendsID != "")
            {
                foreach (var i in publicUserInfo.FriendsID.Split(','))
                {
                    var friend = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(Convert.ToInt32(i)); }).Result;
                    if (friend != null)
                    {
                        FriendList.Add(friend);
                    }

                }
                FriendLabel = "Friends (" + FriendList.Count().ToString() + "/30)";
            }
            else
            {
                FriendLabel = "Friends (0/30)";
            }
                    
        }
        private async Task ViewFriend(object sender)
        {
            UserInfo friendToDelete = (UserInfo)sender;
            string stop = "";
            await navigation.PushAsync(new EditProfile("View Profile",friendToDelete.LoginId));
        }
            private async Task DeleteFriend(object sender)
        {
            UserInfo friendToDelete = (UserInfo)sender;
            List<string> newFriendList = new List<string>();
            string stringFormatFriendList = "";

            foreach (var friendID in FriendList)
            {
                if (friendID.Id == friendToDelete.Id)
                {

                }
                else
                {
                    newFriendList.Add(friendID.Id.ToString());
                }
            }
            foreach (var id in newFriendList)
            {
                stringFormatFriendList += id + ",";
            }
            if(stringFormatFriendList != "")
            {
                publicUserInfo.FriendsID = stringFormatFriendList.Remove(stringFormatFriendList.Length - 1);
            }
            else
            {
                publicUserInfo.FriendsID = "";
            }
            
            await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            SetupPendingFriend();
            SetupFriend();
        }
        private async Task AddFriend()
        {
            var popupPage = new AddFriendPopupPage(navigation);
            //var popupPage = new PostWorkoutPopupPage(publicUser, (float)250);
            //popupPage.CallbackEvent += (object sender, object e) => CallbackMethod(e);
            await navigation.PushPopupAsync(popupPage);



        }
        private string FormatFriendString(UserInfo formatUserInfo)
        {
            List<string> newPendingFriendList = new List<string>();
            string stringFormatPendingFriendList = "";
            if (formatUserInfo.PendingID.Contains(','))
            {
                foreach (var i in formatUserInfo.PendingID.Split(','))
                {
                    if (i == PFTopUser.Id.ToString())
                    {

                    }
                    else
                    {
                        newPendingFriendList.Add(i);
                    }
                }
                foreach (var id in newPendingFriendList)
                {
                    stringFormatPendingFriendList += id + ",";
                }
            }
            else
            {
                
                stringFormatPendingFriendList = "";
            }
            if (stringFormatPendingFriendList.Count() == 0)
            {
                stringFormatPendingFriendList = "";
            }
            else
            {
                stringFormatPendingFriendList = stringFormatPendingFriendList.Remove((stringFormatPendingFriendList.Length - 1));
            }
            return stringFormatPendingFriendList;
        }
        private async Task ProcessFriendRequest(object sender)
        {
            string command = (string)sender;
            //List<string> newPendingFriendList = new List<string>();
            //string stringFormatPendingFriendList = "";
            string stringFormatPendingFriendList = FormatFriendString(publicUserInfo);
            //if (publicUserInfo.PendingID.Contains(','))
            //{
            //foreach (var i in publicUserInfo.PendingID.Split(','))
            //{
            //    if (i == PFTopUser.Id.ToString())
            //    {

            //    }
            //    else
            //    {
            //        newPendingFriendList.Add(i);
            //    }
            //}
            //foreach (var id in newPendingFriendList)
            //{
            //    stringFormatPendingFriendList += id + ",";
            //}
            if (command == "Confirm")
            {
                             
                if(publicUserInfo.FriendsID != "")
                {
                    publicUserInfo.FriendsID = publicUserInfo.FriendsID + "," + PFTopUser.Id;
                }
                else
                {
                    publicUserInfo.FriendsID =  PFTopUser.Id.ToString();
                }
                if(PFTopUser.FriendsID != "")
                {
                    PFTopUser.FriendsID = PFTopUser.FriendsID + "," + publicUserInfo.Id;
                }
                else
                {
                    PFTopUser.FriendsID = publicUserInfo.Id.ToString();
                }
                publicUserInfo.PendingID = stringFormatPendingFriendList;
            }
            else if (command == "Cancel")
            {
                publicUserInfo.PendingID = stringFormatPendingFriendList;
            }
            else
            {

            }
            //}
            //else
            //{
            // stringFormatPendingFriendList = publicUserInfo.PendingID;

            //if (command == "Confirm")
            //{
            //    publicUserInfo.FriendsID = publicUserInfo.FriendsID + "," + PFTopUser.Id;
            //    PFTopUser.FriendsID = PFTopUser.FriendsID + "," + publicUserInfo.Id;
            //    //publicUserInfo.PendingID = stringFormatPendingFriendList.Remove(stringFormatPendingFriendList.Length);
            //    publicUserInfo.PendingID = "";
            //}
            //else if (command == "Cancel")
            //{
            //    //publicUserInfo.PendingID = stringFormatPendingFriendList.Remove(stringFormatPendingFriendList.Length);
            //    publicUserInfo.PendingID = "";
            //}
            //else
            //{

            //}
            //}


            await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            await azureRestServ.UpdatePublicUserInfo(PFTopUser);
            SetupPendingFriend();
            SetupFriend();
        }
    }
}
