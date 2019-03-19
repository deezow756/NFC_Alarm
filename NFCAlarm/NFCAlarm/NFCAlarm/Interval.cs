using System;
using System.Collections.Generic;
using System.Text;

namespace NFCAlarm
{
    public class Interval
    {
        public string Time { get; set; }
        public bool Status { get; set; }
        
        public string Name
        {
            get { return Time + " minutes"; }
        }

        public string Image
        {
            get
            {
                if(Status)
                {
                    return "radio_on.png";
                }
                else
                {
                    return "radio_off.png";
                }
            }
        }

        public Interval()
        {
            Status = false;
        }
    }
}
