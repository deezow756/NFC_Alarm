using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class XAlarmManager
    {
        public void SetAlarm(int id, int min, int hour, int day, int month, int year)
        {
            DependencyService.Get<IAlarmManager>().SetAlarm(id, min, hour, day, month, year);
        }

        public void CancelAlarm(int id)
        {
            DependencyService.Get<IAlarmManager>().CancelAlarm(id);
        }
    }
}
