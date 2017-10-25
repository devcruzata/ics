using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Project.Entity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using Project.ViewModel;

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

               sqlParameter[1] = new SqlParameter("@template", SqlDbType.NVarChar, 8000);
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
                       objTemplates.TemplateID = Convert.ToInt64(dr["TemplateID_Auto_Pk"]);

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

        public objResponse AddnewCampaign(string title, long dispositionId, long templateId, bool sendFlag)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@dispositionId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = dispositionId;

                sqlParameter[1] = new SqlParameter("@templateId", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = templateId;

                sqlParameter[2] = new SqlParameter("@sendFlag", SqlDbType.Bit, 10);
                sqlParameter[2].Value = sendFlag;

                sqlParameter[3] = new SqlParameter("@title", SqlDbType.NVarChar, 100);
                sqlParameter[3].Value = title;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNewCampaign", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

                    return Response;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                    return Response;
                }

            }
            catch (Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("sendCampaign", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Response;
            }
        }

        public objResponse GetCampaignData(long CampaignID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@CampaignID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = CampaignID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_sendCampaign", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

                    return Response;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                    return Response;
                }
            }
            catch(Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("GetCampaignData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Response;
            }
        }

        public List<Project.Entity.Campaigns> getAllCampaigns()
        {
            List<Project.Entity.Campaigns> campaigns = new List<Project.Entity.Campaigns>();
            objResponse Response = new objResponse();
            try
            {
                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getAllCampaigns", DB_CONSTANTS.ConnectionString_ICS);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

                    foreach(DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Project.Entity.Campaigns objCampaign = new Project.Entity.Campaigns();
                        objCampaign.Campign_ID = Convert.ToInt64(dr["Campaign_ID_Auto_Pk"]);
                        objCampaign.Title = dr["title"].ToString();
                        objCampaign.Disposition = dr["Disposition"].ToString();
                        objCampaign.CratedAt = Convert.ToDateTime(dr["CreatedAt"]).ToString("MM/dd/yy");
                        objCampaign.Status = dr["Status"].ToString();
                        objCampaign.TotalReciever = dr["TotalReciever"].ToString();

                        campaigns.Add(objCampaign);
                    }
                    return campaigns;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                    return campaigns;
                }
            }
            catch(Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("getAllCampaigns", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return campaigns;
            }
        }

        public objResponse DaleteCampaign(long CampaignId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@CampaignId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = CampaignId;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteCampaign", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                BAL.Common.LogManager.LogError("DaleteCampaign", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }   
    
}
