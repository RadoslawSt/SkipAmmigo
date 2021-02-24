using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Features.ADB2C;
using JumpApp.Models;
using JumpApp.Services;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
    public class WorkoutSessionsViewModel : BaseWorkoutSessionViewModel
    {
        //public ObservableCollection<WorkoutSession> WorkoutSessions { get; set; }
        //public ObservableCollection<WorkoutSession> WorkoutSessionsLimited { get; set; }
        public bool workoutSessionExists { get; set; }

        //public Color rowColor { get; set; }
        private readonly IAzureRestService azureRestServ;
        public string sometext { get; set; }
        public int totalJumps { get; set; }
        public double totalCalories { get; set; }
        public TimeSpan totalDuration { get; set; }
        public DateTime dateTime { get; set; }
        private readonly IRepositoryWrapper repoWrapper;
        Image imgProfile { get; set; }
        //public ImageSource imageUri { get; set; }
        public string lblName { get; set; }
        public string profileImageExtension { get; set; }
        public UserInfo publicUserInfo { get; set; }

        //public IWorkoutSessionRepository workoutSessionRepo = new WorkoutSessionRepository();

        public WorkoutSessionsViewModel()
        {
            //WorkoutSessions = new ObservableCollection<WorkoutSession>(WorkoutSessions.OrderByDescending(x => x.DateTime));
            //WorkoutSessions = (ObservableCollection<WorkoutSession>)WorkoutSessions.OrderByDescending(x => x.DateTime).ToList();
            publicUserInfo = new UserInfo();
            azureRestServ = DependencyService.Get<IAzureRestService>();
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
            List<int> workoutSessionPerMinWorkoutSessionIDList = repoWrapper.WorkoutSessionPerMin.GetAllAvailableWorkoutSessionsPerMin();
            List<WorkoutSession> WorkoutSessionList = (List<WorkoutSession>)Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(App.userContext.UserIdentifier); }).Result;
            publicUserInfo = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
            profileImageExtension = publicUserInfo.ProfileImageExtension.ToString() + "|" + publicUserInfo.HasProfileImage;
            //workoutSessionExists = false;



            Task.Run(async () => { await SetUpFriendsWorkout(); });

            WorkoutSessions = new ObservableCollection<WorkoutSession>(WorkoutSessionList.OrderByDescending(x=> x.DateTime));
            WorkoutSessionsLimited = new ObservableCollection<WorkoutSession>(WorkoutSessionList.OrderByDescending(x => x.DateTime).Take(10));
            sometext = "Imawesome";

            //imgProfile = B2CAuthenticationService.GetUserImage();
            //imageUri = imgProfile.Source;
           // imageUri = ImageSource.FromUri(new Uri("https://jumpappbackendservice.blob.core.windows.net/profileimages/" + App.userContext.UserIdentifier + publicUserInfo.ProfileImageExtension.ToString()));
            lblName = "Hello " + App.userContext.Name;
            foreach (var item in WorkoutSessionList)
            {
                totalJumps += item.TotalJumps;
                totalCalories += item.Calories;
                totalDuration += item.Duration;
                 
                //if (workoutSessionPerMinWorkoutSessionIDList.Contains(item.WorkoutSessionId))
                //{
                //    //workoutSessionExists.Add(true);
                //}
                //else
                //{
                //   // workoutSessionExists.Add(false);
                //}
            }

        }
        public async Task SetUpFriendsWorkout()
        {
            foreach(var x in publicUserInfo.FriendsID.Split(','))
            {
                UserInfo friend = await azureRestServ.GetPublicUserInfo(Convert.ToInt32(x));
                List<WorkoutSession> friendWorkouts = (List<WorkoutSession>) await azureRestServ.GetWorkoutSessions(friend.LoginId);
                foreach(var workouts in friendWorkouts)
                {
                    FriendsWorkoutSessionsLimited.Add(workouts);
                }
            }
            FriendsWorkoutSessionsLimited.OrderByDescending(x => x.DateTime).Take(10);
        }
    }
}
