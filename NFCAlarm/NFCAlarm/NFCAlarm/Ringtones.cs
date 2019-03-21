using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class Ringtones
    {
        public string[,] GetRingtones()
        {
            return DependencyService.Get<IRingtoneInterface>().GetRingtones();
        }

        public void PlayRingtone(string uri)
        {
            DependencyService.Get<IRingtoneInterface>().PlayRingtone(uri);
        }
        
        public void StopRingtone(string name)
        {
            DependencyService.Get<IRingtoneInterface>().StopRintone(name);

        }
    }
}
