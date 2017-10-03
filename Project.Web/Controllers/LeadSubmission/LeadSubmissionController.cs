using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Project.Entity;
using Project.Web.Models;

namespace Project.Web.Controllers.LeadSubmission
{
    public class LeadSubmissionController : Controller
    {
        //
        // GET: /LeadSubmission/

        public ActionResult LeadSubmission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeadSubmission(LeadSubmisionModel req)
        {
            objResponse Response = new objResponse();
           
            BAL.Leads.LeadsManager objLeadManager = new BAL.Leads.LeadsManager();
            LeadSubmision objLeads = new LeadSubmision();
            try
            {
                objLeads.businessName = req.businessName;
                objLeads.contactName = req.contactName;
                objLeads.email = req.email;
                objLeads.contactPhone = req.contactPhone;
                objLeads.secondaryPhone = req.secondaryPhone;
                objLeads.cooments = req.cooments;

                Response = objLeadManager.SubmitData(objLeads);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage.Equals("Lead Already Exists"))
                    {
                                   
                        return Json("",JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex){
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult mailTest()
        {
            return View();
        }

       

    }
}
