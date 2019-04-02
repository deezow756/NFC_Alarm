using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NFCAlarm
{
    public class Nfc
    {
        public void PushMessage(string code)
        {
            DependencyService.Get<INfcInterface>().PushMessage(code);
        }
    }
}
