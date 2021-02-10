using JumpApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        //UserInfoRepository repo = new UserInfoRepository();

        public EditProfile(string profile, string identification)
        {
            InitializeComponent();
            //this.FindByName("grid");
            //Button btn = new Button();
            //btn.Text = "Hello";
            //btn.BackgroundColor = Color.Red;
            //grid.Children.Add(btn);

            BindingContext = new EditUserInfoModel(Navigation, grid,profile, identification);
            //repoWrapper = DependencyService.Get<IRepositoryWrapper>();
            //azureRestServ = DependencyService.Get<IAzureRestService>();
            //SetUpUserInfo();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Handle_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Alert", "You have been alerted", "OK");
            string stop = "";
            //foreach (var c in scrollView.Children)
            // {
            // if (TooltipEffect.GetHasTooltip(c))
            // {
            //  TooltipEffect.SetHasTooltip(false);
            // TooltipEffect.SetHasTooltip(c, true);
            //}
            // }
        }

        //private void SetUpUserInfo()
        //{
        //    var userInfo = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(AuthUserHelper.GetUserInfo("UserID")); }).Result;
        //    var publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(AuthUserHelper.GetUserInfo("UserID")); }).Result;
        //    List<WorkoutSession> userSessions = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(userInfo.LoginId); }).Result.ToList();

        //    Image imgProfileInfo = AuthUserHelper.GetUserImage();

        //    if (imgProfileInfo != null)
        //    {
        //        imgProfile.HeightRequest = 150;
        //        imgProfile.WidthRequest = 150;
        //        imgProfile.Source = imgProfileInfo.Source;
        //    }

        //    if (userInfo != null)
        //    {
        //        int totalJumps = 0;

        //        foreach(var session in userSessions)
        //        {
        //            totalJumps += session.TotalJumps;
        //        }
        //        lblTotalJumps.Text = "Total Jumps: " + totalJumps.ToString();
        //    }

        //    if(publicUserInfo != null)
        //    {
        //        lblName.Text = publicUserInfo.UserName;

        //        if(publicUserInfo.Rank != "")
        //        {
        //        }
        //        else
        //        {
        //            lblRank.Text = "Rank: " + RankCalculator.CalculateRank(publicUserInfo.Experience);
        //        }

        //        if(publicUserInfo.IsActive == true)
        //        {
        //            lblLastActive.Text = "Active Now";
        //            lblLastActive.TextColor = Color.Green;
        //        }
        //        else
        //        {
        //            lblLastActive.Text = publicUserInfo.LastActiveDateTime.ToString();
        //        }
        //    }
        //}

        //private async void Button_Pressed(object sender, EventArgs e)
        //{
        //    //await App.AuthenticationClient.RemoveAsync(authenticationResult.Account);
        //    App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);

        //    //App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
        //    await Navigation.PopToRootAsync();
        //}
    }
}