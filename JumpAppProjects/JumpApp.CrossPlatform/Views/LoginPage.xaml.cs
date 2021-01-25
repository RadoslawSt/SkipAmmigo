using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Identity.Client;
using JumpApp.Services;
using System.Threading.Tasks;
using System.Linq;
using JumpApp.Models;
using JumpApp.Helpers;
using Acr.UserDialogs;
using System.ComponentModel;
using JumpApp.Features.ADB2C;
using System.Collections.ObjectModel;
using CarouselView.FormsPlugin.Abstractions;
using System.Collections.Generic;

namespace JumpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]

    public partial class LoginPage : ContentPage
    {

        //UserInfoRepository userRepo = new UserInfoRepository();
        private readonly IAzureRestService dataStore;
        //public IDbSync dbSync = new DbSync();
        private int slidePosition = 0;
        //ObservableCollection<string> myItemsSource = new ObservableCollection<string>();
        public LoginPage()
        {
           
                InitializeComponent();
                dataStore = DependencyService.Get<IAzureRestService>();
                ScrollTimer();
                //myItemsSource.Add("test");

            

        }


        protected override async void OnAppearing()
        {

            try
            {

                await B2CAuthenticationService.Instance.AcquireTokenSilent();
               
                
                    await Navigation.PushAsync(new MainPage());
                
                //await B2CAuthenticationService.Instance.SignInAsync();
                //UpdateSignInState(App.userContext);
                //UpdateUserInfo(App.userContext);
                //testc(App.userContext);
                //AuthenticationResult ar = await App.PCA.AcquireTokenSilent(B2CConstants.Scopes,
                //                                                   GetUserByPolicy(App.PCA.Users, B2CConstants.PolicySignUpSignIn))
                //                            .WithAuthority(B2CConstants.PolicySignUpSignIn)
                //                            .ExecuteAsync();
                //  await B2CAuthenticationService.Instance.SignInAsync();
                // await Navigation.PushAsync(new MainPage());

            }
            catch (Exception ex)
            {
                var strop = "";
                // Do nothing, the user isn't logged in
               
            }

        }
        
        async void OnSignInSignOut(object sender, EventArgs e)
        {

            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {
                try
                {
                    if (btnLogin.Text == "Sign In")
                    {

                        await B2CAuthenticationService.Instance.SignInAsync();
                        UpdateSignInState(App.userContext);
                        UpdateUserInfo(App.userContext);
                        using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                        {
                            await testc(App.userContext);
                        }
                        //await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await B2CAuthenticationService.Instance.SignOutAsync();
                        UpdateSignInState(App.userContext);
                        UpdateUserInfo(App.userContext);
                    }
                }
                catch (Exception ex)
                {
                    // Checking the exception message 
                    // should ONLY be done for B2C
                    // reset and not any other error.
                    if (ex.Message.Contains("AADB2C90118"))
                        OnPasswordReset();
                    // Alert if any exception excluding user canceling sign-in dialog
                    else if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                        await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
                }
            }
        }

        public async Task testc(UserContext userContext)
        {

            var azureUser = new CoreUserInfo();

            try
            {
                azureUser = await dataStore.GetCoreUserInfo(userContext.UserIdentifier);

                if (azureUser.CoreUserId == 0)
                {
                    await Navigation.PushAsync(new AddUserInfo());
                }
                else if (azureUser.Sensetivity == 0)
                {
                    await Navigation.PushAsync(new ShakeSetup());
                }
                else
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                string text = ex.ToString();

            }

        }
        //async void OnCallApi(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblApi.Text = $"Calling API {App.ApiEndpoint}";
        //        var userContext = await B2CAuthenticationService.Instance.SignInAsync();
        //        var token = userContext.AccessToken;

        //        // Get data from API
        //        HttpClient client = new HttpClient();
        //        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, App.ApiEndpoint);
        //        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage response = await client.SendAsync(message);
        //        string responseString = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            lblApi.Text = $"Response from API {App.ApiEndpoint} | {responseString}";
        //        }
        //        else
        //        {
        //            lblApi.Text = $"Error calling API {App.ApiEndpoint} | {responseString}";
        //        }
        //    }
        //    catch (MsalUiRequiredException ex)
        //    {
        //        await DisplayAlert($"Session has expired, please sign out and back in.", ex.ToString(), "Dismiss");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}

        async void OnEditProfile(object sender, EventArgs e)
        {
            try
            {
                await B2CAuthenticationService.Instance.EditProfileAsync();
                UpdateSignInState(App.userContext);
                UpdateUserInfo(App.userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        async void OnResetPassword(object sender, EventArgs e)
        {
            try
            {
                await B2CAuthenticationService.Instance.ResetPasswordAsync();
                UpdateSignInState(App.userContext);
                UpdateUserInfo(App.userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        async void OnPasswordReset()
        {
            try
            {
                await B2CAuthenticationService.Instance.ResetPasswordAsync();
                UpdateSignInState(App.userContext);
                UpdateUserInfo(App.userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

        void UpdateSignInState(UserContext userContext)
        {
            var isSignedIn = userContext.IsLoggedOn;
            //btnLogin.Text = isSignedIn ? "Sign out" : "Sign in";
            //btnLogin.IsVisible = isSignedIn;
            //btnCallApi.IsVisible = isSignedIn;
            //slUser.IsVisible = isSignedIn;
            //lblApi.Text = "";
        }
        public void UpdateUserInfo(UserContext userContext)
        {
            //lblName.Text = userContext.Name;
            //lblJob.Text = userContext.JobTitle;
            //lblCity.Text = userContext.City;
        }
        private void ScrollTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                slidePosition++;
                if (slidePosition == 6) slidePosition = 0;
                carousel.Position = slidePosition;
                return true;
            });
        }




        private void Carousel_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {

            //slidePosition = (int)e.NewValue;
            //var check = e.Direction;
            //var check2 = e.NewValue;
            //string stop = "";
        }
    }
}