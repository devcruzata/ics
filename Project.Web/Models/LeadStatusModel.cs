using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class LeadStatusModel
    {
        public long LeadSourceID { get; set; }

        public string SourceName { get; set; }        

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public List<LeadStatus> status { get; set; }
    }
}