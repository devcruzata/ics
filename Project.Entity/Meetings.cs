﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Meetings
    {
       public long Meeting_ID_PK { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Agenda { get; set; }

        public string Summary { get; set; }

        public long RelateTo { get; set; }

        public string RelateToName { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedDate { get; set; }

        public string Status { get; set; }
    }
}
