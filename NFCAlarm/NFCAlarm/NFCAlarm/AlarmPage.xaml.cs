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

        private readonly INfcForms device;

        public AlarmPage()
        {
            InitializeComponent();

            device = DependencyService.Get<INfcForms>();
            device.NewTag += HandleNewTag;
            device.TagConnected += device_TagConnected;
            device.TagDisconnected += device_TagDisconnected;
            

            FileManager fileManager = new FileManager();
            Alarm[] alarms = fileManager.GetAlarms();
            //if (alarms != null)
            //{
            //    for (int i = 0; i < alarms.Length; i++)
            //    {
            //        if (DateTime.Now.Hour == int.Parse(alarms[i].Hour))
            //        {
            //            if (DateTime.Now.Minute == int.Parse(alarms[i].Minute))
            //            {
            //                alarm = alarms[i];
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            alarm = new Alarm() { Name = "Test" };
            //}

            PlaySound();            
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
                Vibration.Vibrate();
            }
        }

        void HandleClicked(object sender, EventArgs e)
        {
            var spRecord = new NdefSpRecord
            {
                Uri = "www.lol.com",
                NfcAction = NdefSpActRecord.NfcActionType.DoAction,
            };
            spRecord.AddTitle(new NdefTextRecord
            {
                Text = "Things and Stuff",
                LanguageCode = "en"
            });
            // Add record to NDEF message
            var msg = new NdefMessage { spRecord };
            try
            {
                device.WriteTag(msg);
                DisplayAlert("yay", "Tag Successfully Writen Too", "Dismiss");
            }
            catch (Exception excp)
            {
                DisplayAlert("Error", excp.Message, "OK");
            }
        }

        void HandleNewTag(object sender, NfcFormsTag e)
        {

#if SILVERLIGHT
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsWriteable.IsToggled = e.IsWriteable;
                IsNDEFSupported.IsToggled = e.IsNdefSupported;

                if (e.TechList != null)
                    listTechs.ItemsSource = e.TechList;

                if (e.IsNdefSupported)
                    listRecords.ItemsSource = readNDEFMEssage(e.NdefMessage);
            });
#else

            IsWriteable.IsToggled = e.IsWriteable;
            IsNDEFSupported.IsToggled = e.IsNdefSupported;

            if (e.TechList != null)
                listTechs.ItemsSource = e.TechList;

            if (e.IsNdefSupported)
                listRecords.ItemsSource = readNDEFMEssage(e.NdefMessage);
            else
            {
                DisplayAlert("error", "NDEF is not supported", "Dismiss");
            }
#endif
        }

        private ObservableCollection<string> readNDEFMEssage(NdefMessage message)
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();

            if (message == null)
            {
                return collection;
            }

            foreach (NdefRecord record in message)
            {
                // Go through each record, check if it's a Smart Poster
                if (record.CheckSpecializedType(false) == typeof(NdefSpRecord))
                {
                    // Convert and extract Smart Poster info
                    var spRecord = new NdefSpRecord(record);
                    collection.Add("URI: " + spRecord.Uri);
                    collection.Add("Titles: " + spRecord.TitleCount());
                    collection.Add("1. Title: " + spRecord.Titles[0].Text);
                    collection.Add("Action set: " + spRecord.ActionInUse());
                }

                if (record.CheckSpecializedType(false) == typeof(NdefUriRecord))
                {
                    // Convert and extract Smart Poster info
                    var spRecord = new NdefUriRecord(record);
                    collection.Add("Text: " + spRecord.Uri);
                }
            }
            return collection;
        }

        void device_TagDisconnected(object sender, NfcFormsTag e)
        {
#if SILVERLIGHT
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsConnected.IsToggled = false;
            });
#else
            IsConnected.IsToggled = false;
#endif

        }

        void device_TagConnected(object sender, NfcFormsTag e)
        {

#if SILVERLIGHT
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsConnected.IsToggled = true;
            });
#else
            IsConnected.IsToggled = true;
#endif

        }
    }
}

        