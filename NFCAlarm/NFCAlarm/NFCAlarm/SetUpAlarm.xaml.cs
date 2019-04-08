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
            DateTime now = DateTime.Now;
            pickerDate.MinimumDate = now;
            pickerDate.MaximumDate = now.AddYears(1);
            if(alarm != null)
            {                
                DateTime dateTime = new DateTime(alarm.Year, alarm.Month, alarm.Day, int.Parse(alarm.Hour), int.Parse(alarm.Minute), 0);
                if (now.Year == alarm.Year)
                {
                    if (now.Month == alarm.Month)
                    {
                        if (now.Day > alarm.Day)
                        {
                            now.AddDays(1);
                            pickerDate.Date = now.Date;
                        }
                        else
                            pickerDate.Date = dateTime.Date;
                    }       
                    else if(now.Month > alarm.Month)
                    {
                        now.AddDays(1);
                        pickerDate.Date = now.Date;
                    }
                }
                else if (now.Year > alarm.Year)
                {
                    now.AddDays(1);
                    pickerDate.Date = now.Date;
                }
                pickerTime.Time = dateTime.TimeOfDay;
                txtName.Text = alarm.Name;
                SetupLists();
            }
            else
            {
                alarm = new Alarm();
                pickerTime.Time = DateTime.Now.TimeOfDay;
                pickerDate.Date = DateTime.Now.Date.AddDays(1);
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Save();
        }

        protected override bool OnBackButtonPressed()
        {
            Save();
            return base.OnBackButtonPressed();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
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
            if (alarm.Status)
            {
                XAlarmManager alarmManager = new XAlarmManager();
                alarmManager.CancelAlarm(alarm);
                alarmManager.SetAlarm(alarm);
            }
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

        bool propChange = true;
        private void PickerTime_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName && propChange)
            {
                DateTime now = DateTime.Now;
                if(pickerDate.Date.Day == now.Day)
                {
                    if(pickerTime.Time.Hours < now.Hour)
                    {
                        if(pickerTime.Time.Minutes < now.Minute)
                        {
                            propChange = false;
                            pickerTime.Time = now.AddMinutes(10).TimeOfDay;
                        }
                    }
                    else if(pickerTime.Time.Minutes < now.Minute)
                    {
                        propChange = false;
                        pickerTime.Time = now.AddMinutes(10).TimeOfDay;
                    }
                }
            }
            propChange = true;
        }
    }
}