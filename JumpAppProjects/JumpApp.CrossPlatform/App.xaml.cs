using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JumpApp.Services;
using JumpApp.Views;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using JumpApp.Features.ADB2C;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace JumpApp
{
    public partial class App : Application
    {

        //public static IPublicClientApplication PCA = B2CAuthenticationService.Instance._pca;
       

        public static UserContext userContext { get; set; }

        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<IAzureRestService, AzureRestService>();
            DependencyService.Register<IRepositoryWrapper, RepositoryWrapper>();
            DependencyService.Register<B2CAuthenticationService>();           
          
            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental"
            });
            // var userContext =  B2CAuthenticationService.Instance.SignInAsync();
            // var isSignedIn = userContext.Result.IsLoggedOn;
            // if(isSignedIn == true)
            // {
            //    MainPage = new NavigationPage(new MainPage());
            // }
            //else
            //{
            //var huh = B2CAuthenticationService.Instance.
            
                MainPage = new NavigationPage(new SplashPage());
           // }
            

            //var isSignedIn = userContext.IsLoggedOn;
            //btnLogin.Text = isSignedIn ? "Sign out" : "Sign in";
            //btnLogin.IsVisible = isSignedIn;

            //MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //await B2CAuthenticationService.Instance.SignInAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
