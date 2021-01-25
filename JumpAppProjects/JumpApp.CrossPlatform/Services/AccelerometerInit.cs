using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using JumpApp.Models;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace JumpApp.Services
{
    public class AccelerometerInit
    {
        public IUserInfoRepository userRepository = new UserInfoRepository();
        private ISimpleAudioPlayer _simpleAudioPlayer;
        SensorSpeed speed = SensorSpeed.Fastest;
        private readonly IAzureRestService azureRestServ;
        CoreUserInfo user = new CoreUserInfo();
        bool hasUpdated = false;
        DateTime lastUpdate;
        float last_x = 0.0f;
        float last_y = 0.0f;
        float last_z = 0.0f;
        public float x = 0.0f;
        public float y = 0.0f;
        public float z = 0.0f;
        public float testx = 0.0f;
        public float testy = 0.0f;
        public float testz = 0.0f;
        //Previous value
        //int ShakeDetectionTimeLapse = 100;
        int ShakeDetectionTimeLapse = 200;
        public double ShakeThreshold = 250;
        public int counter = 0;


        public AccelerometerInit(CoreUserInfo User)
        {
            azureRestServ = DependencyService.Get<IAzureRestService>();
            _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            Stream beepStream = GetType().Assembly.GetManifestResourceStream("JumpApp.Sounds.shukran__beep.wav");
            bool isSuccess = _simpleAudioPlayer.Load(beepStream);
            user = User;
           // var user = userRepository.GetAllUserInfo().FirstOrDefault();
            if (user != null && user.Sensetivity != 0)
            {
                ShakeThreshold = user.Sensetivity;
            }

        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            x = e.Reading.Acceleration.X;
            y = e.Reading.Acceleration.Y;
            z = e.Reading.Acceleration.Z;

            DateTime curTime = System.DateTime.Now;
            if (hasUpdated == false)
            {
                hasUpdated = true;
                lastUpdate = curTime;
                last_x = x;
                last_y = y;
                last_z = z;
            }
            else
            {
                if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
                {
                    float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
                    lastUpdate = curTime;
                    float total = y - last_y;
                    float speed = total / diffTime * 10000;
                    //if(y > 1)
                    //{
                        if (speed > ShakeThreshold)
                        {

                            // DependencyService.Get<IMessage>().ShortAlert("shake detected w/ speed: " + speed);
                            var duration = TimeSpan.FromSeconds(0.1);
                            Vibration.Vibrate(duration);
                            counter++;
                            //lblCounter.Text = "Jumps: " + counter.ToString();
                            _simpleAudioPlayer.Play();
                            Debug.WriteLine("x:" + x.ToString() + " y:" + y.ToString() + " z:" + z.ToString());
                            Debug.WriteLine("Total: " + total.ToString());
                            Debug.WriteLine("Speed: " + speed.ToString());
                            testx = x;
                            testy = y;
                            testz = z;

                        }
                   // }
                    Debug.WriteLine(" y:" + y.ToString());

                    last_x = x;
                    last_y = y;
                    last_z = z;
                }
            }
            //else
            //{
            //    if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
            //    {
            //        float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
            //        lastUpdate = curTime;
            //        float total = x + y + z - last_x - last_y - last_z;
            //        float speed = total / diffTime * 10000;

            //        if (speed > ShakeThreshold)
            //        {

            //            // DependencyService.Get<IMessage>().ShortAlert("shake detected w/ speed: " + speed);
            //            var duration = TimeSpan.FromSeconds(0.1);
            //            Vibration.Vibrate(duration);
            //            counter++;
            //            //lblCounter.Text = "Jumps: " + counter.ToString();
            //            _simpleAudioPlayer.Play();
            //            Debug.WriteLine("x:" + x.ToString() + " y:" + y.ToString() + " z:" + z.ToString());
            //            Debug.WriteLine("Total: " + total.ToString());
            //            Debug.WriteLine("Speed: " + speed.ToString());
            //            testx = x;
            //            testy = y;
            //            testz = z;

            //        }

            //        last_x = x;
            //        last_y = y;
            //        last_z = z;
            //    }
            //}

        }
        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                {
                    Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                    Accelerometer.Stop();
                }

                else
                {
                    Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                    Accelerometer.Start(speed);
                    counter = 0;
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
