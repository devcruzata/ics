using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.GenralSeting
{
  public  class GenralSetingManager
    {
        public objResponse getGenralSeting()
        {
            objResponse Response = new objResponse();
            try
            {
                //SqlParameter[] sqlParameter = new SqlParameter[1];

                //sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
                //sqlParameter[0].Value = CustomerID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetGenralSetings",  DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

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
                BAL.Common.LogManager.LogError("getGenralSeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse manageMemo(string memo)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@memo", SqlDbType.NVarChar, 8000);
                sqlParameter[0].Value = memo;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ManageMemo",sqlParameter ,DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

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
                BAL.Common.LogManager.LogError("manageMemo", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse AddCompanyProfile(AdminSeting objSetings, long logedUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[11];                

                sqlParameter[0] = new SqlParameter("@Company", SqlDbType.NVarChar, 80);
                sqlParameter[0].Value = objSetings.Company;

                sqlParameter[1] = new SqlParameter("@Address", SqlDbType.NVarChar, 200);
                sqlParameter[1].Value = objSetings.Address;

                sqlParameter[2] = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                sqlParameter[2].Value = objSetings.City;

                sqlParameter[3] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
                sqlParameter[3].Value = objSetings.Stete;

                sqlParameter[4] = new SqlParameter("@Country", SqlDbType.NVarChar, 20);
                sqlParameter[4].Value = objSetings.Country;

                sqlParameter[5] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 20);
                sqlParameter[5].Value = objSetings.Zipcode;

                sqlParameter[6] = new SqlParameter("@Phone", SqlDbType.NVarChar, 20);
                sqlParameter[6].Value = objSetings.Phone;

                sqlParameter[7] = new SqlParameter("@Website", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = objSetings.Website;

                sqlParameter[8] = new SqlParameter("@Currency", SqlDbType.NVarChar, 20);
                sqlParameter[8].Value = objSetings.Currency;

                sqlParameter[9] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 820);
                sqlParameter[9].Value = logedUser;

                sqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
                sqlParameter[10].Value = DateTime.Now;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddCompanyProfile", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

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
                BAL.Common.LogManager.LogError("AddCompanyProfile", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
