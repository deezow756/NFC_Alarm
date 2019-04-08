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
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, NfcAdapter.IOnNdefPushCompleteCallback, NfcAdapter.ICreateNdefMessageCallback
    {
        public static string code;
        public static bool startAlarm;

        public bool isForgroundDispatch = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AppData.adapter = NfcAdapter.GetDefaultAdapter(this);
            AppData.activity = this;
            AppData.mainActivity = this;

            SetBeamAktive(false);

            LoadApplication(new App(startAlarm));
        }

        public static void PushCode()
        {
            AppData.mainActivity.SetBeamAktive(true);
        }

        protected override void OnNewIntent(Intent intent)
        {
            Intent = intent;
            if (NfcAdapter.ActionNdefDiscovered == Intent.Action)
            {
                ProcessIntent(intent);           
            }
        }

        public void OnNdefPushComplete(NfcEvent e)
        {
            SetBeamAktive(false);
            EnableBackground();
        }

        public NdefMessage CreateNdefMessage(NfcEvent e)
        {
            DateTime time = DateTime.Now;
            string text = code;
            NdefMessage msg = new NdefMessage(new NdefRecord[] { CreateMimeRecord("sending code", Encoding.UTF8.GetBytes(text)) });
            return msg;
        }

        public NdefRecord CreateMimeRecord(String mimeType, byte[] payload)
        {
            byte[] mimeBytes = Encoding.UTF8.GetBytes(mimeType);
            NdefRecord mimeRecord = new NdefRecord(
                NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
            return mimeRecord;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (NfcAdapter.ActionNdefDiscovered == Intent.Action) ProcessIntent(Intent);
        }

        private void DisableBackground()
        {
            if (AppData.mainActivity.isForgroundDispatch)
            {
                AppData.adapter.DisableForegroundDispatch(AppData.activity);
                AppData.mainActivity.isForgroundDispatch = false;
            }
        }

        public static void EnableBackground()
        {
            if (!AppData.mainActivity.isForgroundDispatch)
            {
                var intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ReceiverReplacePending);

                IntentFilter[] intentFilters = new IntentFilter[] { };
                var pendingIntent = PendingIntent.GetActivity(AppData.activity, 0, intent, 0);

                AppData.adapter.EnableForegroundDispatch(AppData.activity, pendingIntent, intentFilters, null);
                AppData.mainActivity.isForgroundDispatch = true;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            DisableBackground();
        }        

        private void ProcessIntent(Intent intent)
        {
            IParcelable[] rawMsgs = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
            // only one message sent during the beam
            NdefMessage msg = (NdefMessage)rawMsgs[0];
            // record 0 contains the MIME type, record 1 is the AAR, if present
            AlarmPage.record = Encoding.UTF8.GetString(msg.GetRecords()[0].GetPayload());
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

        public void SetBeamAktive(bool active)
        {
            if (active)
            {
                // Beam zum senden aktivieren
                AppData.adapter.SetNdefPushMessageCallback(this, this);      // Callback zum setzen einer NDEF message
                AppData.adapter.SetOnNdefPushCompleteCallback(this, this);   // Callback zum "horchen" für erfolgreiche Sendung der Nachricht
            }
            else
            {

                AppData.adapter.SetNdefPushMessageCallback(null, this);
                AppData.adapter.SetOnNdefPushCompleteCallback(null, this);
            }
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