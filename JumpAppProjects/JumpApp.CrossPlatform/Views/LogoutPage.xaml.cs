using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Identity.Client;
using JumpApp.Services;

namespace JumpApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutPage : ContentPage
	{
        //AuthenticationResult authenticationResult;

        public LogoutPage ()
		{
			InitializeComponent ();
            //authenticationResult = result;
        }
        protected override void OnAppearing()
        {
            //if (App.authenticationResult != null)
            //{
            //    if (App.authenticationResult.User.Name != "unknown")
            //    {
            //        messageLabel.Text = string.Format("Welcome {0}", App.authenticationResult.User.Name);
            //    }
            //    else
            //    {
            //        messageLabel.Text = string.Format("UserId: {0}", App.authenticationResult.User.UniqueId);
            //    }
            //}

            //base.OnAppearing();
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            ////await App.AuthenticationClient.RemoveAsync(authenticationResult.Account);
            // App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
            
            ////App.AuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
            //await Navigation.PopAsync();
        }
    }
}