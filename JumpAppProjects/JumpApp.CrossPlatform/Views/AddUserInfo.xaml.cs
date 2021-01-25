using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.ViewModels;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddUserInfo : ContentPage
	{
        //AuthenticationResult authenticationResult;
        public AddUserInfo ()
		{
			InitializeComponent ();
            //authenticationResult = result;
            BindingContext = new AddUserInfoModel(Navigation);
		}
      
    }
}