using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NFCAlarm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SetUpSoundxaml : ContentPage
	{
        List<Sound> sounds = new List<Sound>();
        SetUpAlarm setup;

		public SetUpSoundxaml (SetUpAlarm setUpAlarm)
		{
			InitializeComponent ();
            setup = setUpAlarm;
            SetupLists();
		}

        private void SetupLists()
        {
            List<Alarm> alarms = new List<Alarm>();
            alarms.Add(setup.alarm);
            listSoundToggle.ItemsSource = alarms;

            Ringtones ringtones = new Ringtones();
            string[,] uris = ringtones.GetRingtones();

            for (int i = 0; i < uris.Length / 2; i++)
            {
                sounds.Add(new Sound() { Uri = uris[i,0], Name = uris[i,1]});
            }

            if(setup.alarm.SoundName == "")
            {
                setup.alarm.SoundName = sounds[0].Name;
                setup.alarm.SoundUri = sounds[0].Uri;            }

            for (int i = 0; i < sounds.Count; i++)
            {
                if (setup.alarm.SoundName == sounds[i].Name)
                {
                    sounds[i].Status = true;
                }
            }
            listSound.ItemsSource = sounds;
        }

        private void SoundToggle_Tapped(object sender, EventArgs e)
        {
            if (setup.alarm.SoundStatus)
            {
                setup.alarm.SoundStatus = false;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listSoundToggle.ItemsSource = alarms;
            }
            else
            {
                setup.alarm.SoundStatus = true;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listSoundToggle.ItemsSource = alarms;
            }
        }

        private void SoundSelected_Tapped(object sender, EventArgs e)
        {
            var ic = ((ImageCell)sender);
            listSound.SelectedItem = null;

            for (int i = 0; i < sounds.Count; i++)
            {
                if (ic.ClassId == sounds[i].Name)
                {
                    sounds[i].Status = true;
                    setup.alarm.SoundName = sounds[i].Name;
                    setup.alarm.SoundUri = sounds[i].Uri;
                    Ringtones ringtones = new Ringtones();
                    ringtones.PlayRingtone(sounds[i].Uri);
                    StopSound(sounds[i].Name);
                }
                else
                    sounds[i].Status = false;
            }

            listSound.ItemsSource = null;
            listSound.ItemsSource = sounds;
        }

        private async void StopSound(string name)
        {
            await Task.Delay(6000);
            Ringtones ringtones = new Ringtones();
            ringtones.StopRingtone(name);
        }
    }
}