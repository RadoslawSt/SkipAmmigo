using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Models;
using JumpApp.Services;
using Xamarin.Forms;

namespace JumpApp.ViewModels
{
    public class WorkoutSessionPerMinListViewModel : BaseViewModel
    {
        public ObservableCollection<WorkoutSessionPerMin> WorkoutSessionsPerMin { get; set; }
        public string date { get; set; }
        public double AveragePace { get; set; }
        public double TotalCalories { get; set; }
        //IWorkoutSessionPerMinRepository workoutSessionPerMinRepo = new WorkoutSessionPerMinRepository();
        //IWorkoutSessionRepository workoutSessionRepo = new WorkoutSessionRepository();
        private readonly IRepositoryWrapper repoWrapper;
        private readonly IAzureRestService azureRestServ;

        public WorkoutSessionPerMinListViewModel(int id, DateTime workoutDate)
        {
            azureRestServ = DependencyService.Get<IAzureRestService>();
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
            double paceTotal = 0;
            var workoutSession2 = Task.Run(async () => { return await azureRestServ.GetWorkoutSessionById(id); }).Result;
            //var workoutSession = workoutSessionRepo.GetWorkoutSession(id);
            
            List<WorkoutSessionPerMin> workoutDetailsList = repoWrapper.WorkoutSessionPerMin.GetWorkoutSessionDetails(id);
            WorkoutSessionsPerMin = new ObservableCollection<WorkoutSessionPerMin>(workoutDetailsList);
          

            date = string.Format("{0:r}", workoutDate);
            foreach(var item in workoutDetailsList)
            {
                paceTotal += item.Pace;
            }
            AveragePace = paceTotal / workoutDetailsList.Count;
            TotalCalories = workoutSession2.Calories;
            
        }
    }
}
