using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Entity;
using BAL.Dashboard;
using Project.Web.Models;

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
            DashboardManager objDashBoard = new DashboardManager();
            objResponse Response = new objResponse();
            DashboardModel objModel = new DashboardModel();
            try
            {
                Response = objDashBoard.GetDahboardData();
                if(Response.ErrorCode == 0)
                {
                    objModel.Memo = Response.ResponseData.Tables[0].Rows[0][0].ToString();             
                    return View(objModel);
                }
                else
                {
                    return View(objModel);
                }
                
            }
            catch(Exception ex)
            {
                return View(objModel);
            }            
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
