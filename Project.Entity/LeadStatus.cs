using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class LeadStatus
    {
        public long LeadStatusID { get; set; }

        public string StatusName { get; set; }

        public string StatusColor { get; set; }

        public string emailTemplate { get; set; }

        public string smsTemplate { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }

        public long UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public DateTime UpdatedDate { get; set; }

       
    }
}
