using JumpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using JumpApp.ViewModels;
using System.Collections;

namespace JumpApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostWorkoutPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public delegate void CallbackDelegate(IList list);
        public CallbackDelegate CallbackMethod { get; set; }
        public bool DisplayWorkoutStats = false;

        public PostWorkoutPopupPage (UserInfo User, float Experience)
		{
			InitializeComponent ();
            lblExperience.Opacity = 0;
            lblExperience.Text = "+"+ Experience.ToString();
            Task.Run(async () => {await FadeIns(); });
            //Task.Run(async () => { await lblExperience.FadeTo(0, 2000); });
            //lblExperience.FadeTo(0, 1000);
            //profileImage.Source = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + User.LoginId + User.ProfileImageExtension.ToString()));
            BindingContext = new PostWorkoutPopupPageViewModel(User,Experience);
        }
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, DisplayWorkoutStats);
        public async Task FadeIns()
        {
           await lblExperience.FadeTo(1, 1000);
           await lblExperience.FadeTo(0, 1000);
        }

        private void BtnWorkoutStats_Clicked(object sender, EventArgs e)
        {
            DisplayWorkoutStats = true;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}