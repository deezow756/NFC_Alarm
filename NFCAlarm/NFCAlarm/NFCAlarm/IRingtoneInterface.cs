using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;

namespace NFCAlarm
{
    public interface IRingtoneInterface
    {
        String[,] GetRingtones();
        void PlayRingtone(string uri);
        void StopRintone(string uri);
    }
}
