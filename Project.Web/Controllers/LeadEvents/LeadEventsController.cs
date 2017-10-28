using BAL.LeadEvents;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.LeadEvents
{
    public class LeadEventsController : Controller
    {
        LeadEventsManager objLeadEventManager = new LeadEventsManager();
         [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("GetLeads")]
        public ActionResult GetLeads()
        {

            List<TextValue> leads = objLeadEventManager.GetLeadForCalender();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Value = "0", Text = "Choose a Lead" });
            foreach (TextValue ld in leads)
            {
                list.Add(new SelectListItem { Value = ld.Value, Text = ld.Text });
            }

            JsonResult jResult = Json(list, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        [HttpPost]
        [ActionName("GetDisposition")]
        public ActionResult GetDisposition()
        {

            List<TextValue> disposition = objLeadEventManager.GetDispositionForCalender();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Value = "0", Text = "Choose a Disposition" });
            foreach (TextValue dp in disposition)
            {
                list.Add(new SelectListItem { Value = dp.Value, Text = dp.Text });
            }

            JsonResult jResult = Json(list, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        [Authorize]
        [HttpPost]
        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration, string RelatedLead, string Description)
        {
            return Project.Web.CalenderHelper.CalenderEvents.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration,RelatedLead,Description);
        }

        public JsonResult GetDiarySummary(double start, double end)
        {
            var fromDate = Project.Web.CalenderHelper.CalenderEvents.ConvertFromUnixTimestamp(start);
            var toDate = Project.Web.CalenderHelper.CalenderEvents.ConvertFromUnixTimestamp(end);
            var ApptListForDate = objLeadEventManager.GetEventsInDatetimeRange(fromDate, toDate);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                               // someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaryEvents(string start, string end)
        {
            //   var fromDate = Project.Web.CalenderHelper.CalenderEvents.ConvertFromUnixTimestamp(start);
            //  var toDate =   Project.Web.CalenderHelper.CalenderEvents.ConvertFromUnixTimestamp(end);
            var fromDate = Convert.ToDateTime(start);
            var toDate = Convert.ToDateTime(end);
            var ApptListForDate = objLeadEventManager.LoadEventsInDatetimeRange(fromDate,toDate);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                               // className = e.ClassName,
                                //someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEventInfo(string EventId)
        {
            objResponse Response = new objResponse();
            Response = objLeadEventManager.GetEventInfo(Convert.ToInt32(EventId));
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            LeadModel objModel = new LeadModel();

            objModel.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
            objModel.ContName = Response.ResponseData.Tables[0].Rows[0]["ContactName"].ToString();
            objModel.bPhone = Response.ResponseData.Tables[0].Rows[0]["BusinessPhone"].ToString();
            objModel.Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
            objModel.bPhone = Response.ResponseData.Tables[0].Rows[0]["BusinessPhone"].ToString();
            objModel.LeadSource = Response.ResponseData.Tables[0].Rows[0]["sourcename"].ToString();
            objModel.LeadStatus = Response.ResponseData.Tables[0].Rows[0]["Status"].ToString();
            objModel.LeadEventDuration = Response.ResponseData.Tables[0].Rows[0]["AppointmentLength"].ToString();
            objModel.LastNote = Response.ResponseData.Tables[1].Rows[0]["Note"].ToString();
            //string LeadEventStart = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["DateTimeScheduled"]).ToString("s");

            // temp1 = LeadEventStart.Split(' ').ToList();
            // objModel.LeadEventStartDate = temp1[0];
            // objModel.LeadEventStartTime = temp1[1] + " " + temp1[2];

            //string LeadEventEnd = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_end"]).AddDays(-1).ToString("MM/dd/yyyy hh:mm tt");
            //  string LeadEventEnd = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["DateTimeScheduled"]).AddMinutes(Convert.ToInt32(Response.ResponseData.Tables[0].Rows[0]["AppointmentLength"])).ToString("s");

            //  temp2 = LeadEventEnd.Split(' ').ToList();
            //  objModel.LeadEventEndDate = temp2[0];
            //  objModel.LeadEventEndTime = temp2[1] + " " + temp2[2];

            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public bool UpdateEvent(string EventId,string Title, string NewEventDate, string NewEventTime, string NewEventDuration, string NewDisposition, string Description)
        {
            return Project.Web.CalenderHelper.CalenderEvents.UpdateEvent(EventId, Title, NewEventDate, NewEventTime, NewEventDuration, NewDisposition, Description);
        }

        public JsonResult TransferEvent(string StartDate, string EndDate)
        {
            objResponse Response = new objResponse();
            SessionHelper session = new SessionHelper();
            List<Events> events = new List<Events>();
            List<string> temp1 = new List<string>();
            string nd=""; 
            try
            {
                //objLeadEventManager.test("0");
                //DateTime fromDate = Convert.ToDateTime(StartDate);
                //objLeadEventManager.test("1");
                //DateTime toDate = Convert.ToDateTime(StartDate).AddDays(1);
                //objLeadEventManager.test("2");
                //DateTime newToDate  = Convert.ToDateTime(EndDate);
                //objLeadEventManager.test("3");
                //List<string> temp4 = new List<string>();      
                //temp4 = newToDate.ToString().Split(' ').ToList();


                objLeadEventManager.test("5");
                DateTime fromDate = DateTime.ParseExact(StartDate, "MM/dd/yy", CultureInfo.InvariantCulture);
                objLeadEventManager.test("6");
                DateTime toDate = DateTime.ParseExact(StartDate, "MM/dd/yy", CultureInfo.InvariantCulture).AddDays(1);
                objLeadEventManager.test("7");
                DateTime newToDate = DateTime.ParseExact(EndDate, "MM/dd/yy", CultureInfo.InvariantCulture);
                objLeadEventManager.test("8");
                List<string> temp4 = new List<string>();
                temp4 = newToDate.ToString().Split(' ').ToList();



                var ApptListForDate = objLeadEventManager.GetLeadEventsInDatetimeRangeForTransfer(fromDate, toDate);
                foreach (var ev in ApptListForDate)
                {
                    temp1 = ev.StartDateString.Split(' ').ToList();
                    string osDate = temp1[0];
                   // string osTime = temp1[1] + " " + temp1[2];
                    string osTime = temp1[1] ;
                    nd = newToDate + " " + osTime;
                    objLeadEventManager.test(nd);
                    //DateTime nStart = DateTime.ParseExact(newToDate + " "+osTime, "MM/dd/yy HH:mm", CultureInfo.InvariantCulture);

                    DateTime nStart = Convert.ToDateTime(temp4[0] + " " + osTime);



                    Response = objLeadEventManager.TransferLeadEvent(ev.ID, nStart);

                }
                return Json("1", JsonRequestBehavior.AllowGet);

              
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("TransferEvent conto Method.", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult DeleteEvent(string EventId)
        {
            objResponse Response = new objResponse();
            SessionHelper session = new SessionHelper();
            try
            {
                Response = Response = objLeadEventManager.DeleteEvent(Convert.ToInt32(EventId));
                if (Response.ErrorCode == 0)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
