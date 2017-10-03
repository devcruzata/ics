using BAL.User;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Project.Web.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        UserManager objUserManager = new UserManager();
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }
        //
        // GET: /Authentication/
        [HttpGet]
        public ActionResult Login()
        {
            //if (Convert.ToString(User.Identity.Name) != "")
            //{
            //    UserModel objUserModel = objJavaScriptSerializer.Deserialize<UserModel>(User.Identity.Name);

            //    if (objUserModel.UserType == "SUP")
            //    {
            //        return RedirectToRoute("SuperAdmin");
            //    }
            //    else if (objUserModel.UserType == "ADM")
            //    {
            //        //return RedirectToRoute("Admin");
            //        return RedirectToAction("AdminHome", "Home");
            //    }
            //    else
            //    {
            //        return RedirectToRoute("User");
            //    }
               
            //}
            LoginModel objModel = new LoginModel();
            if (Request.Cookies["ICSCookies"] != null)
            {
                HttpCookie getCookie = Request.Cookies["ICSCookies"];
                objModel.UserName = Convert.ToString(getCookie.Values["UserName"]);
                objModel.Password = Convert.ToString(getCookie.Values["Password"]);
                objModel.RememberMe = true;               
                return View(objModel);
            }
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            objResponse response = new objResponse();
            UserModel objUser = new UserModel();
            try
            {                
                if (model.RememberMe == true)
                {
                    Response.Cookies["ICSCookies"]["UserName"] = model.UserName;
                    Response.Cookies["ICSCookies"]["Password"] = model.Password;
                    Response.Cookies["ICSCookies"].Expires = DateTime.Now.AddDays(28);
                }
                else
                {
                    Response.Cookies["ICSCookies"].Expires = DateTime.Now.AddDays(-1);
                }
                response = objUserManager.validateUser(model.UserName, model.Password);

                if (response.ErrorCode == 0)
                {
                    if (response.ErrorMessage != "Incorrect UserName" && response.ErrorMessage != "Incorrect Password" && response.ErrorMessage != "Deactivated User. Please Contact Administrator For Activation")
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                       // objUser.FirstName = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString();
                      //  objUser.LastName = response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                      //  objUser.UserName = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                      //  objUser.Mobile = response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString();
                     //   objUser.Email = response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString();
                      //  objUser.Addres = response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString();
                      //  objUser.UserType = response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                      //  objUser.SubscriptionId = response.ResponseData.Tables[0].Rows[0]["Subscription_ID"].ToString();
                      //  objUser.Pin = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["PIN"]);

                       // FormsService.SignIn(objJavaScriptSerializer.Serialize(objUser), model.RememberMe);
                        Session["User"] = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        Session["User_Type"] = response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString();
                        Session["UserName"] = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        Session["UserID"] = response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                       // Session["GroupID"] = response.ResponseData.Tables[0].Rows[0]["User_Group"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            Username = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString()
                           
                        };

                        session.UserPermissionsSession = new UserPermissionsSession()
                        {
                            AssociatedLeads = response.ResponseData.Tables[1].Rows[0]["AssociatedLeads"].ToString(),
                            SystemwideLeads = response.ResponseData.Tables[1].Rows[0]["SystemwideLeads"].ToString(),
                            Calendar = response.ResponseData.Tables[1].Rows[0]["Calendar"].ToString(),
                            EditTask = response.ResponseData.Tables[1].Rows[0]["EditEvent"].ToString(),
                            LeadNotes = response.ResponseData.Tables[1].Rows[0]["LeadNotes"].ToString(),
                            Documents = response.ResponseData.Tables[1].Rows[0]["Documents"].ToString(),
                            ManageDocuments = response.ResponseData.Tables[1].Rows[0]["ManageDocuments"].ToString(),
                            HelpDeskTickets = response.ResponseData.Tables[1].Rows[0]["HelpDeskTickets"].ToString(),
                            LeadDistribution = response.ResponseData.Tables[1].Rows[0]["LeadDistribution"].ToString(),
                            ResidualsAccess = response.ResponseData.Tables[1].Rows[0]["ResidualsAccess"].ToString(),
                            ResidualManagerView = response.ResponseData.Tables[1].Rows[0]["ResidualManagerView"].ToString(),
                            MerchantinfoAssociatedLead = response.ResponseData.Tables[1].Rows[0]["MerchantInfo_Associated_Leads"].ToString(),
                            MerchantinfoSystemwideLeads = response.ResponseData.Tables[1].Rows[0]["Merchant_Info_Systemwide_Leads"].ToString(),
                            ManageMerchant = response.ResponseData.Tables[1].Rows[0]["ManageMerchant"].ToString(),
                            Statements = response.ResponseData.Tables[1].Rows[0]["Statements"].ToString(),
                            TriggerStatementDownload = response.ResponseData.Tables[1].Rows[0]["StatementDownload"].ToString(),
                            PortfolioActivityAssociatedleads = response.ResponseData.Tables[1].Rows[0]["PortfolioActivities_Associated_Leads"].ToString(),
                            PortfolioActivitySystemwideleads = response.ResponseData.Tables[1].Rows[0]["PortfolioActivites_Systemwide_Leads"].ToString(),
                            ProposalGenerator = response.ResponseData.Tables[1].Rows[0]["PraposalGenreator"].ToString(),
                            MerchantApplication = response.ResponseData.Tables[1].Rows[0]["MerchantApplication"].ToString(),
                            UnderwritngApplicationSubmissions = response.ResponseData.Tables[1].Rows[0]["UnderwritingAppSubmission"].ToString(),
                            Agenttracker = response.ResponseData.Tables[1].Rows[0]["AgentTracker"].ToString(),
                            MessagingSystem = response.ResponseData.Tables[1].Rows[0]["MessagingSystem"].ToString(),
                            SiteManagement = response.ResponseData.Tables[1].Rows[0]["SiteManagement"].ToString(),
                            UserManagement = response.ResponseData.Tables[1].Rows[0]["UserManagement"].ToString()
                        };


                       // if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "Administrator")
                       // {
                           // return RedirectToRoute("MyAccount");
                            return RedirectToRoute("AdminDashboard");
                       // }
                       // else if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "ADM")
                      //  {
                            //return RedirectToRoute("Admin");
                      //      return RedirectToAction("AdminHome", "Home");
                      //  }
                      //  else
                      //  {
                      //      return RedirectToRoute("User");
                      //  }

                    }
                    else
                    {
                        ViewBag.Error_Msg = response.ErrorMessage;
                        TempData["Error_Msg"] = response.ErrorMessage;
                        // return View();
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = response.ErrorMessage;
                    TempData["Error_Msg"] = response.ErrorMessage;
                    // return View();
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                //return View();
                return RedirectToAction("Login", "Authentication");
            }
        }

        [HttpPost]
        public ActionResult Authenticate(LoginModel model)
        {
            objResponse Response = new objResponse();
            UserModel objUser = new UserModel();
            try
            {
                Response = objUserManager.validateUser(model.UserName, model.Password);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Incorrect UserName" && Response.ErrorMessage != "Incorrect Password" && Response.ErrorMessage != "Deactivated User. Please Contact Administrator For Activation")
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        Session["User"] = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        Session["UserID"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = Response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
                            Subscription_ID = Response.ResponseData.Tables[0].Rows[0]["Subscription_ID"].ToString()
                           // PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()
                        };


                        // if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "Administrator")
                        // {
                        // return RedirectToRoute("MyAccount");
                        return RedirectToRoute("AdminDashboard");
                        // }
                        // else if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "ADM")
                        //  {
                        //return RedirectToRoute("Admin");
                        //      return RedirectToAction("AdminHome", "Home");
                        //  }
                        //  else
                        //  {
                        //      return RedirectToRoute("User");
                        //  }

                    }
                    else
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        TempData["Error_Msg"] = Response.ErrorMessage;
                        // return View();
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    TempData["Error_Msg"] = Response.ErrorMessage;
                    // return View();
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                //return View();
                return RedirectToAction("Login", "Authentication");
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("~/");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize]
        public ActionResult LockScreen()
        {
            SessionHelper session = new SessionHelper();
            ViewBag.Username = session.UserSession.Email;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LockScreen(string Username , string Password , string LastPage)
        {
            objResponse response = new objResponse();
            try
            {
                response = objUserManager.validateUser(Username, Password);

                if (response.ErrorCode == 0)
                {
                    if (response.ErrorMessage != "Incorrect UserName" && response.ErrorMessage != "Incorrect Password")
                    {
                        FormsAuthentication.SetAuthCookie(Username, false);
                        Session["User"] = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        Session["User_Type"] = response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        Session["UserID"] = response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            Username = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
                            Subscription_ID = response.ResponseData.Tables[0].Rows[0]["Subscription_ID"].ToString()
                           // PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()

                        };

                        session.UserPermissionsSession = new UserPermissionsSession()
                        {
                            AssociatedLeads = response.ResponseData.Tables[1].Rows[0]["AssociatedLeads"].ToString(),
                            SystemwideLeads = response.ResponseData.Tables[1].Rows[0]["SystemwideLeads"].ToString(),
                            Calendar = response.ResponseData.Tables[1].Rows[0]["Calendar"].ToString(),
                            EditTask = response.ResponseData.Tables[1].Rows[0]["EditEvent"].ToString(),
                            LeadNotes = response.ResponseData.Tables[1].Rows[0]["LeadNotes"].ToString(),
                            Documents = response.ResponseData.Tables[1].Rows[0]["Documents"].ToString(),
                            ManageDocuments = response.ResponseData.Tables[1].Rows[0]["ManageDocuments"].ToString(),
                            HelpDeskTickets = response.ResponseData.Tables[1].Rows[0]["HelpDeskTickets"].ToString(),
                            LeadDistribution = response.ResponseData.Tables[1].Rows[0]["LeadDistribution"].ToString(),
                            ResidualsAccess = response.ResponseData.Tables[1].Rows[0]["ResidualsAccess"].ToString(),
                            ResidualManagerView = response.ResponseData.Tables[1].Rows[0]["ResidualManagerView"].ToString(),
                            MerchantinfoAssociatedLead = response.ResponseData.Tables[1].Rows[0]["MerchantInfo_Associated_Leads"].ToString(),
                            MerchantinfoSystemwideLeads = response.ResponseData.Tables[1].Rows[0]["Merchant_Info_Systemwide_Leads"].ToString(),
                            ManageMerchant = response.ResponseData.Tables[1].Rows[0]["ManageMerchant"].ToString(),
                            Statements = response.ResponseData.Tables[1].Rows[0]["Statements"].ToString(),
                            TriggerStatementDownload = response.ResponseData.Tables[1].Rows[0]["StatementDownload"].ToString(),
                            PortfolioActivityAssociatedleads = response.ResponseData.Tables[1].Rows[0]["PortfolioActivities_Associated_Leads"].ToString(),
                            PortfolioActivitySystemwideleads = response.ResponseData.Tables[1].Rows[0]["PortfolioActivites_Systemwide_Leads"].ToString(),
                            ProposalGenerator = response.ResponseData.Tables[1].Rows[0]["PraposalGenreator"].ToString(),
                            MerchantApplication = response.ResponseData.Tables[1].Rows[0]["MerchantApplication"].ToString(),
                            UnderwritngApplicationSubmissions = response.ResponseData.Tables[1].Rows[0]["UnderwritingAppSubmission"].ToString(),
                            Agenttracker = response.ResponseData.Tables[1].Rows[0]["AgentTracker"].ToString(),
                            MessagingSystem = response.ResponseData.Tables[1].Rows[0]["MessagingSystem"].ToString(),
                            SiteManagement = response.ResponseData.Tables[1].Rows[0]["SiteManagement"].ToString(),
                            UserManagement = response.ResponseData.Tables[1].Rows[0]["UserManagement"].ToString()
                        };
                        // if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "Administrator")
                        // {
                        // return RedirectToRoute("MyAccount");
                        return RedirectToRoute("AdminDashboard");
                        // }
                        // else if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "ADM")
                        //  {
                        //return RedirectToRoute("Admin");
                        //      return RedirectToAction("AdminHome", "Home");
                        //  }
                        //  else
                        //  {
                        //      return RedirectToRoute("User");
                        //  }             

                    }
                    else
                    {
                        ViewBag.Error_Msg = response.ErrorMessage;
                        TempData["Error_Msg"] = response.ErrorMessage;
                        // return View();
                        return RedirectToAction("LockScreen", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = response.ErrorMessage;
                    TempData["Error_Msg"] = response.ErrorMessage;
                    // return View();
                    return RedirectToAction("LockScreen", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Lock Screen Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                //return View();
                return RedirectToAction("Login", "Authentication");
            }
        }

        

        [HttpPost]
        public ActionResult ActivateAccount(string Username, string Password, string Activation_ID)
        {
            objResponse response = new objResponse();
            UserModel objUser = new UserModel();
            try
            {
                response = objUserManager.ActivateAccount(Username, Password, Activation_ID);

                if (response.ErrorCode == 0)
                {
                    if (response.ErrorMessage != "Incorrect UserName" && response.ErrorMessage != "Incorrect Password" && response.ErrorMessage != "Deactivated User. Please Contact Administrator For Activation")
                    {
                        FormsAuthentication.SetAuthCookie(Username, false);
                        
                        Session["User"] = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        Session["User_Type"] = response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString();
                        Session["UserName"] = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        Session["UserID"] = response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            Username = response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString()
                          
                        };


                        if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "Administrator")
                        {
                            return RedirectToRoute("MyAccount");
                        }
                        else if (response.ResponseData.Tables[0].Rows[0]["User_Role"].ToString() == "ADM")
                        {
                           
                            return RedirectToAction("AdminHome", "Home");
                        }
                        else
                        {
                            return RedirectToRoute("User");
                        }

                    }
                    else
                    {
                        ViewBag.Error_Msg = response.ErrorMessage;
                        TempData["Error_Msg"] = response.ErrorMessage;                       
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = response.ErrorMessage;
                    TempData["Error_Msg"] = response.ErrorMessage;                    
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));                
                return RedirectToAction("Login", "Authentication");
            }
        }

    }
}
