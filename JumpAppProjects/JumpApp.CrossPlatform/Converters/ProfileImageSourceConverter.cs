using JumpApp.Models;
using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Converters
{
    public class ProfileImageSourceConverter : IValueConverter
    {
        public UserInfo publicUserInfo { get; set; }
      
        private IAzureRestService azureRestServ = new AzureRestService();
      
        public ProfileImageSourceConverter()
        {
            publicUserInfo = new UserInfo();
            azureRestServ = DependencyService.Get<IAzureRestService>();
    
        }
       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Label lblProfileExtension = (Label)parameter;
            string profileExtension = "";
            string hasProfileImage = "";

            if (lblProfileExtension.Text == null || lblProfileExtension.Text == "")
            {
                if(lblProfileExtension.FormattedText != null)
                {
                    profileExtension = lblProfileExtension.FormattedText.Spans[0].Text;
                    hasProfileImage = lblProfileExtension.FormattedText.Spans[2].Text;
                }
               
            }
            else
            {
                var testSplit = lblProfileExtension.Text.Split('|');
                profileExtension = testSplit[0];
                hasProfileImage = testSplit[1];
            }
            
            
            // repetitionCheck = publicUserInfo.ProfileImageExtension;
            //if(repetitionCheck != publicUserInfo.ProfileImageExtension)
            //{
      
            //}
        
            if (hasProfileImage == "True")
            {
                return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + value.ToString() + profileExtension));
            }
            else
            {
                return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/PlaceholderImage"));
            }
                //return !string.IsNullOrEmpty($"{value}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
