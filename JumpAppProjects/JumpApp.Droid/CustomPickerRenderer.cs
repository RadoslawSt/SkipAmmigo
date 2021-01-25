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
using JumpApp.Controls;
using JumpApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace JumpApp.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control?.SetPadding(20, 20, 20, 20);
            if (e.OldElement != null || e.NewElement != null)
            {
                var customPicker = e.NewElement as CustomPicker;
                

                Android.Graphics.Color phCol = customPicker.PlaceholderColour.ToAndroid();
                Android.Graphics.Color textCol = customPicker.TextColour.ToAndroid();
                Android.Graphics.Color bgCol = customPicker.BackgroundColour.ToAndroid();
               
                
                Control.SetBackgroundColor(bgCol);
                Control.SetHintTextColor(phCol);
                Control.SetTextColor(textCol);
               
            }
        }
    }
}