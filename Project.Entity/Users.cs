using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Users
    {
        public long User_ID { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string URole { get; set; }

        public string URoleName { get; set; }

        public string Username { get; set; }

        public string TimeZone { get; set; }

        public string Agent_App_Link { get; set; }

        public string Processer { get; set; }

        public string Sales_No { get; set; }

        public string Sales_Id { get; set; }

        public string BirthDay { get; set; }

        public long CratedBy_ID { get; set; }

        public string CreatedBy_Name { get; set; }

        public DateTime CratedDate { get; set; }

        public long UpdatedBy_ID { get; set; }

        public string UpdatedBY_Name { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime LastLogin { get; set; }

        public string Status { get; set; }

        public string Password { get; set; }

        public string Group { get; set; }
    }
}
