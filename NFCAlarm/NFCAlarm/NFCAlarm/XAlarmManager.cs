using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class XAlarmManager
    {
        public void SetAlarm(Alarm alarm)
        {
            DependencyService.Get<IAlarmManager>().SetAlarm(alarm);
        }

        public void CancelAlarm(Alarm alarm)
        {
            DependencyService.Get<IAlarmManager>().CancelAlarm(alarm);
        }
    }
}