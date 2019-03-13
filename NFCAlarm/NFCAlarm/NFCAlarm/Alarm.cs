using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public class Alarm
    {
        public string Name { get; set; }
        public DateTime TimeDate { get; set; }
        public string RingTone { get; set; }
        public bool Vibrate { get; set; }
        public int Snooze { get; set; }
        public bool Status { get; set; }

        public string ImageName
        {
            get
            {
                if(Status)
                {
                    return "toggle_on.png";
                }
                else
                {
                    return "toggle_off.png";
                }
            }
        }

        public string Time
        {
            get
            {
                return TimeDate.Hour.ToString() + ":" + TimeDate.Minute.ToString();
            }
        }

        public string ClassID
        {
            get
            {
                return Name + Time;
            }
        }

        public void Toggle()
        {
            if(Status)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }
        }
    }
}
