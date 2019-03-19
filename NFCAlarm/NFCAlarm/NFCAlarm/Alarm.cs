using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace NFCAlarm
{
    public class Alarm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
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

        private string alarmsCounterPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AlarmCounter.json");

        public Alarm()
        {
            if (File.Exists(alarmsCounterPath))
            {
                string counter = File.ReadAllText(alarmsCounterPath);
                ID = int.Parse(counter);
            }
            else
            {
                File.WriteAllText(alarmsCounterPath, "0");
            }
            Name = "";
            var dateTime = DateTime.Now;
            Minute = dateTime.Minute;
            Hour = dateTime.Hour;
            Day = dateTime.Day;
            Month = dateTime.Month;
            Year = dateTime.Year;
            SoundStatus = false;
            SoundName = "";
            Vibrate = false;
            SnoozeStatus = false;
            SnoozeTime = 5;
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
            get { return Hour.ToString() + ":" + Minute.ToString(); }
        }

        [JsonIgnore]
        public string SnoozeName
        {
            get { return SnoozeTime.ToString() + " Minutes, " + SnoozeTimes.ToString() + " Times"; }
        }

        [JsonIgnore]
        public string ClassID
        {
            get { return ID.ToString(); }            
        }

        [JsonIgnore]
        public string VibrateToggleName
        {
            get
            {
                if (Vibrate)
                    return "On";
                else
                    return "Off";
            }
        }

        [JsonIgnore]
        public string SoundToggleName
        {
            get
            {
                if (SoundStatus)
                    return "On";
                else
                    return "Off";
            }
        }

        [JsonIgnore]
        public string SnoozeToggleName
        {
            get
            {
                if (SnoozeStatus)                
                    return "On";                
                else
                    return "Off";
            }
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
