using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;


using NdefLibrary.Ndef;
using Poz1.NFCForms.Abstract;
using System.Collections.ObjectModel;
using Android.Content;

namespace NFCAlarm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPage : ContentPage
    {
        public Alarm alarm;
        public static string record;
        public static string id;
        public string curCode;

        public bool CancelVibration = false;

        AlarmEnterCode enterCode;

        public AlarmPage()
        {
            InitializeComponent();

            FileManager fileManager = new FileManager();
            Alarm[] alarms = fileManager.GetAlarms();
            if (alarms != null)
            {
                alarm = alarms[0];
                if (id != null)
                {
                    for (int i = 0; i < alarms.Length; i++)
                    {
                        if (alarms[i].ID.ToString() == id)
                        {
                            alarm = alarms[i];
                        }
                    }

                    if (alarm != null)
                    {
                        txtName.Text = alarm.Name;
                        txtTime.Text = alarm.Time;
                        txtMessage.Text = "Scan Your Phone On Arduino";
                        PlaySound();
                        StartVibrate();
                        SendRandomCode();
                        CheckForRecord();
                    }
                }
            }
            else
            {
                txtName.Text = "No Alarms";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            CancelAlarm();
            return base.OnBackButtonPressed();
        }

        private void SendRandomCode()
        {
            Random random = new Random();
            int code = random.Next(999999);
            curCode = code.ToString();
            Nfc nfc = new Nfc();
            nfc.PushMessage(code.ToString());
        }

        private void PlaySound()
        {
            if (alarm.SoundStatus)
            {
                Ringtones ringtones = new Ringtones();
                ringtones.PlayRingtone(alarm.SoundUri);
            }
        }

        private async void StartVibrate()
        {
            if (alarm.Vibrate && CancelVibration == false)
            {
               MainThread.BeginInvokeOnMainThread( ()=>
               {
                   Vibration.Vibrate(3000);
               });
                
                await Task.Delay(3500);
                StartVibrate();
            }
        }

        public void CancelAlarm()
        {
            Ringtones ringtones = new Ringtones();
            ringtones.StopRingtone(alarm.SoundName);
            CancelVibration = true;
            Vibration.Cancel();
            alarm.Status = false;
            FileManager fileManager = new FileManager();
            fileManager.SaveAlarm(alarm);
            Navigation.PopAsync();
        }

        private async void CheckForRecord()
        {
            if(record == null)
            {
                await Task.Delay(500);
                CheckForRecord();
            }
            else if(record == "true")
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    enterCode = new AlarmEnterCode(this, CodeEntry, curCode);
                });                
                record = null;
            }
        }
    }
}

        