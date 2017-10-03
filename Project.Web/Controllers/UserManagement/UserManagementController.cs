using BAL.User;
using OfficeOpenXml;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.FileImporter;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.UserManagement
{
    public class UserManagementController : Controller
    {
        UserManager objUserManager = new UserManager();
        SessionHelper session;

        //
        // GET: /UserManagement/

        [Authorize]
        public ActionResult UserHome()
        {
            UserModel objModel = new UserModel();
            objModel.users = objUserManager.GetUsers();
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxAddUser(string FirstName, string LastName, string EmailAdd, string UserName, string Mobile, string UserRole, string TimeZone, string App_Link, string Processer, string sales_ID, string Sales_No, string Birthday, string UserGroup)
        {
            objResponse Response = new objResponse();
            Users objUsers = new Users();
            UserModel objUserModel = new UserModel();
            session = new SessionHelper();
            try
            {
                objUsers.FName = FirstName;
                objUsers.LName = LastName;
                objUsers.Email = EmailAdd;
                objUsers.Username = UserName;
                objUsers.Mobile = Mobile;
                objUsers.URole = UserRole;
                objUsers.TimeZone = TimeZone;
                objUsers.Agent_App_Link = App_Link;
                objUsers.Processer = Processer;
                objUsers.Sales_No = Sales_No;
                objUsers.Sales_Id = sales_ID;
               // objUsers.BirthDay = BAL.Helper.Helper.ConvertToDateNullable(Birthday, "MM/dd/yyyy");
                objUsers.BirthDay = Birthday;
                objUsers.Password = BAL.Helper.Helper.GenerateRandomPassword();
                objUsers.CratedBy_ID = Convert.ToInt64(session.UserSession.UserId);
                objUsers.Group = UserGroup;


                Response = objUserManager.AddUser(objUsers);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "User Already Exists")
                    {

                        string body = "Dear " + FirstName + " " + LastName + ", <br/><br/>You are successfully registered to ICS. Following are the your account credentials <br/><br/>email address / username : " + UserName + "<br/>password : " + objUsers.Password + "<br/><br/>If you have any questions or trouble logging on please contact to administrator.<br/><br/>All the best,<br/><br/>ICS";
                        //  BAL.Helper.Helper.SendEmail(objUsers.Email, "Welcome To ICS", body);

                        BAL.Helper.Helper.SendEmailUsingGoDaddy(objUsers.Email, "Welcome To ICS", body);

                        objUserModel.users = objUserManager.GetUsers();
                        return View(objUserModel);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult InviteUser(string FirstName, string LastName, string Email, string UserRole, string Group)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Guid ActId = System.Guid.NewGuid();
                Response = objUserManager.InviteUser(FirstName, LastName, Email, UserRole, Group, Convert.ToInt64(session.UserSession.UserId), ActId.ToString());

                if (Response.ErrorCode == 0)
                {
                    string Link = ConfigurationManager.AppSettings["Activation_Link"] + "?&Activation_ID=" + ActId.ToString();
                    string body = "Dear " + FirstName + " " + LastName + ", <br/><br/>Please Click the below link for activate your account." + "<br/><br/><a href=" + Link + ">" + Link + "</a><br/><br/>All the best,<br/><br/>ICS";
                    //BAL.Helper.Helper.SendEmail(objUsers.Email, "Welcome To ICS", body);

                    BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "Welcome To ICS", body);
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AccountActivation(string Activation_ID)
        {
            objResponse Response = new objResponse();
            try
            {
                Response = BAL.Utility.UtilityManager.getUserNameByActivationID(Activation_ID);
                if (Response.ErrorCode == 0)
                {
                    ViewBag.FName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString();
                    ViewBag.LName = Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                }
                else
                {
                    ViewBag.FName = "";
                    ViewBag.LName = "";
                }
                ViewBag.ActivationId = Activation_ID;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ActivationId = Activation_ID;
                ViewBag.FName = "";
                ViewBag.LName = "";
                BAL.Common.LogManager.LogError("AccountActivation Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
        }

        public ActionResult UploadUserExcel()
        {
            return View();
        }

        [Authorize]
        public ActionResult DownLoadSample(string file_path)
        {
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Import_Sample_Dir"]) + file_path;
                string contentType = "application/pdf";
                return File(newFilePath, contentType, file_path);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DownLoadSample Req", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View("500");
            }
        }

        [Authorize]
        public ActionResult UploadExcelFile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ImportExcelFileToUserDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<Users> users = new List<Users>();

            DataTable dt = new DataTable();

            // string Result="";
            try
            {
                string fname;

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        }

                        var excel = new ExcelPackage(file.InputStream);
                        dt = ExcelPackageExtensions.ToDataTable(excel);
                        int count = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            Users objUser = new Users();

                            objUser.Username = dr[0].ToString();
                            objUser.FName = dr[1].ToString();
                            objUser.LName = dr[2].ToString();
                            objUser.Mobile = dr[3].ToString();
                            objUser.Email = dr[4].ToString();
                            objUser.URole = dr[5].ToString();
                            objUser.Group = dr[6].ToString();
                            objUser.TimeZone = dr[7].ToString();
                            objUser.Agent_App_Link = dr[8].ToString();
                            objUser.Processer = dr[9].ToString();
                            objUser.Sales_No = dr[10].ToString();
                            objUser.Sales_Id = dr[11].ToString();
                            objUser.BirthDay = dr[12].ToString();
                            objUser.Password = BAL.Helper.Helper.GenerateRandomPassword();
                            objUser.CratedBy_ID = Convert.ToInt64(session.UserSession.UserId);

                            Response = objUserManager.AddUser(objUser);
                            if (Response.ErrorCode == 0)
                            {
                                if (Response.ErrorMessage != "User Already Exists")
                                {
                                    count++;
                                }
                            }
                            else
                            {
                                return Json("Fail", JsonRequestBehavior.AllowGet);
                            }

                        }
                        return Json("Success," + count.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ImportExcelFileToUserDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(string FirstName, string LastName, string Email, string User_ID_PK)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                string newPass = BAL.Helper.Helper.GenerateRandomPassword();
                Response = objUserManager.ResetPassword(User_ID_PK,newPass ,Convert.ToInt64(session.UserSession.UserId));

                if (Response.ErrorCode == 0)
                {
                   
                    string body = "Dear " + FirstName + " " + LastName + ", <br/><br/>Your ICS account password is successfully reset." + "<br/><br/>Below is your new password. <br/><h5>"+newPass+"</h5><br/><br/>All the best,<br/><br/>ICS";
                    //BAL.Helper.Helper.SendEmail(objUsers.Email, "Welcome To ICS", body);

                    BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "ICS Password Reset", body);
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ActionName("GetUserRoles")]
        public ActionResult GetUserRoles()
        {

            List<TextValue> roles = objUserManager.GetUserRolesForDropDown();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Value = "0", Text = "Choose a Role" });
            foreach (TextValue rl in roles)
            {
                list.Add(new SelectListItem { Value = rl.Value, Text = rl.Text });
            }

            JsonResult jResult = Json(list, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetUserForEdit(string UserID)
        {
            objResponse Response = new objResponse();
            UserModel objUser = new UserModel();
            try
            {
                Response = objUserManager.GetUserForEdit(UserID);

                if (Response.ErrorCode == 0)
                {
                    objUser.User_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]);
                    objUser.FName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString();
                    objUser.LName = Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                    objUser.Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString();
                    objUser.Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                    objUser.Mobile = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString();
                    objUser.URole = Response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString();
                    objUser.TimeZone = Response.ResponseData.Tables[0].Rows[0]["Time_Zone"].ToString();
                    objUser.Agent_App_Link = Response.ResponseData.Tables[0].Rows[0]["Agent_app_Link"].ToString();
                    objUser.Processer = Response.ResponseData.Tables[0].Rows[0]["Processer"].ToString();
                    objUser.Sales_No = Response.ResponseData.Tables[0].Rows[0]["Sales_No"].ToString();
                    objUser.Sales_Id = Response.ResponseData.Tables[0].Rows[0]["Sales_ID"].ToString();
                    objUser.BirthDay = Response.ResponseData.Tables[0].Rows[0]["Birthday"].ToString();
                    objUser.Group = Response.ResponseData.Tables[0].Rows[0]["User_Group"].ToString();

                    return Json(objUser, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetUserForEdit Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxUpdateUser(string FirstName, string LastName, string EmailAdd, string Mobile, string UserRole, string TimeZone, string App_Link, string Processer, string sales_ID, string Sales_No, string Birthday, string UserGroup,string UserID)
        {
            objResponse Response = new objResponse();
            Users objUsers = new Users();
            UserModel objUserModel = new UserModel();
            session = new SessionHelper();
            try
            {
                objUsers.FName = FirstName;
                objUsers.LName = LastName;
                objUsers.Email = EmailAdd;                
                objUsers.Mobile = Mobile;
                objUsers.URole = UserRole;
                objUsers.TimeZone = TimeZone;
                objUsers.Agent_App_Link = App_Link;
                objUsers.Processer = Processer;
                objUsers.Sales_No = Sales_No;
                objUsers.Sales_Id = sales_ID;
                //objUsers.BirthDay = BAL.Helper.Helper.ConvertToDateNullable(Birthday, "MM/dd/yyyy"); 
                objUsers.BirthDay = Birthday;
                objUsers.CratedBy_ID = Convert.ToInt64(session.UserSession.UserId);
                objUsers.Group = UserGroup;
                objUsers.User_ID = Convert.ToInt64(UserID);


                Response = objUserManager.UpdateUser(objUsers);

                if (Response.ErrorCode == 0)
                {

                    objUserModel.users = objUserManager.GetUsers();
                    return View("AjaxAddUser",objUserModel);                
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdateUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
