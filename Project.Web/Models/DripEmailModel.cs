using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class DripEmailModel
    {
        public string destinationEmail { get; set; }

        public string emailTemplate { get; set; }
    }
}