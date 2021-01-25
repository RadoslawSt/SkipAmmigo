using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JumpApp.Models;
using JumpApp.Services;

namespace JumpApp.Views.MyWorkouts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyWorkoutsPage : ContentPage
	{
        private readonly IRepositoryWrapper repoWrapper;
        public MyWorkoutsPage ()
		{
			InitializeComponent ();
            BindingContext = new WorkoutSessionsViewModel();
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            List<int> workoutSessionPerMinWorkoutSessionIDList = repoWrapper.WorkoutSessionPerMin.GetAllAvailableWorkoutSessionsPerMin();
            var workoutDetails = e.Item as WorkoutSession;
            if(workoutSessionPerMinWorkoutSessionIDList.Contains(workoutDetails.WorkoutSessionId))
            {
                await Navigation.PushAsync(new MyWorkoutsPerMinPage(workoutDetails.WorkoutSessionId, workoutDetails.DateTime));
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("No Info to Display");
            }
            
        }
    }
}