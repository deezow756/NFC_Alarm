using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using NFCAlarm.Droid;

[assembly: Dependency(typeof(NfcLoader))]
namespace NFCAlarm.Droid
{
    public class NfcLoader : INfcInterface
    {
        public void PushMessage(string code)
        {
            MainActivity.code = code;
            MainActivity.PushCode();
        }
    }
}