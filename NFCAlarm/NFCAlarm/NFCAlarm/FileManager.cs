using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NFCAlarm
{
    public class FileManager
    {
        private string alarmsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Alarm.json");

        public Alarm[] GetAlarms()
        {
            List<Alarm> alarms = new List<Alarm>();
            string[] jsons;

            if (File.Exists(alarmsPath))
            {
                jsons = File.ReadAllLines(alarmsPath);

                for (int i = 0; i < jsons.Length; i++)
                {
                    alarms.Add(JsonConvert.DeserializeObject<Alarm>(jsons[i]));
                }

                return alarms.ToArray();
            }
            else
                return null;
        }

        public void SaveAlarms(Alarm[] alarms)
        {
            List<string> jsons = new List<string>();

            for (int i = 0; i < alarms.Length; i++)
            {
                jsons.Add(JsonConvert.SerializeObject(alarms[i]));
            }

            File.WriteAllLines(alarmsPath, jsons.ToArray());
        }

        public void SaveAlarm(Alarm alarm)
        {
            Alarm[] alarms = GetAlarms();

            for (int i = 0; i < alarms.Length; i++)
            {
                if(alarms[i].ID == alarm.ID)
                {
                    alarms[i] = alarm;
                    break;
                }
            }

            SaveAlarms(alarms);
        }

        public void DeleteAlarm(Alarm alarm)
        {
            List<Alarm> alarms = new List<Alarm>(GetAlarms());

            for (int i = 0; i < alarms.Count; i++)
            {
                if(alarm.ID == alarms[i].ID)
                {
                    alarms.Remove(alarms[i]);
                }
            }

            SaveAlarms(alarms.ToArray());
        }
    }
}
