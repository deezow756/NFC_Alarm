using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    class Sound
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool Status { get; set; }

        public string Image
        {
            get
            {
                if (Status)
                {
                    return "radio_on.png";
                }
                else
                {
                    return "radio_off.png";
                }
            }
        }

        public Sound()
        {
            Status = false;
        }
    }
}
