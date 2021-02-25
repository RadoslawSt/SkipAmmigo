using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ImageSource = Xamarin.Forms.ImageSource;
using Xamvvm;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using JumpApp.Services;
using DLToolkit.Forms.Controls;
using JumpApp.Models;
using JumpApp.Views;
using Rg.Plugins.Popup.Extensions;
using JumpApp.Interface;

namespace JumpApp.ViewModels
{
    public class MyPopupPageModel : BasePageModel
    {
        public ICommand TakePictureCommand { get; private set; }
        public ICommand SelectPictureCommand { get; private set; }
        public ICommand ConfirmPictureCommand { get; private set; }
        public ICommand CancelPictureCommand { get; private set; }
        public ICommand PreviewPictureCommand { get; private set; }
        public ICommand ConfirmFriendCommand { get; private set; }
        public ICommand ConfirmAchievementCommand { get; private set; }

        private IAzureRestService azureRestServ = new AzureRestService();
        public INavigation navigation;
        // private IAzureRestService azureRestServ = new AzureRestService();
        public UserInfo publicUserInfo;
        public MyPopupPage PopupPage { get; set; }
        public int achievementID { get; set; }

        public MyPopupPageModel(INavigation Navigation, MyPopupPage parent_myPopupPage, AddFriendPopupPage parent_addFriend, AchievementPopupPage parent_achievementPopup, int AchievementID)
        {
            navigation = Navigation;
            azureRestServ = DependencyService.Get<IAzureRestService>();
            PopupPage = parent_myPopupPage;
            PreviewTransformations = new List<ITransformation>();

            TakePictureCommand = new Command(async (object obj) => await TakePicture(obj));
            SelectPictureCommand = new Command(async (object obj) => await SelectPicture(obj));
            ConfirmPictureCommand = new Command(async () => await ConfirmPicture());
            CancelPictureCommand = new Command(async () => await CancelPicture());
            ConfirmFriendCommand = new Command(async (object obj) => await ConfirmFriend(obj));
            PreviewPictureCommand = new Command(async (object obj) => await PreviewPicture(obj));
            ConfirmAchievementCommand = new Command(async () => await ConfirmAchievement());

            FirstScreenVisibility = true;
            SecondScreenVisibility = false;
            AddFriendLabelText = "Add Friends ID Below";
            GifVisibility = true;
            achievementID = AchievementID;
            Zoom = 1d;

            AchievementTitle = "";
            AchievementIconSource = "";
            AchievementDetails = "";

            if(achievementID != 0)
            {
                Task.Run(async () => {await SetupAchievement(); });
            }
        }
        private async Task SetupAchievement()
        {
            publicUserInfo = await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier);

            switch (achievementID)
            {
                case 1:
                    AchievementTitle = "Achieve Rank Legend";
                    AchievementIconSource = "AchievementLegend.png";
                    AchievementDetails = "Detail 1";
                    break;
                case 2:
                    AchievementTitle = "Achieve Rank Professional";
                    AchievementIconSource = "AchievementProfessional.png";
                    AchievementDetails = "Detail 2";
                    break;
                case 3:
                    AchievementTitle = "Set Workout reminder";
                    AchievementIconSource = "Bell.png";
                    AchievementDetails = "Detail 3";
                    break;
                case 4:
                    AchievementTitle = "Add 10 Friends";
                    AchievementIconSource = "Crowd.png";
                    AchievementDetails = "Detail 4";
                    break;
                case 5:
                    AchievementTitle = "Change your profile image";
                    AchievementIconSource = "User.png";
                    AchievementDetails = "Detail 5";
                    break;
                case 6:
                    AchievementTitle = "Jump for 20 minutes in one session";
                    AchievementIconSource = "Marketing.png";
                    AchievementDetails = "Detail 6";
                    break;
                case 7:
                    AchievementTitle = "Add a friend";
                    AchievementIconSource = "Person.png";
                    AchievementDetails = "Detail 7";
                    break;
                case 8:
                    AchievementTitle = "Burn accumulated 4000 calories";
                    AchievementIconSource = "Pizza.png";
                    AchievementDetails = "Detail 8";
                    break;
                case 9:
                    AchievementTitle = "Achieve 50,000 total jumps";
                    AchievementIconSource = "Structure.png";
                    AchievementDetails = "Detail 9";
                    break;
                case 10:
                    AchievementTitle = "Accumulate total of 3H of jumping session";
                    AchievementIconSource = "TimeManagement.png";
                    AchievementDetails = "Detail 10";
                    break;
                default:
                    break;
            }
            if(publicUserInfo.Achievements == "")
            {
                publicUserInfo.Achievements = achievementID.ToString();
            }
            else
            {
                publicUserInfo.Achievements = publicUserInfo.Achievements + "," + achievementID;
            }

            await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            achievementID = 0;
        }
        private async Task ConfirmAchievement()
        {
            await navigation.PopPopupAsync();
        }
        private async Task ConfirmFriend(object sender)
        {
            Entry entrySender = (Entry)sender;

            publicUserInfo = await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier);
            UserInfo requestedPublicUserInfo = await azureRestServ.GetPublicUserInfoFriendlyLogin(entrySender.Text);
            if(requestedPublicUserInfo.PendingID == "")
            {
                requestedPublicUserInfo.PendingID = publicUserInfo.Id.ToString();              
            }
            else
            {
                requestedPublicUserInfo.PendingID = requestedPublicUserInfo.PendingID + "," + publicUserInfo.Id;               
            }

            await azureRestServ.UpdatePublicUserInfo(requestedPublicUserInfo);
            AddFriendLabelText = "Invitation Send";
            await Task.Delay(2000);
            await navigation.PopPopupAsync();
        }
        private async Task ConfirmPicture()
        {
             publicUserInfo = await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier);

            
            int oldProfileImageExtension = publicUserInfo.ProfileImageExtension;
            int newProfileImageExtension = oldProfileImageExtension +1;
            publicUserInfo.ProfileImageExtension = newProfileImageExtension;
            //publicUserInfo.HasProfileImage = true;

            await azureRestServ.UpdatePublicUserInfo(publicUserInfo);
            await ProfileImageBlobStorageService.SavePhoto(DisplayImageByteArray, App.userContext.UserIdentifier + newProfileImageExtension.ToString());
            await ProfileImageBlobStorageService.DeleteBlobFileAsync(publicUserInfo.LoginId + oldProfileImageExtension);
           //await Task.Delay(10000);
            //MyPopupPage test;
            PopupPage.profileImageExtension = newProfileImageExtension;
            await navigation.PopPopupAsync();
            //await navigation.PopAsync();
            
        }
        private async Task CancelPicture()
        {
            await navigation.PopPopupAsync();
        }
        private async Task PreviewPicture(object sender)
        {
            ImageCropView cropView = (ImageCropView)sender;
            try
            {
                var result = await cropView.GetImageAsJpegAsync();
                
                //byte[] bytes = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    //await _MediaFile.GetStream().CopyToAsync(ms);
                    await result.CopyToAsync(ms);
                    DisplayImageByteArray = ms.ToArray();
                    //_MediaFile.Dispose();
                    //bytes = ms.ToArray();
                }

                var imageSource = Xamarin.Forms.ImageSource.FromStream(() =>
                {
                    return new MemoryStream(DisplayImageByteArray);
                });

                DisplayImage = imageSource;
            }
            catch (System.Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Error");
            }
        }
        private async Task TakePicture(object sender)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
                DependencyService.Get<IMessage>().ShortAlert("No Camera Available");
                return;
            }
            else
            {
                _MediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "myImage.jpg",
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 25,
                    DefaultCamera = CameraDevice.Rear,
                    AllowCropping = true
                });

                if (_MediaFile == null) return;
                // imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 25,
                    
                };

                using (var memoryStream = new MemoryStream())
                {
                    await _MediaFile.GetStream().CopyToAsync(memoryStream);
                    //DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 150);
                    SavedImage = ImageSource.FromStream(() => new MemoryStream(DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 500, 500)));
                }

                //SavedImage = ImageSource.FromStream(() => _MediaFile.GetStream());
                FirstScreenVisibility = false;
                SecondScreenVisibility = true;
                
                await Task.Run(async () =>
                {
                   
                    await Task.Delay(2500);

                    GifVisibility = false;
                    await PreviewPicture(sender);

                });
                //using (var memoryStream = new MemoryStream())
                //{
                //    await _MediaFile.GetStream().CopyToAsync(memoryStream);
                //    _MediaFile.Dispose();

                //    //using (var image = await CrossImageEdit.Current.CreateImageAsync(memoryStream.ToArray()))
                //    //{
                //    //    var croped = await Task.Run(() =>
                //    //            image.Crop(10, 20, 250, 100)
                //    //                 .Rotate(180)
                //    //                 .Resize(100, 0)

                //    //    );
                //    //}

                //    //new ImageCropper()
                //    //{

                //    //    Success = (imageFile) =>
                //    //    {
                //    //        Device.BeginInvokeOnMainThread(() =>
                //    //        {
                //    //            Image image = new Image();
                //    //            //var test = memoryStream.ToArray();
                //    //            //image.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                //    //            image.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                //    //            //imageView.Source = ImageSource.FromFile(imageFile);
                //    //        });
                //    //    }
                //    //}.Show(this);
                //    /*DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 150);*/

                //    //await ProfileImageBlobStorageService.SavePhoto(DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 200), "Test7");
                //}

                // UploadedUrl.Text = "Image URL:";
            }
        }
        private async Task SelectPicture(object sender)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //await DisplayAlert("Error", "This is not support on your device.", "OK");
                DependencyService.Get<IMessage>().ShortAlert("This is not supported on your device");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 25
                };
                _MediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_MediaFile == null) return;

                using (var memoryStream = new MemoryStream())
                {
                    await _MediaFile.GetStream().CopyToAsync(memoryStream);
                    //DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 150);
                    SavedImage = ImageSource.FromStream(() => new MemoryStream(DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 500, 500)));
                }

                

                FirstScreenVisibility = false;
                SecondScreenVisibility = true;
               
                await Task.Run(async () =>
                {

                    await Task.Delay(2500);

                    GifVisibility = false;
                    await PreviewPicture(sender);
                });
                //imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                // UploadedUrl.Text = "Image URL:";
            }
        }  
        
        public string AchievementTitle
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
        public string AchievementIconSource
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
        public string AchievementDetails
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }

        public string AddFriendLabelText
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
        public byte[] DisplayImageByteArray
        {
            get { return GetField<byte[]>(); }
            set { SetField(value); }
        }
        public bool FirstScreenVisibility
        {
            get { return GetField<bool>(); }
            set { SetField(value); }
        }
        public bool SecondScreenVisibility
        {
            get { return GetField<bool>(); }
            set { SetField(value); }
        }
        public bool GifVisibility
        {
            get { return GetField<bool>(); }
            set { SetField(value); }
        }
        public MediaFile _MediaFile
        {
            get { return GetField<MediaFile>(); }
            set { SetField(value); }
        }
        public string URL
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }
        
        public ImageSource SavedImage
        {
            get { return GetField<ImageSource>(); }
            set { SetField(value); }
        }
        public ImageSource DisplayImage
        {
            get { return GetField<ImageSource>(); }
            set { SetField(value); }
        }

        public List<ITransformation> PreviewTransformations
        {
            get { return GetField<List<ITransformation>>(); }
            set { SetField(value); }
        }

        public List<ITransformation> Transformations
        {
            get { return GetField<List<ITransformation>>(); }
            set { SetField(value); }
        }

        public ICommand RotateCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand ManualOffsetCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public int Rotation
        {
            get { return GetField<int>(); }
            set { SetField(value); }
        }

        public double XOffset
        {
            get { return GetField<double>(); }
            set { SetField(value); }
        }

        public double YOffset
        {
            get { return GetField<double>(); }
            set { SetField(value); }
        }

        public double Zoom
        {
            get { return GetField<double>(); }
            set { SetField(value); }
        }
    }
}
