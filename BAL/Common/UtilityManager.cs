using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Common
{
   public static class UtilityManager
    {
      

       public static List<TextValue> GetSourceForDropDown( long PIN)
       {

           objResponse Response = new objResponse();
           List<TextValue> Category = new List<TextValue>();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetSourceForDropDown", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Source_Val"].ToString();
                       objText.Text = dr["Source_Text"].ToString();
                       Category.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSourceForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Category;
       }       

       public static List<TextValue> GetLeadsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> User = new List<TextValue>();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadsForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Lead_ID_Auto_PK"].ToString();
                       objText.Text = dr["Name"].ToString();
                       User.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetLeadsForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return User;
       }

       public static List<TextValue> GetClientsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> User = new List<TextValue>();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClientsForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Client_ID_Auto_PK"].ToString();
                       objText.Text = dr["Name"].ToString();
                       User.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetClientsForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return User;
       }

      
    }
}
