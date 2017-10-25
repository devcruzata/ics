using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Utility
{
   public static class UtilityManager
    {
       public static objResponse GetusersForActivation()
       {
           objResponse Response = new objResponse();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUsersForActivation", DB_CONSTANTS.ConnectionString_ICS);


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
               BAL.Common.LogManager.LogError("GetusersForActivation", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public static objResponse getInvitedusersForActivation()
       {
           objResponse Response = new objResponse();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetInvitedUsersForActivation", DB_CONSTANTS.ConnectionString_ICS);


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
               BAL.Common.LogManager.LogError("getInvitedusersForActivation", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public static void sendWelcomeMail(string Name , string Email,string Username , string Password)
       {
           try
           {
               string body = "Dear " + Name + ", <br/><br/>You are successfully registered to ICS. Following are the your account credentials <br/><br/>email address / username : " + Username + "<br/>password : " + Password + "<br/><br/>If you have any questions or trouble logging on please contact to administrator.<br/><br/>All the best,<br/><br/>ICS";
               //BAL.Helper.Helper.SendEmail(Email, "Welcome To ICS", body);

               BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "Welcome To ICS", body);
               
           }
           catch (Exception ex)
           {
               BAL.Common.LogManager.LogError("sendWelcomeMail", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));               
           }
       }

       public static void sendInvitationMail(string Name, string Email, string Link)
       {
           try
           {
               string body = "Dear " + Name + ", <br/><br/>Please Click the below link for activate your account." + "<br/><br/><a href=" + Link + ">" + Link + "</a><br/><br/>All the best,<br/><br/>ICS";
               //BAL.Helper.Helper.SendEmail(Email, "Welcome To ICS", body);

               BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "Welcome To ICS", body);

           }
           catch (Exception ex)
           {
               BAL.Common.LogManager.LogError("sendInvitationMail", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
       }

       public static objResponse getUserNameByActivationID(string Activation_ID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Activation_ID", SqlDbType.NVarChar, 40);
               sqlParameter[0].Value = Activation_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUserNameByActId", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               BAL.Common.LogManager.LogError("getUserNameByActivationID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public static List<TextValue> GetSattusForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> status = new List<TextValue>();

           try
           {
               //SqlParameter[] sqlParameter = new SqlParameter[1];

               //sqlParameter[0] = new SqlParameter("@Group_ID", SqlDbType.BigInt, 10);
               //sqlParameter[0].Value = Group_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetStatusForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Status_ID_Auto_PK"].ToString();
                       objText.Text = dr["Status_Name"].ToString();
                       status.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSattusForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return status;
       }

       public static List<TextValue> GetTemplatesForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> templates = new List<TextValue>();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTemplatesForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["TemplateID_Auto_Pk"].ToString();
                       objText.Text = dr["title"].ToString();
                       templates.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetTemplatesForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return templates;
       }

       public static List<TextValue> GetSourceForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> source = new List<TextValue>();

           try
           {
               //SqlParameter[] sqlParameter = new SqlParameter[1];

               //sqlParameter[0] = new SqlParameter("@Group_ID", SqlDbType.BigInt, 10);
               //sqlParameter[0].Value = Group_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetSourceForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Source_ID_Auto_PK"].ToString();
                       objText.Text = dr["Source_Name"].ToString();
                       source.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSourceForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return source;
       }

       public static List<TextValue> GetUsersForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> source = new List<TextValue>();

           try
           {
               //SqlParameter[] sqlParameter = new SqlParameter[1];

               //sqlParameter[0] = new SqlParameter("@Group_ID", SqlDbType.BigInt, 10);
               //sqlParameter[0].Value = Group_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUsersForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["User_ID_Auto_PK"].ToString();
                       objText.Text = dr["UName"].ToString();
                       source.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetUsersForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return source;
       }

       public static List<TextValue> GetGroupsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> group = new List<TextValue>();

           try
           {


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetGroupsForDropDown", DB_CONSTANTS.ConnectionString_ICS);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Group_ID"].ToString();
                       objText.Text = dr["Group_Name"].ToString();
                       group.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSourceForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return group;
       }

       public static List<Project.Entity.Activity> getActivityByRelateToID(long RelateToID  , string LogedUserRole)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Activity> activity = new List<Project.Entity.Activity>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@LogedUserRole", SqlDbType.NVarChar, 30);
               sqlParameter[0].Value = LogedUserRole;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = RelateToID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetActivities", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Activity objActivity = new Project.Entity.Activity();
                       objActivity.Activity_ID = Convert.ToInt64(dr["Activity_ID_Auto_PK"]);
                       objActivity.Title = Convert.ToString(dr["Title"]);
                       objActivity.RelateTo_ID = Convert.ToInt64(dr["RelateTo_ID_Fk"]);
                       objActivity.RelateTo_Name = Convert.ToString(dr["ContactName"]);
                       objActivity.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                       objActivity.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("d MMM yyyy");
                       objActivity.CreatedTime = Convert.ToDateTime(dr["CreatedDate"]).ToString("hh:mm tt");
                       objActivity.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                       objActivity.Status = Convert.ToString(dr["Status"]);
                       objActivity.ActivityType = Convert.ToString(dr["ActivityType"]);
                       objActivity.FromAdd = Convert.ToString(dr["FromAddress"]);
                       objActivity.ToAdd = Convert.ToString(dr["ToAddress"]);


                       activity.Add(objActivity);
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
               BAL.Common.LogManager.LogError("getActivityByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return activity;
       }

       public static List<Project.Entity.Tasks> getTasksByRelateToID(long RelateToID, string LogedUserRole,long LogedUserId)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Tasks> Task = new List<Project.Entity.Tasks>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];              

               sqlParameter[0] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = RelateToID;

               sqlParameter[1] = new SqlParameter("@LogedUserRole", SqlDbType.NVarChar, 30);
               sqlParameter[1].Value = LogedUserRole;

               sqlParameter[2] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 30);
               sqlParameter[2].Value = LogedUserId;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTasks", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Tasks objTask = new Project.Entity.Tasks();
                       objTask.Task_ID = Convert.ToInt64(dr["Task_ID_Auto_PK"]);
                       objTask.Title = Convert.ToString(dr["Title"]);
                       objTask.StartDate = Convert.ToDateTime(dr["StartDate"]).ToString("MMM ddd d");
                       objTask.EndDate = Convert.ToDateTime(dr["EndDate"]).ToString("d MMM yyyy");
                       objTask.Description = Convert.ToString(dr["Description"]);
                       objTask.RelateTo = Convert.ToInt64(dr["RelateTo_ID"]);
                       objTask.RelateToName = Convert.ToString(dr["ContactName"]);
                       objTask.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                       objTask.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                       objTask.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("d MMM yyyy");
                       objTask.Status = Convert.ToString(dr["Status"]);
                       objTask.AssignTo = Convert.ToString(dr["AssignTo_ID"]);
                       objTask.AssignToName = Convert.ToString(dr["AssignBy"]);
                       Task.Add(objTask);
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
               BAL.Common.LogManager.LogError("getTasksByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Task;
       }

       public static List<Project.Entity.Docs> getDocsRelatedToID(string LoedUserRole, string RelatedToID, string RelationType, long LogedUserID)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Docs> Documents = new List<Project.Entity.Docs>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@LoedUserRole", SqlDbType.NVarChar, 30);
               sqlParameter[0].Value = LoedUserRole;

               sqlParameter[1] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = LogedUserID;

               sqlParameter[2] = new SqlParameter("@RelatedToID", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = Convert.ToInt64(RelatedToID);

               sqlParameter[3] = new SqlParameter("@RelationType", SqlDbType.NVarChar, 30);
               sqlParameter[3].Value = RelationType;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDocumentsRelatedToID", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Docs objDoc = new Project.Entity.Docs();
                       objDoc.Doc_ID_Auto_PK = Convert.ToInt64(dr["Doc_ID_Auto_PK"]);
                       objDoc.Title = Convert.ToString(dr["Title"]);
                       objDoc.FileName = Convert.ToString(dr["FileName"]);
                       objDoc.FileID = Convert.ToString(dr["FileID"]);
                       objDoc.UploadedDate = Convert.ToDateTime(dr["UploadedDate"]);
                       objDoc.RelateToLead_ID = Convert.ToInt64(dr["RelateToLead_ID_FK"]);
                       objDoc.RelateToLead_Name = Convert.ToString(dr["LeadName"]);
                       objDoc.DocOwner_ID = Convert.ToInt64(dr["Owner_ID_Fk"]);
                       objDoc.DocOwner_Name = Convert.ToString(dr["OwnerName"]);
                       Documents.Add(objDoc);
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
               BAL.Common.LogManager.LogError("getDocsRelatedToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Documents;
       }

       public static List<Project.Entity.Notes> getNotesByRelateToID(string LogedUserRole, long RelateToID, long LoagedUSerID)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Notes> notes = new List<Project.Entity.Notes>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@LogedUserRole", SqlDbType.NVarChar, 30);
               sqlParameter[0].Value = LogedUserRole;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = RelateToID;

               sqlParameter[2] = new SqlParameter("@LoagedUSerID", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = LoagedUSerID;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetNotes", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   if (Response.ResponseData.Tables[0].Rows[0][0].ToString() != "No Record Found")
                   {
                       foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                       {
                           Project.Entity.Notes objNote = new Project.Entity.Notes();
                           objNote.Note_ID = Convert.ToInt64(dr["Notes_ID_Auto_PK"]);
                           objNote.Title = Convert.ToString(dr["Title"]);
                           objNote.Description = Convert.ToString(dr["Note"]);
                           objNote.RelatedLead_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           objNote.RelatedLead_Name = Convert.ToString(dr["ContactName"]);
                           //objNote.RelatedOpportunity_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           //objNote.RelatedOpportunity_Name = Convert.ToString(dr["Name"]);
                           //objNote.RelatedContact_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           //objNote.RelatedContact_Name = Convert.ToString(dr["Name"]);
                           objNote.Note_Owner_ID = Convert.ToString(dr["NoteOwner_ID"]);
                           objNote.Note_Owner_Name = Convert.ToString(dr["CreatedByName"]);
                           objNote.DateTaken = Convert.ToDateTime(dr["CreatedDate"]).ToString("g");


                           notes.Add(objNote);
                       }
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
               BAL.Common.LogManager.LogError("getNotesByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return notes;
       }

       public static objResponse getEmailTemplate(long rID)
       {
           objResponse Response = new objResponse();
       
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@rID", SqlDbType.BigInt, 30);
               sqlParameter[0].Value = rID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getEmailTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   
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
               BAL.Common.LogManager.LogError("getEmailTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public static objResponse getSmsTemplate(long rID,string dispId)
       {
           objResponse Response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@rID", SqlDbType.BigInt, 30);
               sqlParameter[0].Value = rID;

               sqlParameter[1] = new SqlParameter("@dispId", SqlDbType.BigInt, 30);
               sqlParameter[1].Value = Convert.ToInt64(dispId);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getSmsTemplate", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;

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
               BAL.Common.LogManager.LogError("getSmsTemplate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       
    }
}
