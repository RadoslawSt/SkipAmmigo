using JumpApp.Models;
using JumpApp.Services;
using JumpApp.ViewModels;
using JumpApp.Views.MyWorkouts;
using JumpApp.Views.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JumpApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        //int animationCounter = 3;
        private readonly IRepositoryWrapper repoWrapper;
        //private int profileImageExtension { get; set; }

        public HomePage()
        {
            InitializeComponent();
            //profileImageExtension = ProfileImageExtension;
            //BindingContext = new WorkoutSessionsViewModel();
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
        }
        protected override async void OnAppearing()
        {
            try
            {
                BindingContext =  new WorkoutSessionsViewModel();
                //await Task.Run(async () => { BindingContext = new WorkoutSessionsViewModel(); });
                // fade to 0 opacity, 2s
                //await lblNameText.FadeTo(0, 2000);
                // return to full opacity, 2s
               // lvWorkoutList.BeginRefresh();
                lblNameText.Opacity = 0;
                lblLatestWorkouts.Opacity = 0;
                gridButtons.Opacity = 0;
                lvWorkoutList.Opacity = 0;
                await lblNameText.FadeTo(1, 750);
                await lblLatestWorkouts.FadeTo(1, 750);
                await gridButtons.FadeTo(1, 250);
                await lvWorkoutList.FadeTo(1, 500);
            }
            catch
            {

            }
        }
        private async void LvWorkoutList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            List<int> workoutSessionPerMinWorkoutSessionIDList = repoWrapper.WorkoutSessionPerMin.GetAllAvailableWorkoutSessionsPerMin();
            var workoutDetails = e.Item as WorkoutSession;
            if (workoutSessionPerMinWorkoutSessionIDList.Contains(workoutDetails.WorkoutSessionId))
            {
                await Navigation.PushAsync(new MyWorkoutsPerMinPage(workoutDetails.WorkoutSessionId, workoutDetails.DateTime));
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("No Info to Display");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkoutPage());
        }
       

        private void BtnOwnWorkouts_Clicked(object sender, EventArgs e)
        {
            lvFriendsWorkoutList.IsVisible = false;
            lvWorkoutList.IsVisible = true;
        }

        private void BtnFriendsWorkouts_Clicked(object sender, EventArgs e)
        {
            lvWorkoutList.IsVisible = false;
            lvFriendsWorkoutList.IsVisible = true;
        }
        //private async Task<bool> AnimationText()
        //{
        //    bool cycleCompleted = false;
        //    lbl.IsVisible = true;
        //    lbl.Text = animationCounter.ToString();
        //    if (animationCounter <= 3 && animationCounter >=1)
        //    {
        //        new Animation {
        //    { 0, 0.8, new Animation (v => lbl.Scale = v, 20, 6) },
        //    }.Commit(this, "ChildAnimations", 16, 500, null);

        //    await Task.Delay(1000);

        //        animationCounter--;
        //        await AnimationText();
        //     }
        //    if(animationCounter == 1)
        //    {               
        //        cycleCompleted = true;
        //        return cycleCompleted;
        //    }            
        //    cycleCompleted = false;
        //    return cycleCompleted;
        //}
        //private async void BtnAnimate_Clicked(object sender, EventArgs e)
        //{
        //    animationCounter = 3;
        //    await AnimationText();
        //    if (AnimationText().IsCompleted)
        //    {
        //        lbl.IsVisible = false;
        //    }

        //}
    }
}