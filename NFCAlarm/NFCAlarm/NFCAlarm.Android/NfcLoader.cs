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
        private NdefMessage CreateNdefMessage(string code)
        {
            DateTime time = DateTime.Now;
            string text = code;
            NdefMessage msg = new NdefMessage(new NdefRecord[] { CreateMimeRecord ("sending code", Encoding.UTF8.GetBytes(text))
            /**
			* The Android Application Record (AAR) is commented out. When a device
			* receives a push with an AAR in it, the application specified in the AAR
			* is guaranteed to run. The AAR overrides the tag dispatch system.
			* You can add it back in to guarantee that this
			* activity starts when receiving a beamed message. For now, this code
			* uses the tag dispatch system.
			*/
            //,NdefRecord.CreateApplicationRecord("com.example.android.beam")
            });
            return msg;
        }

        private NdefRecord CreateMimeRecord(String mimeType, byte[] payload)
        {
            byte[] mimeBytes = Encoding.UTF8.GetBytes(mimeType);
            NdefRecord mimeRecord = new NdefRecord(
                NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
            return mimeRecord;
        }

        public void PushMessage(string code)
        {
            AppData.adapter.SetNdefPushMessage(CreateNdefMessage(code), AppData.activity, AppData.activity);
            MainActivity.EnableBackground();
        }
    }
}