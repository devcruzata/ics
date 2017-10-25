using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.LeadEvents
{
    public class LeadEventsManager
    {
        public List<TextValue> GetLeadForCalender()
        {
            objResponse Response = new objResponse();
            List<TextValue> leadList = new List<TextValue>();
            try
            {


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadForCalender", DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        TextValue leads = new TextValue();
                        leads.Text = dr["DBA"].ToString();
                        leads.Value = dr["Lead_ID_Auto_PK"].ToString();
                        //leads.Add(string.Format("{0}-{1}", dr["DBA"], dr["Lead_ID_Auto_PK"]));
                        //leadList.Add(string.Format("{0}/{1}", dr["DBA"].ToString(), dr["Lead_ID_Auto_PK"].ToString()));   
                        leadList.Add(leads);
                    }
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetLeadForCalender", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return leadList;
        }

        public List<TextValue> GetDispositionForCalender()
        {
            objResponse Response = new objResponse();
            List<TextValue> disp = new List<TextValue>();
            try
            {


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDispositionForCalender", DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        TextValue disposition = new TextValue();
                        disposition.Value = dr["Status_ID_Auto_PK"].ToString();
                        disposition.Text = dr["Status_Name"].ToString();                      
                        disp.Add(disposition);
                    }
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetLeadForCalender", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return disp;
        }

        public objResponse AddNewLeadEvent(string Title, DateTime DateTimeScheduled, int AppointmentLength, long RLead, string Note,long CreatedBy)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;

                sqlParameter[1] = new SqlParameter("@DateTimeScheduled", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = DateTimeScheduled;

                sqlParameter[2] = new SqlParameter("@AppointmentLength", SqlDbType.Int, 100);
                sqlParameter[2].Value = AppointmentLength;

                sqlParameter[3] = new SqlParameter("@RLead", SqlDbType.BigInt, 16);
                sqlParameter[3].Value = RLead;

                sqlParameter[4] = new SqlParameter("@Note", SqlDbType.NVarChar, 200);
                sqlParameter[4].Value = Note;

                sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 16);
                sqlParameter[5].Value = CreatedBy;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNewLeadEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddNewLeadEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public List<LeadEvent> GetEventsInDatetimeRange(DateTime startDate, DateTime endDate)
        {
            objResponse Response = new objResponse();
            List<LeadEvent> result = new List<LeadEvent>();
            int i = 0;
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@startDate", SqlDbType.DateTime, 1000);
                sqlParameter[0].Value = startDate;

                sqlParameter[1] = new SqlParameter("@endDate", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = endDate;              


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadEvents", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                    foreach(DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        LeadEvent rec = new LeadEvent();
                        rec.ID = Convert.ToInt64(dr["LeadEvent_Id_Auto_Pk"]); //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                       // rec.Relatedlead = -1;
                        string StringDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["DateTimeScheduled"]));
                        rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                        rec.EndDateString = StringDate + "T23:59:59";
                        rec.Title = dr["Title"].ToString();
                        result.Add(rec);
                        i++;
                    }
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetEventsInDatetimeRange", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;
        }

        public List<LeadEvent> LoadEventsInDatetimeRange(DateTime startDate, DateTime endDate)
        {
            objResponse Response = new objResponse();
            List<LeadEvent> result = new List<LeadEvent>();
            int i = 0;
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@startDate", SqlDbType.DateTime, 1000);
                sqlParameter[0].Value = startDate;

                sqlParameter[1] = new SqlParameter("@endDate", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = endDate;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadEvents", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        LeadEvent rec = new LeadEvent();
                        rec.ID = Convert.ToInt64(dr["LeadEvent_Id_Auto_Pk"]); //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                        rec.StartDateString = Convert.ToDateTime(dr["DateTimeScheduled"]).ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                        rec.EndDateString = Convert.ToDateTime(dr["DateTimeScheduled"]).AddMinutes(Convert.ToInt32(dr["AppointmentLength"])).ToString("s"); // field AppointmentLength is in minutes
                        rec.Title = dr["Title"].ToString();               
                        rec.StatusColor = dr["Colour"].ToString(); ;
                        result.Add(rec);
                    }
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadEventsInDatetimeRange", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;
        }

        public objResponse GetEventInfo(int EventId)
        {

            objResponse Response = new objResponse();
            string result = "";
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@Event_ID", SqlDbType.Int, 100);
                sqlParameter[0].Value = EventId;



                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadEventInfoById", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";
                    // result = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetEventInfo calmanager", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;

        }

        public objResponse UpdateLeadEvent(long EventId, string Title, DateTime DateTimeScheduled, int AppointmentLength, long Disposition, string Note, long CreatedBy)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[7];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;

                sqlParameter[1] = new SqlParameter("@DateTimeScheduled", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = DateTimeScheduled;

                sqlParameter[2] = new SqlParameter("@AppointmentLength", SqlDbType.Int, 100);
                sqlParameter[2].Value = AppointmentLength;

                sqlParameter[3] = new SqlParameter("@Disposition", SqlDbType.BigInt, 16);
                sqlParameter[3].Value = Disposition;

                sqlParameter[4] = new SqlParameter("@Note", SqlDbType.NVarChar, 200);
                sqlParameter[4].Value = Note;

                sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 16);
                sqlParameter[5].Value = CreatedBy;

                sqlParameter[6] = new SqlParameter("@EventId", SqlDbType.BigInt, 16);
                sqlParameter[6].Value = EventId;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateLeadEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("UpdateLeadEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public List<LeadEvent> GetLeadEventsInDatetimeRangeForTransfer(DateTime startDate, DateTime endDate)
        {
            objResponse Response = new objResponse();
            List<LeadEvent> result = new List<LeadEvent>();
            int i = 0;
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@startDate", SqlDbType.DateTime, 1000);
                sqlParameter[0].Value = startDate;

                sqlParameter[1] = new SqlParameter("@endDate", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = endDate;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadEvents", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        LeadEvent rec = new LeadEvent();
                        rec.ID = Convert.ToInt64(dr["LeadEvent_Id_Auto_Pk"]); //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                        rec.StartDateString = Convert.ToDateTime(dr["DateTimeScheduled"]).ToString("MM/dd/yy HH:mm"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                        //rec.EndDateString = Convert.ToDateTime(dr["DateTimeScheduled"]).AddMinutes(Convert.ToInt32(dr["AppointmentLength"])).ToString("MM/dd/yy HH:mm"); // field AppointmentLength is in minutes
                        rec.Title = dr["Title"].ToString();
                        rec.StatusColor = dr["Colour"].ToString(); ;
                        result.Add(rec);
                    }
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadEventsInDatetimeRange", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;
        }
             

        public objResponse TransferLeadEvent(long EventId, DateTime StartDate)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@EventId", SqlDbType.Int, 10);
                sqlParameter[0].Value = EventId;

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.DateTime, 60);
                sqlParameter[1].Value = StartDate;               


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_TransferLeadEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("TransferLeadEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse DeleteEvent(int EventId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@EventId", SqlDbType.Int, 10);
                sqlParameter[0].Value = EventId;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteLeadEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("DeleteEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
