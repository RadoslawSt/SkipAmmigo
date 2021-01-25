using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace JumpApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Label = "SplashActivity2", MainLauncher = false, NoHistory = true)]
    public class SplashActivity2 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashLayout);
            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
            // Create your application here
        }
        private void LoadActivity()
        {
           // System.Threading.Thread.Sleep(5000); // Simulate a long pause    
            RunOnUiThread(() => StartActivity(typeof(MainActivity)));
        }
    }
}