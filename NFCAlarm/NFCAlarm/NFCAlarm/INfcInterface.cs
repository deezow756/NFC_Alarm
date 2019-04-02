using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public interface INfcInterface
    {
        void PushMessage(string code);
    }
}
