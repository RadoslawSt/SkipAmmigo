using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JumpApp.Controls
{
   public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root):base(root)
        {
            BarBackgroundColor = Color.DarkBlue;
            BarTextColor = Color.Black;
        }
    }
}
