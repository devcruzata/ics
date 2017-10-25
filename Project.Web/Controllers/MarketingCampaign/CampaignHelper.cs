using Project.ViewModel;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Project.Web.Controllers.MarketingCampaign
{
    public class CampaignHelperss
    {

        public static string PopulateEmailBody(string body, string templatePath)
        {
            string template = string.Empty;
            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
            {
                template = reader.ReadToEnd();
            }
            template = template.Replace("{body}", body);

            //template = template.Replace("{name}", leadName);
            return template;
        }

        public async Task<bool> sendCampaign(List<TextValue> toList,string tempalte)
        {
              var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("dev.cruzata@gmail.com", "MAS");
            var subject = "MAS";
            List<EmailAddress> recievers = new List<EmailAddress>();
            foreach (var to in toList)
            {
                EmailAddress rcv = new EmailAddress(to.Value, to.Text);
                recievers.Add(rcv);
            }

            //var tos = new EmailAddress("abhishekkhemariya29@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = PopulateEmailBody(tempalte, ConfigurationManager.AppSettings["mailTemplatedir"].ToString());
             var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recievers, subject, plainTextContent, htmlContent);
            //var msg = MailHelper.CreateSingleEmail(from, tos, subject, plainTextContent,htmlContent);
            var response = await client.SendEmailAsync(msg);

            return true;
        }
    }
}