using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using JumpApp.Services;
using JumpApp.Models;
using System.Threading.Tasks;
using JumpApp.Views;
using Microsoft.Identity.Client;
using JumpApp.Features.ADB2C;

namespace JumpApp.ViewModels
{
    public class AddUserInfoModel : BaseUserViewModel
    {
        public ICommand AddUserInfoCommand { get; private set; }
        
        public IAzureRestService dataStore = new AzureRestService();
        
        public IDbSync dbSync = new DbSync();

        public AddUserInfoModel(INavigation Navigation)
        {
            navigation = Navigation;
            userValidator = new UserInfoValidator();
            userInfo = new CoreUserInfo();
            publicUserInfo = new UserInfo();

            userRepo = new UserInfoRepository();
            //authenticationResult = result;
            AddUserInfoCommand = new Command(async () => await AddUserInfo());
            
        }

        async Task AddUserInfo()
        {
            var validationResult = userValidator.Validate(userInfo);
            //CoreUserInfo user = await dataStore.GetCoreUserInfo(1);
            if (validationResult.IsValid)
            {
               
               
                userInfo.LoginId = App.userContext.UserIdentifier;
                publicUserInfo.LoginId = App.userContext.UserIdentifier;
                

                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add User Information", "Do you want to proceed", "OK", "Cancel");
                if (isUserAccept)
                {
                    try
                    {
                        publicUserInfo.Experience = 0;
                        publicUserInfo.HasProfileImage = false;
                        publicUserInfo.Rank = "";
                        publicUserInfo.Achievements = "0";
                        publicUserInfo.ProfileImageExtension = 1;
                        publicUserInfo.LastActiveDateTime = DateTime.Now;
                        publicUserInfo.IsActive = App.userContext.IsLoggedOn;
                        publicUserInfo.UserName = App.userContext.Name;
                        try
                        {
                            bool isSaved = await B2CAuthenticationService.SaveUserImageFB(App.userContext.UserIdentifier);
                            if(isSaved == true)
                            {
                                publicUserInfo.HasProfileImage = true;
                                publicUserInfo.ProfileImageExtension = 1;
                            }
                            else
                            {
                                publicUserInfo.HasProfileImage = false;
                                publicUserInfo.ProfileImageExtension = 0;
                            }
                            
                        }
                        catch (Exception)
                        {
                            publicUserInfo.HasProfileImage = false;
                            publicUserInfo.ProfileImageExtension = 0;
                        }
                        
                        //bool hasImage = await AuthUserHelper.SaveUserImageFB(userInfo.LoginId);
                        //if (hasImage == true)
                        //{
                        //    publicUserInfo.HasProfileImage = true;
                        //    //await dataStore.UpdatePublicUserInfo(publicUserInfo);
                        //}
                        await dataStore.AddCoreUserInfo(userInfo);
                        await dataStore.AddPublicUserInfo(publicUserInfo);
                        // AuthUserHelper.SaveUserImage(userInfo.LoginId);
                        //userRepo.InsertUserInfo(userInfo);

                        await navigation.PushAsync(new ShakeSetup());
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                   
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add User Information", validationResult.Errors[0].ErrorMessage, "Ok");
            }
        }
        //public bool IsViewAll => userRepo.GetAllUserInfo().Count > 0 ? true : false;
    }
}
