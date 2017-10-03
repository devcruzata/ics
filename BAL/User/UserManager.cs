using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.User
{
    public class UserManager
    {
        public objResponse validateUser(string UserName, string Password)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
                sqlParameter[0].Value = UserName;

                sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 20);
                sqlParameter[1].Value = Password;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ValidateUser", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("validate User", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddUser(Users objUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[16];

                sqlParameter[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 60);
                sqlParameter[0].Value = objUser.FName;

                sqlParameter[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 60);
                sqlParameter[1].Value = objUser.LName;

                sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
                sqlParameter[2].Value = objUser.Email;

                sqlParameter[3] = new SqlParameter("@Username", SqlDbType.NVarChar, 80);
                sqlParameter[3].Value = objUser.Username;

                sqlParameter[4] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 13);
                sqlParameter[4].Value = objUser.Mobile;

                sqlParameter[5] = new SqlParameter("@UserRole", SqlDbType.BigInt, 40);
                sqlParameter[5].Value = Convert.ToInt64(objUser.URole);

                sqlParameter[6] = new SqlParameter("@TimeZone", SqlDbType.NVarChar, 40);
                sqlParameter[6].Value = objUser.TimeZone;

                sqlParameter[7] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 20);
                sqlParameter[7].Value = objUser.CratedBy_ID;

                sqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
                sqlParameter[8].Value = DateTime.Now;

                sqlParameter[9] = new SqlParameter("@Password", SqlDbType.NVarChar, 30);
                sqlParameter[9].Value = objUser.Password;

                sqlParameter[10] = new SqlParameter("@Agent_App_Link", SqlDbType.NVarChar, 100);
                sqlParameter[10].Value = objUser.Agent_App_Link;

                sqlParameter[11] = new SqlParameter("@Processer", SqlDbType.NVarChar, 100);
                sqlParameter[11].Value = objUser.Processer;

                sqlParameter[12] = new SqlParameter("@Sales_No", SqlDbType.NVarChar, 100);
                sqlParameter[12].Value = objUser.Sales_No;

                sqlParameter[13] = new SqlParameter("@Sales_Id", SqlDbType.NVarChar, 50);
                sqlParameter[13].Value = objUser.Sales_Id;

                sqlParameter[14] = new SqlParameter("@BirthDay", SqlDbType.NVarChar, 30);
                sqlParameter[14].Value = objUser.BirthDay;

                sqlParameter[15] = new SqlParameter("@UserGroup", SqlDbType.NVarChar, 50);
                sqlParameter[15].Value = objUser.Group;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUser", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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

        public List<Users> GetUsers()
        {
            objResponse Response = new objResponse();
            List<Users> user = new List<Users>();
            try
            {

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllUsers", DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Users objUser = new Users();
                        objUser.User_ID = Convert.ToInt64(dr["User_ID_Auto_PK"]);
                        objUser.FName = dr["User_FirstName"].ToString();
                        objUser.LName = dr["User_LastName"].ToString();
                        objUser.Email = dr["User_Email"].ToString();
                        objUser.Username = dr["UserName"].ToString();
                        objUser.Mobile = dr["User_Contact"].ToString();
                        objUser.URoleName = dr["RoleName"].ToString();
                        objUser.TimeZone = dr["Time_Zone"].ToString();
                        objUser.Status = dr["Status"].ToString();
                        if (objUser.Status != "Deactive")
                        {
                            objUser.LastLogin = Convert.ToDateTime(dr["LastLogin"]);
                        }


                        user.Add(objUser);
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
                BAL.Common.LogManager.LogError("GetUsers", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return user;
        }

        public objResponse ActivateAccount(string UserName, string Password, string Act_ID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];

                sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
                sqlParameter[0].Value = UserName;

                sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 20);
                sqlParameter[1].Value = Password;

                sqlParameter[2] = new SqlParameter("@Act_ID", SqlDbType.NVarChar, 40);
                sqlParameter[2].Value = Act_ID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ActivateAccount", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("ActivateAccount", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse InviteUser(string FName, string LName, string Email, string Role, string Group, long CreatedBy_ID, string Activation_ID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[8];

                sqlParameter[0] = new SqlParameter("@FName", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = FName;

                sqlParameter[1] = new SqlParameter("@LName", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = LName;

                sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
                sqlParameter[2].Value = Email;

                sqlParameter[3] = new SqlParameter("@Group", SqlDbType.NVarChar, 80);
                sqlParameter[3].Value = Group;

                sqlParameter[4] = new SqlParameter("@Role", SqlDbType.NVarChar, 13);
                sqlParameter[4].Value = Role;

                sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 40);
                sqlParameter[5].Value = CreatedBy_ID;

                sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
                sqlParameter[6].Value = DateTime.Now;

                sqlParameter[7] = new SqlParameter("@Activation_ID", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = Activation_ID;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_InviteUser", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("InviteUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }


        public objResponse ResetPassword(string User_ID, string Password, long CreatedBy_ID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@User_ID", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = Convert.ToInt64(User_ID);

                sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = Password;

                sqlParameter[2] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 40);
                sqlParameter[2].Value = CreatedBy_ID;

                sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
                sqlParameter[3].Value = DateTime.Now;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ResetUserPassword", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("ResetPassword", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public List<TextValue> GetUserRolesForDropDown()
        {
            objResponse Response = new objResponse();
            List<TextValue> RoleList = new List<TextValue>();
            try
            {


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUserRoleForDropdown", DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        TextValue roles = new TextValue();
                        roles.Text = dr["User_Role_Desc"].ToString();
                        roles.Value = dr["User_Role_ID_Auto_Pk"].ToString();
                          
                        RoleList.Add(roles);
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
                BAL.Common.LogManager.LogError("GetUserRolesForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return RoleList;
        }

        public objResponse UpdateUser(Users objUser)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[17];

                sqlParameter[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 60);
                sqlParameter[0].Value = objUser.FName;

                sqlParameter[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 60);
                sqlParameter[1].Value = objUser.LName;

                sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
                sqlParameter[2].Value = objUser.Email;

                sqlParameter[3] = new SqlParameter("@Username", SqlDbType.NVarChar, 80);
                sqlParameter[3].Value = objUser.Username;

                sqlParameter[4] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 13);
                sqlParameter[4].Value = objUser.Mobile;

                sqlParameter[5] = new SqlParameter("@UserRole", SqlDbType.BigInt, 40);
                sqlParameter[5].Value = Convert.ToInt64(objUser.URole);

                sqlParameter[6] = new SqlParameter("@TimeZone", SqlDbType.NVarChar, 40);
                sqlParameter[6].Value = objUser.TimeZone;

                sqlParameter[7] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 20);
                sqlParameter[7].Value = objUser.CratedBy_ID;

                sqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
                sqlParameter[8].Value = DateTime.Now;

                sqlParameter[9] = new SqlParameter("@Password", SqlDbType.NVarChar, 30);
                sqlParameter[9].Value = objUser.Password;

                sqlParameter[10] = new SqlParameter("@Agent_App_Link", SqlDbType.NVarChar, 100);
                sqlParameter[10].Value = objUser.Agent_App_Link;

                sqlParameter[11] = new SqlParameter("@Processer", SqlDbType.NVarChar, 100);
                sqlParameter[11].Value = objUser.Processer;

                sqlParameter[12] = new SqlParameter("@Sales_No", SqlDbType.NVarChar, 100);
                sqlParameter[12].Value = objUser.Sales_No;

                sqlParameter[13] = new SqlParameter("@Sales_Id", SqlDbType.NVarChar, 50);
                sqlParameter[13].Value = objUser.Sales_Id;

                sqlParameter[14] = new SqlParameter("@BirthDay", SqlDbType.NVarChar, 30);
                sqlParameter[14].Value = objUser.BirthDay;

                sqlParameter[15] = new SqlParameter("@UserGroup", SqlDbType.NVarChar, 50);
                sqlParameter[15].Value = objUser.Group;

                sqlParameter[16] = new SqlParameter("@User_ID", SqlDbType.BigInt, 10);
                sqlParameter[16].Value = objUser.User_ID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateUser", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("UpdateUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse GetUserForEdit(string User_ID)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@User_ID", SqlDbType.BigInt, 50);
                sqlParameter[0].Value = Convert.ToInt64(User_ID);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUserForEdit", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                BAL.Common.LogManager.LogError("GetUserForEdit", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
