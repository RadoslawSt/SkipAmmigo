using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JumpApp.Interface;
//using Stormlion.ImageCropper;
using System.Diagnostics;
using JumpApp.ViewModels;
using FFImageLoading.Work;
using Xamvvm;
using System.Collections;
//using Plugin.ImageEdit;

namespace JumpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IBasePage<MyPopupPageModel>
    {
        //private MediaFile _mediaFile;
        //private string URL { get; set; }
        public delegate void CallbackDelegate(IList list);
        public CallbackDelegate CallbackMethod { get; set; }
        public int profileImageExtension { get; set; }
        public MyPopupPage(INavigation Navigation)
        {
            
            InitializeComponent();
            BindingContext = new MyPopupPageModel(Navigation, this,null);
            
            
            //saveButton.Command = new BaseCommand(async (arg) =>
            //{
            //    try
            //    {
            //        var result = await cropView.GetImageAsJpegAsync();
            //        byte[] bytes = null;

            //        using (MemoryStream ms = new MemoryStream())
            //        {
            //            result.CopyTo(ms);
            //            bytes = ms.ToArray();
            //        }

            //        var imageSource = Xamarin.Forms.ImageSource.FromStream(() =>
            //        {
            //            return new MemoryStream(bytes);
            //        });

            //        ((MyPopupPageModel)BindingContext).SavedImage = imageSource;
            //    }
            //    catch (System.Exception ex)
            //    {
            //        await DisplayAlert("Error", ex.Message, "Ok");
            //    }
            //});
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
        public event EventHandler<object> CallbackEvent;
        protected override void OnDisappearing() => CallbackEvent?.Invoke(this, profileImageExtension);
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //}

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when Background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a Background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        //private async void btnTakePic_Clicked(object sender, EventArgs e)
        //{

        //    await CrossMedia.Current.Initialize();
        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
        //        return;
        //    }
        //    else
        //    {
        //        _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //        {
        //            Directory = "Sample",
        //            Name = "myImage.jpg",
        //            PhotoSize = PhotoSize.Custom,
        //            CustomPhotoSize = 25,
        //            AllowCropping = true
        //        });

        //        if (_mediaFile == null) return;
        //        // imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
        //        var mediaOption = new PickMediaOptions()
        //        {
        //            PhotoSize = PhotoSize.Custom,
        //            CustomPhotoSize = 25

        //        };

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await _mediaFile.GetStream().CopyToAsync(memoryStream);
        //            _mediaFile.Dispose();

        //            //using (var image = await CrossImageEdit.Current.CreateImageAsync(memoryStream.ToArray()))
        //            //{
        //            //    var croped = await Task.Run(() =>
        //            //            image.Crop(10, 20, 250, 100)
        //            //                 .Rotate(180)
        //            //                 .Resize(100, 0)
                                     
        //            //    );
        //            //}

        //            //new ImageCropper()
        //            //{

        //            //    Success = (imageFile) =>
        //            //    {
        //            //        Device.BeginInvokeOnMainThread(() =>
        //            //        {
        //            //            Image image = new Image();
        //            //            //var test = memoryStream.ToArray();
        //            //            //image.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        //            //            image.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        //            //            //imageView.Source = ImageSource.FromFile(imageFile);
        //            //        });
        //            //    }
        //            //}.Show(this);
        //            /*DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 150);*/

        //            //await ProfileImageBlobStorageService.SavePhoto(DependencyService.Get<IMediaService>().ResizeImage(memoryStream.ToArray(), 150, 200), "Test7");
        //        }

        //        // UploadedUrl.Text = "Image URL:";
        //    }
        //}
     
        //private async void btnSelectPic_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("Error", "This is not support on your device.", "OK");
        //        return;
        //    }
        //    else
        //    {
        //        var mediaOption = new PickMediaOptions()
        //        {
        //            PhotoSize = PhotoSize.Medium
        //        };
        //        _mediaFile = await CrossMedia.Current.PickPhotoAsync();
        //        if (_mediaFile == null) return;
        //        //imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
        //        // UploadedUrl.Text = "Image URL:";
        //    }
        //}
       
    }
}