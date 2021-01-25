using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Models;
using JumpApp.Services;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
    public class BaseWorkoutSessionViewModel : INotifyPropertyChanged
    {
        public WorkoutSession workoutSession;
        public ObservableCollection<WorkoutSession> workoutSessions = new ObservableCollection<WorkoutSession>();
        public ObservableCollection<WorkoutSession> workoutSessionsLimited = new ObservableCollection<WorkoutSession>();
        //public IWorkoutSessionRepository workoutSessionRepo;
        // public UserInfo publicUserInfo = new UserInfo();
        //private IAzureRestService azureRestServ = new AzureRestService();
        public ObservableCollection<WorkoutSession> WorkoutSessions
        {
            get { return workoutSessions; }
            set { workoutSessions = value; }
        }
        public ObservableCollection<WorkoutSession> WorkoutSessionsLimited
        {
            get { return workoutSessionsLimited; }
            set { workoutSessionsLimited = value; }
        }
        public TimeSpan Duration
        {
            get => workoutSession.Duration;
            set
            {
                
                workoutSession.Duration = value;
                NotifyPropertyChanged("Duration");
            }
        }
        public int TotalJumps
        {
            get => workoutSession.TotalJumps;
            set
            {
                workoutSession.TotalJumps = value;
                NotifyPropertyChanged("TotalJumps");
            }
        }
        public double Calories
        {
            get => workoutSession.Calories;
            set
            {
                workoutSession.Calories = value;
                NotifyPropertyChanged("Calories");
            }

        }
        public int AveragePace
        {
            get => workoutSession.AveragePace;
            set
            {
                workoutSession.AveragePace = value;
                NotifyPropertyChanged("AveragePace");
            }
        }
        public int Experience
        {
            get => workoutSession.Experience;
            set
            {
                workoutSession.Experience = value;
                NotifyPropertyChanged("Experience");
            }
        }
        public DateTime DateTime
        {
            
            get => workoutSession.DateTime;
            set
            {
                
                workoutSession.DateTime = DateTime.Now;
                NotifyPropertyChanged("DateTime");
            }
        }
        public string UserId
        {
            get => workoutSession.LoginID;
            set
            {
                workoutSession.LoginID = value;
                NotifyPropertyChanged("UserId");
            }
        }
        //public ImageSource ProfileImageSource
        //{
        //    get
        //    {
        //        publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(UserId); }).Result;
        //        if (publicUserInfo.HasProfileImage == true)
        //        {
        //            return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + UserId + publicUserInfo.ProfileImageExtension.ToString()));
        //        }
        //        else
        //        {
        //            return ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/PlaceholderImage"));
        //        }
        //    }
        //    set
        //    {
        //        ImageSource source;
        //        source = value;
        //        NotifyPropertyChanged("ProfileImageSource");
        //    }
        //}
        #region INotifyPropertyChanged      
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
