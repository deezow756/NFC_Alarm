using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using System;
using System.IO;
using System.Text;

namespace NFCAlarm.Droid
{
    [Activity(Label = "NFCAlarm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    //[IntentFilter(new[] { NfcAdapter.ActionTechDiscovered })]
    //[MetaData(NfcAdapter.ActionTechDiscovered, Resource = "@xml/nfc")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, NfcAdapter.ICreateNdefMessageCallback, NfcAdapter.IOnNdefPushCompleteCallback
    {        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            NfcAdapter adapter = NfcAdapter.GetDefaultAdapter(this);

            LoadApplication(new App());
        }

        public NdefMessage CreateNdefMessage(NfcEvent e)
        {
            throw new NotImplementedException();
        }

        public void OnNdefPushComplete(NfcEvent e)
        {
            throw new NotImplementedException();
        }
    }
}