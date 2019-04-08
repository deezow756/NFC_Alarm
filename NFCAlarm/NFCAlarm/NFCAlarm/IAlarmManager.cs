using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public interface IAlarmManager
    {
        void SetAlarm(Alarm alarm);
        void CancelAlarm(Alarm alarm);
    }
}
