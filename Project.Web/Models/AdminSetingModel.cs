using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class AdminSetingModel
    {
        public long GenralSeting_ID_Auto_PK { get; set; }

        public long Customer_ID { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Stete { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Currency { get; set; }

        public List<AdminSeting> seting { get; set; }

        public List<Groups> groups { get; set; }

        public List<LeadSource> source { get; set; }

        public List<LeadStatus> status { get; set; }

        public List<UserRoles> roles { get; set; }
    }
}