using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using JumpApp.Services;
using Plugin.SimpleAudioPlayer;
using System.IO;
using Microsoft.Identity.Client;
using JumpApp.Models;

namespace JumpApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShakeSetup : ContentPage
    {

        public UserInfoRepository userRepository = new UserInfoRepository();
        //AuthenticationResult authenticationResult;
        AccelerometerInit accelerometerInit { get; set; }
        IAzureRestService dataStore = new AzureRestService();
        public IDbSync dbSync = new DbSync();
        private bool _isRunning;
        CoreUserInfo coreUserInfo = new CoreUserInfo();

        public ShakeSetup()
        {
            InitializeComponent();
            coreUserInfo = Task.Run(async () => { return await dataStore.GetCoreUserInfo(App.userContext.UserIdentifier); }).Result;
            //authenticationResult = result;
            accelerometerInit = new AccelerometerInit(coreUserInfo);
            ThresholdSlider.Value = accelerometerInit.ShakeThreshold;
            ThresholdValue.Text = ThresholdSlider.Value.ToString();
            ThresholdSlider.ValueChanged += ThresholdSlider_ValueChanged;
        }

        private void ThresholdSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ThresholdValue.Text = ThresholdSlider.Value.ToString();
            accelerometerInit.ShakeThreshold = ThresholdSlider.Value;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            accelerometerInit.ToggleAccelerometer();
            if (lblshake.Text == "On")
            {
                _isRunning = false;
                lblshake.Text = "Off";
                btnStart.Text = "Start";
            }
            else
            {

                _isRunning = true;
                RunTimer();
                btnStart.Text = "Stop";
                lblshake.Text = "On";
            }

        }
        public void RunTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), () =>
            {
                lblCounter.Text = "Jumps: " + accelerometerInit.counter.ToString();
                return _isRunning;
            });
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if(lblshake.Text == "On")
            {
                accelerometerInit.ToggleAccelerometer();
            }
            
            await AcceptSensitivity();
        }
        private async Task AcceptSensitivity()
        {
            try
            {
               // var user = userRepository.GetAllUserInfo().FirstOrDefault();
               // user.Sensetivity = Convert.ToInt32(ThresholdSlider.Value);
                //userRepository.UpdateUserInfo(user);
                //await Navigation.PushAsync(new MainPage(authenticationResult));
                
                coreUserInfo.Sensetivity = Convert.ToInt32(ThresholdSlider.Value);
                await dataStore.UpdateCoreUserInfo(coreUserInfo);
                //userRepository.UpdateUserInfo(coreUserInfo);
                //await dbSync.SyncCoreUserInfo(coreUserInfo, "From Azure");
                await Navigation.PushAsync(new MainPage());
                string stop = "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}