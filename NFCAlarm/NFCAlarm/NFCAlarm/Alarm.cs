using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NFCAlarm
{
    public class Alarm
    {
        public string Name { get; set; }
        public DateTime TimeDate { get; set; }
        public bool SoundStatus { get; set; }
        public string SoundPath { get; set; }
        public string SoundName { get; set; }
        public bool Vibrate { get; set; }
        public bool SnoozeStatus { get; set; }
        public int SnoozeTime { get; set; }
        public int SnoozeTimes { get; set; }
        public bool Status { get; set; }

        private string toggleOn = "toggle_on.png";
        private string toggleOff = "toggle_off.png";

        public Alarm()
        {
            Name = "";
            TimeDate = DateTime.Now;
            SoundStatus = false;
            SoundName = "";
            Vibrate = false;
            SnoozeStatus = false;
            SnoozeTime = 0;
            SnoozeTimes = 0;
            Status = false;          
        }

        [JsonIgnore]
        public string ImageName
        {
            get
            {
                if (Status) return toggleOn;
                else return toggleOff;
            }
        }

        [JsonIgnore]
        public string SoundImageName
        {
            get
            {
                if (SoundStatus) return toggleOn;
                else return toggleOff;
            }
        }

        [JsonIgnore]
        public string VibrateImageName
        {
            get
            {
                if (Vibrate) return toggleOn;
                else return toggleOff;
            }
        }

        [JsonIgnore]
        public string SnoozeImageName
        {
            get
            {
                if (SnoozeStatus) return toggleOn;
                else return toggleOff;
            }
        }

        [JsonIgnore]
        public string Time
        {
            get { return TimeDate.Hour.ToString() + ":" + TimeDate.Minute.ToString(); }
        }

        [JsonIgnore]
        public string SnoozeName
        {
            get { return SnoozeTime.ToString() + " Minutes, " + SnoozeTimes.ToString() + " Times"; }
        }

        [JsonIgnore]
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
