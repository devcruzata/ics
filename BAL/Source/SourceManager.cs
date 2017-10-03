using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Source
{
   public class SourceManager
    {
       public List<LeadSource> GetAllSource()
       {
           objResponse Response = new objResponse();
           List<LeadSource> source = new List<LeadSource>();
           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllSource", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       LeadSource objSource = new LeadSource();
                       objSource.LeadSourceID = Convert.ToInt64(dr["Source_ID_Auto_PK"]);
                       objSource.SourceName = Convert.ToString(dr["Source_Name"]);
                       objSource.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                       objSource.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                       objSource.UpdatedByName = Convert.ToString(dr["UpdatedByName"]);
                       objSource.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                       objSource.Status = Convert.ToString(dr["Status"]);
                       objSource.LinkedSalesRep = Convert.ToString(dr["SRepName"]);

                       source.Add(objSource);
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
               BAL.Common.LogManager.LogError("GetAllSource", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return source;
       }

       public objResponse AddSource(string Name, long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
               sqlParameter[0].Value = Name;

               sqlParameter[1] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
               sqlParameter[1].Value = LogedUser;              

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddSource", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("AddSource", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse EditSource(string Name, long SourceID, long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
               sqlParameter[0].Value = Name;

               sqlParameter[1] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
               sqlParameter[1].Value = LogedUser;

               sqlParameter[2] = new SqlParameter("@SourceID", SqlDbType.BigInt, 20);
               sqlParameter[2].Value = SourceID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditSource", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("EditSource", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }


       public string DeleteSource(long SourceID)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@SourceID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = SourceID;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteSource", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("DeleteSource", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse LinkSource(long SourceID, long salesRepId) 
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@SourceID", SqlDbType.BigInt, 100);
               sqlParameter[0].Value = SourceID;

               sqlParameter[1] = new SqlParameter("@salesRepId", SqlDbType.BigInt, 20);
               sqlParameter[1].Value = salesRepId;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LinkSource", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("LinkSource", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
