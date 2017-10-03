using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class GroupsModel
    {
        public long Group_ID { get; set; }

        public string Group_Name { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public List<Groups> groups { get; set; }
    }
}