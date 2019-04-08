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
using Java.Util;

namespace NFCAlarm.Droid
{
    //[BroadcastReceiver(Enabled = true)]
    //[IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class OnBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
            {
                FileManager fileManager = new FileManager();
                Alarm[] alarms = fileManager.GetAlarms();

                for (int i = 0; i < alarms.Length; i++)
                {
                    if (alarms[i].Status)
                    {
                        Intent newIntent = new Intent(context, typeof(AlarmReceiver));
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, alarms[i].ID, newIntent, 0);

                        Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
                        calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                        calendar.Set(CalendarField.Year, alarms[i].Year);
                        calendar.Set(CalendarField.Month, alarms[i].Month);
                        calendar.Set(CalendarField.DayOfMonth, alarms[i].Day);
                        calendar.Set(CalendarField.HourOfDay, int.Parse(alarms[i].Hour));
                        calendar.Set(CalendarField.Minute, int.Parse(alarms[i].Minute));
                        calendar.Set(CalendarField.Second, 0);

                        AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
                        manager.SetExact(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + 5 * 1000, pendingIntent);
                    }
                }
            }
        }

    }
}
    
