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
        Alarm alarm;
        public static string record;
        public string curCode;

        AlarmEnterCode enterCode;

        public AlarmPage()
        {
            InitializeComponent();

            FileManager fileManager = new FileManager();
            Alarm[] alarms = fileManager.GetAlarms();
            if (alarms != null)
            {
                alarm = alarms[0];
                //for (int i = 0; i < alarms.Length; i++)
                //{
                //    if (DateTime.Now.Hour == int.Parse(alarms[i].Hour))
                //    {
                //        if (DateTime.Now.Minute == int.Parse(alarms[i].Minute))
                //        {
                //            alarm = alarms[i];
                //            break;
                //        }
                //    }
                //}
                txtName.Text = alarm.Name;
                txtTime.Text = alarm.Time;
                txtMessage.Text = "Scan Your Phone On Arduino";
                PlaySound();
                StartVibrate();
                SendRandomCode();
                CheckForRecord();
            }
            else
            {
                txtName.Text = "No Alarms";
            }            
           }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PlaySound();
            StartVibrate();
            SendRandomCode();
            CheckForRecord();
        }

        protected override bool OnBackButtonPressed()
        {
            Ringtones ringtones = new Ringtones();
            ringtones.StopRingtone(alarm.SoundName);
            Vibration.Cancel();
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Ringtones ringtones = new Ringtones();
            ringtones.StopRingtone(alarm.SoundName);
            Vibration.Cancel();
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

        private void StartVibrate()
        {
            if (alarm.Vibrate)
            {
                Vibration.Vibrate(99999999);
            }
        }

        public void CancelAlarm()
        {
            Ringtones ringtones = new Ringtones();
            ringtones.StopRingtone(alarm.SoundName);
            Vibration.Cancel();
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
                enterCode = new AlarmEnterCode(this, CodeEntry, curCode);
                record = null;
            }
        }
    }
}

        