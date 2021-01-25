using JumpApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JumpApp.Services
{
    public class ListviewColourDataTemplate : DataTemplateSelector
    {
        public DataTemplate HasData { get; set; }
        public DataTemplate MissingData { get; set; }
        private readonly IAzureRestService azureRestServ;
        private readonly IRepositoryWrapper repoWrapper;
        //private bool workoutSessionExist = false;
        public ListviewColourDataTemplate()
        {
            azureRestServ = DependencyService.Get<IAzureRestService>();
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            WorkoutSession item2 = (WorkoutSession)item;            
            List<int> workoutSessionPerMinWorkoutSessionIDList = repoWrapper.WorkoutSessionPerMin.GetAllAvailableWorkoutSessionsPerMin();
          
            // return ((List<string>)((ListView)container).ItemsSource).IndexOf(item as string) % 2 == 0 ? HasData : MissingData;
            //return ((List<string>)((ListView)container).ItemsSource).IndexOf(item as string) 
            if (workoutSessionPerMinWorkoutSessionIDList.Contains(item2.WorkoutSessionId))
            {
                //workoutSessionExist = true;
                return HasData;
            }
            return MissingData;
             
        }

    }
}
