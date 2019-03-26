using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace NFCAlarm.Droid
{
    class VibrateLoader
    {
        VibrateSetting vibrateSetting;
        VibrateType vibrateType;
        AudioAttributes audioAttributes;
        AudioManager audioManager;

        public void GetVibrations()
        {  
            audioManager = (AudioManager)Android.App.Application.Context.GetSystemService(Context.AudioService);
            vibrateSetting = new VibrateSetting();

        }
    }
}