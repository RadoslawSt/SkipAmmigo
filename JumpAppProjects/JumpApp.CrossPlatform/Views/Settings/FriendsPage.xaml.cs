using JumpApp.ViewModels;
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
	public partial class FriendsPage : ContentPage
	{
		public FriendsPage ()
		{
			InitializeComponent ();
            BindingContext = new FriendsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}