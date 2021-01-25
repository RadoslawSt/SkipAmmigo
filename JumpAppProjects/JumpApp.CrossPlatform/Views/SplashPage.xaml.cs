using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScaleIcon();
        }
        private async void ScaleIcon()
        {
            // wait until the UI is present
            await Task.Delay(300);

            // animate the splash logo
            await SplashIcon.ScaleTo(0.5, 500, Easing.CubicInOut);
            var animationTasks = new[]{
                SplashIcon.ScaleTo(100.0, 1000, Easing.CubicInOut),
                SplashIcon.FadeTo(0, 700, Easing.CubicInOut)
            };
            await Task.WhenAll(animationTasks);

            //// navigate to main page
            using (UserDialogs.Instance.Loading("Getting few things ready...", null, null, true, MaskType.Black))
            {
                Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync(false);
            }
        }
    }
}