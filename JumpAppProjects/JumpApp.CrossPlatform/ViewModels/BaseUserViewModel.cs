using System;
using System.Collections.Generic;
using System.Text;
using JumpApp.Helpers;
using JumpApp.Models;
using JumpApp.Services;
using FluentValidation;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace JumpApp.ViewModels
{
    public class BaseUserViewModel : INotifyPropertyChanged
    {
        public CoreUserInfo userInfo;
        public UserInfo publicUserInfo;
        public INavigation navigation;
        public UserInfoValidator userValidator;
        public IUserInfoRepository userRepo;
        public ObservableCollection<UserInfo> pendingFriendList = new ObservableCollection<UserInfo>();
        public ObservableCollection<UserInfo> friendList = new ObservableCollection<UserInfo>();
        public string pfName;
        public DateTime pfLastActive;
        public UserInfo pfTopUser;
        public string pfLoginID;
        public string pfProfileImageExtension;

        //Core User Info
        public int Weight
        {
            get => userInfo.Weight;
            set
            {
                userInfo.Weight = value;
                NotifyPropertyChanged("Weight");
            }
        }
        public ImageSource ProfileImageSource
        {

            get
            {
                ImageSource source;

                if (publicUserInfo.HasProfileImage == true)
                {
                    //return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + userInfo.LoginId));
                    source = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + userInfo.LoginId + publicUserInfo.ProfileImageExtension.ToString()));
                    return source;
                    //return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/Test3"));
                }
                else
                {
                    return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/PlaceholderImage"));
                }
            }
            set
            {
                ImageSource source;
                source = value;
                NotifyPropertyChanged("ProfileImageSource");
            }
        }

        public int ProfileImageExtension
        {
            get => publicUserInfo.ProfileImageExtension;
            set
            {
                publicUserInfo.ProfileImageExtension = value;
                NotifyPropertyChanged("ProfileImageExtension");
            }
        }
        public int Height
        {
            get => userInfo.Height;
            set
            {
                userInfo.Height = value;
                NotifyPropertyChanged("Height");
            }
        }
        public string Gender
        {
            get => userInfo.Gender;
            set
            {
                userInfo.Gender = value;
                NotifyPropertyChanged("Gender");
            }
        }
        public DateTime DOB
        {
            get => userInfo.Dob;
            set
            {
                userInfo.Dob = value;
                NotifyPropertyChanged("DOB");
            }
        }
        public int Sensitivity
        {
            get => userInfo.Sensetivity;
            set
            {
                userInfo.Sensetivity = value;
                NotifyPropertyChanged("Sensetivity");
            }
        }
        public string LoginId
        {
            get => userInfo.LoginId;
            set
            {
                userInfo.LoginId = value;
                NotifyPropertyChanged("LoginId");
            }
        }

        //Public UserInfo
        public bool HasProfileImage
        {
            get => publicUserInfo.HasProfileImage;
            set
            {
                publicUserInfo.HasProfileImage = value;
                NotifyPropertyChanged("ProfileImageID");
            }
        }
        public int Experience
        {
            get => publicUserInfo.Experience;
            set
            {
                publicUserInfo.Experience = value;
                NotifyPropertyChanged("Experience");
            }
        }
        public string Rank
        {
            get => publicUserInfo.Rank;
            set
            {
                publicUserInfo.Rank = value;
                NotifyPropertyChanged("Rank");
            }
        }
        public string Achievements
        {
            get => publicUserInfo.Achievements;
            set
            {
                publicUserInfo.Achievements = value;
                NotifyPropertyChanged("Achievements");
            }
        }
        public bool IsActive
        {
            get => publicUserInfo.IsActive;
            set
            {
                publicUserInfo.IsActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }
        public DateTime LastActiveDateTime
        {
            get => publicUserInfo.LastActiveDateTime;
            set
            {
                publicUserInfo.LastActiveDateTime = value;
                NotifyPropertyChanged("LastActiveDateTime");
            }
        }
        public string FriendsID
        {
            get => publicUserInfo.FriendsID;
            set
            {
                publicUserInfo.FriendsID = value;
                NotifyPropertyChanged("FriendsID");
            }
        }
        public string PendingID
        {
            get => publicUserInfo.PendingID;
            set
            {
                publicUserInfo.PendingID = value;
                NotifyPropertyChanged("PendingID");
            }
        }
        public ObservableCollection<UserInfo> FriendList
        {
            get { return friendList; }
            set
            {
                friendList = value;
                NotifyPropertyChanged("FriendList");
            }

        }
        public ObservableCollection<UserInfo> PendingFriendList
        {
            get { return pendingFriendList; }
            set
            {
                pendingFriendList = value;
                NotifyPropertyChanged("PendingFriendList");
            }
        }

        public string UserName
        {
            get => publicUserInfo.UserName;
            set
            {
                publicUserInfo.UserName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string PFName
        {
            get { return pfName; }
            set
            {
                pfName = value;
                NotifyPropertyChanged("PFName");
            }
        }
        public DateTime PFLastActive
        {
            get { return pfLastActive; }
            set
            {
                pfLastActive = value;
                NotifyPropertyChanged("PFLastActive");
            }
        }
        public UserInfo PFTopUser
        {
            get { return pfTopUser; }
            set
            {
                pfTopUser = value;
                NotifyPropertyChanged("PFTopUser");
            }
        }
        public string PFLoginID
        {
            get { return pfLoginID; }
            set
            {
                pfLoginID = value;
                NotifyPropertyChanged("PFLoginID");
            }
        }
        public string PFProfileImageExtension
        {
            get { return pfProfileImageExtension; }
            set
            {
                pfProfileImageExtension = value;
                NotifyPropertyChanged("PFProfileImageExtension");
            }
        }

        #region INotifyPropertyChanged      
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
