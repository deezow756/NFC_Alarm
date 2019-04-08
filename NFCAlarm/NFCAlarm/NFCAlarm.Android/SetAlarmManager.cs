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
        public void CancelAlarm(int id)
        {
            Intent newIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, id, newIntent, 0);

            AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            manager.Cancel(pendingIntent);
        }

        public void SetAlarm(int id, int min, int hour, int day, int month, int year)
        {
            Intent newIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, id, newIntent, 0);

            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            calendar.TimeInMillis = SystemClock.CurrentThreadTimeMillis();
            calendar.Set(CalendarField.Year, year);
            calendar.Set(CalendarField.Month, month);
            calendar.Set(CalendarField.DayOfMonth, day);
            calendar.Set(CalendarField.HourOfDay, hour);
            calendar.Set(CalendarField.Minute, min);
            calendar.Set(CalendarField.Second, 0);

            AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            manager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }    
    }
}