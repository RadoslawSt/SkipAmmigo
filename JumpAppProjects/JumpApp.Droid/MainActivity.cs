using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Android.Webkit;
using JumpApp.Services;
using JumpApp.ViewModels;
using Android.Content;
using Android.Runtime;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using FFImageLoading.Transformations;
//using FFImageLoading.Forms.Droid;
using FFImageLoading.Forms.Platform;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using JumpApp.Features.ADB2C;
using Microsoft.Identity.Client;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;

namespace JumpApp.Droid
{
    [Activity(Label = "SkipAmmigo", Icon = "@drawable/Icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //MobileServiceUser user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            this.Window.AddFlags(WindowManagerFlags.Fullscreen | WindowManagerFlags.TurnScreenOn);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var stBarHeight = typeof(FormsAppCompatActivity).GetField("statusBarHeight", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (stBarHeight == null)
                {
                    stBarHeight = typeof(FormsAppCompatActivity).GetField("_statusBarHeight", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                }
                stBarHeight?.SetValue(this, 0);
            }

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
         
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            DependencyService.Register<IParentWindowLocatorService, AndroidParentWindowLocatorService>();
            //CachedImageRenderer.Init(enableFastRenderer: false);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            //CachedImageRenderer.InitImageViewHandler();
            var ignore1 = typeof(CircleTransformation);
           // Stormlion.ImageCropper.Droid.Platform.Init();
            //App.AuthenticationClient.PlatformParameters = new PlatformParameters(this);
            UserDialogs.Init(this);
           
           // CachedImageRenderer.Init(true);
            LoadApplication(new App());
            //App.AuthenticationClient.PlatformParameters = new PlatformParameters(this);
        }
       
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

    }
}

