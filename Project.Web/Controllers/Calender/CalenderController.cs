using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Calender
{
    public class CalenderController : Controller
    {
        BAL.Calender.CalenderManager objCalender = new BAL.Calender.CalenderManager();
        SessionHelper session;
        //
        // GET: /Calender/

        public ActionResult ManageCalender()
        {
            return View();
        }

        public ActionResult testCalender()
        {
            return View();
        }


        [HttpPost]
        [ActionName("GetLeads")]
        public ActionResult GetLeads()
        {          

            List<TextValue> leads = objCalender.GetLeadForCalender();

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
        public JsonResult SaveEvent(string Title, string StartDate, string EndDate, string RelatedLead, string Description)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                  DateTime fromDate = Convert.ToDateTime(StartDate);
                 // DateTime toDate = Convert.ToDateTime(EndDate).AddDays(-1);
                  DateTime toDate = Convert.ToDateTime(EndDate);

                // Response = Response = objCalender.AddNewEvent(Title, BAL.Helper.Helper.ConvertToDateNullable(StartDate, "YYYY/MM/DD hh:mm a"), BAL.Helper.Helper.ConvertToDateNullable(StartDate, "YYYY/MM/DD hh:mm a"), EventColor,Description);
                  Response = Response = objCalender.AddNewEvent(Title, fromDate, toDate, RelatedLead, Description,session.UserSession.UserId);
                //Response = Response = objCalender.AddNewEvent(Title, StartDate, EndDate, RelatedLead, Description, session.UserSession.UserId);
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
                BAL.Common.LogManager.LogError("SaveEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetEventInfo(string EventId)
        {
            objResponse Response = new objResponse();
            Response = objCalender.GetEventInfo(Convert.ToInt32(EventId));
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            LeadModel objModel = new LeadModel();

            objModel.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
            objModel.ContName = Response.ResponseData.Tables[0].Rows[0]["ContactName"].ToString();
            objModel.bPhone = Response.ResponseData.Tables[0].Rows[0]["BusinessPhone"].ToString();
            objModel.Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
            string LeadEventStart = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_start"]).ToString("MM/dd/yy hh:mm tt");

            temp1 = LeadEventStart.Split(' ').ToList();
            objModel.LeadEventStartDate = temp1[0];
            objModel.LeadEventStartTime = temp1[1] + " " + temp1[2];

            //string LeadEventEnd = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_end"]).AddDays(-1).ToString("MM/dd/yyyy hh:mm tt");
            string LeadEventEnd = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_end"]).ToString("MM/dd/yy hh:mm tt");

            temp2 = LeadEventEnd.Split(' ').ToList();
            objModel.LeadEventEndDate = temp2[0];
            objModel.LeadEventEndTime = temp2[1] + " " + temp2[2];

            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEvntDate(string EventID, string StartDate, string EndDate)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                DateTime fromDate = Convert.ToDateTime(StartDate);
                //DateTime toDate = Convert.ToDateTime(EndDate).AddDays(-1);
                DateTime toDate = Convert.ToDateTime(EndDate);

                Response = Response = objCalender.UpdateEventDate(Convert.ToInt32(EventID), fromDate, toDate);
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
                BAL.Common.LogManager.LogError("UpdateEvntDate conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }      


        public JsonResult GetEvents(string start, string end)
        {
            var ApptListForDate = objCalender.LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.EventColor,
                                allDay = false,
                                currentTimezone = "local",
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }       

        [HttpPost]
        public JsonResult UpdateEvent(string Title, string StartDate, string EndDate, string Description, string DateFlag, string EventId)
        {  
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
               // List<string> temp1 = new List<string>();
               // List<string> temp2 = new List<string>();

                 
                //   temp1= StartDate.Split(' ').ToList(); 
               //    temp2= EndDate.Split(' ').ToList();
                   

             //   DateTime fromDate = Convert.ToDateTime(temp1[0].Split('-').ToList()[2]+"/"+temp1[0].Split('-').ToList()[0]+"/"+temp1[0].Split('-').ToList()[1]+" "+temp1[1]+" "+temp1[2]);
               // DateTime toDate = Convert.ToDateTime(temp2[0].Split('-').ToList()[2]+"/"+temp2[0].Split('-').ToList()[0]+"/"+temp2[0].Split('-').ToList()[1]+" "+temp2[1]+" "+temp2[2]).AddDays(1);
             //   DateTime toDate = Convert.ToDateTime(temp2[0].Split('-').ToList()[2] + "/" + temp2[0].Split('-').ToList()[0] + "/" + temp2[0].Split('-').ToList()[1] + " " + temp2[1] + " " + temp2[2]);
                
                  //  DateTime fromDate = Convert.ToDateTime(StartDate);
                  //  DateTime toDate = Convert.ToDateTime(EndDate).AddDays(1);
                    //DateTime toDate = Convert.ToDateTime(EndDate);

                    DateTime fromDate = Convert.ToDateTime(StartDate);
                    DateTime toDate = Convert.ToDateTime(EndDate);
                Response = Response = objCalender.UpdateEventType1(Convert.ToInt32(EventId), Title, fromDate, toDate, Description, Convert.ToInt64(session.UserSession.UserId));
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
                BAL.Common.LogManager.LogError("UpdateEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult TransferEvent(string StartDate, string EndDate)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<Events> events = new List<Events>();
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            try
            {
                DateTime fromDate = Convert.ToDateTime(StartDate);
                DateTime toDate = Convert.ToDateTime(StartDate).AddDays(1);

                 List<string> temp3 = new List<string>();
                 List<string> temp4 = new List<string>();


                   temp3= fromDate.ToString().Split(' ').ToList(); 
                   temp4= toDate.ToString().Split(' ').ToList();


                //  DateTime fromDate = Convert.ToDateTime(temp3[0].Split('-').ToList()[2]+"/"+temp1[0].Split('-').ToList()[0]+"/"+temp1[0].Split('-').ToList()[1]+" "+temp1[1]+" "+temp1[2]);
                //  DateTime toDate = Convert.ToDateTime(temp3[0].Split('-').ToList()[2]+"/"+temp2[0].Split('-').ToList()[0]+"/"+temp2[0].Split('-').ToList()[1]+" "+temp2[1]+" "+temp2[2]).AddDays(1);
                //   DateTime toDate = Convert.ToDateTime(temp2[0].Split('-').ToList()[2] + "/" + temp2[0].Split('-').ToList()[0] + "/" + temp2[0].Split('-').ToList()[1] + " " + temp2[1] + " " + temp2[2]);

                //Response = objCalender.GetEventForTransfer(StartDate);
                //if(Response.ErrorCode == 0)
                //{
                //foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                //{
                //    Events objevents = new Events();
                //    objevents.Event_Id = Convert.ToInt64(dr["Event_ID_Auto_PK"]);

                //    objevents.StartDate = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_start"]).ToString("MM/dd/yy hh:mm tt");
                //    objevents.EndDate = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["event_end"]).ToString("MM/dd/yy hh:mm tt");

                //    events.Add(objevents);                  
                //}
                var ApptListForDate = objCalender.LoadAppointmentSummaryInDateRange(temp3[0], temp4[0]);
                foreach (var ev in ApptListForDate)
                    {
                        temp1 = ev.StartDateString.Split(' ').ToList();
                        string osDate = temp1[0];
                        string osTime = temp1[1] + " " + temp1[2];
                        DateTime nStart = Convert.ToDateTime(EndDate+" "+temp1[1] + " " + temp1[2]);

                        temp2 = ev.EndDateString.Split(' ').ToList();
                        string oeDate = temp2[0];
                        string oeTime = temp2[1] + " " + temp2[2];
                        DateTime nEnd = Convert.ToDateTime(EndDate + " " + temp2[1] + " " + temp2[2]);

                        Response = objCalender.TransferEvent(ev.ID,nStart, nEnd);
                        
                    }
                    return Json("1", JsonRequestBehavior.AllowGet);

                //}
                //else
                //{
                //    return Json("0", JsonRequestBehavior.AllowGet);
                //}
            }
            catch(Exception ex)
            {
                BAL.Common.LogManager.LogError("TransferEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult DeleteEvent(string EventId)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = Response = objCalender.DeleteEvent(Convert.ToInt32(EventId));
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

        //[HttpPost]
        //public List<string> GetLeads(string term)
        //{
        //    List<string> leadlist = new List<string>();
        //    objResponse Rewsponse = new objResponse();

          
        //    leadlist = objCalender.GetLeadForCalender(term);
        //    //var leads = (from lead in leadlist
        //    //             select new
        //    //             {
        //    //                 label = lead.Text,
        //    //                 val = lead.Value
        //    //             }).ToList();
        //    return leadlist;
           

        //}

    }
}
