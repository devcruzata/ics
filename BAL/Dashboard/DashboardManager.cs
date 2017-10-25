using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dashboard
{
   public class DashboardManager
    {
        public objResponse GetDahboardData()
        {
            objResponse Response = new objResponse();
            try
            {
              //  SqlParameter[] sqlParameter = new SqlParameter[16];
              
              //  sqlParameter[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 60);
              //  sqlParameter[0].Value = objUser.FName;

                

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDashBaordData", DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }
    }
}
