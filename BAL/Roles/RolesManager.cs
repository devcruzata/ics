using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Roles
{
   public class RolesManager
    {
       public objResponse AddRole(UserRoles objRoles)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[26];

               sqlParameter[0] = new SqlParameter("@AssociatedLeads", SqlDbType.NVarChar, 5);
               sqlParameter[0].Value = objRoles.AssociatedLeads;

               sqlParameter[1] = new SqlParameter("@SystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[1].Value = objRoles.SystemwideLeads;

               sqlParameter[2] = new SqlParameter("@EditTask", SqlDbType.NVarChar, 5);
               sqlParameter[2].Value = objRoles.EditTask;

               sqlParameter[3] = new SqlParameter("@LeadNotes", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = objRoles.LeadNotes;

               sqlParameter[4] = new SqlParameter("@Documents", SqlDbType.NVarChar, 5);
               sqlParameter[4].Value = objRoles.Documents;

               sqlParameter[5] = new SqlParameter("@ManageDocuments", SqlDbType.NVarChar, 5);
               sqlParameter[5].Value = objRoles.ManageDocuments;

               sqlParameter[6] = new SqlParameter("@HelpDeskTickets", SqlDbType.NVarChar, 5);
               sqlParameter[6].Value = objRoles.HelpDeskTickets;

               sqlParameter[7] = new SqlParameter("@LeadDistribution", SqlDbType.NVarChar, 5);
               sqlParameter[7].Value = objRoles.LeadDistribution;

               sqlParameter[8] = new SqlParameter("@ResidualManagerView", SqlDbType.NVarChar, 5);
               sqlParameter[8].Value = objRoles.ResidualManagerView;

               sqlParameter[9] = new SqlParameter("@ResidualsAccess", SqlDbType.NVarChar, 5);
               sqlParameter[9].Value = objRoles.ResidualsAccess;

               sqlParameter[10] = new SqlParameter("@MerchantinfoAssociatedLead", SqlDbType.NVarChar, 5);
               sqlParameter[10].Value = objRoles.MerchantinfoAssociatedLead;

               sqlParameter[11] = new SqlParameter("@MerchantinfoSystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[11].Value = objRoles.MerchantinfoSystemwideLeads;

               sqlParameter[12] = new SqlParameter("@ManageMerchant", SqlDbType.NVarChar, 5);
               sqlParameter[12].Value = objRoles.ManageMerchant;

               sqlParameter[13] = new SqlParameter("@Statements", SqlDbType.NVarChar, 5);
               sqlParameter[13].Value = objRoles.Statements;

               sqlParameter[14] = new SqlParameter("@TriggerStatementDownload", SqlDbType.NVarChar, 5);
               sqlParameter[14].Value = objRoles.TriggerStatementDownload;

               sqlParameter[15] = new SqlParameter("@PortfolioActivityAssociatedleads", SqlDbType.NVarChar, 5);
               sqlParameter[15].Value = objRoles.PortfolioActivityAssociatedleads;

               sqlParameter[16] = new SqlParameter("@PortfolioActivitySystemwideleads", SqlDbType.NVarChar, 5);
               sqlParameter[16].Value = objRoles.PortfolioActivitySystemwideleads;

               sqlParameter[17] = new SqlParameter("@MerchantApplication", SqlDbType.NVarChar, 5);
               sqlParameter[17].Value = objRoles.MerchantApplication;

               sqlParameter[18] = new SqlParameter("@UnderwritngApplicationSubmissions", SqlDbType.NVarChar, 5);
               sqlParameter[18].Value = objRoles.UnderwritngApplicationSubmissions;

               sqlParameter[19] = new SqlParameter("@Agenttracker", SqlDbType.NVarChar, 5);
               sqlParameter[19].Value = objRoles.Agenttracker;

               sqlParameter[20] = new SqlParameter("@MessagingSystem", SqlDbType.NVarChar, 5);
               sqlParameter[20].Value = objRoles.MessagingSystem;

               sqlParameter[21] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
               sqlParameter[21].Value = objRoles.RoleName;

               sqlParameter[22] = new SqlParameter("@Calendar", SqlDbType.NVarChar, 5);
               sqlParameter[22].Value = objRoles.Calendar;

               sqlParameter[23] = new SqlParameter("@ProposalGenerator", SqlDbType.NVarChar, 5);
               sqlParameter[23].Value = objRoles.ProposalGenerator;

               sqlParameter[24] = new SqlParameter("@UserManagement", SqlDbType.NVarChar, 5);
               sqlParameter[24].Value = objRoles.UserManagement;

               sqlParameter[25] = new SqlParameter("@SiteManagement", SqlDbType.NVarChar, 5);
               sqlParameter[25].Value = objRoles.SiteManagement;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUserRole", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddRole", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<UserRoles> GetAllRoles()
       {
           objResponse Response = new objResponse();
           List<UserRoles> roles = new List<UserRoles>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetRoles", DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";

                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       UserRoles objUserRoles = new UserRoles();
                       objUserRoles.RoleName = dr["User_Role_Desc"].ToString();
                       objUserRoles.Role_ID = Convert.ToInt64(dr["User_Role_ID_Auto_Pk"]);

                       roles.Add(objUserRoles);
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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetAllRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return roles;
       }

       public objResponse GetRolesForEdit(long RoleID)
       {
           objResponse Response = new objResponse();           
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = RoleID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetRolesForEdit", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetAllRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

        public objResponse DeleteRoles(long RoleID)
        {
            objResponse Response = new objResponse();
            try
            {

                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = RoleID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteRoles", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("DeleteRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse UpdateRole(UserRoles objRoles)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[27];

               sqlParameter[0] = new SqlParameter("@AssociatedLeads", SqlDbType.NVarChar, 5);
               sqlParameter[0].Value = objRoles.AssociatedLeads;

               sqlParameter[1] = new SqlParameter("@SystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[1].Value = objRoles.SystemwideLeads;

               sqlParameter[2] = new SqlParameter("@EditTask", SqlDbType.NVarChar, 5);
               sqlParameter[2].Value = objRoles.EditTask;

               sqlParameter[3] = new SqlParameter("@LeadNotes", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = objRoles.LeadNotes;

               sqlParameter[4] = new SqlParameter("@Documents", SqlDbType.NVarChar, 5);
               sqlParameter[4].Value = objRoles.Documents;

               sqlParameter[5] = new SqlParameter("@ManageDocuments", SqlDbType.NVarChar, 5);
               sqlParameter[5].Value = objRoles.ManageDocuments;

               sqlParameter[6] = new SqlParameter("@HelpDeskTickets", SqlDbType.NVarChar, 5);
               sqlParameter[6].Value = objRoles.HelpDeskTickets;

               sqlParameter[7] = new SqlParameter("@LeadDistribution", SqlDbType.NVarChar, 5);
               sqlParameter[7].Value = objRoles.LeadDistribution;

               sqlParameter[8] = new SqlParameter("@ResidualManagerView", SqlDbType.NVarChar, 5);
               sqlParameter[8].Value = objRoles.ResidualManagerView;

               sqlParameter[9] = new SqlParameter("@ResidualsAccess", SqlDbType.NVarChar, 5);
               sqlParameter[9].Value = objRoles.ResidualsAccess;

               sqlParameter[10] = new SqlParameter("@MerchantinfoAssociatedLead", SqlDbType.NVarChar, 5);
               sqlParameter[10].Value = objRoles.MerchantinfoAssociatedLead;

               sqlParameter[11] = new SqlParameter("@MerchantinfoSystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[11].Value = objRoles.MerchantinfoSystemwideLeads;

               sqlParameter[12] = new SqlParameter("@ManageMerchant", SqlDbType.NVarChar, 5);
               sqlParameter[12].Value = objRoles.ManageMerchant;

               sqlParameter[13] = new SqlParameter("@Statements", SqlDbType.NVarChar, 5);
               sqlParameter[13].Value = objRoles.Statements;

               sqlParameter[14] = new SqlParameter("@TriggerStatementDownload", SqlDbType.NVarChar, 5);
               sqlParameter[14].Value = objRoles.TriggerStatementDownload;

               sqlParameter[15] = new SqlParameter("@PortfolioActivityAssociatedleads", SqlDbType.NVarChar, 5);
               sqlParameter[15].Value = objRoles.PortfolioActivityAssociatedleads;

               sqlParameter[16] = new SqlParameter("@PortfolioActivitySystemwideleads", SqlDbType.NVarChar, 5);
               sqlParameter[16].Value = objRoles.PortfolioActivitySystemwideleads;

               sqlParameter[17] = new SqlParameter("@MerchantApplication", SqlDbType.NVarChar, 5);
               sqlParameter[17].Value = objRoles.MerchantApplication;

               sqlParameter[18] = new SqlParameter("@UnderwritngApplicationSubmissions", SqlDbType.NVarChar, 5);
               sqlParameter[18].Value = objRoles.UnderwritngApplicationSubmissions;

               sqlParameter[19] = new SqlParameter("@Agenttracker", SqlDbType.NVarChar, 5);
               sqlParameter[19].Value = objRoles.Agenttracker;

               sqlParameter[20] = new SqlParameter("@MessagingSystem", SqlDbType.NVarChar, 5);
               sqlParameter[20].Value = objRoles.MessagingSystem;

               sqlParameter[21] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
               sqlParameter[21].Value = objRoles.RoleName;

               sqlParameter[22] = new SqlParameter("@Calendar", SqlDbType.NVarChar, 5);
               sqlParameter[22].Value = objRoles.Calendar;

               sqlParameter[23] = new SqlParameter("@ProposalGenerator", SqlDbType.NVarChar, 5);
               sqlParameter[23].Value = objRoles.ProposalGenerator;

               sqlParameter[24] = new SqlParameter("@UserManagement", SqlDbType.NVarChar, 5);
               sqlParameter[24].Value = objRoles.UserManagement;

               sqlParameter[25] = new SqlParameter("@SiteManagement", SqlDbType.NVarChar, 5);
               sqlParameter[25].Value = objRoles.SiteManagement;

               sqlParameter[26] = new SqlParameter("@Role_ID", SqlDbType.BigInt, 10);
               sqlParameter[26].Value = objRoles.Role_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateUserRole", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("UpdateRole", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
