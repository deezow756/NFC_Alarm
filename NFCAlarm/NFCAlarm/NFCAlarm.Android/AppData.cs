﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Nfc;

namespace NFCAlarm.Droid
{
    public class AppData
    {
        public static NfcAdapter adapter;
        public static Activity activity;
        public static MainActivity mainActivity;
    }
}