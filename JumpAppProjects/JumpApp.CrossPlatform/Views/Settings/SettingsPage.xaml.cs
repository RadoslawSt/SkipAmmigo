using Acr.UserDialogs;
using JumpApp.Controls;
using JumpApp.Features.ADB2C;
using JumpApp.Services;
using JumpApp.ViewModels;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public List<AccountSettingsPageItem> accountMenuList { get; set; }
        public List<AccountSettingsPageItem> supportMenuList { get; set; }
        //AuthenticationResult authenticationResult;
        //UserInfoRepository repo = new UserInfoRepository();
        //private readonly IRepositoryWrapper repoWrapper;
        //private readonly IAzureRestService azureRestServ;
        public SettingsPage()
        {
            InitializeComponent();
            SetUpPageElements();
            // authenticationResult = result;
            //repoWrapper = RepoWrapper;
            // repoWrapper = DependencyService.Get<IRepositoryWrapper>();
            // azureRestServ = DependencyService.Get<IAzureRestService>();
            //var user = repo.GetAllUserInfo().FirstOrDefault();
            //SetUpUserInfo();

        }
        public void SetUpPageElements()
        {
            accountMenuList = new List<AccountSettingsPageItem>();
            supportMenuList = new List<AccountSettingsPageItem>();

            var setting1 = new AccountSettingsPageItem() { id = 1, Title = "Edit Profile", IconStart = "EditUser.png", IconEnd = "Next.png" };
            var setting2 = new AccountSettingsPageItem() { id = 2, Title = "Adjust Calibration", IconStart = "Sliders.png", IconEnd = "Next.png" };
            var setting3 = new AccountSettingsPageItem() { id = 3, Title = "Link Account", IconStart = "Configuration.png", IconEnd = "Next.png" };

            accountMenuList.Add(setting1);
            accountMenuList.Add(setting2);
            accountMenuList.Add(setting3);

            lvAccount.ItemsSource = accountMenuList;

            //var support1 = new AccountSettingsPageItem() { id = 1, Title = "Help", IconStart = "Home.png", IconEnd = "Next.png" };
            //var support2 = new AccountSettingsPageItem() { id = 2, Title = "Terms of Service", IconStart = "About.png", IconEnd = "Next.png" };
            //var support3 = new AccountSettingsPageItem() { id = 3, Title = "Burned Calorie Food Equivalent", IconStart = "Configuration.png", IconEnd = "Next.png" };

            //supportMenuList.Add(support1);
            //supportMenuList.Add(support2);
            //supportMenuList.Add(support3);

            //lvSupport.ItemsSource = supportMenuList;
        }

        async private void LvItem_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {
                var myselecteditem = e.Item as AccountSettingsPageItem;

                switch (myselecteditem.Title)
                {

                    case "Edit Profile":
                        
                            await Navigation.PushAsync(new EditProfile());
                            //await Navigation.PushAsync(new CustomNavigationPage((Page)Activator.CreateInstance(typeof(EditProfile))));
                        
                        break;
                    case "Adjust Calibration":
                        await Navigation.PushAsync(new ShakeSetup());

                        break;
                    case "Link Account":
                        await Navigation.PushAsync(new LinkAccount());
                        break;

                    case "Help":
                        await Navigation.PushAsync(new HelpPage());

                        break;
                    case "Terms of Service":
                        await Navigation.PushAsync(new TermsOfServicePage());

                        break;
                    case "Burned Calorie Food Equivalent":
                        await Navigation.PushAsync(new BCFEPage());

                        break;
                    default:
                        break;

                }
                 ((ListView)sender).SelectedItem = null;
                // IsPresented = false;
            }
        }

        private async void LogoutButton_ClickedAsync(object sender, EventArgs e)
        {
            //App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
            await B2CAuthenticationService.Instance.SignOutAsync();
            await Navigation.PopToRootAsync();
        }


        //private void SetUpUserInfo()
        //{
        //    var user = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(AuthUserHelper.GetUserInfo("UserID")); }).Result;
        //    Image imgProfileInfo = AuthUserHelper.GetUserImage();

        //    //azureRestServ.AddWorkoutSession(repoWrapper.WorkoutSession.GetWorkoutSessions());

        //    lblName.Text = AuthUserHelper.GetUserInfo("FullName");
        //    lblEmail.Text = AuthUserHelper.GetUserInfo("Email");
        //    lblSocialMedia.Text = AuthUserHelper.GetUserInfo("SocialMedia");
        //    lblId.Text = AuthUserHelper.GetUserInfo("UserID");

        //    if (user != null)
        //    {
        //        lblDOB.Text = user.Dob.Date.ToString();
        //        lblGender.Text = user.Gender;
        //        lblHeight.Text = user.Height.ToString();
        //        lblWeight.Text = user.Weight.ToString();
        //        lblSensitivity.Text = user.Sensetivity.ToString();
        //    }
        //    if (imgProfileInfo != null)
        //    {
        //        imgProfile.HeightRequest = imgProfileInfo.HeightRequest;
        //        imgProfile.WidthRequest = imgProfileInfo.WidthRequest;
        //        imgProfile.Source = imgProfileInfo.Source;
        //    }
        //}
        //protected override void OnAppearing()
        //{
        //    if (App.authenticationResult != null)
        //    {
        //        if (App.authenticationResult.User.Name != "unknown")
        //        {
        //            //messageLabel.Text = string.Format("Welcome {0}", authenticationResult.User.Name);
        //        }
        //        else
        //        {
        //            //messageLabel.Text = string.Format("UserId: {0}", authenticationResult.User.UniqueId);
        //        }
        //    }

        //    base.OnAppearing();
        //}
        ////public void UpdateUserInfo(AuthenticationResult ar)
        ////{
        ////    JObject user = ParseIdToken(ar.IdToken);
        ////    lblName.Text = user["given_name"]?.ToString() + " " + user["family_name"]?.ToString();
        ////    lblEmail.Text = user["emails"]?.ToString();
        ////    lblSocialMedia.Text = user["idp"]?.ToString();
        ////    lblId.Text = user["sub"]?.ToString();
        ////}
        ////JObject ParseIdToken(string idToken)
        ////{
        ////    // Get the piece with actual user info
        ////    idToken = idToken.Split('.')[1];
        ////    idToken = Base64UrlDecode(idToken);
        ////    return JObject.Parse(idToken);
        ////}
        ////private string Base64UrlDecode(string s)
        ////{
        ////    s = s.Replace('-', '+').Replace('_', '/');
        ////    s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
        ////    var byteArray = Convert.FromBase64String(s);
        ////    var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
        ////    return decoded;
        ////}
        //async void OnLogoutButtonClicked(object sender, EventArgs e)
        //{
        //    //await App.AuthenticationClient.RemoveAsync(authenticationResult.Account);
        //    App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);

        //    //App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
        //    await Navigation.PopToRootAsync();
        //}

        //private void BtnDeleteUserInfo_Clicked(object sender, EventArgs e)
        //{
        //    var users = repo.GetAllUserInfo();
        //    foreach (var user in users)
        //    {
        //        repo.DeleteUserInfo(user);
        //    }
        //}
    }
}