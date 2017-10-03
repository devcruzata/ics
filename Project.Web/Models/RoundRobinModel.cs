using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class RoundRobinModel
    {
        public List<LeadSource> source { get; set; }

        public List<Users> salesRep { get; set; }

        public string salesRepId { get; set; }
    }
}