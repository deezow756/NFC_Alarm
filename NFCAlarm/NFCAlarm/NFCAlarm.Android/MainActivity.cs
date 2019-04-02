using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Views;
using Android.Widget;
using NFCAlarm.Droid;
using Poz1.NFCForms.Abstract;
using Poz1.NFCForms.Droid;
using System;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm.Droid
{
    [Activity(Label = "NFCAlarm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation), 
        IntentFilter(new[] { "android.nfc.action.ADAPTER_STATE_CHANGED" },
        Categories = new[] { "android.intent.category.DEFAULT" })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AppData.adapter = NfcAdapter.GetDefaultAdapter(this);
            AppData.activity = this;

            LoadApplication(new App());
        }

        protected override void OnNewIntent(Intent intent)
        {
            Intent = intent;
            if (NfcAdapter.ActionNdefDiscovered == Intent.Action)
            {
                IParcelable[] rawMsgs = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
                // only one message sent during the beam
                NdefMessage msg = (NdefMessage)rawMsgs[0];
                // record 0 contains the MIME type, record 1 is the AAR, if present
                AlarmPage.record = Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload());
                
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        private void DisableBackground()
        {
            AppData.adapter.DisableForegroundDispatch(AppData.activity);
        }

        public static void EnableBackground()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ReceiverReplacePending);

            IntentFilter[] intentFilters = new IntentFilter[] { };
            var pendingIntent = PendingIntent.GetActivity(AppData.activity, 0, intent, 0);

            AppData.adapter.EnableForegroundDispatch(AppData.activity, pendingIntent, null, null);
        }

        protected override void OnPause()
        {
            base.OnPause();
            AppData.adapter.DisableForegroundDispatch(this);
        }        

        private void ProcessIntent(Intent intent)
        {
            IParcelable[] rawMsgs = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
            // only one message sent during the beam
            NdefMessage msg = (NdefMessage)rawMsgs[0];
            // record 0 contains the MIME type, record 1 is the AAR, if present
            Toast.MakeText(AppData.activity, Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload()), ToastLength.Long);
            //string msg_typ = msg.GetType().ToString();
            //string rec_typ = msg.GetRecords()[0].GetType().ToString();
            //byte[] rec_typinfo = msg.GetRecords()[0].GetTypeInfo();
            //byte[] rec_id = msg.GetRecords()[0].GetId();
            //byte[] rec_daten = (byte[])msg.GetRecords()[0];
            //byte[] rec_nutzdaten = msg.GetRecords()[0].GetPayload();
            //cList.Clear();
            //cList.Add(ByteArrayToText(rec_daten));
            //ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, cList);
            //cListView.Adapter = ListAdapter;
        }      

        public static string BytearrayToHexstring(byte[] bytes)
        {
            string result = "";
            try
            {
                for (int i = 0; i < bytes.Length; i++) result += string.Format("{0:X2}", bytes[i]);
                return result;
            }
            catch
            {
                return "";
            }
        }

        public static string ByteArrayToText(byte[] bytes)
        {
            string text = "";
            try
            {
                int l = bytes.Length;
                for (int i = 0; i < l; i++) text += (char)bytes[i];
                return text;
            }
            catch
            {
                return "";
            }
        }
    }
}