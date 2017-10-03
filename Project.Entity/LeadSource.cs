using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class LeadSource
    {
        public long LeadSourceID { get; set; }

        public string SourceName { get; set; }

        public string Status { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }

        public long UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string LinkedSalesRep { get; set; }
    }
}
