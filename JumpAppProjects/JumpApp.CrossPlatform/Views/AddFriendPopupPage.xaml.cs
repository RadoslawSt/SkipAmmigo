using JumpApp.ViewModels;
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
	public partial class AddFriendPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
		public AddFriendPopupPage (INavigation Navigation)
		{
			InitializeComponent ();
            BindingContext = new MyPopupPageModel(Navigation, null,this);
        }
	}
}