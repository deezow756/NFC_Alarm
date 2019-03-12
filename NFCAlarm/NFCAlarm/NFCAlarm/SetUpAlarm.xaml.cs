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
	public partial class SetUpAlarm : ContentPage
	{
        private Alarm alarm;

		public SetUpAlarm (Alarm alarm)
		{
			InitializeComponent ();
            this.alarm = alarm;
		}
	}
}