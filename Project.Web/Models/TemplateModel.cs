using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Entity;

namespace Project.Web.Models
{
    public class TemplateModel
    {
        public long TemplateID { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public string status{get;set;}

        public List<Templates> templates {get;set;}
    }
}