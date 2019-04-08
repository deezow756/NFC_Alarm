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

namespace NFCAlarm.Droid
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            MainActivity.startAlarm = true;
            string id = intent.DataString;
            AlarmPage.id = id;
            Intent startIntent = new Intent(context, typeof(MainActivity));
            startIntent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(startIntent);
        }
    }
}