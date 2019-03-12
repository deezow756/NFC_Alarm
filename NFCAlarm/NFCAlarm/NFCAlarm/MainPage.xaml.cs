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

        public MainPage()
        {
            InitializeComponent();
            FileManager fileManager = new FileManager();
            alarms = fileManager.GetAlarms();
            if(alarms != null)
            {
                alarmsList.ItemsSource = alarms.ToList();
            }
            else
            {
                alarms = new Alarm[1];
                alarms[0] = new Alarm(){ Name = "Test", Status = false, TimeDate = DateTime.Now };
                fileManager.SaveAlarms(alarms);
                alarmsList.ItemsSource = alarms.ToList();
            }
            SetTime();
        }

        async void SetTime()
        {
            DateTime time = DateTime.Now;
            Device.BeginInvokeOnMainThread(() =>
            {
                txtTime.Text = time.Hour.ToString() + ":" + time.Minute.ToString();
            });
            await Task.Delay(1000);
            SetTime();
        }

        private void ImageCell_Tapped(object sender, EventArgs e)
        {
            var ic = ((ImageCell)sender);

            alarmsList.SelectedItem = null;

            Alarm alarm = null;

            for (int i = 0; i < alarms.Length; i++)
            {
                if(alarms[i].Name == ic.Text)
                {
                    alarm = alarms[i];
                }
            }

            if(alarm != null)
                Navigation.PushAsync(new SetUpAlarm(alarm));
        }
    }
}
