using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NFCAlarm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetUpAlarm : ContentPage
	{
        public Alarm alarm;

        public SetUpAlarm()
        {
            InitializeComponent();
            Setup();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Alarm> alarms = new List<Alarm>();
            alarms.Add(alarm);
            
            listAlarmSound.ItemsSource = null;
            listAlarmVibrate.ItemsSource = null;

            listAlarmVibrate.ItemsSource = alarms;
            listAlarmSound.ItemsSource = alarms;
        }

        public SetUpAlarm (Alarm alarm)
		{
			InitializeComponent ();
            this.alarm = alarm;
            Setup();
		}

        private void Setup()
        {
            if(alarm != null)
            {
                DateTime dateTime = new DateTime(alarm.Year, alarm.Month, alarm.Day, int.Parse(alarm.Hour), int.Parse(alarm.Minute), 0);
                pickerTime.Time = dateTime.TimeOfDay;
                pickerDate.Date = dateTime.Date;
                txtName.Text = alarm.Name;
                SetupLists();
            }
            else
            {
                alarm = new Alarm();
                pickerTime.Time = DateTime.Now.TimeOfDay;
                pickerDate.Date = DateTime.Now.Date;
                SetupLists();
            }
        }

        private void SetupLists()
        {
            List<Alarm> alarms = new List<Alarm>();
            alarms.Add(alarm);            
            
            listAlarmVibrate.ItemsSource = alarms;
            listAlarmSound.ItemsSource = alarms;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            alarm.Name = txtName.Text;
            if (pickerTime.Time.Hours.ToString().Length == 1)
            {
                alarm.Hour = "0" + pickerTime.Time.Hours.ToString();
            }
            else
                alarm.Hour = pickerTime.Time.Hours.ToString();
            if (pickerTime.Time.Minutes.ToString().Length == 1)
            {
                alarm.Minute = "0" + pickerTime.Time.Minutes.ToString();
            }
            else
                alarm.Minute = pickerTime.Time.Minutes.ToString();
            alarm.Day = pickerDate.Date.Day;
            alarm.Month = pickerDate.Date.Month;
            alarm.Year = pickerDate.Date.Year;
            FileManager fileManager = new FileManager();
            fileManager.SaveAlarm(alarm);
            Navigation.PopAsync();
        }

        //private void BtnMonday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnTuesday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnWednesday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnThursday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnFriday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnSaturday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void BtnSunday_Clicked(object sender, EventArgs e)
        //{

        //}

        //private void VibrateTrigger_Tapped(object sender, EventArgs e)
        //{
        //    listAlarmVibrate.SelectedItem = null;
        //    Navigation.PushAsync(new SetUpVibrate(this));
        //}

        private void SoundTrigger_Tapped(object sender, EventArgs e)
        {
            listAlarmSound.SelectedItem = null;
            Navigation.PushAsync(new SetUpSoundxaml(this));
        }

        private void VibrateToggle_Clicked(object sender, EventArgs e)
        {
            var btn = ((ImageButton)sender);            

            if(alarm.Vibrate)
            {
                alarm.Vibrate = false;
                btn.Source = "toggle_off.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
            else
            {
                alarm.Vibrate = true;
                btn.Source = "toggle_on.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
        }

        private void SoundToggle_Clicked(object sender, EventArgs e)
        {
            var btn = ((ImageButton)sender);

            if (alarm.SoundStatus)
            {
                alarm.SoundStatus = false;
                btn.Source = "toggle_off.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
            else
            {
                alarm.SoundStatus = true;
                btn.Source = "toggle_on.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
        }
    }
}