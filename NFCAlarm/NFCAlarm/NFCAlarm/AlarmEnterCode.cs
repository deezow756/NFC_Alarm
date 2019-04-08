using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class AlarmEnterCode
    {
        AlarmPage alarmPage;
        string code;

        StackLayout stackLayout;
        Entry entry;
        Button button;

        public AlarmEnterCode(AlarmPage alarmPage, StackLayout stackLayout, string code)
        {
            this.alarmPage = alarmPage;
            this.code = code;
            this.stackLayout = stackLayout;
            Setup();
        }

        private void Setup()
        {
            entry = new Entry();
            entry.Placeholder = "Enter Code Here";

            button = new Button();
            button.Text = "Press Me";
            button.Clicked += Button_Clicked;

            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(button);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(entry.Text == code)
            {
                Ringtones ringtones = new Ringtones();
                ringtones.StopRingtone(alarmPage.alarm.SoundName);
                alarmPage.CancelVibration = true;
                Vibration.Cancel();
                alarmPage.alarm.Status = false;
                FileManager fileManager = new FileManager();
                fileManager.SaveAlarm(alarmPage.alarm);
                CloseApp closeApp = new CloseApp();
            }
        }
    }
}
