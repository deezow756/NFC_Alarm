using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NFCAlarm
{
    public partial class MainPage : ContentPage
    {
        Alarm[] alarms;
        bool firstboot = false;

        public MainPage()
        {
            InitializeComponent();
            SetUp();
            firstboot = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (firstboot)
            {
                SetUp();
            }
        }

        private void SetUp()
        {
            SetTime();
            FileManager fileManager = new FileManager();
            alarms = fileManager.GetAlarms();
            if (alarms != null)
            {
                alarmsTextList.ItemsSource = null;
                alarmsTextList.ItemsSource = alarms.ToList();
            }
            else
            {
                txtError.Text = "No Alarms";
            }
        }

        async void SetTime()
        {
            string mins = "";
            string hours = "";
            DateTime time = DateTime.Now;

            if (time.Hour.ToString().Length == 1)
            {
                hours = "0" + time.Hour.ToString();
            }
            else
                hours = time.Hour.ToString();
            if (time.Minute.ToString().Length == 1)
            {
                mins = "0" + time.Minute.ToString();
            }
            else
                mins = time.Minute.ToString();

            Device.BeginInvokeOnMainThread(() =>
            {
                txtTime.Text = hours + ":" + mins;
            });
            await Task.Delay(1000);
            SetTime();
        }

        private void ImageCell_Tapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);

            alarmsTextList.SelectedItem = null;

            Alarm alarm = null;

            for (int i = 0; i < alarms.Length; i++)
            {
                if(alarms[i].ClassID == vc.ClassId)
                {
                    alarm = alarms[i];
                }
            }

            if(alarm != null)
                Navigation.PushAsync(new SetUpAlarm(alarm));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var btn = ((ImageButton)sender);

            //alarmsButtonList.SelectedItem = null;

            for (int i = 0; i < alarms.Length; i++)
            {
                if (alarms[i].ClassID == btn.ClassId)
                {
                    alarms[i].Toggle();
                    if(alarms[i].Status)
                    {
                        XAlarmManager alarmManager = new XAlarmManager();
                        alarmManager.SetAlarm(alarms[i]);
                    }
                    else
                    {
                        XAlarmManager alarmManager = new XAlarmManager();
                        alarmManager.CancelAlarm(alarms[i]);
                    }
                    FileManager fileManager = new FileManager();
                    fileManager.SaveAlarm(alarms[i]);
                    btn.Source = alarms[i].ImageName;
                    btn.HeightRequest = 120;
                    btn.WidthRequest = 120;
                    break;
                }
            }
        }

        private async void ImageButton_ClickedDelete(object sender, EventArgs e)
        {
            var btn = ((ImageButton)sender);

            var result = await DisplayAlert("Warning", "Are You Sure You Want To Delete This Alarm", "Yes", "No");

            if(result)
            {
                FileManager fileManager = new FileManager();
                List<Alarm> lstalarms = new List<Alarm>(fileManager.GetAlarms());

                for (int i = 0; i < lstalarms.Count; i++)
                {
                    if(alarms[i].ClassID == btn.ClassId)
                    {
                        fileManager.DeleteAlarm(lstalarms[i]);
                        XAlarmManager alarmManager = new XAlarmManager();
                        alarmManager.CancelAlarm(lstalarms[i]);
                    }
                }
            }

            SetUp();
        }

            private void AlarmsTextList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            alarmsTextList.SelectedItem = null;
        }

        private void BtnAddAlarm_Clicked(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();
            if (alarms == null)
            {
                alarms = new Alarm[1];
                alarms[0] = new Alarm();               
            }
            else
            {
                List<Alarm> lstalarms = new List<Alarm>(alarms);
                lstalarms.Add(new Alarm());

                alarms = lstalarms.ToArray();
            }
            fileManager.SaveAlarms(alarms);
            SetUp();
        }
    }
}
