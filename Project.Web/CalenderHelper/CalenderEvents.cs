using BAL.LeadEvents;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Project.Web.CalenderHelper
{
    public class CalenderEvents
    {
        
        public int ID;
        public string Title;
        public int SomeImportantKeyID;
        public string StartDateString;
        public string EndDateString;
        public string StatusString;
        public string StatusColor;
        public string ClassName;


        //public static List<CalenderEvents> LoadAllAppointmentsInDateRange(double start, double end)
        //{
        //    var fromDate = ConvertFromUnixTimestamp(start);
        //    var toDate = ConvertFromUnixTimestamp(end);
        //    using (DiaryContainer ent = new DiaryContainer())
        //    {
        //        var rslt = ent.AppointmentDiary.Where(s => s.DateTimeScheduled >= fromDate && System.Data.Objects.EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate);

        //        List<DiaryEvent> result = new List<DiaryEvent>();
        //        foreach (var item in rslt)
        //        {
        //            DiaryEvent rec = new DiaryEvent();
        //            rec.ID = item.ID;
        //            rec.SomeImportantKeyID = item.SomeImportantKey;
        //            rec.StartDateString = item.DateTimeScheduled.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
        //            rec.EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
        //            rec.Title = item.Title + " - " + item.AppointmentLength.ToString() + " mins";
        //            rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
        //            rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
        //            string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
        //            rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
        //            rec.StatusColor = ColorCode;
        //            result.Add(rec);
        //        }

        //        return result;
        //    }

        //}


        //public static List<CalenderEvents> LoadAppointmentSummaryInDateRange(double start, double end)
        //{

        //    var fromDate = ConvertFromUnixTimestamp(start);
        //    var toDate = ConvertFromUnixTimestamp(end);
        //  //  toDate = toDate.AddMinutes()
        //    using (DiaryContainer ent = new DiaryContainer())
        //    {
        //        var rslt = ent.AppointmentDiary.Where(s => s.DateTimeScheduled >= fromDate && System.Data.Objects.EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate)
        //                                                .GroupBy(s => System.Data.Objects.EntityFunctions.TruncateTime(s.DateTimeScheduled))
        //                                                .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

        //        List<CalenderEvents> result = new List<CalenderEvents>();
        //        int i = 0;
        //        foreach (var item in rslt)
        //        {
        //            CalenderEvents rec = new CalenderEvents();
        //            rec.ID = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
        //            rec.SomeImportantKeyID = -1;
        //            string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
        //            rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
        //            rec.EndDateString = StringDate + "T23:59:59";
        //            rec.Title = "Booked: " + item.Count.ToString();
        //            result.Add(rec);
        //            i++;
        //        }

        //        return result;
        //    }

        //}

        //public static void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        //{
        //    // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
        //    using (DiaryContainer ent = new DiaryContainer())
        //    {
        //        var rec = ent.AppointmentDiary.FirstOrDefault(s => s.ID == id);
        //        if (rec != null)
        //        {
        //            DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
        //            rec.DateTimeScheduled = DateTimeStart;
        //            if (!String.IsNullOrEmpty(NewEventEnd))
        //            {
        //                TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
        //                rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
        //            }
        //            ent.SaveChanges();
        //        }
        //    }

        //}


        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration,string RLead,string description)
        {
            LeadEventsManager objLeadEventManager = new LeadEventsManager();
            SessionHelper session = new SessionHelper();           
            try
            {
                CalenderEventModel rec = new CalenderEventModel();
                rec.Title = Title;
                var sd = NewEventDate + " " + NewEventTime;
                rec.DateTimeScheduled = DateTime.ParseExact(sd, "MM/dd/yy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);
                rec.RelatedLead = Convert.ToInt64(RLead);
                //  ent.AppointmentDiary.Add(rec);
                //  ent.SaveChanges();             
                objLeadEventManager.AddNewLeadEvent(rec.Title, rec.DateTimeScheduled, rec.AppointmentLength, rec.RelatedLead, description, Convert.ToInt64(HttpContext.Current.Session["UserID"]));
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool UpdateEvent(string EventId,string Title, string NewEventDate, string NewEventTime, string NewEventDuration, string Disposition, string description)
        {
            LeadEventsManager objLeadEventManager = new LeadEventsManager();
            SessionHelper session = new SessionHelper();
            try
            {
                CalenderEventModel rec = new CalenderEventModel();
                rec.Title = Title;
                var sd = NewEventDate + " " + NewEventTime;
                rec.DateTimeScheduled = DateTime.ParseExact(sd, "MM/dd/yy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);                  
                objLeadEventManager.UpdateLeadEvent(Convert.ToInt64(EventId),rec.Title, rec.DateTimeScheduled, rec.AppointmentLength, Convert.ToInt64(Disposition), description, Convert.ToInt64(HttpContext.Current.Session["UserID"]));
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}