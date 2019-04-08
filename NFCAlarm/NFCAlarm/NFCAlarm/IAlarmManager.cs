using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public interface IAlarmManager
    {
        void SetAlarm(int id, int min, int hour, int day, int month, int year);
        void CancelAlarm(int id);
    }
}
