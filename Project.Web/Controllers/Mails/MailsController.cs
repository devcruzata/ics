using BAL.Mail;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Mails
{
    public class MailsController : Controller
    {
        MailManager objMailManager = new MailManager();
        SessionHelper session;
        //
        // GET: /Mails/

       // [Authorize]
      //  [HttpPost]
        //public ActionResult SendMail(MailModel objMailModel)
        //{
        //    objResponse Response = new objResponse();
        //    Project.Entity.Mails objMail = new Project.Entity.Mails();
        //    try
        //    {
        //        objMail.ToAddress = objMailModel.ToAddress;
        //        objMail.RelateTo_ID = objMailModel.RelateTo_ID;
        //        objMail.RelateTo_Name = objMailModel.RelateTo_Name;
        //        objMail.CcAddress = objMailModel.CcAddress;
        //        objMail.BccAddress = objMailModel.BccAddress;
        //        objMail.FromAddress = objMailModel.FromAddress;
        //        objMail.MailBy_ID = objMailModel.MailBy_ID;
        //        objMail.MailBy_Name = objMailModel.MailBy_Name;
        //        objMail.Subject = objMailModel.Subject;
        //        objMail.MailBody = objMailModel.MailBody;
        //        objMail.Date = DateTime.Now.ToString();

        //        MailManager.SendSimpleMessageBySMTP("abhishekkhemariya29@gmail.com", "shabbir@cruzata.com", "abhishekkhemariya29@gmail.com", "abhishekkhemariya29@gmail.com", objMail.Subject, objMail.MailBody);

        //        return Json("success", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("SendMail Contro Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return Json("success", JsonRequestBehavior.AllowGet);
        //    }
        //}

        [Authorize]
        [HttpPost]
        public ActionResult SendEmail()
        {
            objResponse Response = new objResponse();
            Project.Entity.Mails objMail = new Project.Entity.Mails();
            session = new SessionHelper();
            try
            {

                Int64 LeadID = 0;


                if (Request.Form["LeadID"].ToString() != "")
                {
                    LeadID = Convert.ToInt64(Request.Form["LeadID"]);
                }


                string Toname = Request.Form["ToName"].ToString();
                string To = Request.Form["To"].ToString();
                string Cc = Request.Form["Cc"].ToString();
                string Bcc = Request.Form["Bcc"].ToString();
                string Subject = Request.Form["subject"].ToString();                
                string Body = Request.Form["body"].ToString();

                objMail.ToAddress = To;
                objMail.RelateTo_ID = LeadID;
                objMail.RelateTo_Name = Toname;
                objMail.CcAddress = Cc;
                objMail.BccAddress = Bcc;
                objMail.FromAddress = session.UserSession.Username;
                objMail.MailBy_ID = session.UserSession.UserId;
                objMail.MailBy_Name = session.UserSession.FullName;
                objMail.Subject = Subject;
                objMail.MailBody = Body;
                objMail.Date = DateTime.Now.ToString();


                var message = new MailMessage();

                message.To.Add(new MailAddress(To));
                if (objMail.CcAddress != "")
                {
                    message.CC.Add(new MailAddress(objMail.CcAddress));
                }
                if (objMail.BccAddress != "")
                {
                    message.Bcc.Add(new MailAddress(objMail.BccAddress));
                }
                
                message.Subject = Subject;
                message.Body = string.Format(Body, objMail.MailBy_Name, objMail.FromAddress, Body);
                message.IsBodyHtml = true;
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];                  

                    if (file != null && file.ContentLength > 0)
                    {
                        message.Attachments.Add(new Attachment(file.InputStream,Path.GetFileName(file.FileName)));
                    }
                }

                //var fromAddress = ConfigurationManager.AppSettings["smtpUser"].ToString();               
                //string fromPassword = ConfigurationManager.AppSettings["smtpPass"].ToString();
                //using(var smtp = new SmtpClient())
                //{
                //    var credential = new NetworkCredential
                //    {
                //        UserName = fromAddress,
                //        Password = fromPassword
                //    };
                //    smtp.Credentials = credential;
                    
                //    smtp.Host = ConfigurationManager.AppSettings["smtpServer"].ToString();
                //    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString());
                //    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpSSL"].ToString());
                //    //DeliveryMethod = SmtpDeliveryMethod.Network,
                //    //UseDefaultCredentials = false,
                //    //Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                //     smtp.Send(message);
                //}

                BAL.Helper.Helper.SendEmail(message);
                //BAL.Helper.Helper.SendEmailUsingGoDaddy(message);
                return Json("success", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SendEmail Post method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
