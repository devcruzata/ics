using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Entity;

namespace Project.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult AdminHome()
        {
            return View();
        }

        [Authorize]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize]
        public ActionResult AdminDashboard_V_2()
        {
            return View();
        }

        [Authorize]
        public ActionResult testDashboard()
        {
            return View();
        }

        //[Authorize]
        //public ActionResult getChartData()
        //{
        //    objResponse response = new objResponse();
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}
