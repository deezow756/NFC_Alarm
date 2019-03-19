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
        private Alarm alarm;

        public SetUpAlarm()
        {
            InitializeComponent();
            Setup();
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
                pickerTime.Time = alarm.TimeDate.TimeOfDay;
                pickerDate.Date = alarm.TimeDate.Date;
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
            listAlarmSnooze.ItemsSource = alarms;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {

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

        private void VibrateTrigger_Tapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);
            listAlarmVibrate.SelectedItem = null;
        }

        private void SoundTrigger_Tapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);
            listAlarmSound.SelectedItem = null;
        }

        private void SnoozeTrigger_Tapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);
            listAlarmSnooze.SelectedItem = null;
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

        private void SnoozeToggle_Clicked(object sender, EventArgs e)
        {
            var btn = ((ImageButton)sender);

            if (alarm.SnoozeStatus)
            {
                alarm.SnoozeStatus = false;
                btn.Source = "toggle_off.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
            else
            {
                alarm.SnoozeStatus = true;
                btn.Source = "toggle_on.png";
                btn.WidthRequest = 120;
                btn.HeightRequest = 120;
            }
        }
    }
}