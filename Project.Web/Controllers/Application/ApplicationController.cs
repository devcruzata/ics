using BAL.MerchantApplication;
using Newtonsoft.Json;
//using BAL.Utility;
using Project.Entity;
using Project.ViewModel;
using Project.Web.ApplicationHelper;
using Project.Web.ApplicationHelper.structure;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Project.Web.Controllers.Application
{
    public class ApplicationController : Controller
    {
       // ApiHelper.ApiHelper objApiHelper = new ApiHelper.ApiHelper();
        BAL.MerchantApplication.AplicationManager objAppManager = new BAL.MerchantApplication.AplicationManager();
        SessionHelper session;
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();

        //[Authorize]
        public ActionResult ApplicationHome()
        {          
            
                Session["Stage"] = "0";
                Session["App_ID"] = "";           
                     
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult GetCurrentStage()
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objAppManager.GetAppStage(HttpContext.Session["App_ID"].ToString());
                if (Response.ErrorCode == 0)
                {
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetCurrentStage conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        
       // [Authorize]
        public ActionResult getAllSalesReps()
        {
            ApplicationModel objAppModel = new ApplicationModel();
            //objAppModel.agents = objApiHelper.getSalesReps();
            return View();
        }

       

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep1(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
           
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                //string  Application = MerAppUtility.aapendData(objApp,"~/Template/i_pay_template.xml","1");
                //objApp.App = XElement.Parse(Application);
                Response = objAppManager.AddAppStep1(objApp);

                if (Response.ErrorCode == 0)
                {
                     HttpContext.Session["Stage"] = "1";
                     HttpContext.Session["App_ID"] = Response.ErrorMessage;
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep1 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep2(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                Response = objAppManager.AddAppStep2(objApp);

                if (Response.ErrorCode == 0)
                {
                    Session["Stage"] = "1";
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep2 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep3(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                Response = objAppManager.AddAppStep3(objApp);

                if (Response.ErrorCode == 0)
                {
                    Session["Stage"] = "2";
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep3 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep4(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                Response = objAppManager.AddAppStep4(objApp);

                if (Response.ErrorCode == 0)
                {
                    Session["Stage"] = "3";
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep4 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep5(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                Response = objAppManager.AddAppStep5(objApp);

                if (Response.ErrorCode == 0)
                {
                    Session["Stage"] = "4";
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep5 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AjaxAddAppStep6(ApplicationModel objModel)
        {
            objResponse Response = new objResponse();
            Applications objApp = new Applications();
            session = new SessionHelper();
            try
            {
                objApp = objJavaScriptSerializer.Deserialize<Project.Entity.Applications>(objJavaScriptSerializer.Serialize(objModel));

                Response = objAppManager.AddAppStep6(objApp);

                if (Response.ErrorCode == 0)
                {
                    Session["Stage"] = "5";
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddAppStep6 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        //[Authorize]
        [HttpPost]
        public ActionResult ResumeApplication(string AppEmail , string AppPassword)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.ResumeApp(AppEmail,AppPassword);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {
                        Session["App_ID"] = Response.ResponseData.Tables[0].Rows[0]["Merchant_App_ID_Auto_PK"].ToString();
                        Session["Stage"] = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();
                        objAppView.MerchantApp_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Merchant_App_ID_Auto_PK"]);

                        if (Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString() == "1")
                        {
                            objAppView.LBN = Response.ResponseData.Tables[0].Rows[0]["LegalBusinessName"].ToString();
                            objAppView.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
                            objAppView.bDesc = Response.ResponseData.Tables[0].Rows[0]["BusinessDesc"].ToString();
                            objAppView.OwnershipTyp = Response.ResponseData.Tables[0].Rows[0]["OwnershipType"].ToString();
                            objAppView.yr = Response.ResponseData.Tables[0].Rows[0]["year_in_Busi"].ToString();
                            objAppView.mt = Response.ResponseData.Tables[0].Rows[0]["month_In_Busi"].ToString();
                            objAppView.website = Response.ResponseData.Tables[0].Rows[0]["Website"].ToString();
                            objAppView.ftID = Response.ResponseData.Tables[0].Rows[0]["Fedral_Tax_ID"].ToString();
                            objAppView.LAddress1 = Response.ResponseData.Tables[0].Rows[0]["LAddress1"].ToString();
                            objAppView.LAddress2 = Response.ResponseData.Tables[0].Rows[0]["LAddress2"].ToString();
                            objAppView.city = Response.ResponseData.Tables[0].Rows[0]["LCity"].ToString();
                            objAppView.state = Response.ResponseData.Tables[0].Rows[0]["LState"].ToString();
                            objAppView.zip = Response.ResponseData.Tables[0].Rows[0]["LZip"].ToString();
                            objAppView.LPhoneNo = Response.ResponseData.Tables[0].Rows[0]["LPhoneNo"].ToString();
                            objAppView.LFaxNo = Response.ResponseData.Tables[0].Rows[0]["LFaxNo"].ToString();
                            objAppView.bEmail = Response.ResponseData.Tables[0].Rows[0]["Busi_Email"].ToString();
                            objAppView.MAddress1 = Response.ResponseData.Tables[0].Rows[0]["MAddress1"].ToString();
                            objAppView.MAddress2 = Response.ResponseData.Tables[0].Rows[0]["MAddress2"].ToString();
                            objAppView.mcity = Response.ResponseData.Tables[0].Rows[0]["MCity"].ToString();
                            objAppView.mstate = Response.ResponseData.Tables[0].Rows[0]["MState"].ToString();
                            objAppView.mzip = Response.ResponseData.Tables[0].Rows[0]["MZip"].ToString();
                        }

                        if (Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString() == "2")
                        {
                            objAppView.FName = Response.ResponseData.Tables[0].Rows[0]["FirstName"].ToString();
                            objAppView.LName = Response.ResponseData.Tables[0].Rows[0]["LastName"].ToString();
                            objAppView.DOB = Response.ResponseData.Tables[0].Rows[0]["DOB"].ToString();
                            objAppView.SocSecurity = Response.ResponseData.Tables[0].Rows[0]["SocialSecurityNumber"].ToString();
                            objAppView.DLicense = Response.ResponseData.Tables[0].Rows[0]["DriverLicense"].ToString();
                            objAppView.DLicenseState = Response.ResponseData.Tables[0].Rows[0]["DriverLicenseSate"].ToString();
                            objAppView.RAddress1 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress1"].ToString();
                            objAppView.RAddress2 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress2"].ToString();
                            objAppView.rcity = Response.ResponseData.Tables[0].Rows[0]["rcity"].ToString();
                            objAppView.rstate = Response.ResponseData.Tables[0].Rows[0]["rstate"].ToString();
                            objAppView.rzip = Response.ResponseData.Tables[0].Rows[0]["rzip"].ToString();
                        }

                        if (Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString() == "3")
                        {
                            objAppView.MerchantType = Response.ResponseData.Tables[0].Rows[0]["MerchantType"].ToString();
                            objAppView.RSwiped = Response.ResponseData.Tables[0].Rows[0]["RetailSwiped"].ToString();
                            objAppView.RKeyed = Response.ResponseData.Tables[0].Rows[0]["RetailKeyed"].ToString();
                            objAppView.internet = Response.ResponseData.Tables[0].Rows[0]["internet"].ToString();
                            objAppView.MTOrders = Response.ResponseData.Tables[0].Rows[0]["MonthlyOrders"].ToString();
                            objAppView.avgTicket = Response.ResponseData.Tables[0].Rows[0]["AverageTicket"].ToString();
                            objAppView.highTicket = Response.ResponseData.Tables[0].Rows[0]["HighTicket"].ToString();
                            objAppView.MVolume = Response.ResponseData.Tables[0].Rows[0]["MonthlyVolume"].ToString();
                        }

                        if (Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString() == "4")
                        {
                            objAppView.BName = Response.ResponseData.Tables[0].Rows[0]["BankName"].ToString();
                            objAppView.BCity = Response.ResponseData.Tables[0].Rows[0]["BankCity"].ToString();
                            objAppView.BState = Response.ResponseData.Tables[0].Rows[0]["BankState"].ToString();
                            objAppView.BZip = Response.ResponseData.Tables[0].Rows[0]["BankZip"].ToString();
                            objAppView.BRoutNumber = Response.ResponseData.Tables[0].Rows[0]["BankRouteNumber"].ToString();
                            objAppView.BacNumber = Response.ResponseData.Tables[0].Rows[0]["BankAcNumber"].ToString();
                        }

                        if (Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString() == "5")
                        {
                            objAppView.Equipment = Response.ResponseData.Tables[0].Rows[0]["Equipment"].ToString();
                            objAppView.DebitQual = Response.ResponseData.Tables[0].Rows[0]["DebitQual"].ToString();
                            objAppView.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQual"].ToString();
                            objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNonQual"].ToString();
                            objAppView.CreditQual = Response.ResponseData.Tables[0].Rows[0]["CreditQual"].ToString();
                            objAppView.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQual"].ToString();
                            objAppView.CreditNonQual = Response.ResponseData.Tables[0].Rows[0]["CreditNONQual"].ToString();
                            objAppView.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitQualPerItem"].ToString();
                            objAppView.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQualPerItem"].ToString();
                            objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNONQualPerItem"].ToString();
                            objAppView.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditQualPerItem"].ToString();
                            objAppView.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQualPerItem"].ToString();
                            objAppView.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditNONQualPerItem"].ToString();
                            objAppView.DebitTransFee = Response.ResponseData.Tables[0].Rows[0]["DebitTransFee"].ToString();
                            objAppView.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0]["ReturnTransFee"].ToString();
                            objAppView.EBTTransFee = Response.ResponseData.Tables[0].Rows[0]["EBTTransFee"].ToString();
                            objAppView.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0]["ElectronicAVSFee"].ToString();
                            objAppView.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0]["AMEZTransFee"].ToString();
                            objAppView.StatementFee = Response.ResponseData.Tables[0].Rows[0]["StatementFee"].ToString();
                            objAppView.MontMini = Response.ResponseData.Tables[0].Rows[0]["MonthlyMinimum"].ToString();
                            objAppView.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0]["ChargeBackFee"].ToString();
                            objAppView.BatchFee = Response.ResponseData.Tables[0].Rows[0]["BatchFee"].ToString();
                            objAppView.GatewayAccessFee = Response.ResponseData.Tables[0].Rows[0]["GatewayAccessFee"].ToString();
                            objAppView.GatewayPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["GatewayPerAuthFee"].ToString();
                            objAppView.GatewaySetUpFee = Response.ResponseData.Tables[0].Rows[0]["GatewaySetupFee"].ToString();
                            objAppView.WirelessAccessFee = Response.ResponseData.Tables[0].Rows[0]["WirelessAccessFee"].ToString();
                            objAppView.WirelessPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["WirelessPerAuthFee"].ToString();
                            objAppView.WirelessSetupFee = Response.ResponseData.Tables[0].Rows[0]["WirelessSetupFee"].ToString();
                            objAppView.RetrievalFee = Response.ResponseData.Tables[0].Rows[0]["RetrievalFee"].ToString();
                        }                        

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet); 
                        
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
                BAL.Common.LogManager.LogError("ResumeApplication conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

       // [Authorize]
        [HttpPost]
        public ActionResult LoadStep1(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.LoadAppStep1(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {
                       

                        objAppView.LBN = Response.ResponseData.Tables[0].Rows[0]["LegalBusinessName"].ToString();
                        objAppView.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
                        objAppView.bDesc = Response.ResponseData.Tables[0].Rows[0]["BusinessDesc"].ToString();
                        objAppView.OwnershipTyp = Response.ResponseData.Tables[0].Rows[0]["OwnershipType"].ToString();
                        objAppView.yr = Response.ResponseData.Tables[0].Rows[0]["year_in_Busi"].ToString();
                        objAppView.mt = Response.ResponseData.Tables[0].Rows[0]["month_In_Busi"].ToString();
                        objAppView.website = Response.ResponseData.Tables[0].Rows[0]["Website"].ToString();
                        objAppView.ftID = Response.ResponseData.Tables[0].Rows[0]["Fedral_Tax_ID"].ToString();
                        objAppView.LAddress1 = Response.ResponseData.Tables[0].Rows[0]["LAddress1"].ToString();
                        objAppView.LAddress2 = Response.ResponseData.Tables[0].Rows[0]["LAddress2"].ToString();
                        objAppView.city = Response.ResponseData.Tables[0].Rows[0]["LCity"].ToString();
                        objAppView.state = Response.ResponseData.Tables[0].Rows[0]["LState"].ToString();
                        objAppView.zip = Response.ResponseData.Tables[0].Rows[0]["LZip"].ToString();
                        objAppView.LPhoneNo = Response.ResponseData.Tables[0].Rows[0]["LPhoneNo"].ToString();
                        objAppView.LFaxNo = Response.ResponseData.Tables[0].Rows[0]["LFaxNo"].ToString();
                        objAppView.bEmail = Response.ResponseData.Tables[0].Rows[0]["Busi_Email"].ToString();
                        objAppView.MAddress1 = Response.ResponseData.Tables[0].Rows[0]["MAddress1"].ToString();
                        objAppView.MAddress2 = Response.ResponseData.Tables[0].Rows[0]["MAddress2"].ToString();
                        objAppView.mcity = Response.ResponseData.Tables[0].Rows[0]["MCity"].ToString();
                        objAppView.mstate = Response.ResponseData.Tables[0].Rows[0]["MState"].ToString();
                        objAppView.mzip = Response.ResponseData.Tables[0].Rows[0]["MZip"].ToString();                        

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("LoadStep1 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult LoadStep2(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.LoadAppStep2(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {                        

                        objAppView.FName = Response.ResponseData.Tables[0].Rows[0]["FirstName"].ToString();
                        objAppView.LName = Response.ResponseData.Tables[0].Rows[0]["LastName"].ToString();
                        objAppView.DOB = Response.ResponseData.Tables[0].Rows[0]["DOB"].ToString();
                        objAppView.SocSecurity = Response.ResponseData.Tables[0].Rows[0]["SocialSecurityNumber"].ToString();
                        objAppView.DLicense = Response.ResponseData.Tables[0].Rows[0]["DriverLicense"].ToString();
                        objAppView.DLicenseState = Response.ResponseData.Tables[0].Rows[0]["DriverLicenseSate"].ToString();
                        objAppView.RAddress1 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress1"].ToString();
                        objAppView.RAddress2 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress2"].ToString();
                        objAppView.rcity = Response.ResponseData.Tables[0].Rows[0]["rcity"].ToString();
                        objAppView.rstate = Response.ResponseData.Tables[0].Rows[0]["rstate"].ToString();
                        objAppView.rzip = Response.ResponseData.Tables[0].Rows[0]["rzip"].ToString();

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("LoadStep2 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

       // [Authorize]
        [HttpPost]
        public ActionResult LoadStep3(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.LoadAppStep3(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {                        

                        objAppView.MerchantType = Response.ResponseData.Tables[0].Rows[0]["MerchantType"].ToString();
                        objAppView.RSwiped = Response.ResponseData.Tables[0].Rows[0]["RetailSwiped"].ToString();
                        objAppView.RKeyed = Response.ResponseData.Tables[0].Rows[0]["RetailKeyed"].ToString();
                        objAppView.internet = Response.ResponseData.Tables[0].Rows[0]["internet"].ToString();
                        objAppView.MTOrders = Response.ResponseData.Tables[0].Rows[0]["MonthlyOrders"].ToString();
                        objAppView.avgTicket = Response.ResponseData.Tables[0].Rows[0]["AverageTicket"].ToString();
                        objAppView.highTicket = Response.ResponseData.Tables[0].Rows[0]["HighTicket"].ToString();
                        objAppView.MVolume = Response.ResponseData.Tables[0].Rows[0]["MonthlyVolume"].ToString();

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("LoadStep3 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

      //  [Authorize]
        [HttpPost]
        public ActionResult LoadStep4(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.LoadAppStep4(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {                       

                        objAppView.BName = Response.ResponseData.Tables[0].Rows[0]["BankName"].ToString();
                        objAppView.BCity = Response.ResponseData.Tables[0].Rows[0]["BankCity"].ToString();
                        objAppView.BState = Response.ResponseData.Tables[0].Rows[0]["BankState"].ToString();
                        objAppView.BZip = Response.ResponseData.Tables[0].Rows[0]["BankZip"].ToString();
                        objAppView.BRoutNumber = Response.ResponseData.Tables[0].Rows[0]["BankRouteNumber"].ToString();
                        objAppView.BacNumber = Response.ResponseData.Tables[0].Rows[0]["BankAcNumber"].ToString();

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("LoadStep4 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult LoadStep5(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationViewModel objAppView = new ApplicationViewModel();
            try
            {
                Response = objAppManager.LoadAppStep5(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {
                       

                        objAppView.Equipment = Response.ResponseData.Tables[0].Rows[0]["Equipment"].ToString();
                        objAppView.DebitQual = Response.ResponseData.Tables[0].Rows[0]["DebitQual"].ToString();
                        objAppView.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQual"].ToString();
                        objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNonQual"].ToString();
                        objAppView.CreditQual = Response.ResponseData.Tables[0].Rows[0]["CreditQual"].ToString();
                        objAppView.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQual"].ToString();
                        objAppView.CreditNonQual = Response.ResponseData.Tables[0].Rows[0]["CreditNONQual"].ToString();
                        objAppView.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitQualPerItem"].ToString();
                        objAppView.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQualPerItem"].ToString();
                        objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNONQualPerItem"].ToString();
                        objAppView.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditQualPerItem"].ToString();
                        objAppView.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQualPerItem"].ToString();
                        objAppView.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditNONQualPerItem"].ToString();
                        objAppView.DebitTransFee = Response.ResponseData.Tables[0].Rows[0]["DebitTransFee"].ToString();
                        objAppView.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0]["ReturnTransFee"].ToString();
                        objAppView.EBTTransFee = Response.ResponseData.Tables[0].Rows[0]["EBTTransFee"].ToString();
                        objAppView.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0]["ElectronicAVSFee"].ToString();
                        objAppView.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0]["AMEZTransFee"].ToString();
                        objAppView.StatementFee = Response.ResponseData.Tables[0].Rows[0]["StatementFee"].ToString();
                        objAppView.MontMini = Response.ResponseData.Tables[0].Rows[0]["MonthlyMinimum"].ToString();
                        objAppView.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0]["ChargeBackFee"].ToString();
                        objAppView.BatchFee = Response.ResponseData.Tables[0].Rows[0]["BatchFee"].ToString();
                        objAppView.GatewayAccessFee = Response.ResponseData.Tables[0].Rows[0]["GatewayAccessFee"].ToString();
                        objAppView.GatewayPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["GatewayPerAuthFee"].ToString();
                        objAppView.GatewaySetUpFee = Response.ResponseData.Tables[0].Rows[0]["GatewaySetupFee"].ToString();
                        objAppView.WirelessAccessFee = Response.ResponseData.Tables[0].Rows[0]["WirelessAccessFee"].ToString();
                        objAppView.WirelessPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["WirelessPerAuthFee"].ToString();
                        objAppView.WirelessSetupFee = Response.ResponseData.Tables[0].Rows[0]["WirelessSetupFee"].ToString();
                        objAppView.RetrievalFee = Response.ResponseData.Tables[0].Rows[0]["RetrievalFee"].ToString();

                        objAppView.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("LoadStep5 conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> SubmitApplication(string ApplicationId)
        {
            objResponse Response = new objResponse();
            ApplicationModel objAppView = new ApplicationModel();
            AppRequest objRequest = new AppRequest();
            Principal objPrincipal = new Principal();
            
            try
            {
                Response = objAppManager.GetAppData(ApplicationId);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "No Record Found")
                    {                           
                        
                            objAppView.MerchantApp_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Merchant_App_ID_Auto_PK"]);

                        
                            objAppView.LBN = Response.ResponseData.Tables[0].Rows[0]["LegalBusinessName"].ToString();
                            objAppView.DBA = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
                            objAppView.bDesc = Response.ResponseData.Tables[0].Rows[0]["BusinessDesc"].ToString();
                            objAppView.OwnershipTyp = Response.ResponseData.Tables[0].Rows[0]["OwnershipType"].ToString();
                            objAppView.yr = Response.ResponseData.Tables[0].Rows[0]["year_in_Busi"].ToString();
                            objAppView.mt = Response.ResponseData.Tables[0].Rows[0]["month_In_Busi"].ToString();
                            objAppView.website = Response.ResponseData.Tables[0].Rows[0]["Website"].ToString();
                            objAppView.ftID = Response.ResponseData.Tables[0].Rows[0]["Fedral_Tax_ID"].ToString();
                            objAppView.LAddress1 = Response.ResponseData.Tables[0].Rows[0]["LAddress1"].ToString();
                            objAppView.LAddress2 = Response.ResponseData.Tables[0].Rows[0]["LAddress2"].ToString();
                            objAppView.city = Response.ResponseData.Tables[0].Rows[0]["LCity"].ToString();
                            objAppView.state = Response.ResponseData.Tables[0].Rows[0]["LState"].ToString();
                            objAppView.zip = Response.ResponseData.Tables[0].Rows[0]["LZip"].ToString();
                            objAppView.LPhoneNo = Response.ResponseData.Tables[0].Rows[0]["LPhoneNo"].ToString();
                            objAppView.LFaxNo = Response.ResponseData.Tables[0].Rows[0]["LFaxNo"].ToString();
                            objAppView.bEmail = Response.ResponseData.Tables[0].Rows[0]["Busi_Email"].ToString();
                            objAppView.MAddress1 = Response.ResponseData.Tables[0].Rows[0]["MAddress1"].ToString();
                            objAppView.MAddress2 = Response.ResponseData.Tables[0].Rows[0]["MAddress2"].ToString();
                            objAppView.mcity = Response.ResponseData.Tables[0].Rows[0]["MCity"].ToString();
                            objAppView.mstate = Response.ResponseData.Tables[0].Rows[0]["MState"].ToString();
                            objAppView.mzip = Response.ResponseData.Tables[0].Rows[0]["MZip"].ToString();

                            
                       
                            objAppView.FName = Response.ResponseData.Tables[0].Rows[0]["FirstName"].ToString();
                            objAppView.LName = Response.ResponseData.Tables[0].Rows[0]["LastName"].ToString();
                            objAppView.DOB = Response.ResponseData.Tables[0].Rows[0]["DOB"].ToString();
                            objAppView.SocSecurity = Response.ResponseData.Tables[0].Rows[0]["SocialSecurityNumber"].ToString();
                            objAppView.DLicense = Response.ResponseData.Tables[0].Rows[0]["DriverLicense"].ToString();
                            objAppView.DLicenseState = Response.ResponseData.Tables[0].Rows[0]["DriverLicenseSate"].ToString();
                            objAppView.RAddress1 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress1"].ToString();
                            objAppView.RAddress2 = Response.ResponseData.Tables[0].Rows[0]["ResidanceAddress2"].ToString();
                            objAppView.rcity = Response.ResponseData.Tables[0].Rows[0]["rcity"].ToString();
                            objAppView.rstate = Response.ResponseData.Tables[0].Rows[0]["rstate"].ToString();
                            objAppView.rzip = Response.ResponseData.Tables[0].Rows[0]["rzip"].ToString();
                            
                       
                            objAppView.MerchantType = 
                            objAppView.RSwiped = Response.ResponseData.Tables[0].Rows[0]["RetailSwiped"].ToString();
                            objAppView.RKeyed = Response.ResponseData.Tables[0].Rows[0]["RetailKeyed"].ToString();
                            objAppView.internet = Response.ResponseData.Tables[0].Rows[0]["internet"].ToString();
                            objAppView.MTOrders = Response.ResponseData.Tables[0].Rows[0]["MonthlyOrders"].ToString();
                            objAppView.avgTicket = Response.ResponseData.Tables[0].Rows[0]["AverageTicket"].ToString();
                            objAppView.highTicket = Response.ResponseData.Tables[0].Rows[0]["HighTicket"].ToString();
                            objAppView.MVolume = Response.ResponseData.Tables[0].Rows[0]["MonthlyVolume"].ToString();                         


                            objAppView.BName = Response.ResponseData.Tables[0].Rows[0]["BankName"].ToString();
                            objAppView.BCity = Response.ResponseData.Tables[0].Rows[0]["BankCity"].ToString();
                            objAppView.BState = Response.ResponseData.Tables[0].Rows[0]["BankState"].ToString();
                            objAppView.BZip = Response.ResponseData.Tables[0].Rows[0]["BankZip"].ToString();
                            objAppView.BRoutNumber = Response.ResponseData.Tables[0].Rows[0]["BankRouteNumber"].ToString();
                            objAppView.BacNumber = Response.ResponseData.Tables[0].Rows[0]["BankAcNumber"].ToString();                       
                       
                       
                            objAppView.Equipment = Response.ResponseData.Tables[0].Rows[0]["Equipment"].ToString();
                            objAppView.DebitQual = Response.ResponseData.Tables[0].Rows[0]["DebitQual"].ToString();
                            objAppView.DebitMIDQual = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQual"].ToString();
                            objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNonQual"].ToString();
                            objAppView.CreditQual = Response.ResponseData.Tables[0].Rows[0]["CreditQual"].ToString();
                            objAppView.CreditMIDQual = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQual"].ToString();
                            objAppView.CreditNonQual = Response.ResponseData.Tables[0].Rows[0]["CreditNONQual"].ToString();
                            objAppView.DebitQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitQualPerItem"].ToString();
                            objAppView.DebitMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQualPerItem"].ToString();
                            objAppView.DebitNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["DebitNONQualPerItem"].ToString();
                            objAppView.CreditQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditQualPerItem"].ToString();
                            objAppView.CreditMIDQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQualPerItem"].ToString();
                            objAppView.CreditNonQualPerItem = Response.ResponseData.Tables[0].Rows[0]["CreditNONQualPerItem"].ToString();
                            objAppView.DebitTransFee = Response.ResponseData.Tables[0].Rows[0]["DebitTransFee"].ToString();
                            objAppView.ReturnTransFee = Response.ResponseData.Tables[0].Rows[0]["ReturnTransFee"].ToString();
                            objAppView.EBTTransFee = Response.ResponseData.Tables[0].Rows[0]["EBTTransFee"].ToString();
                            objAppView.ElectroAVSFee = Response.ResponseData.Tables[0].Rows[0]["ElectronicAVSFee"].ToString();
                            objAppView.AMEXTransFee = Response.ResponseData.Tables[0].Rows[0]["AMEZTransFee"].ToString();
                            objAppView.StatementFee = Response.ResponseData.Tables[0].Rows[0]["StatementFee"].ToString();
                            objAppView.MontMini = Response.ResponseData.Tables[0].Rows[0]["MonthlyMinimum"].ToString();
                            objAppView.ChargeBackFee = Response.ResponseData.Tables[0].Rows[0]["ChargeBackFee"].ToString();
                            objAppView.BatchFee = Response.ResponseData.Tables[0].Rows[0]["BatchFee"].ToString();
                            objAppView.GatewayAccessFee = Response.ResponseData.Tables[0].Rows[0]["GatewayAccessFee"].ToString();
                            objAppView.GatewayPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["GatewayPerAuthFee"].ToString();
                            objAppView.GatewaySetUpFee = Response.ResponseData.Tables[0].Rows[0]["GatewaySetupFee"].ToString();
                            objAppView.WirelessAccessFee = Response.ResponseData.Tables[0].Rows[0]["WirelessAccessFee"].ToString();
                            objAppView.WirelessPerAuthFee = Response.ResponseData.Tables[0].Rows[0]["WirelessPerAuthFee"].ToString();
                            objAppView.WirelessSetupFee = Response.ResponseData.Tables[0].Rows[0]["WirelessSetupFee"].ToString();
                            objAppView.RetrievalFee = Response.ResponseData.Tables[0].Rows[0]["RetrievalFee"].ToString();

                            string CretedTime = DateTime.Now.ToUniversalTime().ToString("u");
                         //   string AppXml = ApplicationHelper.MerAppUtility.PopulateXml(objAppView, "~/Template/AppTemplate.xml", CretedTime);

                            foreach (DataColumn dc in Response.ResponseData.Tables[0].Columns)
                            {
                                if (dc.ColumnName.ToString() == "BankName")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.BankName;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankName"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankName")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.AlternateBankDDA;
                                    objAppDetail.Value = "FALSE";
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankCity")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.City;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankCity"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankState")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.State;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankState"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankZip")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.Zip;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankZip"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankRouteNumber")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RoutingNo;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankRouteNumber"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BankAcNumber")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.AcountNo;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BankAcNumber"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }


                                // BusinessInfo----------


                                if (dc.ColumnName.ToString() == "LAddress1")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LAddress;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LAddress1"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["LAddress1"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LCity")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LCity;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LCity"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LState")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LState;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LState"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LZip")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LZip;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LZip"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "Fedral_Tax_ID")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.FedralTaxId;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["Fedral_Tax_ID"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "month_In_Busi")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MonthsInBusiness;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["month_In_Busi"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "year_in_Busi")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.YearsInBusiness;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["year_in_Busi"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "Fedral_Tax_ID")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.VerifyFedralTaxId;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["Fedral_Tax_ID"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DBA")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DBA;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DBA"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LegalBusinessName")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LegalBusinessname;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LegalBusinessName"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "Busi_Email")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.BEmail;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["Busi_Email"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LFaxNo")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.BFax;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LFaxNo"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "LPhoneNo")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.BPhone;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LPhoneNo"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "Website")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.Website;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["Website"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MAddress1")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MAddress;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MAddress1"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["MAddress1"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MCity")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MCity;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MCity"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MState")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MState;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MState"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MZip")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MZip;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MZip"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MerchantType")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MerchantType;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MerchantType"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }                                

                                if (dc.ColumnName.ToString() == "OwnershipType")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.TypeOfOwner;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["OwnershipType"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "internet")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.internet;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["internet"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MonthlyOrders")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MOTO;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MonthlyOrders"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "RetailKeyed")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RetailKeyed;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["RetailKeyed"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "RetailSwiped")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RetailSwiped;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["RetailKeyed"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "AverageTicket")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.AvgTicket;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["AverageTicket"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "HighTicket")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.HighTicket;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["HighTicket"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MonthlyVolume")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MonthlySalesVolume;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MonthlyVolume"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);

                                }

                                // Fee Section-------------

                                if (dc.ColumnName.ToString() == "CreditMIDQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.CreditMidQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);

                                }

                                if (dc.ColumnName.ToString() == "CreditNONQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.CreditNonQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["CreditNONQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "CreditQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.CreditQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["CreditQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "CreditMIDQualPerItem")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.CreditMidQualPerItem;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["CreditMIDQualPerItem"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "CreditNONQualPerItem")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.CreditNonQualPerItem;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["CreditNONQualPerItem"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitMIDQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitMidQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitNONQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitNonQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitNONQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitQual")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitQual;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitQual"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitMIDQualPerItem")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitMidQualPerItem;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitMIDQualPerItem"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitNONQualPerItem")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitNonQualPerItem;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitNONQualPerItem"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "GatewayAccessFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.GatewayMonthlyFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["GatewayAccessFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "GatewayPerAuthFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.GatewayAuthFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["GatewayPerAuthFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "GatewaySetupFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.GatewaySetupFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["GatewaySetupFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "WirelessAccessFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.WirelessMonthlyFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["WirelessAccessFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "WirelessPerAuthFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.WirelessAuthFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["WirelessPerAuthFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "WirelessSetupFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.WirelessSetupFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["WirelessSetupFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "ReturnTransFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.ReturnTransFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["ReturnTransFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "ChargeBackFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.ChargebackFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["ChargeBackFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "EBTTransFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.EBTTransFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["EBTTransFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "DebitTransFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DebitTransFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DebitTransFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "RetrievalFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RetrievalFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["RetrievalFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }
                               

                                if (dc.ColumnName.ToString() == "StatementFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.Statment;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["StatementFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "MonthlyMinimum")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.MonthlyMinimum;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["MonthlyMinimum"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "ElectronicAVSFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.ElecAVSFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["ElectronicAVSFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "BatchFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.BatchFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["BatchFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }

                                if (dc.ColumnName.ToString() == "AMEZTransFee")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.AmexFee;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["AMEZTransFee"].ToString();
                                    objRequest.FieldValues.Add(objAppDetail);
                                }


                                // Principal info --------------------

                                if (dc.ColumnName.ToString() == "rcity")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RCity;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["rcity"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "DOB")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.DOB;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["DOB"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "FirstName")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.FName;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["FirstName"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "LastName")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.LName;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["LastName"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "rAddress1")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RAddress;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["rAddress1"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["rAddress2"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "SocialSecurityNumber")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.SSN;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["SocialSecurityNumber"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "rstate")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RState;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["rstate"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "rzip")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.RZip;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["rzip"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                if (dc.ColumnName.ToString() == "SocialSecurityNumber")
                                {
                                    AppDetail objAppDetail = new AppDetail();
                                    objAppDetail.FieldId = AppFieldId.VerifySSN;
                                    objAppDetail.Value = Response.ResponseData.Tables[0].Rows[0]["SocialSecurityNumber"].ToString();
                                    objPrincipal.FieldValues.Add(objAppDetail);
                                    objRequest.Principals.Add(objPrincipal);
                                }

                                
                            }

                            Equipment objEqp = new Equipment();
                            objEqp.EquipmentTypeID = "G";
                            objEqp.EquipmentID = "31201748-DF1B-4728-9B14-1FD3C740FDEF";
                            objEqp.ProcessorID = "8F574AC9-C212-4059-BD0B-C4408E7F44DA";

                            AppDetail objapp1 = new AppDetail();
                            objapp1.FieldId = "f27596a0-a2b5-4be1-a1b6-380c3df48fba";
                            objapp1.Value = "iphone";

                            AppDetail objapp2 = new AppDetail();
                            objapp2.FieldId = "788c86af-0b3e-4db9-80e1-54dc71d62283";
                            objapp2.Value = "Verizon";

                            AppDetail objapp3 = new AppDetail();
                            objapp3.FieldId = "6316efea-f3f1-4f72-ae46-a8cfa3fe144f";
                            objapp3.Value = "5c";

                            objEqp.FieldValues.Add(objapp1);
                            objEqp.FieldValues.Add(objapp2);
                            objEqp.FieldValues.Add(objapp3);
                            string con = JsonConvert.SerializeObject(objRequest);
                           // AppDetail objAppDetail = new AppDetail();
                            objRequest.Equipments = objEqp;
                            objRequest.SalesrepID = "c934fc64-26f2-4f27-a98b-0a13fcff70d1";
                            objRequest.PackageID = "ddfdba10-c29f-4abc-bc01-c110c65212e6";

                            string url = "https://api-sandbox.ipaymentinc.com/";
                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            //request.Headers.Add("apikey", "866BFC85-69AB-412C-BCEF-DEA31614A12F");
                            //request.Headers.Add("apisecret", "5C1ACD75-36F7-47C6-8580-DBFA352B5EA1");
                            //request.Method = WebRequestMethods.Http.Post;
                            //request.ContentType = "application/xml";
                            //request.ContentLength = AppXml.Length;
                            //request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                            //using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                            //{                               
                            //    writer.Write(AppXml);
                            //}


                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            //byte[] bytes;
                            //bytes = System.Text.Encoding.ASCII.GetBytes(objRequest);
                            //request.Headers.Add("apikey", "866BFC85-69AB-412C-BCEF-DEA31614A12F");
                            //request.Headers.Add("apisecret", "5C1ACD75-36F7-47C6-8580-DBFA352B5EA1");
                            ////request.ContentType = "text/xml; encoding='utf-8'";
                            //request.ContentType = "application/json";
                            //request.ContentLength = bytes.Length;
                            //request.Method = "POST";
                            //Stream requestStream = request.GetRequestStream();
                            //requestStream.Write(bytes, 0, bytes.Length);
                            //requestStream.Close();
                            //HttpWebResponse response;
                            //response = (HttpWebResponse)request.GetResponse();
                            //if (response.StatusCode == HttpStatusCode.OK)
                            //{
                            //    Stream responseStream = response.GetResponseStream();
                            //    string responseStr = new StreamReader(responseStream).ReadToEnd();
                                
                            //}

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(url);

                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("apikey", "866BFC85-69AB-412C-BCEF-DEA31614A12F");
                                client.DefaultRequestHeaders.Add("apisecret", "5C1ACD75-36F7-47C6-8580-DBFA352B5EA1");
                                
                              //  StringContent content = new StringContent(JsonConvert.SerializeObject(objRequest));
                                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                                StringContent content = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, "application/json");
                                // HTTP POST
                                HttpResponseMessage response = await client.PostAsync("MerchantApplicationAPI/V2/applications", content);
                                if (response.IsSuccessStatusCode)
                                {
                                    string data = await response.Content.ReadAsStringAsync();
                                    //  product = JsonConvert.DeserializeObject<Product>(data);
                                }
                            }

                        return Json(objAppView, JsonRequestBehavior.AllowGet);

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
                BAL.Common.LogManager.LogError("ResumeApplication conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        
        }

    }
}
