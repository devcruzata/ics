using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Document
{
   public class DocumentManager
    {
       public List<Project.Entity.Docs> getDocs(long PIN , long LogedUserID)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Docs> Documents = new List<Project.Entity.Docs>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = LogedUserID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDocuments", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               BAL.Common.LogManager.LogError("getDocs", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Documents;
       }

       public string DeleteDocument(Int64 Doc_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Doc_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Doc_id_pk;
               

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteDoc", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
               BAL.Common.LogManager.LogError("DeleteDocument", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse AddDoc(string Title , string LeadID ,string FileName ,string OwnerId,string FileID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[9];

               sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 200);
               sqlParameter[0].Value = Title;

               sqlParameter[1] = new SqlParameter("@LeadID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = Convert.ToInt64(LeadID);               

               sqlParameter[2] = new SqlParameter("@OwnerId", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = Convert.ToInt64(OwnerId);

               sqlParameter[3] = new SqlParameter("@FileName", SqlDbType.NVarChar, 200);
               sqlParameter[3].Value = FileName;

               sqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[4].Value = Convert.ToInt64(OwnerId);

               sqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[5].Value = DateTime.Now;

               sqlParameter[6] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[6].Value = "Active";               

               sqlParameter[7] = new SqlParameter("@UploadedDate", SqlDbType.Date, 50);
               sqlParameter[7].Value = DateTime.Now;

               sqlParameter[8] = new SqlParameter("@FileID", SqlDbType.NVarChar, 50);
               sqlParameter[8].Value = FileID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNewDoc", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddDoc", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Docs> getDocsRelatedToID(long PIN, string RelatedToID, string RelationType, long LogedUserID)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Docs> Documents = new List<Project.Entity.Docs>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

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
    }
}
