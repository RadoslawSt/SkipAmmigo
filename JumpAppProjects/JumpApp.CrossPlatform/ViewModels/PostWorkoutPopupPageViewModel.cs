using JumpApp.Controls;
using JumpApp.Models;
using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
    public class PostWorkoutPopupPageViewModel : ExtendedBindableObject
    {
        private double _progress;
        private ImageSource _profileImage;
        public UserInfo user { get; set; }
        public float _experience;
        public double initialProgress;

        public PostWorkoutPopupPageViewModel(UserInfo User, float Experience)
        {
            //Progress = 0.0;
            
            user = User;
            _progress = (float)RankCalculator.RankProgress(user.Experience);
            _experience = (float)RankCalculator.RankProgress((int)Experience);
            initialProgress = Progress;
            Task.Run(async () => { await UpdaterAsync(); });
        }
        public ICommand AcceptCommand => new Command(Accept);
        public ICommand WorkoutStatsCommand => new Command(WorkoutStats);
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
        public ImageSource ProfileImage
        {
            get => ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + user.LoginId + user.ProfileImageExtension.ToString()));
            set => SetProperty(ref _profileImage, value);
        }
        public float Experience
        {
            get => _experience;
            set => SetProperty(ref _experience, value);
        }
        public void Accept()
        {
            Progress = initialProgress + Experience;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
        public void WorkoutStats()
        {

        }
        public async Task UpdaterAsync()
        {
            float remainingExperience = Experience;
            float valueDeduction = Experience / 100;

            while (remainingExperience > 0 && Progress < 1.0)
            {
                Progress = Progress + valueDeduction;
                remainingExperience = remainingExperience - valueDeduction;
                await Task.Delay(30);

            }


        }
    }

}
