using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Sms
{
   public class SmsResponse
    {
        public string recepientCount { get; set; }

        public string to { get; set; }

        public string messageid { get; set; }

        public string result { get; set; }

        public string errortext { get; set; }

        public string price { get; set; }

        public string currency_symbol { get; set; }

        public string currency_type { get; set; }
    }
}
