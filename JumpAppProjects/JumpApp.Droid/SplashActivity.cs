using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace JumpApp.Droid
{
    [Activity(Label = "SkipAmmigo", Icon = "@drawable/Icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            //SetContentView(Resource.Layout.SplashLayout);
           

            //Log.Debug(TAG, "SplashActivity.OnCreate");
        }
        //private void LoadActivity()
        //{
        //    //System.Threading.Thread.Sleep(5000); // Simulate a long pause    
        //    RunOnUiThread(() => StartActivity(typeof(MainActivity)));
        //}
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
       
    }
}