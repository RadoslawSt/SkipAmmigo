using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using JumpApp.Features.ADB2C;
namespace JumpApp.Services
{
    public static class AuthUserHelper
    {
       //public UserContext userContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        /// <param name="info">
        /// FirstName
        /// LastName
        /// FullName
        /// Email
        /// SocialMedia
        /// UserID</param>
        /// <returns></returns>
        public static string GetUserInfo(string info)
        {
          //UserContext userContext = new UserContext();
            JObject user = ParseIdToken(info);
            string result = "";
           

            switch(info)
            {
                case "FirstName":
                    result = user["given_name"]?.ToString();
                        break;
                case "LastName":
                    result = user["family_name"]?.ToString();
                    break;
                case "FullName":
                    result = user["given_name"]?.ToString() + " " + user["family_name"]?.ToString();
                    break;
                case "Email":
                    result = user["email"]?.ToString();
                    break;
                case "SocialMedia":
                    result = user["idp"]?.ToString();
                    break;
                case "UserID":
                    result = user["sub"]?.ToString();
                    break;

                default:
                    break;
            }
            
                return result;
            
            
        }
        //public async static void SaveUserImage(string LoginID)
        //{
        //    using (var webClient = new WebClient())
        //    {
        //        byte[] bytes = null;

        //        webClient.DownloadDataCompleted +=
        //           async delegate (object sender, DownloadDataCompletedEventArgs e)
        //           {
        //               bytes = e.Result;

        //               if (bytes != null)
        //               {
        //                   await ProfileImageBlobStorageService.SavePhoto(bytes, LoginID);

        //               }
        //           };

        //        await webClient.DownloadDataTaskAsync(new Uri(urlString.Trim().Insert(5, ":")));
        //        //return "Created";                                                                                                    
        //    }
        //}
        //public async static Task<bool> SaveUserImageFB(string LoginID)
        //{
        //    var userContext = await B2CAuthenticationService.Instance.SignInAsync();
        //    JObject user = ParseIdToken(userContext.AccessToken);
        //    string result = user["picture"]?.ToString();
        //    bool isCreated = false;
        //    if (result != null)
        //    {
        //        List<string> splitResult = result.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        //        splitResult = splitResult.Select(x => x.Trim().Replace("\"", "").Replace(",", "")).ToList();
        //        foreach (string item in splitResult)
        //        {
        //            if (item.Contains(":"))
        //            {
        //                List<string> splitItem = item.Split(':').ToList();
        //                string key = splitItem[0];
        //                string value = splitItem[1];
        //                switch (splitItem[0])
        //                {                            
        //                    case "url":
        //                        string urlString = splitItem[1] + splitItem[2];
        //                        using (var webClient = new WebClient())
        //                        {                                  
        //                            byte[] bytes = null;

        //                            webClient.DownloadDataCompleted +=
        //                                delegate (object sender, DownloadDataCompletedEventArgs e)
        //                                {
        //                                    bytes = e.Result;

        //                                    if (bytes != null)
        //                                    {
        //                                         ProfileImageBlobStorageService.SavePhoto(bytes, LoginID);
        //                                        //await dataStore.UpdatePublicUserInfo(updatePublicUserInfo);
        //                                        isCreated = true;
        //                                    }
        //                                };

        //                            await webClient.DownloadDataTaskAsync(new Uri(urlString.Trim().Insert(5, ":")));
                                                                                                                                       
        //                        }
        //                        break;

        //                    default:
        //                        break;
        //                }
        //            }
                    
        //        }
        //    }
        //    return isCreated;
        //    //return "Missing";
        //    //return imgProfile;
        //}

        //private static void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //public static Image GetUserImage()
        //{
        //    Image imgProfile = new Image();
        //    var userContext =  B2CAuthenticationService.Instance.SignInAsync();
        //    JObject user = ParseIdToken(userContext.Result.AccessToken);
        //    string result = user["picture"]?.ToString();
        //    //result = result.Split(new string[] { @"\r\n" }, StringSplitOptions.None).ToList();
        //   if(result != null)
        //    {
        //        List<string> splitResult = result.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        //        splitResult = splitResult.Select(x => x.Trim().Replace("\"", "").Replace(",", "")).ToList();
        //        foreach (string item in splitResult)
        //        {
        //            if (item.Contains(":"))
        //            {
        //                List<string> splitItem = item.Split(':').ToList();
        //                string key = splitItem[0];
        //                string value = splitItem[1];
        //                switch (splitItem[0])
        //                {
        //                    case "height":
        //                        imgProfile.HeightRequest = Convert.ToInt32(value);
        //                        break;

        //                    case "width":
        //                        imgProfile.WidthRequest = Convert.ToInt32(value);
        //                        break;

        //                    case "url":
        //                        string urlString = splitItem[1] + splitItem[2];

        //                        imgProfile.Source = ImageSource.FromUri(new Uri(urlString.Trim().Insert(5, ":")));

        //                        break;

        //                    default:
        //                        break;
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    else
        //    {
        //        imgProfile.Source = ImageSource.FromFile("PlaceholderImage.jpg");
        //        imgProfile.WidthRequest = 200;
        //        imgProfile.HeightRequest = 200;
        //    }
            
        //    return imgProfile;
        //}
       private static JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }
        private static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
