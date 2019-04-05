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
using Android.Database;
using Xamarin.Forms;
using NFCAlarm.Droid;

[assembly: Dependency(typeof(RingtoneLoader))]
namespace NFCAlarm.Droid
{
    class RingtoneLoader : IRingtoneInterface
    {
        Ringtone ringtone;
        Context context = Android.App.Application.Context;

        public string[,] GetRingtones()
        {            
            RingtoneManager ringtoneManager = new RingtoneManager(context);
            string[,] uris = new string[ringtoneManager.Cursor.Count,2];
            for (int i = 0; i < ringtoneManager.Cursor.Count; i++)
            {
                uris[i,0] = ringtoneManager.GetRingtoneUri(i).ToString();
                uris[i, 1] = ringtoneManager.GetRingtone(ringtoneManager.GetRingtonePosition(ringtoneManager.GetRingtoneUri(i))).GetTitle(context);
                }
            return uris;
        }

        public void PlayRingtone(string uri)
        {
            RingtoneManager ringtoneManager = new RingtoneManager(context);
            Android.Net.Uri _uri = Android.Net.Uri.Parse(uri);

            AudioManager audioManager = (AudioManager)context.GetSystemService(Context.AudioService);

            if (ringtone != null)
            {
                if (ringtone.IsPlaying)
                {
                    ringtone.Stop();
                }
            }

            ringtone = ringtoneManager.GetRingtone(ringtoneManager.GetRingtonePosition(_uri));

            ringtone.StreamType = Stream.Alarm;

            ringtone.Play();
        }

        public void StopRintone(string name)
        {
            if(ringtone.GetTitle(context) == name)
            {
                if(ringtone.IsPlaying)
                {
                    ringtone.Stop();
                }
            }
        }
    }
}