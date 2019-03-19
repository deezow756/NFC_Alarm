using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    class Repeat
    {
        public bool Status { get; set; }
        public string Times { get; set; }

        public string Name
        {
            get
            {
                if(int.Parse(Times) < 9)
                {
                    return Times + " Times";
                }
                else
                {
                    return "Continuously";
                }
            }
        }

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

        public Repeat()
        {
            Status = false;
        }
    }
}
