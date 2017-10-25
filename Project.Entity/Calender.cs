﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Calender
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int EventOwner { get; set; }

        public string StartDateString { get; set; }

        public string EndDateString { get; set; }

        public string StatusString { get; set; }

        public string StatusColor { get; set; }

        public string ClassName { get; set; }

        public string EventColor { get; set; }
    }

    public class LeadEvent
    {
        public long ID;
        public string Title;
        public long Relatedlead;
        public string StartDateString;
        public string EndDateString;
        public string StatusString;
        public string StatusColor;
        public string ClassName;
    }
}
