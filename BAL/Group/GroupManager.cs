using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Group
{
    public class GroupManager
    {
        public List<Groups> GetAllGroups()
        {
            objResponse Response = new objResponse();
            List<Groups> groups = new List<Groups>();
            try
            {

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllGroups", DB_CONSTANTS.ConnectionString_ICS);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Groups objGroup = new Groups();
                        objGroup.Group_ID = Convert.ToInt64(dr["Group_ID"]);
                        objGroup.Group_Name = Convert.ToString(dr["Group_Name"]);
                        objGroup.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                        objGroup.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        objGroup.UpdatedByName = Convert.ToString(dr["UpdatedByName"]);
                        objGroup.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objGroup.Status = Convert.ToString(dr["Status"]);

                        groups.Add(objGroup);
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
                BAL.Common.LogManager.LogError("GetAllGroups", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));  
            }
            return groups;
        }

        public objResponse AddGroup(string Name , long LogedUser)
        {
            objResponse Response = new objResponse();           
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = Name;

                sqlParameter[1] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
                sqlParameter[1].Value = LogedUser;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddGroup", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                BAL.Common.LogManager.LogError("AddGroup", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse EditGroup(string Name,long GroupID ,long LogedUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];

                sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = Name;

                sqlParameter[1] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 20);
                sqlParameter[1].Value = LogedUser;

                sqlParameter[2] = new SqlParameter("@GroupID", SqlDbType.BigInt, 20);
                sqlParameter[2].Value = GroupID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditGroup", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                BAL.Common.LogManager.LogError("EditGroup", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public string DeleteGroup(long GroupID)
        {
            objResponse response = new objResponse();
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@GroupID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = GroupID;

                DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteGroup", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

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
                BAL.Common.LogManager.LogError("DeleteGroup", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;
        }
    }
}
