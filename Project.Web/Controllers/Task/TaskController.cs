using BAL.Task;
using BAL.Utility;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Task
{
    public class TaskController : Controller
    {
        SessionHelper session;
        TaskManager objTaskManager = new TaskManager();
        //
        // GET: /Task/

        [Authorize]
        [HttpPost]
        public ActionResult AjaxAddTask(string Titele, string StartDate, string EndDate, string StartTime, string EndTime, string RelateTo, string Description, string RemindMe, string Hours, string Minutes, string AssignTo)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            LeadModel objModel = new LeadModel();
            try
            {
                if (AssignTo == "" || AssignTo == null)
                {
                    AssignTo = session.UserSession.UserId.ToString();
                }
                Response = objTaskManager.AddTask(Titele, BAL.Helper.Helper.ConvertToDateNullable(StartDate + " " + StartTime, "dd/MM/yyyy HH:mm"), BAL.Helper.Helper.ConvertToDateNullable(StartDate + " " + StartTime, "dd/MM/yyyy HH:mm"), Convert.ToInt64(RelateTo), Description, RemindMe, Hours, Minutes, "Planed", AssignTo, session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {                    
                    //objModel.Task = UtilityManager.getTasksByRelateToID(Convert.ToInt64(RelateTo), session.UserSession.UserType, session.UserSession.UserId);
                    //return View(objModel);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddTask conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
