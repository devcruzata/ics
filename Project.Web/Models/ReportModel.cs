using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ReportModel
    {
        public string sDateString { get; set; }
        public string eDateString { get; set; }
        public string reportType { get; set; }
        public DateTime? eDate { get; set; }
        public DateTime? sDate { get; set; }
        public bool hasReport { get; set; }
        public string errorMessage { get; set; }
        public List<ReportItem> Report { get; set; }
    }

    public class ReportItem
    {

        public string SeriolNo { set; get; }

        public string noOfLeads { get; set; }

        public string salesRepName { get; set; }

        public string sourceName { get; set; }
    }
}