﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class CloseApp
    {
        public CloseApp()
        {
            DependencyService.Get<ICloseApp>().CloseApplication();
        }
    }
}