using JumpApp.Services;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JumpApp.Features.ADB2C
{
    public class B2CAuthenticationService
    {
        private readonly IPublicClientApplication _pca;


        private static readonly Lazy<B2CAuthenticationService> lazy = new Lazy<B2CAuthenticationService>
           (() => new B2CAuthenticationService());

        public static B2CAuthenticationService Instance { get { return lazy.Value; } }


        private B2CAuthenticationService()
        {

            // default redirectURI; each platform specific project will have to override it with its own
            var builder = PublicClientApplicationBuilder.Create(B2CConstants.ClientID)
                .WithB2CAuthority(B2CConstants.AuthoritySignInSignUp)

                .WithIosKeychainSecurityGroup(B2CConstants.IOSKeyChainGroup)
                .WithRedirectUri($"msal{B2CConstants.ClientID}://auth");

            // Android implementation is based on https://github.com/jamesmontemagno/CurrentActivityPlugin
            // iOS implementation would require to expose the current ViewControler - not currently implemented as it is not required
            // UWP does not require this
            var windowLocatorService = DependencyService.Get<IParentWindowLocatorService>();

            if (windowLocatorService != null)
            {
                builder = builder.WithParentActivityOrWindow(() => windowLocatorService?.GetCurrentParentWindow());
            }

            _pca = builder.Build();
        }

        public async Task SignInAsync()
        {
            //UserContext newContext;
            try
            {
                // acquire token silent
                 await AcquireTokenSilent();
            }
            catch (MsalUiRequiredException)
            {
                // acquire token interactive
                 await SignInInteractively();
            }
            //return App.userContext;
        }

        public async Task AcquireTokenSilent()
        {
           
                IEnumerable<IAccount> accounts = await _pca.GetAccountsAsync();
                AuthenticationResult authResult = await _pca.AcquireTokenSilent(B2CConstants.Scopes, GetAccountByPolicy(accounts, B2CConstants.PolicySignUpSignIn))
                   .WithB2CAuthority(B2CConstants.AuthoritySignInSignUp)
                   .ExecuteAsync();

                App.userContext = UpdateUserInfo(authResult);
                //return App.userContext;
            }
        

        public async Task ResetPasswordAsync()
        {
            AuthenticationResult authResult = await _pca.AcquireTokenInteractive(B2CConstants.Scopes)
                .WithPrompt(Prompt.NoPrompt)
                .WithAuthority(B2CConstants.AuthorityPasswordReset)
                .ExecuteAsync();

             App.userContext = UpdateUserInfo(authResult);

           
        }

        public async Task EditProfileAsync()
        {
            IEnumerable<IAccount> accounts = await _pca.GetAccountsAsync();

            AuthenticationResult authResult = await _pca.AcquireTokenInteractive(B2CConstants.Scopes)
                .WithAccount(GetAccountByPolicy(accounts, B2CConstants.PolicyEditProfile))
                .WithPrompt(Prompt.NoPrompt)
                .WithAuthority(B2CConstants.AuthorityEditProfile)
                .ExecuteAsync();

             App.userContext = UpdateUserInfo(authResult);

            
        }

        private async Task SignInInteractively()
        {
            IEnumerable<IAccount> accounts = await _pca.GetAccountsAsync();

            AuthenticationResult authResult = await _pca.AcquireTokenInteractive(B2CConstants.Scopes)
                .WithUseEmbeddedWebView(true)
                .WithAccount(GetAccountByPolicy(accounts, B2CConstants.PolicySignUpSignIn))
                .ExecuteAsync();

             App.userContext = UpdateUserInfo(authResult);
            //return newContext;
        }

        public async Task SignOutAsync()
        {

            IEnumerable<IAccount> accounts = await _pca.GetAccountsAsync();
            while (accounts.Any())
            {
                await _pca.RemoveAsync(accounts.FirstOrDefault());
                accounts = await _pca.GetAccountsAsync();
            }
            App.userContext = new UserContext();
            App.userContext.IsLoggedOn = false;
            
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public UserContext UpdateUserInfo(AuthenticationResult ar)
        {
            var newContext = new UserContext();
            newContext.IsLoggedOn = false;
            JObject user = ParseIdToken(ar.IdToken);

            newContext.AccessToken = ar.AccessToken;
            newContext.Name = user["name"]?.ToString();
            newContext.UserIdentifier = user["sub"]?.ToString();

            newContext.GivenName = user["given_name"]?.ToString();
            newContext.FamilyName = user["family_name"]?.ToString();

            newContext.StreetAddress = user["streetAddress"]?.ToString();
            newContext.City = user["city"]?.ToString();
            newContext.Province = user["state"]?.ToString();
            newContext.PostalCode = user["postalCode"]?.ToString();
            newContext.Country = user["country"]?.ToString();

            newContext.JobTitle = user["jobTitle"]?.ToString();
            newContext.Picture = user["picture"]?.ToString();

            var emails = user["email"] as JArray;
            if (emails != null)
            {
                newContext.EmailAddress = emails[0].ToString();
            }
            newContext.IsLoggedOn = true;

            return newContext;
        }

        JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }
        //public static Image GetUserImage()
        //{
        //    Image imgProfile = new Image();
        //    //var userContext = B2CAuthenticationService.Instance.SignInAsync();
        //    //JObject user = ParseIdToken(userContext.Result.AccessToken);
        //    //string result = user["picture"]?.ToString();
        //    //result = result.Split(new string[] { @"\r\n" }, StringSplitOptions.None).ToList();
        //    string pictureString = App.userContext.Picture;
        //    if (pictureString != null)
        //    {
        //        List<string> splitResult = pictureString.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
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
        public async static Task<bool> SaveUserImageFB(string LoginID)
        {
            //var userContext = await B2CAuthenticationService.Instance.SignInAsync();
            //JObject user = ParseIdToken(userContext.AccessToken);
            //string result = user["picture"]?.ToString();
            string pictureString = App.userContext.Picture;
            bool isCreated = false;
            if (pictureString != null)
            {
                try
                {
                    List<string> splitResult = pictureString.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                    splitResult = splitResult.Select(x => x.Trim().Replace("\"", "").Replace(",", "")).ToList();
                    foreach (string item in splitResult)
                    {
                        if (item.Contains(":"))
                        {
                            List<string> splitItem = item.Split(':').ToList();
                            string key = splitItem[0];
                            string value = splitItem[1];
                            switch (splitItem[0])
                            {
                                case "url":
                                    string urlString = splitItem[1] + splitItem[2];
                                    using (var webClient = new WebClient())
                                    {
                                        byte[] bytes = null;

                                        webClient.DownloadDataCompleted += async
                                            delegate (object sender, DownloadDataCompletedEventArgs e)
                                        {
                                            bytes = e.Result;

                                            if (bytes != null)
                                            {
                                                await ProfileImageBlobStorageService.SavePhoto(bytes, LoginID);
                                                //await dataStore.UpdatePublicUserInfo(updatePublicUserInfo);
                                                isCreated = true;
                                            }
                                        };

                                        await webClient.DownloadDataTaskAsync(new Uri(urlString.Trim().Insert(5, ":")));

                                    }
                                    break;

                                default:
                                    break;
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    isCreated = false;
                   
                }

            }
          
            return isCreated;
            //return "Missing";
            //return imgProfile;
        }
    }
}
