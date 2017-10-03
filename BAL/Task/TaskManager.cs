using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Task
{
    public class TaskManager
    {
        public objResponse AddTask(string Title, DateTime StartDate, DateTime EndDate, long Relate_To_ID, string Description, string RemindMe, string Hours, string Minutes, string Status, string AssignTo, long OwnerID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[13];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.DateTime, 60);
                sqlParameter[1].Value = StartDate;

                sqlParameter[2] = new SqlParameter("@Relate_To_ID", SqlDbType.BigInt, 10);
                sqlParameter[2].Value = Relate_To_ID;

                sqlParameter[3] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                sqlParameter[3].Value = Description;

                sqlParameter[4] = new SqlParameter("@RemindMe", SqlDbType.NVarChar, 2);
                sqlParameter[4].Value = RemindMe;

                sqlParameter[5] = new SqlParameter("@Hours", SqlDbType.NVarChar, 3);
                sqlParameter[5].Value = Hours;

                sqlParameter[6] = new SqlParameter("@Minutes", SqlDbType.NVarChar, 3);
                sqlParameter[6].Value = Minutes;

                sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 3);
                sqlParameter[7].Value = Status;

                sqlParameter[8] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
                sqlParameter[8].Value = OwnerID;

                sqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
                sqlParameter[9].Value = DateTime.Now;             
                

                sqlParameter[10] = new SqlParameter("@EndDate", SqlDbType.DateTime, 60);
                sqlParameter[10].Value = EndDate;

                sqlParameter[11] = new SqlParameter("@AssignTo", SqlDbType.NVarChar, 60);
                sqlParameter[11].Value = AssignTo;

                sqlParameter[12] = new SqlParameter("@OwnerID", SqlDbType.BigInt, 10);
                sqlParameter[12].Value = OwnerID;

                //sqlParameter[13] = new SqlParameter("@StartTime", SqlDbType.NVarChar, 8);
                //sqlParameter[13].Value = StartTime;

                //sqlParameter[14] = new SqlParameter("@EndTime", SqlDbType.NVarChar, 8);
                //sqlParameter[14].Value = EndTime;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddTask", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("AddTask", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public List<Project.Entity.Tasks> getTasksByRelateToID(long PIN, long RelateToID,long LogedUserID)
        {
            objResponse Response = new objResponse();
            List<Project.Entity.Tasks> Task = new List<Project.Entity.Tasks>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];

                sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = PIN;

                sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = RelateToID;

                sqlParameter[2] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
                sqlParameter[2].Value = LogedUserID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTasks", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Project.Entity.Tasks objTask = new Project.Entity.Tasks();
                        objTask.Task_ID = Convert.ToInt64(dr["Task_ID_Auto_PK"]);
                        objTask.Title = Convert.ToString(dr["Title"]);
                        objTask.StartDate = Convert.ToDateTime(dr["StartDate"]).ToString("d MMM yyyy");
                        objTask.EndDate = Convert.ToDateTime(dr["EndDate"]).ToString("d MMM yyyy");
                        objTask.Description = Convert.ToString(dr["Description"]);
                        objTask.RelateTo = Convert.ToInt64(dr["RelateTo_ID"]);
                        objTask.RelateToName = Convert.ToString(dr["Name"]);
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
    }
}
