using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using NFCAlarm.Droid;

[assembly: Dependency(typeof(CloseAppLoader))]
namespace NFCAlarm.Droid
{
    public class CloseAppLoader : ICloseApp
    {
        public void CloseApplication()
        {
            Activity activity = (Activity)Android.App.Application.Context;
            activity.FinishAffinity();
        }
    }
}