using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JumpApp.Controls
{
    public class CustomPicker : Picker
    {
        public Color PlaceholderColour
        {
            get { return (Xamarin.Forms.Color)App.Current.Resources["MintCream"]; }
        }

        public Color TextColour
        {
            get { return (Color)App.Current.Resources["MintCream"]; }
        }

        public Color BackgroundColour
        {
            get { return (Color)App.Current.Resources["BackgroundLight"]; }
        }
    }
}
