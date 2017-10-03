using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Project.Web.Controllers.AdminSeting
{
    public class AdminSetingController : Controller
    {
        BAL.Group.GroupManager objGroupManager = new BAL.Group.GroupManager();
        BAL.Source.SourceManager objSourceManager = new BAL.Source.SourceManager();
        BAL.GenralSeting.GenralSetingManager objGenralSetingManager = new BAL.GenralSeting.GenralSetingManager();
        BAL.LeadStatuses.LeadStatusmanager objLeadStatusManager = new BAL.LeadStatuses.LeadStatusmanager();
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();

        BAL.Roles.RolesManager objRoleManager = new BAL.Roles.RolesManager();
        SessionHelper session;
        //
        // GET: /AdminSeting/
        [Authorize]
        public ActionResult SetingHome()
        {
            objResponse Response = new objResponse();
            AdminSetingModel objSetings = new AdminSetingModel();
            session = new SessionHelper();
            try
            {
                Response = objGenralSetingManager.getGenralSeting();
                if (Response.ErrorCode == 0)
                {
                    objSetings.GenralSeting_ID_Auto_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Genral_Seting_ID_Auto_PK"]);
                    objSetings.Company = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ComapanyName"]);
                    objSetings.Address = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Address"]);
                    objSetings.City = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["City"]);
                    objSetings.Stete = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["State"]);
                    objSetings.Country = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Country"]);
                    objSetings.Zipcode = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Zipcode"]);
                    objSetings.Phone = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Phone"]);
                    objSetings.Website = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Website"]);
                    objSetings.Currency = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Currency"]);

                    objSetings.groups = objGroupManager.GetAllGroups();
                    objSetings.source = objSourceManager.GetAllSource();
                    objSetings.status = objLeadStatusManager.GetAllStatus();
                    objSetings.roles = objRoleManager.GetAllRoles();
                    return View(objSetings);
                }
                else
                {
                    return View(objSetings);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SetingsHome conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetings);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxGenralSetings(AdminSetingModel objModel)
        {
            objResponse Response = new objResponse();
            AdminSetingModel objSetings = new AdminSetingModel();
            Project.Entity.AdminSeting objGenralSetings = new Project.Entity.AdminSeting();
            session = new SessionHelper();
            try
            {
                objGenralSetings.Company = objModel.Company;
                objGenralSetings.Address = objModel.Address;
                objGenralSetings.City = objModel.City;
                objGenralSetings.Stete = objModel.Stete;
                objGenralSetings.Country = objModel.Country;
                objGenralSetings.Zipcode = objModel.Zipcode;
                objGenralSetings.Phone = objModel.Phone;
                objGenralSetings.Website = objModel.Website;
                objGenralSetings.Currency = objModel.Currency;
                //objGenralSetings.Customer_ID = Convert.ToInt64(session.UserSession.PIN);

                Response = objGenralSetingManager.AddCompanyProfile(objGenralSetings, Convert.ToInt64(session.UserSession.UserId));

                if (Response.ErrorCode == 0)
                {

                    Response = objGenralSetingManager.getGenralSeting();
                    if (Response.ErrorCode == 0)
                    {
                        objSetings.GenralSeting_ID_Auto_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Genral_Seting_ID_Auto_PK"]);
                        objSetings.Company = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ComapanyName"]);
                        objSetings.Address = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Address"]);
                        objSetings.City = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["City"]);
                        objSetings.Stete = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["State"]);
                        objSetings.Country = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Country"]);
                        objSetings.Zipcode = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Zipcode"]);
                        objSetings.Phone = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Phone"]);
                        objSetings.Website = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Website"]);
                        objSetings.Currency = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Currency"]);

                    }
                    return View(objSetings);
                }
                else
                {
                    return View(objSetings);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxGenralSetings Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetings);
            }
        }

        [Authorize]
        public ActionResult GroupsHome()
        {
            GroupsModel objModel = new GroupsModel();
            objModel.groups = objGroupManager.GetAllGroups();
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddGroup(string GroupName)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();

            GroupsModel objGrpModel = new GroupsModel();            
            try
            {

                Response = objGroupManager.AddGroup(GroupName,Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Group Already Exists")
                    {
                        objGrpModel.groups = objGroupManager.GetAllGroups();
                        return View("TempGroup", objGrpModel);
                       // return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                      // objGrpModel.groups = objGroupManager.GetAllGroups();
                       return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                   // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("AddGroup Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }           
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditGroup(string GroupName , string GroupID)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();

            GroupsModel objGrpModel = new GroupsModel();
            try
            {

                Response = objGroupManager.EditGroup(GroupName, Convert.ToInt64(GroupID), Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Group Already Exists")
                    {
                        objGrpModel.groups = objGroupManager.GetAllGroups();
                        return View("TempGroup", objGrpModel);
                        //return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //  objGrpModel.groups = objGroupManager.GetAllGroups();
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("EditGroup Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteGroup(string GroupID)
        {
            string response = "";
            GroupsModel objGrpModel = new GroupsModel();
            try
            {
                response = objGroupManager.DeleteGroup(Convert.ToInt64(GroupID));
                if (response == "1")
                {
                    objGrpModel.groups = objGroupManager.GetAllGroups();
                    return View("TempGroup", objGrpModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

                
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteGroup Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult LeadSourceHome()
        {
            LeadSourceModel objModel = new LeadSourceModel();
            objModel.source = objSourceManager.GetAllSource();
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddLeadSource(string SourceName)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            LeadSourceModel objSource = new LeadSourceModel();
           
            try
            {

                Response = objSourceManager.AddSource(SourceName, Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Source Already Exists")
                    {
                        objSource.source = objSourceManager.GetAllSource(); ;
                        return View("LeadSourceHome", objSource);
                       // return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //  objGrpModel.groups = objGroupManager.GetAllGroups();
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("AddLeadSource Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditSource(string SourceName, string SourceID)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();

            LeadSourceModel objSource = new LeadSourceModel();
            try
            {

                Response = objSourceManager.EditSource(SourceName, Convert.ToInt64(SourceID), Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    objSource.source = objSourceManager.GetAllSource(); ;
                    return View("LeadSourceHome", objSource);
                      //  return Json("Success", JsonRequestBehavior.AllowGet);                   
                }
                else
                {
                    // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("EditSource Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteLeadSource(string SourceID)
        {
            string response = "";
            LeadSourceModel objSource = new LeadSourceModel();
            try
            {
                response = objSourceManager.DeleteSource(Convert.ToInt64(SourceID));

                if (response == "1")
                {
                    objSource.source = objSourceManager.GetAllSource(); ;
                    return View("LeadSourceHome", objSource);
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteSource Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult MailSetingHome()
        {
            return View();
        }

        [Authorize]
        public ActionResult LeadStatusHome()
        {
            LeadStatusModel objModel = new LeadStatusModel();
            objModel.status = objLeadStatusManager.GetAllStatus();
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddLeadStatus(string StatusName, string StatusColour, string template, string TextTemplate)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            LeadStatusModel objModel = new LeadStatusModel();

            try
            {

                Response = objLeadStatusManager.AddStatus(StatusName, StatusColour, template, TextTemplate, Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Source Already Exists")
                    {
                        objModel.status = objLeadStatusManager.GetAllStatus();
                        return View("LeadStatusHome", objModel);
                        //return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //  objGrpModel.groups = objGroupManager.GetAllGroups();
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("AddLeadStatus Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditStatus(string StatusName, string StatusColour, string StatusID, string template, string TextTemplate)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            LeadStatusModel objModel = new LeadStatusModel();
            try
            {

                Response = objLeadStatusManager.EditStatus(StatusName, StatusColour, template, TextTemplate, Convert.ToInt64(StatusID), Convert.ToInt64(session.UserSession.UserId));
                if (Response.ErrorCode == 0)
                {
                    objModel.status = objLeadStatusManager.GetAllStatus();
                    return View("LeadStatusHome", objModel);
                  //  return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // objGrpModel.groups = objGroupManager.GetAllGroups();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //objGrpModel.groups = objGroupManager.GetAllGroups();
                BAL.Common.LogManager.LogError("EditStatus Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteLeadStatus(string StatusID)
        {
            string response = "";
            LeadStatusModel objModel = new LeadStatusModel();
            try
            {
                response = objLeadStatusManager.DeleteStatus(Convert.ToInt64(StatusID));

                if (response == "1")
                {
                    objModel.status = objLeadStatusManager.GetAllStatus();
                    return View("LeadStatusHome", objModel);
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteLeadStatus Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetTemplate(string StatusID)
        {
            objResponse Response = new objResponse();
            LeadStatusModel objModel = new LeadStatusModel();
            try
            {
                Response = objLeadStatusManager.GetTemplate(Convert.ToInt64(StatusID));

                if (Response.ErrorCode == 0)
                {
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetTemplate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult TestSeting()
        {
            objResponse Response = new objResponse();
            AdminSetingModel objSetings = new AdminSetingModel();
            session = new SessionHelper();
            try
            {
                Response = objGenralSetingManager.getGenralSeting();
                if (Response.ErrorCode == 0)
                {
                    objSetings.GenralSeting_ID_Auto_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Genral_Seting_ID_Auto_PK"]);
                    objSetings.Company = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ComapanyName"]);
                    objSetings.Address = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Address"]);
                    objSetings.City = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["City"]);
                    objSetings.Stete = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["State"]);
                    objSetings.Country = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Country"]);
                    objSetings.Zipcode = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Zipcode"]);
                    objSetings.Phone = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Phone"]);
                    objSetings.Website = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Website"]);
                    objSetings.Currency = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Currency"]);

                    objSetings.groups = objGroupManager.GetAllGroups();
                    objSetings.source = objSourceManager.GetAllSource();
                    objSetings.status = objLeadStatusManager.GetAllStatus();
                    return View(objSetings);
                }
                else
                {
                    return View(objSetings);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SetingsHome conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetings);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxManageRoles(RolesModel objModel)
        {
            objResponse Response = new objResponse();
            UserRoles objRoles = new UserRoles();
            RolesModel objRolesModel = new RolesModel();
            try
            {
                objRoles = objJavaScriptSerializer.Deserialize<Project.Entity.UserRoles>(objJavaScriptSerializer.Serialize(objModel));
                Response = objRoleManager.AddRole(objRoles);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Role Already Exists")
                    {
                        objRolesModel.roles = objRoleManager.GetAllRoles();
                        return View(objRolesModel);
                    }
                    else
                    {
                        return Json("Role Already Exists", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxManageRoles Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult GetRolesForEdit(string RoleID)
        {
            objResponse Response = new objResponse();
            RolesModel objUserRoles = new RolesModel();
            try
            {
                Response = objRoleManager.GetRolesForEdit(Convert.ToInt64(RoleID));
                if (Response.ErrorCode == 0)
                {
                    objUserRoles.RoleName = Response.ResponseData.Tables[0].Rows[0]["User_Role_Desc"].ToString();
                    objUserRoles.Role_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_Role_ID_Auto_Pk"]);

                    objUserRoles.AssociatedLeads = Response.ResponseData.Tables[0].Rows[0]["AssociatedLeads"].ToString();
                    objUserRoles.SystemwideLeads = Response.ResponseData.Tables[0].Rows[0]["SystemwideLeads"].ToString();
                    objUserRoles.Calendar = Response.ResponseData.Tables[0].Rows[0]["Calendar"].ToString();
                    objUserRoles.EditTask = Response.ResponseData.Tables[0].Rows[0]["EditEvent"].ToString();
                    objUserRoles.LeadNotes = Response.ResponseData.Tables[0].Rows[0]["LeadNotes"].ToString();
                    objUserRoles.Documents = Response.ResponseData.Tables[0].Rows[0]["Documents"].ToString();
                    objUserRoles.ManageDocuments = Response.ResponseData.Tables[0].Rows[0]["ManageDocuments"].ToString();
                    objUserRoles.UserManagement = Response.ResponseData.Tables[0].Rows[0]["UserManagement"].ToString();
                    objUserRoles.SiteManagement = Response.ResponseData.Tables[0].Rows[0]["SiteManagement"].ToString();
                    objUserRoles.HelpDeskTickets = Response.ResponseData.Tables[0].Rows[0]["HelpDeskTickets"].ToString();
                    objUserRoles.LeadDistribution = Response.ResponseData.Tables[0].Rows[0]["LeadDistribution"].ToString();
                    objUserRoles.ResidualsAccess = Response.ResponseData.Tables[0].Rows[0]["ResidualsAccess"].ToString();
                    objUserRoles.ResidualManagerView = Response.ResponseData.Tables[0].Rows[0]["ResidualManagerView"].ToString();
                    objUserRoles.MerchantinfoAssociatedLead = Response.ResponseData.Tables[0].Rows[0]["MerchantInfo_Associated_Leads"].ToString();
                    objUserRoles.MerchantinfoSystemwideLeads = Response.ResponseData.Tables[0].Rows[0]["Merchant_Info_Systemwide_Leads"].ToString();
                    objUserRoles.ManageMerchant = Response.ResponseData.Tables[0].Rows[0]["ManageMerchant"].ToString();
                    objUserRoles.Statements = Response.ResponseData.Tables[0].Rows[0]["Statements"].ToString();
                    objUserRoles.TriggerStatementDownload = Response.ResponseData.Tables[0].Rows[0]["StatementDownload"].ToString();
                    objUserRoles.PortfolioActivityAssociatedleads = Response.ResponseData.Tables[0].Rows[0]["PortfolioActivities_Associated_Leads"].ToString();
                    objUserRoles.PortfolioActivitySystemwideleads = Response.ResponseData.Tables[0].Rows[0]["PortfolioActivites_Systemwide_Leads"].ToString();
                    objUserRoles.ProposalGenerator = Response.ResponseData.Tables[0].Rows[0]["PraposalGenreator"].ToString();
                    objUserRoles.MerchantApplication = Response.ResponseData.Tables[0].Rows[0]["MerchantApplication"].ToString();
                    objUserRoles.UnderwritngApplicationSubmissions = Response.ResponseData.Tables[0].Rows[0]["UnderwritingAppSubmission"].ToString();
                    objUserRoles.Agenttracker = Response.ResponseData.Tables[0].Rows[0]["AgentTracker"].ToString();
                    objUserRoles.MessagingSystem = Response.ResponseData.Tables[0].Rows[0]["MessagingSystem"].ToString();

                    return Json(objUserRoles, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetRolesForEdit Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxUpdateRoles(RolesModel objModel)
        {
            objResponse Response = new objResponse();
            UserRoles objRoles = new UserRoles();
            RolesModel objRolesModel = new RolesModel();
            try
            {
                objRoles = objJavaScriptSerializer.Deserialize<Project.Entity.UserRoles>(objJavaScriptSerializer.Serialize(objModel));
                Response = objRoleManager.UpdateRole(objRoles);

                if (Response.ErrorCode == 0)
                {                    
                        objRolesModel.roles = objRoleManager.GetAllRoles();
                        return View("AjaxManageRoles",objRolesModel);                   
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdateRoles Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
