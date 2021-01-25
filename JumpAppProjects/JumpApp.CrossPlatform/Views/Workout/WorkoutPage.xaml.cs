using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JumpApp.Models;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using JumpApp.Views.MyWorkouts;

namespace JumpApp.Views.Workout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        private bool _isRunning;
        int animationCounter = 0;
        int totalJumps;
        double caloriesBurned;
        int jumpsNewValue;
        int difference = 0;
        int prevTotalJumps = 0;
        //int previousPace = 0;
        //int pace = 0;
        TimeSpan TotalTime = new TimeSpan();
        AccelerometerInit accelerometerInit { get; set; }
        private readonly IRepositoryWrapper repoWrapper;
        private readonly IAzureRestService azureRestServ;
        //IUserInfoRepository userRepo = new UserInfoRepository();
        //IWorkoutSessionRepository workoutSessionRepo = new WorkoutSessionRepository();
        //IWorkoutSessionPerMinRepository workoutSessionPerMinRepo = new WorkoutSessionPerMinRepository();
        List<WorkoutSessionPerMin> workoutDetailsList; 
        CoreUserInfo user = new CoreUserInfo();
        UserInfo publicUser = new UserInfo();
        //public ICommand AddWorkoutSessionCommand { get; private set; }
        public WorkoutPage()
        {
            InitializeComponent();
            
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
            azureRestServ = DependencyService.Get<IAzureRestService>();
            user = Task.Run(async () => { return await azureRestServ.GetCoreUserInfo(App.userContext.UserIdentifier); }).Result;
            publicUser = Task.Run(async () => { return await azureRestServ.GetPublicUserInfo(App.userContext.UserIdentifier); }).Result;
            accelerometerInit = new AccelerometerInit(user);
            //user = repoWrapper.CoreUserInfo.GetAllUserInfo().FirstOrDefault();
        }

        private async void BtnStart_Clicked(object sender, EventArgs e)
        {
            workoutDetailsList = new List<WorkoutSessionPerMin>();
            if (tglSwitch.IsToggled == true)
            {
                switch (Picker.SelectedIndex)
                {
                    case 0:
                        animationCounter = 3;
                        break;

                    case 1:
                        animationCounter = 5;
                        break;

                    default:
                        break;
                }

                await AnimationText();

                if (AnimationText().IsCompleted)
                {
                    TotalTime = new TimeSpan();
                   // lblTimer.Text = "00:00:00";
                    lblCountdown.IsVisible = false;
                    lblJumps.IsVisible = true;
                    btnStart.IsEnabled = false;
                    btnStop.IsEnabled = true;
                    _isRunning = true;
                    RunTimer();
                    accelerometerInit.ToggleAccelerometer();
                }
            }
            else
            {
                TotalTime = new TimeSpan();
               // lblTimer.Text = "00:00:00";
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
                _isRunning = true;
                RunTimer();
                accelerometerInit.ToggleAccelerometer();
            }

        }
        private async Task<bool> AnimationText()
        {
            bool cycleCompleted = false;
            lblCountdown.IsVisible = true;
            lblJumps.IsVisible = false;
            lblCountdown.Text = animationCounter.ToString();
            if (animationCounter <= 5 && animationCounter >= 1)
            {
                new Animation {
            { 0, 0.8, new Animation (v => lblCountdown.Scale = v, 20, 6) },
            }.Commit(this, "ChildAnimations", 16, 500, null);

                await Task.Delay(1000);

                animationCounter--;
                await AnimationText();
            }
            if (animationCounter == 1)
            {
                cycleCompleted = true;
                return cycleCompleted;
            }
            cycleCompleted = false;
            return cycleCompleted;
        }
        private void BtnLock_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            _isRunning = false;
            accelerometerInit.ToggleAccelerometer();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
           
            int Jumps = Convert.ToInt32(lblJumps.Text);
            if (prevTotalJumps != Jumps)
            {
                difference = (Jumps - prevTotalJumps);
                workoutDetailsList.Add(new WorkoutSessionPerMin { Jumps = difference, Minute = TotalTime, TotalJumps = Jumps, Pace = (Jumps / TotalTime.TotalSeconds) * 60 });
            }

            //int sessionID = workoutSessionRepo.GetWorkoutSessions().LastOrDefault().Id;
            Task.Run(async() => { await AddWorkoutSession(); });
            //int workoutID = repoWrapper.WorkoutSession.GetWorkoutSessions().LastOrDefault().WorkoutSessionId;
            //int workoutSessionID = azureRestServ.GetWorkoutSessions(AuthUserHelper.GetUserInfo("UserID")).Result.LastOrDefault().Id;
            //int workoutSessionID = Task.Run(() => { return azureRestServ.GetWorkoutSessions(AuthUserHelper.GetUserInfo("UserID")); }).Result.LastOrDefault().WorkoutSessionId;
            //int workoutSessionID = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(AuthUserHelper.GetUserInfo("UserID")); }).Result.LastOrDefault().WorkoutSessionId;
            //var workoutSessionID = azureRestServ.GetWorkoutSessions(AuthUserHelper.GetUserInfo("UserID")).Result;
            //var test = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(AuthUserHelper.GetUserInfo("UserID")); }).Result;

            //Task.Run(() => { AddWorkoutSessionDetails(workoutSessionID); });

            //var popupPage = new PostWorkoutPopupPage(publicUser, (float)Convert.ToInt32(lblJumps.Text) / 2);
            Task.Run(async () => { await SetUpPopupPage(); });
        }
        public async Task GetLatestWorkoutSessionID()
        {

        }
        public async Task AddWorkoutSession()
        {

            int test = (int)caloriesBurned;
            WorkoutSession session = new WorkoutSession
            {
                Duration = TotalTime,
                TotalJumps = Convert.ToInt32(lblJumps.Text),
                Calories = Convert.ToDouble(caloriesBurned.ToString("0.#")),
                Experience = Convert.ToInt32(lblJumps.Text) / 2,
                DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss")),
                AveragePace = (int)(Convert.ToInt32(lblJumps.Text) / TotalTime.TotalSeconds),
                LoginID = App.userContext.UserIdentifier
            };


            //workoutSessionRepo.InsertWorkoutSession(session);
            List<WorkoutSession> workoutSessions = new List<WorkoutSession>();

            workoutSessions.Add(session);
            await azureRestServ.AddWorkoutSession(workoutSessions);
            // repoWrapper.WorkoutSession.InsertWorkoutSession(session);
            publicUser.Experience = publicUser.Experience + Convert.ToInt32(lblJumps.Text) / 2;
            await azureRestServ.UpdatePublicUserInfo(publicUser);
            int workoutSessionID = Task.Run(async () => { return await azureRestServ.GetWorkoutSessions(App.userContext.UserIdentifier); }).Result.LastOrDefault().WorkoutSessionId;
            AddWorkoutSessionDetails(workoutSessionID); 
        }
        async Task SetUpPopupPage()
        {
            var popupPage = new PostWorkoutPopupPage(publicUser, (float)Convert.ToInt32(lblJumps.Text) / 2);
            //var popupPage = new PostWorkoutPopupPage(publicUser, (float)250);
            popupPage.CallbackEvent += (object sender, object e) => CallbackMethod(e);
            await Navigation.PushPopupAsync(popupPage);
        }
        public void CallbackMethod(object DisplayWorkoutStats)
        {
            bool displayWorkoutStats = (bool)DisplayWorkoutStats;

            if(displayWorkoutStats == true)
            {
                Navigation.PushAsync(new MyWorkoutsPage());
            }
            else
            {

            }
        }
        public void AddWorkoutSessionDetails(int workoutID)
        {
            double previousPace = 0;
            foreach (var item in workoutDetailsList)
            {
                if (previousPace != 0)
                {
                    if (previousPace < item.Pace)
                    {
                        item.PaceChangeIcon = "GreenArrow.png";
                    }
                    else if (previousPace > item.Pace)
                    {
                        item.PaceChangeIcon = "RedArrow.png";
                    }
                    else
                    {
                        //neutral implement at somepoint
                    }
                }
                else
                {
                    item.PaceChangeIcon = "GreenArrow.png";
                }
                item.WorkoutId = workoutID;
                previousPace = item.Pace;
                //workoutSessionPerMinRepo.InsertWorkoutSessionDetails(item);
                repoWrapper.WorkoutSessionPerMin.InsertWorkoutSessionDetails(item);
            }

        }
        
        public void RunTimer()
        {

            TimeSpan TimeElement = new TimeSpan();
            totalJumps = 0;

            int jumpsOldValue = 0;
            jumpsNewValue = 0;
            progressbar.Progress = 0;

            caloriesBurned = 0;
            //int jumpsPerMin = 0;
            difference = 0;
            prevTotalJumps = 0;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                TotalTime = TotalTime + TimeElement.Add(new TimeSpan(0, 0, 1));
                lblTimer.Text = string.Format("{0:hh\\:mm\\:ss}", TotalTime);

                //lblJumps.Text = jumps.ToString();
                progressbar.Progress = (progressbar.Progress + 0.0166666666666667);
                if (progressbar.Progress == 1)
                {
                    totalJumps = Convert.ToInt32(lblJumps.Text);
                    if (prevTotalJumps != totalJumps)
                    {


                        difference = (totalJumps - prevTotalJumps);
                        workoutDetailsList.Add(new WorkoutSessionPerMin { Jumps = difference, Minute = TotalTime, TotalJumps = totalJumps, Pace = (totalJumps / TotalTime.TotalSeconds) * 60 });
                    }
                    prevTotalJumps = totalJumps;
                    accelerometerInit.counter = 0;

                    progressbar.Progress = 0;
                    //jumpsPerMin = 0;
                }
                // returning true will fire task again in 2 minutes.
                return _isRunning;
            });
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), () =>
                     {
                         jumpsNewValue = totalJumps + accelerometerInit.counter;

                         lblJumps.Text = jumpsNewValue.ToString();
                         lblx.Text = "x: "+accelerometerInit.testx.ToString();
                         lbly.Text = "y: " + accelerometerInit.testy.ToString();
                         lblz.Text = "z: " + accelerometerInit.testz.ToString();
                         lblAveragePace.Text = CalculateAveragePace(accelerometerInit.counter, TotalTime).ToString("0.#");
                         if (jumpsOldValue != jumpsNewValue)
                         {
                             //totalJumps = jumpsNewValue;
                             //lblCaloriesBurned.Text = CaloriesCalculation(accelerometerInit.counter, TotalTime).ToString("0.##");
                             caloriesBurned += CaloriesCalculation(CalculateAveragePace(accelerometerInit.counter, TotalTime));
                             lblCaloriesBurned.Text = caloriesBurned.ToString("0.##");
                         }
                         jumpsOldValue = jumpsNewValue;
                         return _isRunning;
                     });


        }

        private double CalculateAveragePace(int stepCount, TimeSpan time)
        {
            double result = 0.0;
            double number = 0.0;
            if (time.Seconds != 0)
            {
                number = (double)stepCount / time.Seconds;
                result = number * 60;
            }
            return result;
        }
        //private double CaloriesCalculation(int stepCount, TimeSpan time)
        //{
        //    var user = userRepo.GetAllUserInfo().FirstOrDefault();
        //    int userWeight = Convert.ToInt32(user.Weight * 2.205);
        //    var formula70Jumps = (.074 * userWeight) * (1);
        //    var formula125Jumps = (.080 * userWeight) * (1);
        //    var formula145Jumps = (.089 * userWeight) * (1);
        //    var caloriesPerJump = 0.0;

        //    if (stepCount <= 70)
        //    {

        //        caloriesPerJump = (formula70Jumps / 70) * stepCount;
        //    }
        //    else if (stepCount > 70 && stepCount <= 125)
        //    {                 
        //        caloriesPerJump = ((formula125Jumps / 125) * (stepCount - 70)) + formula70Jumps;
        //    }
        //    else
        //    {
        //        caloriesPerJump = ((formula145Jumps / 145) * (stepCount - 125)) + formula125Jumps;
        //    }
        //    //var value = (.074 * userWeight) * (time.TotalSeconds / 60);



        //    return caloriesPerJump;
        //}
        private double CaloriesCalculation(double averagePace)
        {
            //var user = userRepo.GetAllUserInfo().FirstOrDefault();
            int userWeight = user.Weight;
            double MET = 0;
            int METDivide = 0;
            var caloriesPerJump = 0.0;

            if (averagePace < 100)
            {
                MET = 8.8;
                METDivide = 100;
            }
            else if (averagePace > 120)
            {
                MET = 12.3;
                METDivide = 120;
            }
            else
            {
                MET = 11.8;
                METDivide = 120;
            }
            double formula = (MET * userWeight * 3.5) / 200;

            caloriesPerJump = formula / METDivide;

            return caloriesPerJump;
        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch tgl = (Switch)sender;

            if (tgl.IsToggled == true)
            {
                Picker.IsEnabled = true;
            }
            else
            {
                Picker.IsEnabled = false;
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}