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
                alarms = new Alarm[1];
                alarms[0] = new Alarm() { Name = "Test" };
                fileManager.SaveAlarms(alarms);
                alarmsTextList.ItemsSource = alarms.ToList();
            }
            firstboot = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (firstboot)
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
                    alarms = new Alarm[1];
                    alarms[0] = new Alarm() { Name = "Test" };
                    fileManager.SaveAlarms(alarms);
                    alarmsTextList.ItemsSource = alarms.ToList();
                }
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
                    btn.Source = alarms[i].ImageName;
                    btn.HeightRequest = 120;
                    btn.WidthRequest = 120;
                    break;
                }
            }
        }

        private void AlarmsTextList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            alarmsTextList.SelectedItem = null;
        }

        private void BtnTest_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlarmPage());
        }
    }
}
