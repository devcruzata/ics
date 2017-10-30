using BAL.Utility;
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
using System.Web.Script.Serialization;
using Project.Web.Controllers.EmailTemplate;
using BAL.Sms;
using System.Threading.Tasks;

namespace Project.Web.Controllers.Leads
{
    public class LeadsController : Controller
    {
        BAL.Leads.LeadsManager objLeadManager = new BAL.Leads.LeadsManager();
        SessionHelper session;
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
        //
        // GET: /Leads/
        [Authorize]
        public ActionResult LeadHome()
        {
            LeadModel objModel = new LeadModel();
            session = new SessionHelper();
            List<TextValue> status = new List<TextValue>();
            List<TextValue> users = new List<TextValue>();

            status = UtilityManager.GetSattusForDropDown();
            List<SelectListItem> list = new List<SelectListItem>();
            // list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var sta in status)
            {
                list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
            }

            users = UtilityManager.GetUsersForDropDown();
            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose a User" });

            foreach (var user in users)
            {
                list2.Add(new SelectListItem { Value = user.Value, Text = user.Text });
            }

            ViewBag.Status_List = list;
            ViewBag.UserList = list2;
            objModel.leads = objLeadManager.getAllLeads(session.UserSession.UserId.ToString(), session.UserSession.UserType);
            return View(objModel);
        }

        [Authorize]
        public ActionResult ManageLead()
        {
            
            session = new SessionHelper();
            List<TextValue> status = new List<TextValue>();
            List<TextValue> source = new List<TextValue>();
            List<TextValue> group = new List<TextValue>();
            List<TextValue> users = new List<TextValue>();
           
                status = UtilityManager.GetSattusForDropDown();
                List<SelectListItem> list = new List<SelectListItem>();
                 list.Add(new SelectListItem { Value = "0", Text = "Choose a Status" });

                foreach (var sta in status)
                {
                    list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
                }

                source = UtilityManager.GetSourceForDropDown();
                List<SelectListItem> list1 = new List<SelectListItem>();
                 list1.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

                foreach (var sour in source)
                {
                    list1.Add(new SelectListItem { Value = sour.Value, Text = sour.Text });
                }

                users = UtilityManager.GetUsersForDropDown();
                List<SelectListItem> list3 = new List<SelectListItem>();
                 list3.Add(new SelectListItem { Value = "0", Text = "Choose a User" });

                foreach (var user in users)
                {
                    list3.Add(new SelectListItem { Value = user.Value, Text = user.Text });
                }

                      

            group = UtilityManager.GetGroupsForDropDown();
            List<SelectListItem> list2 = new List<SelectListItem>();
             list2.Add(new SelectListItem { Value = "0", Text = "Choose a Group" });

            foreach (var grp in group)
            {
                list2.Add(new SelectListItem { Value = grp.Value, Text = grp.Text });
            }



            ViewBag.Status_List = list;
            ViewBag.Source_List = list1;
            ViewBag.Group_List = list2;
            ViewBag.UserList = list3;
            
            return View();
        }        

        [Authorize]
        [HttpPost]
        public ActionResult AjaxAddLead(LeadModel objModel)
        {
            Project.Entity.Leads objLeads = new Project.Entity.Leads();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            DripEmailHelper objDrip = new DripEmailHelper();
          
            try
            {
                objLeads = objJavaScriptSerializer.Deserialize<Project.Entity.Leads>(objJavaScriptSerializer.Serialize(objModel));
                
                if (objModel.AssignToID == 0)
                {
                    objLeads.AssignToID = Convert.ToInt64(session.UserSession.UserId);
                }
                else
                {
                    objLeads.AssignToID = objModel.AssignToID;
                }
                objLeads.CreatedBy = Convert.ToInt64(session.UserSession.UserId);



                Response = objLeadManager.AddLeadStep1(objLeads);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "")
                    {
                        objLeads.Lead_ID = Convert.ToInt64(Response.ErrorMessage);
                        Response = objLeadManager.AddLeadStep2(objLeads);
                        if (Response.ErrorCode == 0)
                        {
                            if (Response.ErrorMessage != "")
                            {
                                Response = objLeadManager.AddLeadStep3(objLeads);
                                if (Response.ErrorCode == 0)
                                {
                                    if (Response.ErrorMessage != "")
                                    {
                                        Response = objLeadManager.AddLeadStep4(objLeads);
                                        if (Response.ErrorCode == 0)
                                        {
                                            if (Response.ErrorMessage != "")
                                            {
                                                objDrip.shootEmail(objLeads.Lead_ID);
                                              //  objText.SendTextNotiFicationS(objLeads.Lead_ID);
                                                return Json("0", JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json("4", JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json("4", JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json("3", JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json("3", JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json("2", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("2", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("5", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult ViewLead(string LeadID)
        {
            objResponse Response = new objResponse();
            LeadModel objModel = new LeadModel();
            session = new SessionHelper();
            try
            {
                Response = objLeadManager.getLeadsByID(Convert.ToInt64(LeadID));
                if (Response.ErrorCode == 0)
                {
                    objModel.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
                    objModel.ContName = Response.ResponseData.Tables[0].Rows[0]["ContactName"].ToString();
                    objModel.bPhone = Response.ResponseData.Tables[0].Rows[0]["BusinessPhone"].ToString();
                    objModel.Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
                    objModel.website = Response.ResponseData.Tables[0].Rows[0]["Website"].ToString();
                    objModel.CreatedByName = Response.ResponseData.Tables[0].Rows[0]["CreatedByName"].ToString();
                    objModel.Lead_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Lead_ID_Auto_PK"]);

                    objModel.Activity = UtilityManager.getActivityByRelateToID(Convert.ToInt64(LeadID), session.UserSession.UserType);
                    objModel.Task = UtilityManager.getTasksByRelateToID(Convert.ToInt64(LeadID), session.UserSession.UserType, session.UserSession.UserId);
                    objModel.Doc = UtilityManager.getDocsRelatedToID(session.UserSession.UserType, LeadID, "LEAD", session.UserSession.UserId);
                    objModel.Notes = UtilityManager.getNotesByRelateToID(session.UserSession.UserType, Convert.ToInt64(LeadID), session.UserSession.UserId);

                    return View(objModel);
                }
                else
                {
                    return View(objModel);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ViewLead get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }
        }

        [Authorize]
        public ActionResult EditLead(string LeadID)
        {
            objResponse Response = new objResponse();
            LeadModel objLeadModel = new LeadModel();
            try
            {

                Response = objLeadManager.getLeadForEditByID(Convert.ToInt64(LeadID));
                if (Response.ErrorCode == 0)
                {
                    objLeadModel.Lead_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][0]);
                    objLeadModel.DBA = Response.ResponseData.Tables[0].Rows[0][1].ToString();
	                objLeadModel.ContName = Response.ResponseData.Tables[0].Rows[0][2].ToString();
                    objLeadModel.bPhone = Response.ResponseData.Tables[0].Rows[0][3].ToString();
                    objLeadModel.Email = Response.ResponseData.Tables[0].Rows[0][4].ToString();
                    objLeadModel.website = Response.ResponseData.Tables[0].Rows[0][5].ToString();
		            objLeadModel.AltNo = Response.ResponseData.Tables[0].Rows[0][6].ToString();
                    objLeadModel.currProcessing = Response.ResponseData.Tables[0].Rows[0][7].ToString();
                    objLeadModel.EstMontVolume = Response.ResponseData.Tables[0].Rows[0][8].ToString();
                    objLeadModel.MerAppUsername =Response.ResponseData.Tables[0].Rows[0][9].ToString();
                    objLeadModel.MerAppPassword =Response.ResponseData.Tables[0].Rows[0][10].ToString();
                    objLeadModel.LBusiName =Response.ResponseData.Tables[0].Rows[0][11].ToString();
                    objLeadModel.OwnershipType =Response.ResponseData.Tables[0].Rows[0][12].ToString();
                    objLeadModel.YrInBusiness = Response.ResponseData.Tables[0].Rows[0][13].ToString();
                    objLeadModel.MtInBusiness = Response.ResponseData.Tables[0].Rows[0][14].ToString();
                    objLeadModel.BDescription = Response.ResponseData.Tables[0].Rows[0][15].ToString();
                    objLeadModel.LAddress1 = Response.ResponseData.Tables[0].Rows[0][16].ToString();
                    objLeadModel.LAddress2 = Response.ResponseData.Tables[0].Rows[0][17].ToString();
                    objLeadModel.city = Response.ResponseData.Tables[0].Rows[0][18].ToString();
                    objLeadModel.state = Response.ResponseData.Tables[0].Rows[0][19].ToString();
                    objLeadModel.zip = Response.ResponseData.Tables[0].Rows[0][20].ToString();
                    objLeadModel.LPhoneNo = Response.ResponseData.Tables[0].Rows[0][21].ToString();
                    objLeadModel.LFaxNo = Response.ResponseData.Tables[0].Rows[0][22].ToString();
                    objLeadModel.MAddress1 =Response.ResponseData.Tables[0].Rows[0][23].ToString();
                    objLeadModel.MAddress2 =Response.ResponseData.Tables[0].Rows[0][24].ToString();
                    objLeadModel.mcity =Response.ResponseData.Tables[0].Rows[0][25].ToString();
                    objLeadModel.mstate =Response.ResponseData.Tables[0].Rows[0][26].ToString();
                    objLeadModel.mzip = Response.ResponseData.Tables[0].Rows[0][27].ToString();
                   // objLeadModel.CreatedBy = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][28]);

                    objLeadModel.FName = Response.ResponseData.Tables[0].Rows[0][28].ToString();
                    objLeadModel.LName = Response.ResponseData.Tables[0].Rows[0][29].ToString();
                    objLeadModel.DOB = Response.ResponseData.Tables[0].Rows[0][30].ToString();	
                    objLeadModel.SocSecurity = Response.ResponseData.Tables[0].Rows[0][31].ToString();
		            objLeadModel.DLicense = Response.ResponseData.Tables[0].Rows[0][32].ToString();
		            objLeadModel.DLicenseState = Response.ResponseData.Tables[0].Rows[0][33].ToString();
                    objLeadModel.RAddress1 = Response.ResponseData.Tables[0].Rows[0][34].ToString();
                    objLeadModel.RAddress2 = Response.ResponseData.Tables[0].Rows[0][35].ToString();
                    objLeadModel.rcity = Response.ResponseData.Tables[0].Rows[0][36].ToString();
                    objLeadModel.rstate = Response.ResponseData.Tables[0].Rows[0][37].ToString();
                    objLeadModel.rzip = Response.ResponseData.Tables[0].Rows[0][38].ToString();
		            objLeadModel.OwnerPhone  =Response.ResponseData.Tables[0].Rows[0][39].ToString();

                    objLeadModel.MerchantType =Response.ResponseData.Tables[0].Rows[0][40].ToString();
                    objLeadModel.RSwiped =Response.ResponseData.Tables[0].Rows[0][41].ToString();
                    objLeadModel.RKeyed =Response.ResponseData.Tables[0].Rows[0][42].ToString();
                    objLeadModel.MTOrders =Response.ResponseData.Tables[0].Rows[0][43].ToString();
                    objLeadModel.internet =Response.ResponseData.Tables[0].Rows[0][44].ToString();
                    objLeadModel.avgTicket =Response.ResponseData.Tables[0].Rows[0][45].ToString();
                    objLeadModel.highTicket =Response.ResponseData.Tables[0].Rows[0][46].ToString();
                    objLeadModel.MVolume =Response.ResponseData.Tables[0].Rows[0][47].ToString();
                    objLeadModel.BName =Response.ResponseData.Tables[0].Rows[0][48].ToString();
                    objLeadModel.BCity =Response.ResponseData.Tables[0].Rows[0][49].ToString();
                    objLeadModel.BState =Response.ResponseData.Tables[0].Rows[0][50].ToString();
                    objLeadModel.BZip =Response.ResponseData.Tables[0].Rows[0][51].ToString();
                    objLeadModel.BRoutNumber =Response.ResponseData.Tables[0].Rows[0][52].ToString();
                    objLeadModel.BacNumber =Response.ResponseData.Tables[0].Rows[0][53].ToString();

                        objLeadModel.Equipment = Response.ResponseData.Tables[0].Rows[0][54].ToString();
	                    objLeadModel.DebitQual = Response.ResponseData.Tables[0].Rows[0][55].ToString();
                        objLeadModel.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0][56].ToString();
                        objLeadModel.DebitNonQual = Response.ResponseData.Tables[0].Rows[0][57].ToString();
                        objLeadModel.CreditQual = Response.ResponseData.Tables[0].Rows[0][58].ToString();
                        objLeadModel.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0][59].ToString();
                        objLeadModel.CreditNonQual = Response.ResponseData.Tables[0].Rows[0][60].ToString();
                        objLeadModel.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0][61].ToString();
                        objLeadModel.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][62].ToString();
                        objLeadModel.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][63].ToString();
                        objLeadModel.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0][64].ToString();
                        objLeadModel.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][65].ToString();
                        objLeadModel.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][66].ToString();
                        objLeadModel.DebitTransFee = Response.ResponseData.Tables[0].Rows[0][67].ToString();
                        objLeadModel.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0][68].ToString();
                        objLeadModel.EBTTransFee = Response.ResponseData.Tables[0].Rows[0][69].ToString();
                        objLeadModel.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0][70].ToString();
                        objLeadModel.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0][71].ToString();
                        objLeadModel.StatementFee = Response.ResponseData.Tables[0].Rows[0][72].ToString();
                        objLeadModel.MontMini = Response.ResponseData.Tables[0].Rows[0][73].ToString();
                        objLeadModel.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0][74].ToString();
                        objLeadModel.BatchFee = Response.ResponseData.Tables[0].Rows[0][75].ToString();
                        objLeadModel.GatewayFee = Response.ResponseData.Tables[0].Rows[0][76].ToString();
                        objLeadModel.WirelessFee = Response.ResponseData.Tables[0].Rows[0][77].ToString();
                        objLeadModel.RetrievalFee = Response.ResponseData.Tables[0].Rows[0][78].ToString();


                        return View(objLeadModel);
                    
                }
                else
                {
                    return View(objLeadModel);
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("EditLead Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objLeadModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxActivity(string LeadID)
        {
            LeadModel objLeadModel = new LeadModel();
            session = new SessionHelper();
            try
            {
                objLeadModel.Activity = UtilityManager.getActivityByRelateToID(Convert.ToInt64(LeadID), session.UserSession.UserType);
                return View(objLeadModel);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxTasks(string LeadID)
        {
            LeadModel objLeads = new LeadModel();
            session = new SessionHelper();
            try
            {
                objLeads.Task = UtilityManager.getTasksByRelateToID(Convert.ToInt64(LeadID), session.UserSession.UserType, session.UserSession.UserId);
                return View(objLeads);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxNotes(string LeadID)
        {
            LeadModel objLeadModel = new LeadModel();
            session = new SessionHelper();
            try
            {
                objLeadModel.Notes = UtilityManager.getNotesByRelateToID(session.UserSession.UserType, Convert.ToInt64(LeadID), session.UserSession.UserId);
                return View(objLeadModel);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxLeadEvents(string LeadID)
        {
            LeadModel objLeadModel = new LeadModel();
            session = new SessionHelper();
            try
            {
                objLeadModel.Notes = UtilityManager.getNotesByRelateToID(session.UserSession.UserType, Convert.ToInt64(LeadID), session.UserSession.UserId);
                return View(objLeadModel);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxDocs(string LeadID)
        {
            LeadModel objLeadsModel = new LeadModel();
            session = new SessionHelper();
            try
            {
                objLeadsModel.Doc = UtilityManager.getDocsRelatedToID(session.UserSession.UserType, LeadID, "LEAD", session.UserSession.UserId);
                return View(objLeadsModel);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> AjaxChangeStatus(string LeadID , string Disp_ID , string Notes)
        {
            LeadModel objLeadsModel = new LeadModel();
            session = new SessionHelper();
            objResponse Response = new objResponse();
            DripEmailHelper objDrip = new DripEmailHelper();
            BAL.Sms.SmsManager objsmsManager = new BAL.Sms.SmsManager();          
            try
            {
                Response = objLeadManager.changeStatus(LeadID, Disp_ID , Notes,session.UserSession.UserId.ToString());

                if (Response.ErrorCode == 0)
                {                   

                    objLeadsModel.leads = objLeadManager.getAllLeads(session.UserSession.UserId.ToString(), session.UserSession.UserType);
                    objDrip.shootEmail(Convert.ToInt64(LeadID));
                    Response = UtilityManager.getSmsTemplate(Convert.ToInt64(LeadID),Disp_ID);
                    string Body = objsmsManager.PopulateBody(Response.ResponseData.Tables[0].Rows[0]["smsTemplate"].ToString(), Response.ResponseData.Tables[1].Rows[0]["ContactName"].ToString());
                    string toNumber = Response.ResponseData.Tables[1].Rows[0]["BusinessPhone"].ToString();
                    var result = await objsmsManager.SendSms(toNumber, Body);                   
                    List<SmsResponse> objSmsResponse = new List<SmsResponse>();
                    objSmsResponse = objsmsManager.XMLResponseParser(result.ToString());
                    if (objSmsResponse[0].result == "0000")
                    {
                        return View(objLeadsModel);
                    }
                    else
                    {
                        BAL.Common.LogManager.LogError("AjaxChangeStatus", 1, Convert.ToString(objSmsResponse[0].errortext), Convert.ToString(objSmsResponse[0].errortext), Convert.ToString(objSmsResponse[0].errortext));
                        return View(objLeadsModel);
                    }
                    
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {               
                BAL.Common.LogManager.LogError("AjaxChangeStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AjaxChangeAssignToId(string LeadID, string AssignTo_ID)
        {
            LeadModel objLeadsModel = new LeadModel();
            session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                Response = objLeadManager.changeAssignToId(LeadID, AssignTo_ID);

                if (Response.ErrorCode == 0)
                {

                    objLeadsModel.leads = objLeadManager.getAllLeads(session.UserSession.UserId.ToString(), session.UserSession.UserType);
                    return View(objLeadsModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxEditLead(LeadModel objModel)
        {
            Project.Entity.Leads objLeads = new Project.Entity.Leads();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                objLeads = objJavaScriptSerializer.Deserialize<Project.Entity.Leads>(objJavaScriptSerializer.Serialize(objModel));

                objLeads.CreatedBy = Convert.ToInt64(session.UserSession.UserId);

                Response = objLeadManager.editLeadStep1(objLeads);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "")
                    {
                        objLeads.Lead_ID = Convert.ToInt64(Response.ErrorMessage);
                        Response = objLeadManager.AddLeadStep2(objLeads);
                        if (Response.ErrorCode == 0)
                        {
                            if (Response.ErrorMessage != "")
                            {
                                Response = objLeadManager.AddLeadStep3(objLeads);
                                if (Response.ErrorCode == 0)
                                {
                                    if (Response.ErrorMessage != "")
                                    {
                                        Response = objLeadManager.AddLeadStep4(objLeads);
                                        if (Response.ErrorCode == 0)
                                        {
                                            if (Response.ErrorMessage != "")
                                            {
                                                return Json("0", JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                return Json("4", JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            return Json("4", JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        return Json("3", JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json("3", JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json("2", JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json("2", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxEditLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("5", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteLead(string Lead_ID_PK)
        {
            string response = "";
            try
            {
                response = objLeadManager.DeleteLead(Convert.ToInt64(Lead_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UploadLeadExcel()
        {
            return View();
        }

        [Authorize]
        public ActionResult DownLoadSample(string file_path)
        {
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Import_Lead_Sample_Dir"]) + file_path;
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
        public ActionResult UploadLeadExcelFile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ImportExcelFileToLeadDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<Project.Entity.Leads> users = new List<Project.Entity.Leads>();

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
                            Project.Entity.Leads objLeadModel = new Project.Entity.Leads();

                            objLeadModel.DBA = dr[1].ToString();
                            objLeadModel.ContName = dr[2].ToString();
                            objLeadModel.bPhone = dr[3].ToString();
                            objLeadModel.Email = dr[4].ToString();
                            objLeadModel.website = dr[5].ToString();
                            objLeadModel.AltNo = dr[6].ToString();
                            if (dr[7].ToString() == "true")
                            {
                                objLeadModel.currProcessing = "true";
                            }
                            else
                            {
                                objLeadModel.currProcessing = "false";
                            }                             
                            objLeadModel.EstMontVolume = dr[7].ToString();
                            objLeadModel.MerAppUsername = dr[8].ToString();
                            objLeadModel.MerAppPassword = dr[9].ToString();
                            objLeadModel.LBusiName = dr[10].ToString();
                            objLeadModel.BDescription = dr[11].ToString();
                            objLeadModel.LAddress1 = dr[12].ToString();
                            objLeadModel.LAddress2 = dr[13].ToString();
                            objLeadModel.city = dr[14].ToString();
                            objLeadModel.state = dr[15].ToString();
                            objLeadModel.zip = dr[16].ToString();
                            objLeadModel.LPhoneNo = dr[17].ToString();
                            objLeadModel.LFaxNo = dr[18].ToString();
                            objLeadModel.MAddress1 = dr[19].ToString();
                            objLeadModel.MAddress2 = dr[20].ToString();
                            objLeadModel.mcity = dr[21].ToString();
                            objLeadModel.mstate = dr[22].ToString();
                            objLeadModel.mzip = dr[23].ToString();
                            //objLeadModel.OwnershipType = dr[24].ToString();
                            //objLeadModel.YrInBusiness = dr[25].ToString();
                            
                           


                            objLeadModel.FName = dr[24].ToString();
                            objLeadModel.LName = dr[25].ToString();
                            objLeadModel.DOB = dr[26].ToString();
                            objLeadModel.SocSecurity = dr[27].ToString();
                            objLeadModel.DLicense = dr[28].ToString();
                            objLeadModel.DLicenseState = dr[29].ToString();
                            objLeadModel.RAddress1 = dr[30].ToString();
                            objLeadModel.RAddress2 = dr[31].ToString();
                            objLeadModel.rcity = dr[32].ToString();
                            objLeadModel.rstate = dr[33].ToString();
                            objLeadModel.rzip = dr[34].ToString();
                            objLeadModel.OwnerPhone = dr[35].ToString();

                            objLeadModel.MerchantType = dr[36].ToString();
                            objLeadModel.RSwiped = dr[37].ToString();
                            objLeadModel.RKeyed = dr[38].ToString();                            
                            objLeadModel.internet = dr[39].ToString();
                            objLeadModel.MTOrders = dr[40].ToString();
                            objLeadModel.avgTicket = dr[41].ToString();
                            objLeadModel.highTicket = dr[42].ToString();
                            objLeadModel.MVolume = dr[43].ToString();
                            objLeadModel.BName = dr[44].ToString();
                            objLeadModel.BCity = dr[45].ToString();
                            objLeadModel.BState = dr[46].ToString();
                            objLeadModel.BZip = dr[47].ToString();
                            objLeadModel.BRoutNumber = dr[48].ToString();
                            objLeadModel.BacNumber = dr[49].ToString();

                            objLeadModel.Equipment = dr[50].ToString();
                            objLeadModel.DebitQual = dr[51].ToString();
                            objLeadModel.DebitMIDQual = dr[52].ToString();
                            objLeadModel.DebitNonQual = dr[53].ToString();
                            objLeadModel.CreditQual = dr[54].ToString();
                            objLeadModel.CreditMIDQual = dr[55].ToString();
                            objLeadModel.CreditNonQual = dr[56].ToString();
                            objLeadModel.DebitQualPerItem = dr[57].ToString();
                            objLeadModel.DebitMIDQualPerItem = dr[58].ToString();
                            objLeadModel.DebitNonQualPerItem = dr[59].ToString();
                            objLeadModel.CreditQualPerItem = dr[60].ToString();
                            objLeadModel.CreditMIDQualPerItem = dr[61].ToString();
                            objLeadModel.CreditNonQualPerItem = dr[62].ToString();
                            objLeadModel.DebitTransFee = dr[63].ToString();
                            objLeadModel.ReturnTransFee = dr[64].ToString();
                            objLeadModel.EBTTransFee = dr[65].ToString();
                            objLeadModel.ElectroAVSFee = dr[66].ToString();
                            objLeadModel.AMEXTransFee = dr[67].ToString();
                            objLeadModel.StatementFee = dr[68].ToString();
                            objLeadModel.MontMini = dr[69].ToString();
                            objLeadModel.ChargeBackFee = dr[70].ToString();
                            objLeadModel.BatchFee = dr[71].ToString();
                            objLeadModel.GatewayFee = dr[72].ToString();
                            objLeadModel.WirelessFee = dr[73].ToString();
                            objLeadModel.RetrievalFee = dr[74].ToString();
                           // objLeadModel.AssignToID = Convert.ToInt64(dr[75]);
                          //  objLeadModel.LeadSource = Convert.ToString(dr[80]);
                          //  objLeadModel.LeadStatus = Convert.ToString(dr[81]);
                         //   objLeadModel.LeadGroup = Convert.ToString(dr[82]);
                            objLeadModel.CreatedBy = Convert.ToInt64(session.UserSession.UserId);
                            

                            bool Result = importLeads(objLeadModel);
                            if (Result == true)
                            {
                               // if (Response.ErrorMessage != "User Already Exists")
                               // {
                                    count++;
                              //  }
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
                BAL.Common.LogManager.LogError("ImportExcelFileToLeadDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        public bool importLeads(Project.Entity.Leads objLeads)
        {
            
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {

                if (objLeads.AssignToID == 0)
                {
                    objLeads.AssignToID = Convert.ToInt64(session.UserSession.UserId);
                }
                
                objLeads.CreatedBy = Convert.ToInt64(session.UserSession.UserId);

                Response = objLeadManager.AddLeadStep1(objLeads);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "")
                    {
                        objLeads.Lead_ID = Convert.ToInt64(Response.ErrorMessage);
                        Response = objLeadManager.AddLeadStep2(objLeads);
                        if (Response.ErrorCode == 0)
                        {
                            if (Response.ErrorMessage != "")
                            {
                                Response = objLeadManager.AddLeadStep3(objLeads);
                                if (Response.ErrorCode == 0)
                                {
                                    if (Response.ErrorMessage != "")
                                    {
                                        Response = objLeadManager.AddLeadStep4(objLeads);
                                        if (Response.ErrorCode == 0)
                                        {
                                            if (Response.ErrorMessage != "")
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("importLeads  Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return false;
            }
        }

        [Authorize]
        public ActionResult testView(string LeadID)
        {
            objResponse Response = new objResponse();
            LeadModel objLeadModel = new LeadModel();
            session = new SessionHelper();

            List<TextValue> status = new List<TextValue>();
            List<TextValue> source = new List<TextValue>();
            List<TextValue> group = new List<TextValue>();
            List<TextValue> users = new List<TextValue>();

            status = UtilityManager.GetSattusForDropDown();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Status" });

            foreach (var sta in status)
            {
                list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
            }

            source = UtilityManager.GetSourceForDropDown();
            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var sour in source)
            {
                list1.Add(new SelectListItem { Value = sour.Value, Text = sour.Text });
            }

            users = UtilityManager.GetUsersForDropDown();
            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a User" });

            foreach (var user in users)
            {
                list3.Add(new SelectListItem { Value = user.Value, Text = user.Text });
            }



            group = UtilityManager.GetGroupsForDropDown();
            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose a Group" });

            foreach (var grp in group)
            {
                list2.Add(new SelectListItem { Value = grp.Value, Text = grp.Text });
            }

            try
            {

                Response = objLeadManager.getLeadForEditByID(Convert.ToInt64(LeadID));
                if (Response.ErrorCode == 0)
                {
                    objLeadModel.Lead_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][0]);
                    objLeadModel.DBA = Response.ResponseData.Tables[0].Rows[0][1].ToString();
                    objLeadModel.ContName = Response.ResponseData.Tables[0].Rows[0][2].ToString();
                    objLeadModel.bPhone = Response.ResponseData.Tables[0].Rows[0][3].ToString();
                    objLeadModel.Email = Response.ResponseData.Tables[0].Rows[0][4].ToString();
                    objLeadModel.website = Response.ResponseData.Tables[0].Rows[0][5].ToString();
                    objLeadModel.AltNo = Response.ResponseData.Tables[0].Rows[0][6].ToString();
                    objLeadModel.currProcessing = Response.ResponseData.Tables[0].Rows[0][7].ToString();
                    objLeadModel.EstMontVolume = Response.ResponseData.Tables[0].Rows[0][8].ToString();
                    objLeadModel.MerAppUsername = Response.ResponseData.Tables[0].Rows[0][9].ToString();
                    objLeadModel.MerAppPassword = Response.ResponseData.Tables[0].Rows[0][10].ToString();
                    objLeadModel.LBusiName = Response.ResponseData.Tables[0].Rows[0][11].ToString();
                    objLeadModel.OwnershipType = Response.ResponseData.Tables[0].Rows[0][12].ToString();
                    objLeadModel.YrInBusiness = Response.ResponseData.Tables[0].Rows[0][13].ToString();
                    objLeadModel.MtInBusiness = Response.ResponseData.Tables[0].Rows[0][14].ToString();
                    objLeadModel.BDescription = Response.ResponseData.Tables[0].Rows[0][15].ToString();
                    objLeadModel.LAddress1 = Response.ResponseData.Tables[0].Rows[0][16].ToString();
                    objLeadModel.LAddress2 = Response.ResponseData.Tables[0].Rows[0][17].ToString();
                    objLeadModel.city = Response.ResponseData.Tables[0].Rows[0][18].ToString();
                    objLeadModel.state = Response.ResponseData.Tables[0].Rows[0][19].ToString();
                    objLeadModel.zip = Response.ResponseData.Tables[0].Rows[0][20].ToString();
                    objLeadModel.LPhoneNo = Response.ResponseData.Tables[0].Rows[0][21].ToString();
                    objLeadModel.LFaxNo = Response.ResponseData.Tables[0].Rows[0][22].ToString();
                    objLeadModel.MAddress1 = Response.ResponseData.Tables[0].Rows[0][23].ToString();
                    objLeadModel.MAddress2 = Response.ResponseData.Tables[0].Rows[0][24].ToString();
                    objLeadModel.mcity = Response.ResponseData.Tables[0].Rows[0][25].ToString();
                    objLeadModel.mstate = Response.ResponseData.Tables[0].Rows[0][26].ToString();
                    objLeadModel.mzip = Response.ResponseData.Tables[0].Rows[0][27].ToString();
                    // objLeadModel.CreatedBy = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][28]);

                    objLeadModel.FName = Response.ResponseData.Tables[0].Rows[0][28].ToString();
                    objLeadModel.LName = Response.ResponseData.Tables[0].Rows[0][29].ToString();
                    objLeadModel.DOB = Response.ResponseData.Tables[0].Rows[0][30].ToString();
                    objLeadModel.SocSecurity = Response.ResponseData.Tables[0].Rows[0][31].ToString();
                    objLeadModel.DLicense = Response.ResponseData.Tables[0].Rows[0][32].ToString();
                    objLeadModel.DLicenseState = Response.ResponseData.Tables[0].Rows[0][33].ToString();
                    objLeadModel.RAddress1 = Response.ResponseData.Tables[0].Rows[0][34].ToString();
                    objLeadModel.RAddress2 = Response.ResponseData.Tables[0].Rows[0][35].ToString();
                    objLeadModel.rcity = Response.ResponseData.Tables[0].Rows[0][36].ToString();
                    objLeadModel.rstate = Response.ResponseData.Tables[0].Rows[0][37].ToString();
                    objLeadModel.rzip = Response.ResponseData.Tables[0].Rows[0][38].ToString();
                    objLeadModel.OwnerPhone = Response.ResponseData.Tables[0].Rows[0][39].ToString();

                    objLeadModel.MerchantType = Response.ResponseData.Tables[0].Rows[0][40].ToString();
                    objLeadModel.RSwiped = Response.ResponseData.Tables[0].Rows[0][41].ToString();
                    objLeadModel.RKeyed = Response.ResponseData.Tables[0].Rows[0][42].ToString();
                    objLeadModel.MTOrders = Response.ResponseData.Tables[0].Rows[0][43].ToString();
                    objLeadModel.internet = Response.ResponseData.Tables[0].Rows[0][44].ToString();
                    objLeadModel.avgTicket = Response.ResponseData.Tables[0].Rows[0][45].ToString();
                    objLeadModel.highTicket = Response.ResponseData.Tables[0].Rows[0][46].ToString();
                    objLeadModel.MVolume = Response.ResponseData.Tables[0].Rows[0][47].ToString();
                    objLeadModel.BName = Response.ResponseData.Tables[0].Rows[0][48].ToString();
                    objLeadModel.BCity = Response.ResponseData.Tables[0].Rows[0][49].ToString();
                    objLeadModel.BState = Response.ResponseData.Tables[0].Rows[0][50].ToString();
                    objLeadModel.BZip = Response.ResponseData.Tables[0].Rows[0][51].ToString();
                    objLeadModel.BRoutNumber = Response.ResponseData.Tables[0].Rows[0][52].ToString();
                    objLeadModel.BacNumber = Response.ResponseData.Tables[0].Rows[0][53].ToString();

                    objLeadModel.Equipment = Response.ResponseData.Tables[0].Rows[0][54].ToString();
                    objLeadModel.DebitQual = Response.ResponseData.Tables[0].Rows[0][55].ToString();
                    objLeadModel.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0][56].ToString();
                    objLeadModel.DebitNonQual = Response.ResponseData.Tables[0].Rows[0][57].ToString();
                    objLeadModel.CreditQual = Response.ResponseData.Tables[0].Rows[0][58].ToString();
                    objLeadModel.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0][59].ToString();
                    objLeadModel.CreditNonQual = Response.ResponseData.Tables[0].Rows[0][60].ToString();
                    objLeadModel.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0][61].ToString();
                    objLeadModel.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][62].ToString();
                    objLeadModel.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][63].ToString();
                    objLeadModel.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0][64].ToString();
                    objLeadModel.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][65].ToString();
                    objLeadModel.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][66].ToString();
                    objLeadModel.DebitTransFee = Response.ResponseData.Tables[0].Rows[0][67].ToString();
                    objLeadModel.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0][68].ToString();
                    objLeadModel.EBTTransFee = Response.ResponseData.Tables[0].Rows[0][69].ToString();
                    objLeadModel.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0][70].ToString();
                    objLeadModel.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0][71].ToString();
                    objLeadModel.StatementFee = Response.ResponseData.Tables[0].Rows[0][72].ToString();
                    objLeadModel.MontMini = Response.ResponseData.Tables[0].Rows[0][73].ToString();
                    objLeadModel.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0][74].ToString();
                    objLeadModel.BatchFee = Response.ResponseData.Tables[0].Rows[0][75].ToString();
                    objLeadModel.GatewayFee = Response.ResponseData.Tables[0].Rows[0][76].ToString();
                    objLeadModel.WirelessFee = Response.ResponseData.Tables[0].Rows[0][77].ToString();
                    objLeadModel.RetrievalFee = Response.ResponseData.Tables[0].Rows[0][78].ToString();

                    objLeadModel.LeadStatus = Response.ResponseData.Tables[0].Rows[0][83].ToString();
                    objLeadModel.LeadGroup = Response.ResponseData.Tables[0].Rows[0][84].ToString();
                    objLeadModel.LeadSource = Response.ResponseData.Tables[0].Rows[0][85].ToString();
                    objLeadModel.AssignToID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][86]);

                    //objLeadModel.Eve = UtilityManager.getTasksByRelateToID(Convert.ToInt64(LeadID), session.UserSession.UserType, session.UserSession.UserId);                   
                    objLeadModel.Notes = UtilityManager.getNotesByRelateToID(session.UserSession.UserType, Convert.ToInt64(LeadID), session.UserSession.UserId);

                    ViewBag.Status_List = list;
                    ViewBag.Source_List = list1;
                    ViewBag.Group_List = list2;
                    ViewBag.UserList = list3;
                    return View(objLeadModel);

                }
                else
                {
                    ViewBag.Status_List = list;
                    ViewBag.Source_List = list1;
                    ViewBag.Group_List = list2;
                    ViewBag.UserList = list3;
                    return View(objLeadModel);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Status_List = list;
                ViewBag.Source_List = list1;
                ViewBag.Group_List = list2;
                ViewBag.UserList = list3;
                BAL.Common.LogManager.LogError("EditLead Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objLeadModel);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult AjaxLeadData(string LeadID)
        {
            LeadModel objLeadModel = new LeadModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();

            List<TextValue> status = new List<TextValue>();
            List<TextValue> source = new List<TextValue>();
            List<TextValue> group = new List<TextValue>();
            List<TextValue> users = new List<TextValue>();

            status = UtilityManager.GetSattusForDropDown();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Status" });

            foreach (var sta in status)
            {
                list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
            }

            source = UtilityManager.GetSourceForDropDown();
            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var sour in source)
            {
                list1.Add(new SelectListItem { Value = sour.Value, Text = sour.Text });
            }

            users = UtilityManager.GetUsersForDropDown();
            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a User" });

            foreach (var user in users)
            {
                list3.Add(new SelectListItem { Value = user.Value, Text = user.Text });
            }



            group = UtilityManager.GetGroupsForDropDown();
            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose a Group" });

            foreach (var grp in group)
            {
                list2.Add(new SelectListItem { Value = grp.Value, Text = grp.Text });
            }

            try
            {
                Response = objLeadManager.getLeadForEditByID(Convert.ToInt64(LeadID));
                if (Response.ErrorCode == 0)
                {
                    objLeadModel.Lead_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][0]);
                    objLeadModel.DBA = Response.ResponseData.Tables[0].Rows[0][1].ToString();
                    objLeadModel.ContName = Response.ResponseData.Tables[0].Rows[0][2].ToString();
                    objLeadModel.bPhone = Response.ResponseData.Tables[0].Rows[0][3].ToString();
                    objLeadModel.Email = Response.ResponseData.Tables[0].Rows[0][4].ToString();
                    objLeadModel.website = Response.ResponseData.Tables[0].Rows[0][5].ToString();
                    objLeadModel.AltNo = Response.ResponseData.Tables[0].Rows[0][6].ToString();
                    objLeadModel.currProcessing = Response.ResponseData.Tables[0].Rows[0][7].ToString();
                    objLeadModel.EstMontVolume = Response.ResponseData.Tables[0].Rows[0][8].ToString();
                    objLeadModel.MerAppUsername = Response.ResponseData.Tables[0].Rows[0][9].ToString();
                    objLeadModel.MerAppPassword = Response.ResponseData.Tables[0].Rows[0][10].ToString();
                    objLeadModel.LBusiName = Response.ResponseData.Tables[0].Rows[0][11].ToString();
                    objLeadModel.OwnershipType = Response.ResponseData.Tables[0].Rows[0][12].ToString();
                    objLeadModel.YrInBusiness = Response.ResponseData.Tables[0].Rows[0][13].ToString();
                    objLeadModel.MtInBusiness = Response.ResponseData.Tables[0].Rows[0][14].ToString();
                    objLeadModel.BDescription = Response.ResponseData.Tables[0].Rows[0][15].ToString();
                    objLeadModel.LAddress1 = Response.ResponseData.Tables[0].Rows[0][16].ToString();
                    objLeadModel.LAddress2 = Response.ResponseData.Tables[0].Rows[0][17].ToString();
                    objLeadModel.city = Response.ResponseData.Tables[0].Rows[0][18].ToString();
                    objLeadModel.state = Response.ResponseData.Tables[0].Rows[0][19].ToString();
                    objLeadModel.zip = Response.ResponseData.Tables[0].Rows[0][20].ToString();
                    objLeadModel.LPhoneNo = Response.ResponseData.Tables[0].Rows[0][21].ToString();
                    objLeadModel.LFaxNo = Response.ResponseData.Tables[0].Rows[0][22].ToString();
                    objLeadModel.MAddress1 = Response.ResponseData.Tables[0].Rows[0][23].ToString();
                    objLeadModel.MAddress2 = Response.ResponseData.Tables[0].Rows[0][24].ToString();
                    objLeadModel.mcity = Response.ResponseData.Tables[0].Rows[0][25].ToString();
                    objLeadModel.mstate = Response.ResponseData.Tables[0].Rows[0][26].ToString();
                    objLeadModel.mzip = Response.ResponseData.Tables[0].Rows[0][27].ToString();
                    // objLeadModel.CreatedBy = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][28]);

                    objLeadModel.FName = Response.ResponseData.Tables[0].Rows[0][28].ToString();
                    objLeadModel.LName = Response.ResponseData.Tables[0].Rows[0][29].ToString();
                    objLeadModel.DOB = Response.ResponseData.Tables[0].Rows[0][30].ToString();
                    objLeadModel.SocSecurity = Response.ResponseData.Tables[0].Rows[0][31].ToString();
                    objLeadModel.DLicense = Response.ResponseData.Tables[0].Rows[0][32].ToString();
                    objLeadModel.DLicenseState = Response.ResponseData.Tables[0].Rows[0][33].ToString();
                    objLeadModel.RAddress1 = Response.ResponseData.Tables[0].Rows[0][34].ToString();
                    objLeadModel.RAddress2 = Response.ResponseData.Tables[0].Rows[0][35].ToString();
                    objLeadModel.rcity = Response.ResponseData.Tables[0].Rows[0][36].ToString();
                    objLeadModel.rstate = Response.ResponseData.Tables[0].Rows[0][37].ToString();
                    objLeadModel.rzip = Response.ResponseData.Tables[0].Rows[0][38].ToString();
                    objLeadModel.OwnerPhone = Response.ResponseData.Tables[0].Rows[0][39].ToString();

                    objLeadModel.MerchantType = Response.ResponseData.Tables[0].Rows[0][40].ToString();
                    objLeadModel.RSwiped = Response.ResponseData.Tables[0].Rows[0][41].ToString();
                    objLeadModel.RKeyed = Response.ResponseData.Tables[0].Rows[0][42].ToString();
                    objLeadModel.MTOrders = Response.ResponseData.Tables[0].Rows[0][43].ToString();
                    objLeadModel.internet = Response.ResponseData.Tables[0].Rows[0][44].ToString();
                    objLeadModel.avgTicket = Response.ResponseData.Tables[0].Rows[0][45].ToString();
                    objLeadModel.highTicket = Response.ResponseData.Tables[0].Rows[0][46].ToString();
                    objLeadModel.MVolume = Response.ResponseData.Tables[0].Rows[0][47].ToString();
                    objLeadModel.BName = Response.ResponseData.Tables[0].Rows[0][48].ToString();
                    objLeadModel.BCity = Response.ResponseData.Tables[0].Rows[0][49].ToString();
                    objLeadModel.BState = Response.ResponseData.Tables[0].Rows[0][50].ToString();
                    objLeadModel.BZip = Response.ResponseData.Tables[0].Rows[0][51].ToString();
                    objLeadModel.BRoutNumber = Response.ResponseData.Tables[0].Rows[0][52].ToString();
                    objLeadModel.BacNumber = Response.ResponseData.Tables[0].Rows[0][53].ToString();

                    objLeadModel.Equipment = Response.ResponseData.Tables[0].Rows[0][54].ToString();
                    objLeadModel.DebitQual = Response.ResponseData.Tables[0].Rows[0][55].ToString();
                    objLeadModel.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0][56].ToString();
                    objLeadModel.DebitNonQual = Response.ResponseData.Tables[0].Rows[0][57].ToString();
                    objLeadModel.CreditQual = Response.ResponseData.Tables[0].Rows[0][58].ToString();
                    objLeadModel.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0][59].ToString();
                    objLeadModel.CreditNonQual = Response.ResponseData.Tables[0].Rows[0][60].ToString();
                    objLeadModel.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0][61].ToString();
                    objLeadModel.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][62].ToString();
                    objLeadModel.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][63].ToString();
                    objLeadModel.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0][64].ToString();
                    objLeadModel.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0][65].ToString();
                    objLeadModel.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0][66].ToString();
                    objLeadModel.DebitTransFee = Response.ResponseData.Tables[0].Rows[0][67].ToString();
                    objLeadModel.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0][68].ToString();
                    objLeadModel.EBTTransFee = Response.ResponseData.Tables[0].Rows[0][69].ToString();
                    objLeadModel.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0][70].ToString();
                    objLeadModel.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0][71].ToString();
                    objLeadModel.StatementFee = Response.ResponseData.Tables[0].Rows[0][72].ToString();
                    objLeadModel.MontMini = Response.ResponseData.Tables[0].Rows[0][73].ToString();
                    objLeadModel.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0][74].ToString();
                    objLeadModel.BatchFee = Response.ResponseData.Tables[0].Rows[0][75].ToString();
                    objLeadModel.GatewayFee = Response.ResponseData.Tables[0].Rows[0][76].ToString();
                    objLeadModel.WirelessFee = Response.ResponseData.Tables[0].Rows[0][77].ToString();
                    objLeadModel.RetrievalFee = Response.ResponseData.Tables[0].Rows[0][78].ToString();

                    objLeadModel.LeadStatus = Response.ResponseData.Tables[0].Rows[0][83].ToString();
                    objLeadModel.LeadGroup = Response.ResponseData.Tables[0].Rows[0][84].ToString();
                    objLeadModel.LeadSource = Response.ResponseData.Tables[0].Rows[0][85].ToString();
                    objLeadModel.AssignToID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][86]);

                   

                    ViewBag.Status_List = list;
                    ViewBag.Source_List = list1;
                    ViewBag.Group_List = list2;
                    ViewBag.UserList = list3;
                    return View(objLeadModel);

                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }               
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]       
        [HttpPost]
        public ActionResult AjaxAssignLead(string Lead_ID, string User_ID)
        {

            objResponse Response = new objResponse();
            session = new Common.SessionHelper();
            LeadModel model = new LeadModel();
            try
            {

                string[] Lead_ID_PK = Lead_ID.Split(',');

                for (int i = 1; i < Lead_ID_PK.Length; i++)
                {
                    if (Lead_ID_PK[i].ToString() != "")
                    {
                        Response = objLeadManager.AssignLead(Convert.ToInt64(Lead_ID_PK[i]), Convert.ToInt64(User_ID));

                        if (Response.ErrorCode != 0)
                        {
                            break;
                        }
                    }
                }

                model.leads = objLeadManager.getAllLeads(session.UserSession.UserId.ToString(), session.UserSession.UserType);
                return View("AjaxAssignLeads", model);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAssignLead Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult AjaxgetSalesRepId(string SourceId)
        {           
            objResponse Response = new objResponse();
            session = new SessionHelper();         

            try
            {
                Response = objLeadManager.getsaleRepByID(Convert.ToInt64(SourceId));
                if (Response.ErrorCode == 0)
                {                    
                    return Json(Response.ResponseData.Tables[0].Rows[0][0].ToString(),JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult GetPricingTemplateData(string PType)
        {
            objResponse response = new objResponse();

            RatesAndFee objRates = new RatesAndFee();
            try
            {
                response = UtilityManager.getPricingTemplateData(PType);
                if(response.ErrorCode == 0)
                {
                    objRates.Rate_ID_Auto_PK = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["Rate_ID_Auto_PK"]);
                    objRates.Rate_Type = response.ResponseData.Tables[0].Rows[0]["Rate_Type"].ToString();
                    objRates.Debit_Qual = response.ResponseData.Tables[0].Rows[0]["Debit_Qual"].ToString();
                    objRates.Debit_MID_Qual = response.ResponseData.Tables[0].Rows[0]["Debit_MID_Qual"].ToString();
                    objRates.Debit_MID_Qual_Per_Item = response.ResponseData.Tables[0].Rows[0]["Debit_MID_Qual_Per_Item"].ToString();
                    objRates.Debit_Non_Qual = response.ResponseData.Tables[0].Rows[0]["Debit_Non_Qual"].ToString();
                    objRates.Credit_Qual = response.ResponseData.Tables[0].Rows[0]["Credit_Qual"].ToString();
                    objRates.Credit_MID_Qual = response.ResponseData.Tables[0].Rows[0]["Credit_MID_Qual"].ToString();
                    objRates.Credit_MID_Qual_Per_Item = response.ResponseData.Tables[0].Rows[0]["Credit_MID_Qual_Per_Item"].ToString();
                    objRates.Credit_Non_Qual = response.ResponseData.Tables[0].Rows[0]["Credit_Non_Qual"].ToString();
                    objRates.Debit_Transaction_Fee = response.ResponseData.Tables[0].Rows[0]["Debit_Transaction_Fee"].ToString();
                    objRates.Return_Transaction_Fee = response.ResponseData.Tables[0].Rows[0]["Return_Transaction_Fee"].ToString();
                    objRates.EBT_Transaction_Fee = response.ResponseData.Tables[0].Rows[0]["EBT_Transaction_Fee"].ToString();
                    objRates.Electronic_AVS_Fee = response.ResponseData.Tables[0].Rows[0]["Electronic_AVS_Fee"].ToString();
                    objRates.AMEX_Trans_Fee = response.ResponseData.Tables[0].Rows[0]["AMEX_Trans_Fee"].ToString();
                    objRates.Statement_Fee = response.ResponseData.Tables[0].Rows[0]["Statement_Fee"].ToString();
                    objRates.MonthlyMinimum = response.ResponseData.Tables[0].Rows[0]["MonthlyMinimum"].ToString();
                    objRates.ChargeBack_Fee = response.ResponseData.Tables[0].Rows[0]["ChargeBack_Fee"].ToString();
                    objRates.Batch_Fee = response.ResponseData.Tables[0].Rows[0]["Batch_Fee"].ToString();
                    objRates.ReserveAccount_Fee = response.ResponseData.Tables[0].Rows[0]["ReserveAccount_Fee"].ToString();
                    objRates.FDR_HelpDesk_Fee = response.ResponseData.Tables[0].Rows[0]["FDR_HelpDesk_Fee"].ToString();
                    objRates.FDR_Asst_Service_Fee = response.ResponseData.Tables[0].Rows[0]["FDR_Asst_Service_Fee"].ToString();
                    objRates.ACH_Change_Fee = response.ResponseData.Tables[0].Rows[0]["ACH_Change_Fee"].ToString();
                    objRates.RetrivalRequest_Fee = response.ResponseData.Tables[0].Rows[0]["RetrivalRequest_Fee"].ToString();
                    objRates.Voice_Auth_Fee = response.ResponseData.Tables[0].Rows[0]["Voice_Auth_Fee"].ToString();
                    objRates.Annual_Fee = response.ResponseData.Tables[0].Rows[0]["Annual_Fee"].ToString();
                    objRates.PCI_NonAction_Fee = response.ResponseData.Tables[0].Rows[0]["PCI_NonAction_Fee"].ToString();
                    objRates.Regulatory_Fee = response.ResponseData.Tables[0].Rows[0]["Regulatory_Fee"].ToString();
                    objRates.Regulatory_NonComplience_Fee = response.ResponseData.Tables[0].Rows[0]["Regulatory_NonComplience_Fee"].ToString();
                    objRates.Early_Termination_Bef1 = response.ResponseData.Tables[0].Rows[0]["Early_Termination_Bef1"].ToString();
                    objRates.Early_Termination_Aft1 = response.ResponseData.Tables[0].Rows[0]["Early_Termination_Aft1"].ToString();
                    objRates.Early_Termination_Bef2 = response.ResponseData.Tables[0].Rows[0]["Early_Termination_Bef2"].ToString();
                    objRates.InterchangeClear_Fee = response.ResponseData.Tables[0].Rows[0]["InterchangeClear_Fee"].ToString();
                    objRates.WirelessSetup_Fee = response.ResponseData.Tables[0].Rows[0]["WirelessSetup_Fee"].ToString();
                    objRates.WirelessMonthly_Fee = response.ResponseData.Tables[0].Rows[0]["WirelessMonthly_Fee"].ToString();
                    objRates.WirelessAuth_Fee = response.ResponseData.Tables[0].Rows[0]["WirelessAuth_Fee"].ToString();
                    objRates.GatewaySetup_Fee = response.ResponseData.Tables[0].Rows[0]["GatewaySetup_Fee"].ToString();
                    objRates.GatewayMonthly_Fee = response.ResponseData.Tables[0].Rows[0]["GatewayMonthly_Fee"].ToString();
                    objRates.GatewayAuth_Fee = response.ResponseData.Tables[0].Rows[0]["GatewayAuth_Fee"].ToString();


                  

                    return Json(objRates, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                
            }
            catch(Exception ex)
            {
                BAL.Common.LogManager.LogError("GetPricingTemplateData Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
