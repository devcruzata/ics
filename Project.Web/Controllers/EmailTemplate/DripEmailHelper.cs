using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using BAL.Helper;
using BAL.Utility;
using Project.Entity;
using Project.Web.Models;

namespace Project.Web.Controllers.EmailTemplate
{
    public class DripEmailHelper
    {
        public bool shootEmail(long recieverId)
        {
            DripEmailModel objdripEmail = new DripEmailModel();
            objResponse Response = new objResponse();
            try
            {
                Response =  UtilityManager.getEmailTemplate(recieverId);

                string mailBody = PopulateBody(Response.ResponseData.Tables[0].Rows[0][0].ToString(), Response.ResponseData.Tables[1].Rows[0][1].ToString(), ConfigurationManager.AppSettings["mailTemplatedir"].ToString());
                string toEmail = Response.ResponseData.Tables[1].Rows[0][0].ToString();

                if (Helper.SendEmail(toEmail, "ICS", mailBody))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string PopulateBody(string body,string leadName ,string templatePath)
        {
            string template = string.Empty;
            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
            {
                template = reader.ReadToEnd();
            }
            template = template.Replace("{body}", body);

            template = template.Replace("{name}", leadName);
            return template;
        }
    }
}