using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DAL;
using Project.Entity;

namespace BAL.Reports
{
   public class ReportsManager
    {
       public objResponse MyReports(DateTime? start, DateTime? end , string reportType)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[5];

               sqlParameter[0] = new SqlParameter("@StartDate", SqlDbType.Date);
               sqlParameter[0].Value = start;

               sqlParameter[1] = new SqlParameter("@EndDate", SqlDbType.Date);
               sqlParameter[1].Value = end;

               sqlParameter[2] = new SqlParameter("@errorCode", SqlDbType.Int);
               sqlParameter[2].Direction = ParameterDirection.Output;

               sqlParameter[3] = new SqlParameter("@errorMessage", SqlDbType.NVarChar, 200);
               sqlParameter[3].Direction = ParameterDirection.Output;

               sqlParameter[4] = new SqlParameter("@reportType", SqlDbType.NVarChar, 200);
               sqlParameter[4].Value = reportType;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_genreateReportType1", sqlParameter, DAL.DB_CONSTANTS.ConnectionString_ICS);

               Response.ErrorCode = Helper.Helper.ConvertToInt(sqlParameter[2].Value.ToString());
               Response.ErrorMessage = sqlParameter[3].Value.ToString();
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               Response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("MyReports", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
