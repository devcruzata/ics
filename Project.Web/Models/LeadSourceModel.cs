using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class LeadSourceModel
    {
        public long LeadSourceID { get; set; }

        public string SourceName { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public List<LeadSource> source { get; set; }
    }
}