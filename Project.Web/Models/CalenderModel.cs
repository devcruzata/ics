using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class CalenderModel
    {
        public int ID {get;set;}
        public string Title { get; set; }
        public int EventOwner { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string StatusString { get; set; }
        public string StatusColor { get; set; }
        public string ClassName { get; set; }


        
    }
}