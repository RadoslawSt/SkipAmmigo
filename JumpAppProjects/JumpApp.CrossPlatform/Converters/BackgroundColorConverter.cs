using JumpApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace JumpApp.Converters
{
    public class BackgroundColorConverter : IValueConverter
    {
        private readonly IRepositoryWrapper repoWrapper;
        public BackgroundColorConverter()
        {
            repoWrapper = DependencyService.Get<IRepositoryWrapper>();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> workoutSessionPerMinWorkoutSessionIDList = repoWrapper.WorkoutSessionPerMin.GetAllAvailableWorkoutSessionsPerMin();

            try
            {
                if(workoutSessionPerMinWorkoutSessionIDList.Contains((int)value))
                {
                    return Color.FromHex("#2E4057");
                }
                else
                {
                    return Color.FromHex("#8BA6A9");
                }
                //if (value != null)
                //{
                //    if ((bool)value == true)
                //        return Color.White;
                    
                //    else
                //        return Color.Gray;
                //}
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
