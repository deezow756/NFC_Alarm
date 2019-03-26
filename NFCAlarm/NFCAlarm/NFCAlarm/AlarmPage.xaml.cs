using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;

namespace NFCAlarm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPage : ContentPage
    {
        Alarm alarm;

        public AlarmPage()
        {
            InitializeComponent();
            FileManager fileManager = new FileManager();
            Alarm[] alarms = fileManager.GetAlarms();
            for (int i = 0; i < alarms.Length; i++)
            {
                if(DateTime.Now.Hour == int.Parse(alarms[i].Hour))
                {
                    if(DateTime.Now.Minute == int.Parse(alarms[i].Minute))
                    {
                        alarm = alarms[i];
                        break;
                    }
                }
            }

            PlaySound();
        }

        private void PlaySound()
        {
            if (alarm.SoundStatus)
            {
                Ringtones ringtones = new Ringtones();
                ringtones.PlayRingtone(alarm.SoundUri);
            }
        }

        private void StartVibrate()
        {
            if (alarm.Vibrate)
            {
                Vibration.Vibrate();
            }
        }
    }
}