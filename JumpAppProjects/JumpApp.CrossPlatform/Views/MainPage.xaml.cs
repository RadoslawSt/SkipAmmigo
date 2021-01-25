using System;
using JumpApp.Views;
using JumpApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using JumpApp.Views.Home;
using JumpApp.Views.MyWorkouts;
using JumpApp.Views.Reminders;
using JumpApp.Views.Settings;
using JumpApp.Views.Share;
using JumpApp.Views.UserWorkouts;
using JumpApp.Views.Workout;
using Microsoft.Identity.Client;
using JumpApp.Services;
using Acr.UserDialogs;
using System.Threading.Tasks;
using JumpApp.Controls;
using JumpApp.Models;
using Xamvvm;
using System.Linq;

namespace JumpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage,IBasePage<MyPopupPageModel>
    {
        public List<MasterPageItem> menuList { get; set; }
        public UserInfo publicUserInfo { get; set; }
        public CoreUserInfo userInfo { get; set; }
        public double RankProgress { get; set; }
        public string RankTitle { get; set; }
        //public string UserName { get; set; }
        private IAzureRestService azureRestServ = new AzureRestService();
        //private int profileImageExtension = 0;
        //AuthenticationResult authenticationResult;

        public MainPage()
        {
            
                InitializeComponent();
                //authenticationResult = App.authenticationResult;
                publicUserInfo = new UserInfo();
                userInfo = new CoreUserInfo();
                azureRestServ = DependencyService.Get<IAzureRestService>();
                publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
                //publicUserInfo.LastActiveDateTime = DateTime.Now;
                //publicUserInfo.IsActive = App.userContext.IsLoggedOn;
                SetUpPageElements();
                SetUpUserInfo();
            //comment
        }

        protected override async void OnAppearing()
        {
            try
            {
                // Look for existing user
               // var result = await App.AuthenticationClient.AcquireTokenSilentAsync(Constants.Scopes);
                
                //userInfo = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(AuthUserHelper.GetUserInfo("UserID")); }).Result;
                // await Navigation.PushAsync(new MainPage(result));
                if (publicUserInfo.HasProfileImage == true)
                {
                    //imgProfile.HeightRequest = 140;
                   // imgProfile.WidthRequest = 140;
                    //imgProfile.Source = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + publicUserInfo.LoginId + publicUserInfo.ProfileImageExtension.ToString()));
                   // profileImageExtension = publicUserInfo.ProfileImageExtension;
                }
                else
                {
                   // imgProfile.Source = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/PlaceholderImage"));
                   // imgProfile.HeightRequest = 140;
                   // imgProfile.WidthRequest = 140;
                }
            }
            catch
            {
                // Do nothing - the user isn't logged in
                await Navigation.PopToRootAsync();
            }
            base.OnAppearing();
        }
        public void SetUpUserInfo()
        {
           // List<WorkoutSession> userSessions = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(publicUserInfo.LoginId); }).Result.ToList();
           //profileImageProgress.Progress = (float)RankCalculator.RankProgress(publicUserInfo.Experience);
            //RankProgress = 
            
            if (publicUserInfo.Rank != "")
            {
                RankTitle = publicUserInfo.Rank;
            }
            else
            {
                RankTitle = "Rank: " + RankCalculator.CalculateRank(publicUserInfo.Experience);
            }
           
           // lblName.Text = publicUserInfo.UserName;
           //lblEmail.Text = AuthUserHelper.GetUserInfo("Email");
        }
        public void SetUpPageElements()
        {
            //publicUserInfo =  await azureRestServ.GetPublicUserInfo(AuthUserHelper.GetUserInfo("UserID"));
            menuList = new List<MasterPageItem>();
            //Image imgProfileInfo = AuthUserHelper.GetUserImage();
            
            
            //Fot Android / IOS icons
            //var page1 = new MasterPageItem() { id = 1, Title = "Home", Icon = "Home.png" };
            var page2 = new MasterPageItem() { id = 2, Title = "Workout", Icon = "SkippingRope.png" };
            var page3 = new MasterPageItem() { id = 3, Title = "My Workouts", Icon = "History.png" };
            //var page4 = new MasterPageItem() { id = 4, Title = "User Workouts", Icon = "ProfileSetting.png" };
            var page5 = new MasterPageItem() { id = 5, Title = "Settings", Icon = "Cog.png" };
            //var page6 = new MasterPageItem() { id = 6, Title = "Reminders", Icon = "ProfileSetting.png" };
            //var page7 = new MasterPageItem() { id = 7, Title = "Share", Icon = "ProfileSetting.png" };

            //menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page3);
            //menuList.Add(page4);
            menuList.Add(page5);
            //menuList.Add(page6);
            //menuList.Add(page7);



            navigationDrawerList.ItemsSource = menuList;

            Detail =  new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            //Detail = new NavigationPage(new HomePage());
        }
        //async private void Loader(Page page)
        //{
        //    using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
        //    {
        //        await Navigation.PushAsync(page)
        //    }
        //}
        async private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {

                var myselecteditem = e.Item as MasterPageItem;
                
                switch (myselecteditem.id)
                {

                   // case 1:
                        //await Navigation.PushAsync(new HomePage());
                        

                        //break;
                    case 2:
                        await Navigation.PushAsync(new WorkoutPage());

                        break;
                    case 3:
                        await Navigation.PushAsync(new MyWorkoutsPage());

                        break;
                    case 4:
                        await Navigation.PushAsync(new UserWorkoutsPage());

                        break;
                    case 5:
                        await Navigation.PushAsync(new SettingsPage());
                        
                        break;
                    case 6:
                        await Navigation.PushAsync(new RemindersPage());

                        break;
                    case 7:
                        await Navigation.PushAsync(new SharePage());

                        break;

                }
            }
            ((ListView)sender).SelectedItem = null;
            IsPresented = false;


        }
    }
}