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
    public partial class SetUpVibrate : ContentPage
    {
        List<Vibrate> vibrates = new List<Vibrate>();
        SetUpAlarm setup;

        public SetUpVibrate(SetUpAlarm setUpAlarm)
        {
            InitializeComponent();
            setup = setUpAlarm;
            vibrates.Add(new Vibrate() { Name = "Basic Call" });
            vibrates.Add(new Vibrate() { Name = "HeartBeat" });
            vibrates.Add(new Vibrate() { Name = "Ticktock" });
            vibrates.Add(new Vibrate() { Name = "Waltz" });
            vibrates.Add(new Vibrate() { Name = "Zig-zig-zig" });
            vibrates.Add(new Vibrate() { Name = "Off-beat" });
            vibrates.Add(new Vibrate() { Name = "Spinning" });
            vibrates.Add(new Vibrate() { Name = "Siren" });
            vibrates.Add(new Vibrate() { Name = "Telephone" });
            vibrates.Add(new Vibrate() { Name = "Ripple" });
            SetupLists();
        }

        private void SetupLists()
        {
            List<Alarm> alarms = new List<Alarm>();
            alarms.Add(setup.alarm);
            listVibrateToggle.ItemsSource = alarms;

            //if(setup.alarm.VibrationName == "" || setup.alarm.VibrationName == null)
            //{
            //    setup.alarm.VibrationName = "Basic Call";
            //}

            //for (int i = 0; i < vibrates.Count; i++)
            //{
            //    if (setup.alarm.VibrationName == vibrates[i].Name)
            //    {
            //        vibrates[i].Status = true;
            //    }
            //}
            listVibrate.ItemsSource = vibrates;
        }

        private void VibrateToggle_Tapped(object sender, EventArgs e)
        {
            if (setup.alarm.Vibrate)
            {
                setup.alarm.Vibrate = false;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listVibrateToggle.ItemsSource = alarms;
            }
            else
            {
                setup.alarm.Vibrate = true;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listVibrateToggle.ItemsSource = alarms;
            }
        }

        private void VibrateSelected_Tapped(object sender, EventArgs e)
        {
            var ic = ((ImageCell)sender);
            listVibrate.SelectedItem = null;

            for (int i = 0; i < vibrates.Count; i++)
            {
                if (ic.ClassId == vibrates[i].Name)
                {
                    vibrates[i].Status = true;
                    //setup.alarm.VibrationName = vibrates[i].Name;
                }
                else
                    vibrates[i].Status = false;
            }

            listVibrate.ItemsSource = null;
            listVibrate.ItemsSource = vibrates;
        }
    }
}