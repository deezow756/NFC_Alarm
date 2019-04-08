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
        public string Minute { get; set; }
        public string Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool SoundStatus { get; set; }
        public string SoundUri { get; set; }
        public string SoundName { get; set; }
        public bool Vibrate { get; set; }
        //public string VibrationName { get; set; }
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
            var time = DateTime.Now;
            if (time.Hour.ToString().Length == 1)
            {
                Hour = "0" + time.Hour.ToString();
            }
            else
                Hour = time.Hour.ToString();
            if (time.Minute.ToString().Length == 1)
            {
                Minute = "0" + time.Minute.ToString();
            }
            else
                Minute = time.Minute.ToString();

            Day = time.Day;
            Month = time.Month;
            Year = time.Year;
            SoundStatus = false;
            SoundName = "";
            Vibrate = false;
            //VibrationName = "Basic Call";
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
        public string Time
        {
            get { return Hour.ToString() + ":" + Minute.ToString(); }
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
    }
}
