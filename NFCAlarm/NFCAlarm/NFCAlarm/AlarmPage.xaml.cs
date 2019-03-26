﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;


using NdefLibrary.Ndef;
using Poz1.NFCForms.Abstract;
using System.Collections.ObjectModel;

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
            //if (alarms != null)
            //{
            //    for (int i = 0; i < alarms.Length; i++)
            //    {
            //        if (DateTime.Now.Hour == int.Parse(alarms[i].Hour))
            //        {
            //            if (DateTime.Now.Minute == int.Parse(alarms[i].Minute))
            //            {
            //                alarm = alarms[i];
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            alarm = new Alarm() { Name = "Test" };
            //}

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

        