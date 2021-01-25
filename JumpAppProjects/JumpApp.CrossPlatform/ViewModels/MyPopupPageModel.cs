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
        private IAzureRestService azureRestServ = new AzureRestService();
        public INavigation navigation;
        // private IAzureRestService azureRestServ = new AzureRestService();
        public UserInfo publicUserInfo;
        public MyPopupPage PopupPage { get; set; }
        public MyPopupPageModel(INavigation Navigation, MyPopupPage parent)
        {
            //azureRestServ = DependencyService.Get<IAzureRestService>();
            PreviewTransformations = new List<ITransformation>();
            TakePictureCommand = new Command(async (object obj) => await TakePicture(obj));
            SelectPictureCommand = new Command(async (object obj) => await SelectPicture(obj));
            ConfirmPictureCommand = new Command(async () => await ConfirmPicture());
            CancelPictureCommand = new Command(async () => await CancelPicture());
            navigation = Navigation;
            azureRestServ = DependencyService.Get<IAzureRestService>();
            PopupPage = parent;
            PreviewPictureCommand = new Command(async (object obj) => await PreviewPicture(obj));
            FirstScreenVisibility = true;
            SecondScreenVisibility = false;
            GifVisibility = true;
            //string testing = AuthUserHelper.GetUserInfo("UserID");
            
            //RotateCommand = new BaseCommand((arg) =>
            //{
            //    var rotation = Rotation + 90;

            //    if (rotation >= 360)
            //        rotation = 0;

            //    Rotation = rotation;
            //});
            
            Zoom = 1d;
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
        //private MediaFile _mediaFile;
        //private string URL { get; set; }
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
