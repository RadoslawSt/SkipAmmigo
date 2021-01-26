using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
namespace Entities.Extensions
{
    public static class ModelMapping
    {
        
        public static void MapItem(this Item dbItem, Item item)
        {
            dbItem.Text = item.Text;
            dbItem.Description = item.Description;
            
        }
        public static void MapUserCoreInfo(this CoreUserInformation dbcoreUser, CoreUserInformation coreUser)
        {
            dbcoreUser.CoreUserId = coreUser.CoreUserId;
            dbcoreUser.Dob = coreUser.Dob;
            dbcoreUser.Gender = coreUser.Gender;
            dbcoreUser.Height = coreUser.Height;
            dbcoreUser.LoginId = coreUser.LoginId;
            dbcoreUser.Sensetivity = coreUser.Sensetivity;
            dbcoreUser.Weight = coreUser.Weight;
        }
        public static void MapUserInfo(this UserInformation dbUserInfo, UserInformation userInfo)
        {
            dbUserInfo.Id = userInfo.Id;
            dbUserInfo.LoginId = userInfo.LoginId;
            dbUserInfo.HasProfileImage = userInfo.HasProfileImage;
            dbUserInfo.Experience = userInfo.Experience;
            dbUserInfo.Rank = userInfo.Rank;
            dbUserInfo.Achievements = userInfo.Achievements;           
            dbUserInfo.IsActive = userInfo.IsActive;
            dbUserInfo.LastActiveDateTime = userInfo.LastActiveDateTime;
            dbUserInfo.UserName = userInfo.UserName;
            dbUserInfo.ProfileImageExtension = userInfo.ProfileImageExtension;
            dbUserInfo.FriendsID = userInfo.FriendsID;
            dbUserInfo.PendingID = userInfo.PendingID;
        }
    }
}
