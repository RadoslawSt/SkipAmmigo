using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JumpApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JumpApp.Services;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;
using JumpApp.ViewModels;
namespace JumpApp.Views.MyWorkouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyWorkoutsPerMinPage : ContentPage
    {
        IWorkoutSessionPerMinRepository workoutSessionPerMinRepo = new WorkoutSessionPerMinRepository();
        List<WorkoutSessionPerMin> workoutDetailsList = new List<WorkoutSessionPerMin>();
        List<Entry> entries = new List<Entry>();
        WorkoutSessionPerMinListViewModel workoutSessionPerMinListViewModel; 
        //List<Entry> entries = new List<Entry>
        //{
        //    new Entry(200)
        //    {
        //        Color = SKColor.Parse("#FF1493"),
        //        Label = "January",
        //        ValueLabel = "200"
        //    },
        //    new Entry(400)
        //    {
        //        Color = SKColor.Parse("#00BFFF"),
        //        Label = "February",
        //        ValueLabel = "400"
        //    },
        //    new Entry(-100)
        //    {
        //        Color = SKColor.Parse("#00CED1"),
        //        Label = "March",
        //        ValueLabel = "-100"
        //    }
        //};

        public MyWorkoutsPerMinPage(int sessionId, DateTime workoutDate)
        {
            InitializeComponent();

            workoutSessionPerMinListViewModel = new WorkoutSessionPerMinListViewModel(sessionId, workoutDate);
            BindingContext = workoutSessionPerMinListViewModel;
            PrepareGraph(sessionId);
            //chartView.Chart = new LineChart { Entries = entries };
            
        }
        public void PrepareGraph(int sessionID)
        {
            workoutDetailsList = workoutSessionPerMinRepo.GetWorkoutSessionDetails(sessionID);

            foreach(var item in workoutDetailsList)
            {
             
                Entry entry = new Entry(item.Jumps)
                {
                    Color = SKColor.Parse("#EA695C"),
                    Label = item.Minute.ToString(),
                    ValueLabel = item.Jumps.ToString()
                    
                };
                entries.Add(entry);
            }
            //chartView.Chart = new LineChart { Entries = entries };
            chartView.Chart = new BarChart { Entries = entries};
            chartView.Chart.LabelTextSize = 25;
        }
    }
}