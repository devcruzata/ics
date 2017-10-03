using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Project.Entity;

namespace BAL.Campaigns
{
   public class CampaignsManager
    {
       public objResponse AddTemplate(string Name, string template)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
               sqlParameter[0].Value = Name;

               sqlParameter[1] = new SqlParameter("@template", SqlDbType.NVarChar, 4000);
               sqlParameter[1].Value = template;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("AddTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Templates> GetAllTemplate()
       {
           objResponse Response = new objResponse();
           List<Templates> templates = new List<Templates>();
           try
           {


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllTemplates", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "success";

                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Templates objTemplates = new Templates();
                       objTemplates.title = dr["title"].ToString();
                       objTemplates.body = dr["templateBody"].ToString();
                       objTemplates.status = dr["status"].ToString();
                       objTemplates.TemplateID = Convert.ToInt64(dr["Template_ID_Auto_PK"]);

                       templates.Add(objTemplates);

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
               BAL.Common.LogManager.LogError("GetAllTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return templates;
       }

       public objResponse GetTemplateForEdit(long templateId)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@templateId", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = templateId;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTemplateForEdit", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("GetTemplateForEdit", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse EditTemplate(long tId,string Name, string template)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
               sqlParameter[0].Value = Name;

               sqlParameter[1] = new SqlParameter("@template", SqlDbType.NVarChar, 4000);
               sqlParameter[1].Value = template;

               sqlParameter[2] = new SqlParameter("@tId", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = tId;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("AddTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse DaleteTemplate(long templateId)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@templateId", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = templateId;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "uspDeleteTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("DaleteTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public async void sendCampaign()
       {
           try
           {
               var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
               var client = new SendGridClient(apiKey);
               var from = new EmailAddress("test@example.com", "Example User");
               var subject = "Sending with SendGrid is Fun";
               var to = new EmailAddress("test@example.com", "Example User");
               var plainTextContent = "and easy to do anywhere, even with C#";
               var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
               var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
               var response = await client.SendEmailAsync(msg);
           }
           catch (Exception ex)
           {

           }
       }
    }
}
