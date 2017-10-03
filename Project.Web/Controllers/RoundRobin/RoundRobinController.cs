using BAL.User;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.RoundRobin
{
    public class RoundRobinController : Controller
    {

        BAL.Source.SourceManager objSourceManager = new BAL.Source.SourceManager();
        UserManager objUserManager = new UserManager();
        //
        // GET: /RoundRobin/
        [Authorize]
        public ActionResult Index()
        {
            RoundRobinModel objRRSet = new RoundRobinModel();
           
            objRRSet.source = objSourceManager.GetAllSource();
            objRRSet.salesRep = objUserManager.GetUsers();

            return View(objRRSet);
        }

        [Authorize]
        public ActionResult SaveSalesRep(string srcId, string srpId)
        {
            objResponse Response = new objResponse();
            RoundRobinModel objRRSet = new RoundRobinModel();
            try
            {
                   objSourceManager.LinkSource(Convert.ToInt64(srcId), Convert.ToInt64(srpId));

                   if (Response.ErrorCode == 0)
                   {
                       objRRSet.source = objSourceManager.GetAllSource();
                       objRRSet.salesRep = objUserManager.GetUsers();
                       return View("tempSetings",objRRSet);
                   }
                   else
                   {
                       return Json("", JsonRequestBehavior.AllowGet);
                   }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SaveSalesRep Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
