using JumpApp.Models;
using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using JumpApp.Controls;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.WindowsAzure.Storage.Blob;
using Rg.Plugins.Popup.Extensions;
using JumpApp.Views;
using Rg.Plugins.Popup.Pages;

namespace JumpApp.ViewModels
{

    public class EditUserInfoModel : BaseUserViewModel
    {
        public ICommand UpdateUserInfoCommand { get; private set; }
        public ICommand SetUpUserInfoCommand { get; private set; }
        public ICommand ChangeProfileImageCommand { get; private set; }
        private IAzureRestService azureRestServ = new AzureRestService();
        public string TotalJumps { get; set; }
        public double ProfileImageWidth { get; set; }
        public double ProfileImageHeight { get; set; }
        public double RankProgress { get; set; }
        public string RankTitle { get; set; }
        public string Active { get; set; }
        public string ActiveColour { get; set; }
        List<AchievementModel> AchievementsList { get; set; }


        public EditUserInfoModel(INavigation Navigation, Grid grid, string Profile, string identifier)
        {
            navigation = Navigation;
            userInfo = new CoreUserInfo();
            publicUserInfo = new UserInfo();
            azureRestServ = DependencyService.Get<IAzureRestService>();

            if (Profile == "View Profile")
            {
                GridVisibility = "False";
                userInfo = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(identifier); }).Result;
                publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(identifier); }).Result;
            }
            else if(Profile == "Edit Profile")
            {
                GridVisibility = "True";
                userInfo = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(App.userContext.UserIdentifier); }).Result;
                publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
                UpdateUserInfoCommand = new Command(async () => await UpdateUserInfo());
                ChangeProfileImageCommand = new Command(async () => await ChangeProfileImage());
            }
            else
            {

            }          
                       
            
            SetUpUserInfo();
            SetUpAchievements();
            CreateGrid(grid);
            var test = ProfileImageBlobStorageService.GetPhotos();
            string stop = "";
        }
        
        public void SetUpAchievements()
        {
            AchievementsList = new List<AchievementModel>();


            AchievementsList.Add(new AchievementModel { AchievementID = 1, AchievementDescription = "Achieve Rank Legend", AchievementTitle = "AchievementLegend", AchievementSource = "AchievementLegend.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 2, AchievementDescription = "Achieve Rank Professional", AchievementTitle = "AchievementProfessional", AchievementSource = "AchievementProfessional.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 3, AchievementDescription = "Set Workout reminder", AchievementTitle = "Bell", AchievementSource = "Bell.png", AchievementAchieved = false });

            AchievementsList.Add(new AchievementModel { AchievementID = 4, AchievementDescription = "Add 10 Friends", AchievementTitle = "Crowd", AchievementSource = "Crowd.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 5, AchievementDescription = "Change your profile image", AchievementTitle = "User", AchievementSource = "User.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 6, AchievementDescription = "Jump for 20 minutes in one session", AchievementTitle = "Marketing", AchievementSource = "Marketing.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 7, AchievementDescription = "Add a friend", AchievementTitle = "Person", AchievementSource = "Person.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 8, AchievementDescription = "Burn accumulated 4000 calories", AchievementTitle = "Pizza", AchievementSource = "Pizza.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 9, AchievementDescription = "Achieve 50,000 total jumps", AchievementTitle = "Structure", AchievementSource = "Structure.png", AchievementAchieved = false });
            AchievementsList.Add(new AchievementModel { AchievementID = 10, AchievementDescription = "Accumulate total of 3H of jumping session", AchievementTitle = "Time Management", AchievementSource = "TimeManagement.png", AchievementAchieved = false });
            if (Achievements != "0")
            {
                List<string> achievedAchievements = Achievements.Split(',').ToList();

                foreach (var item in achievedAchievements)
                {
                    AchievementsList.Find(x => x.AchievementID == Convert.ToInt32(item)).AchievementAchieved = true;
                }
            }
            

            AchievementsList = AchievementsList.OrderByDescending(x => x.AchievementAchieved).ToList();
        }
        public void CreateGrid(Grid grid)
        {
            int counter = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    AchievementModel achievement = AchievementsList[counter];
                    if(achievement.AchievementAchieved == false)
                    {
                        List<string> newSource = achievement.AchievementSource.Split('.').ToList();
                        string newsource2 = newSource[0] + "Gray.png";
                        achievement.AchievementSource = newsource2;
                    }
                    
                    // Insert framed image into grid  
                    Frame fr = new Frame() { BorderColor = Color.Black, CornerRadius = 5, Padding= 1};
                    Image img = new Image() { WidthRequest = 40, HeightRequest = 40, Source =  achievement.AchievementSource};
                    fr.Content = img;
                    grid.Children.Add(fr, x, y);
                    counter++;

                }                
            }
            counter = 0;
            foreach (var child in grid.Children)
            {
                AchievementModel achievement = AchievementsList[counter];
                Frame frame = (Frame)child;
                View view = frame.Content;
                if (TooltipEffect.GetHasTooltip(view))
                {

                }
                else
                {
                    TooltipEffect.SetBackgroundColor(view, Color.Orange);
                    TooltipEffect.SetHasTooltip(view, true);
                    TooltipEffect.SetText(view, achievement.AchievementDescription);
                    if (counter >= 5)
                    {
                        TooltipEffect.SetPosition(view, TooltipPosition.Left);
                    }
                    else
                    {
                        TooltipEffect.SetPosition(view, TooltipPosition.Right);
                    }
                    counter++;
                }               
            }
        }
        public void CallbackMethod(object profileImageExtension)
        {
            if((int)profileImageExtension != 0)
            {
                ProfileImageExtension = (int)profileImageExtension;
                HasProfileImage = true;
            }
            
            //App.Current.MainPage.DisplayAlert("Closed", "Successfully Added to your favorite.", "Ok");
            
            ProfileImageSource = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/Test2"));
        }
        //private async Task OpenAddIncomePopup()
        //{
        //    var popupPage = new MyPopupPage(navigation);
        //    popupPage.CallbackEvent += (object sender, object e) => CallbackMethod(e);
        //    await navigation.PushPopupAsync(popupPage);
        //    //await navigation.PushPopupAsync(new MyPopupPage(navigation));
        //}
        async Task ChangeProfileImage()
        {
            var popupPage = new MyPopupPage(navigation);
            popupPage.CallbackEvent += (object sender, object e) => CallbackMethod(e);
            await navigation.PushPopupAsync(popupPage);
            //await OpenAddIncomePopup();
        }
        async Task UpdateUserInfo()
        {



            //Update User Details
            await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            await azureRestServ.UpdateCoreUserInfo(userInfo);
            DependencyService.Get<IMessage>().ShortAlert("Profile Updated");

            //Save Profile Image in blob storage
            //Image testimage = new Image();


            // testimage = AuthUserHelper.GetUserImage();

            //var photoList = await ProfileImageBlobStorageService.GetPhotos();
            //var firstPhoto = photoList?.FirstOrDefault();
            // testimage.Source = ImageSource.FromUri(firstPhoto?.Uri);
            // testSource = ImageSource.FromUri(firstPhoto?.Uri);
            //var something = GetImageFromUriAsync("https//platform-lookaside.fbsbx.com/platform/profilepic/?asid=10214324226841043&height=200&width=200&ext=1585402501&hash=AeSSbtexfpNNXmOG");

            //bool something = await AuthUserHelper.SaveUserImageFB(userInfo.LoginId);

            //if(something == true)
            //{
            //    publicUserInfo.HasProfileImage = true;
            //    await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            //}

            //if (idontknow == "Created")
            //{
            //    string stophere = "";
            //}

            //StreamImageSource streamImageSource = (StreamImageSource)testimage.Source;
            //System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            //Task<Stream> task = streamImageSource.Stream(cancellationToken);
            //Stream stream = task.Result;

            //byte[] TargetImageByte = ReadFully(stream);
            //await ProfileImageBlobStorageService.SavePhoto(something.Result, userInfo.LoginId);



            //DependencyService.Get<IMessage>().ShortAlert("Profile Updated");
            //Log out
            //App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);

            ////App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
            //await navigation.PopToRootAsync();
        }
       
        public void SetUpUserInfo()
        {
            List<WorkoutSession> userSessions = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(LoginId); }).Result.ToList();
                      
            //ProfileImageSource = AuthUserHelper.GetUserImage().Source;
            ProfileImageHeight = 125;
            ProfileImageWidth = 125;
            //publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(AuthUserHelper.GetUserInfo("UserID")); }).Result;
            //var photoList = ProfileImageBlobStorageService.GetPhotos().Result;
            
            //var firstPhoto = photoList?.FirstOrDefault();
            //testimage.Source = ImageSource.FromUri(firstPhoto?.Uri);
            //Image image = new Image();
            //image.Source = ImageSource.FromUri(firstPhoto?.Uri);
            //Uri deleteme = new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/TestUser");

            //testSource = ImageSource.FromUri(deleteme);

            RankProgress = RankCalculator.RankProgress(Experience);
            if (userInfo != null)
            {
                int totalJumps = 0;

                foreach (var session in userSessions)
                {
                    totalJumps += session.TotalJumps;
                }
                TotalJumps = "Total Jumps: " + totalJumps.ToString();
            }
            if(Rank != "")
            {
                RankTitle = Rank;
            }
            else
            {
                RankTitle = "Rank: " + RankCalculator.CalculateRank(Experience);
            }
            if(IsActive == true)
            {
                Active = "Active Now";
                ActiveColour = "#EA695C";
            }
            else
            {
                Active = LastActiveDateTime.ToString();
                ActiveColour = "#F7FFF6";
            }
        }      
    }
}
