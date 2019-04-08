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
using NFCAlarm.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SetAlarmManager))]
namespace NFCAlarm.Droid
{
    public class SetAlarmManager : IAlarmManager
    {
        public void CancelAlarm(Alarm alarm)
        {
            Intent newIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            newIntent.SetData(Android.Net.Uri.Parse(alarm.ID.ToString()));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, alarm.ID, newIntent, 0);

            AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            manager.Cancel(pendingIntent);
        }

        public void SetAlarm(Alarm alarm)
        {
            Intent newIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            newIntent.SetData(Android.Net.Uri.Parse(alarm.ID.ToString()));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, alarm.ID, newIntent, 0);

            alarm.Intent = pendingIntent.ToString();

            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
            calendar.Set(CalendarField.Year, alarm.Year);
            calendar.Set(CalendarField.Month, alarm.Month - 1);
            calendar.Set(CalendarField.DayOfMonth, alarm.Day);
            calendar.Set(CalendarField.HourOfDay, int.Parse(alarm.Hour));
            calendar.Set(CalendarField.Minute, int.Parse(alarm.Minute));
            calendar.Set(CalendarField.Second, 0);

            AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            manager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }    
    }
}