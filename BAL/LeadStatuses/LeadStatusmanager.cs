using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.LeadStatuses
{
   public class LeadStatusmanager
    {
        public List<LeadStatus> GetAllStatus()
        {
            objResponse Response = new objResponse();
            List<LeadStatus> status = new List<LeadStatus>();
            try
            {

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllStatus", DB_CONSTANTS.ConnectionString_ICS);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        LeadStatus objSource = new LeadStatus();
                        objSource.LeadStatusID = Convert.ToInt64(dr["Status_ID_Auto_PK"]);
                        objSource.StatusName = Convert.ToString(dr["Status_Name"]);
                        objSource.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                        objSource.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objSource.UpdatedByName = Convert.ToString(dr["UpdatedByName"]);
                        objSource.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objSource.StatusColor = Convert.ToString(dr["StatusColour"]);
                        objSource.emailTemplate = Convert.ToString(dr["emaiTemplate"]);
                        objSource.smsTemplate = Convert.ToString(dr["smsTemplate"]);
                      

                        status.Add(objSource);
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
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("GetAllStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return status;
        }

        public objResponse AddStatus(string Name,string color, string Template ,string textTemplate,long LogedUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = Name;

                sqlParameter[1] = new SqlParameter("@color", SqlDbType.NVarChar, 100);
                sqlParameter[1].Value = color;

                sqlParameter[2] = new SqlParameter("@Template", SqlDbType.NVarChar, 4000);
                sqlParameter[2].Value = Template;

                sqlParameter[3] = new SqlParameter("@textTemplate", SqlDbType.NVarChar, 4000);
                sqlParameter[3].Value = textTemplate;

                sqlParameter[4] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
                sqlParameter[4].Value = LogedUser;
                

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("AddStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse EditStatus(string Name, string color,string template ,string textTemplate,long StatusID, long LogedUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = Name;

                sqlParameter[1] = new SqlParameter("@color", SqlDbType.NVarChar, 100);
                sqlParameter[1].Value = color;

                sqlParameter[2] = new SqlParameter("@template", SqlDbType.NVarChar, 4000);
                sqlParameter[2].Value = template;

                sqlParameter[3] = new SqlParameter("@textTemplate", SqlDbType.NVarChar, 4000);
                sqlParameter[3].Value = textTemplate;

                sqlParameter[4] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
                sqlParameter[4].Value = LogedUser;

                sqlParameter[5] = new SqlParameter("@StatusID", SqlDbType.BigInt, 20);
                sqlParameter[5].Value = StatusID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("EditStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }


        public string DeleteStatus(long StatusID)
        {
            objResponse response = new objResponse();
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@StatusID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = StatusID;

                DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

                dt = response.ResponseData.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    response.ErrorCode = 0;
                    response.ErrorMessage = "Success";
                    result = Convert.ToString(dt.Rows[0][0]);
                }

            }
            catch (Exception ex)
            {
                response.ErrorCode = 2001;
                response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("DeleteStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;
        }

        public objResponse GetTemplate(long StatusID)
        {
            objResponse response = new objResponse();
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@StatusID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = StatusID;

                DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_GetTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

                dt = response.ResponseData.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    response.ErrorCode = 0;
                    response.ErrorMessage = Convert.ToString(dt.Rows[0][0]);                   
                }

            }
            catch (Exception ex)
            {
                response.ErrorCode = 2001;
                response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("GetTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return response;
        }
    }
}
