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
	public partial class SetUpSnooze : ContentPage
	{
        List<Interval> intervals = new List<Interval>();
        List<Repeat> repeats = new List<Repeat>();
        SetUpAlarm setup;

        public SetUpSnooze (SetUpAlarm setUpAlarm)
		{
			InitializeComponent ();
            setup = setUpAlarm;
            intervals.Add(new Interval() { Time = "5" });
            intervals.Add(new Interval() { Time = "10" });
            intervals.Add(new Interval() { Time = "15" });
            intervals.Add(new Interval() { Time = "20" });
            repeats.Add(new Repeat() { Times = "0" });
            repeats.Add(new Repeat() { Times = "3" });
            repeats.Add(new Repeat() { Times = "5" });
            repeats.Add(new Repeat() { Times = "9" });
            SetupLists();
		}

        private void SetupLists()
        {
            List<Alarm> alarms = new List<Alarm>();
            alarms.Add(setup.alarm);
            listSnoozeToggle.ItemsSource = alarms;
            if(setup.alarm.SnoozeTime == 0) { setup.alarm.SnoozeTime = 5; }

            for (int i = 0; i < intervals.Count; i++)
            {
                if(setup.alarm.SnoozeTime == int.Parse(intervals[i].Time))
                {
                    intervals[i].Status = true;
                }
            }
            listInterval.ItemsSource = intervals;

            for (int i = 0; i < repeats.Count; i++)
            {
                if(setup.alarm.SnoozeTimes == int.Parse(repeats[i].Times))
                {
                    repeats[i].Status = true;
                }
            }
            listRepeat.ItemsSource = repeats;

        }

        private void SnoozeToggle_Tapped(object sender, EventArgs e)
        {
            if(setup.alarm.SnoozeStatus)
            {
                setup.alarm.SnoozeStatus = false;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listSnoozeToggle.ItemsSource = alarms;
            }
            else
            {
                setup.alarm.SnoozeStatus = true;
                List<Alarm> alarms = new List<Alarm>();
                alarms.Add(setup.alarm);
                listSnoozeToggle.ItemsSource = alarms;
            }
        }

        private void IntervalSelected_Tapped(object sender, EventArgs e)
        {
            var ic = ((ImageCell)sender);
            listInterval.SelectedItem = null;

            for (int i = 0; i < intervals.Count; i++)
            {
                if (ic.ClassId == intervals[i].Time)
                {
                    intervals[i].Status = true;
                    setup.alarm.SnoozeTime = int.Parse(intervals[i].Time);
                }
                else
                    intervals[i].Status = false;
            }

            listInterval.ItemsSource = null;
            listInterval.ItemsSource = intervals;
        }

        private void RepeatSelected_Tapped(object sender, EventArgs e)
        {
            var ic = ((ImageCell)sender);
            listRepeat.SelectedItem = null;

            for (int i = 0; i < repeats.Count; i++)
            {
                if (ic.ClassId == repeats[i].Times)
                {
                    repeats[i].Status = true;
                    setup.alarm.SnoozeTimes = int.Parse(repeats[i].Times);
                }
                else
                    repeats[i].Status = false;
            }

            listRepeat.ItemsSource = null;
            listRepeat.ItemsSource = repeats;
        }
    }
}