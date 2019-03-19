using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public class Alarm
    {
        public string Name { get; set; }
        public DateTime TimeDate { get; set; }
        public bool SoundStatus { get; set; }
        public string SoundName
        {
            get
            {
                if(SoundName == "") { return "No Sound Selected Yet"; }
                else { return SoundName; }
            }
            set { SoundName = value; }
        }
        public bool Vibrate { get; set; }
        public bool SnoozeStatus { get; set; }
        public int SnoozeTime { get; set; }
        public int SnoozeTimes { get; set; }
        public bool Status { get; set; }

        private string toggleOn = "toggle_on.png";
        private string toggleOff = "toggle_off.png";

        public Alarm()
        {

            SoundStatus = false;
            Vibrate = false;
            SnoozeStatus = false;
            Status = false;
            SnoozeTime = 0;
            SnoozeTimes = 0;
            SoundName = "";
        }

        public string ImageName
        {
            get
            {
                if (Status) return toggleOn;
                else return toggleOff;
            }
        }

        public string SoundImageName
        {
            get
            {
                if (SoundStatus) return toggleOn;
                else return toggleOff;
            }
        }

        public string VibrateImageName
        {
            get
            {
                if (Vibrate) return toggleOn;
                else return toggleOff;
            }
        }

        public string SnoozeImageName
        {
            get
            {
                if (SnoozeStatus) return toggleOn;
                else return toggleOff;
            }
        }

        public string Time
        {
            get { return TimeDate.Hour.ToString() + ":" + TimeDate.Minute.ToString(); }
        }

        public string SnoozeName
        {
            get { return SnoozeTime.ToString() + " Minutes, " + SnoozeTimes.ToString() + " Times"; }
        }

        public string ClassID
        {
            get { return Name + Time; }
            
        }

        public void Toggle()
        {
            if (Status) Status = false;
            else Status = true;            
        }

        public void ToggleVibrate()
        {
            if (Vibrate) Vibrate = false;
            else Vibrate = true;
        }

        public void ToggleSound()
        {
            if (SoundStatus) SoundStatus = false;
            else SoundStatus = true;
        }

        public void ToggleSnooze()
        {
            if (SnoozeStatus) SnoozeStatus = false;
            else SnoozeStatus = true;
        }
    }
}
