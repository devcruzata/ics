using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Campaigns;
using BAL.Utility;
using Project.Entity;
using System.Threading.Tasks;
using Project.ViewModel;
using System.Data;

namespace Project.Web.Controllers.MarketingCampaign
{
    public class MarketingCampaignController : Controller
    {
        CampaignsManager objCampManger = new CampaignsManager();
        //
        // GET: /MarketingCampaign/
        [Authorize]
        public ActionResult CampaignHome()
        {
            ViewBag.Campaigns = objCampManger.getAllCampaigns();
            return View();
        }

        [Authorize]
        public ActionResult AddEmailCampaign()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetDisposition()
        {
            objResponse response = new objResponse();
            List<ViewModel.TextValue> status = new List<ViewModel.TextValue>();
            try
            {
                status = UtilityManager.GetSattusForDropDown();
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "Choose a Disposition" });

                foreach (var sta in status)
                {
                    list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetTemplates()
        {
            objResponse response = new objResponse();
            List<ViewModel.TextValue> templates = new List<ViewModel.TextValue>();
            try
            {
                templates = UtilityManager.GetTemplatesForDropDown();
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "Choose a Template" });

                foreach (var sta in templates)
                {
                    list.Add(new SelectListItem { Value = sta.Value, Text = sta.Text });
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult SaveCampaign(string title,string sendFlag,string dispositionID,string templateId)
        {
            objResponse response = new objResponse();
            CampaignHelperss objCap = new CampaignHelperss();
            try
            {
                response = objCampManger.AddnewCampaign(title,Convert.ToInt64(dispositionID),Convert.ToInt64(templateId),false);

                if(response.ErrorCode == 0)
                {
                    ViewBag.Campaigns = objCampManger.getAllCampaigns();
                    return View("TempCampaign");
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
        public async Task<ActionResult> sendCampaign(string CampaignId)
        {
            objResponse response = new objResponse();
            CampaignHelperss objCap = new CampaignHelperss();
            try
            {
                response = objCampManger.GetCampaignData(Convert.ToInt64(CampaignId));

                if (response.ErrorCode == 0)
                {
                    List<TextValue> toList = new List<TextValue>();
                    foreach (DataRow dr in response.ResponseData.Tables[1].Rows)
                    {
                        // EmailAddress to = new EmailAddress(dr["Email"].ToString(), dr["ContactName"].ToString());
                        TextValue objTextValue = new TextValue();
                        objTextValue.Value = dr["Email"].ToString();
                        objTextValue.Text = dr["ContactName"].ToString();
                        toList.Add(objTextValue);
                    }

                    await objCap.sendCampaign(toList, response.ResponseData.Tables[0].Rows[0][0].ToString());
                    ViewBag.Campaigns = objCampManger.getAllCampaigns();
                    return View("TempCampaign");
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json("",JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteCampaign(string campaignId)
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objCampManger.DaleteCampaign(Convert.ToInt64(campaignId));

                if(Response.ErrorCode == 0)
                {
                    ViewBag.Campaigns = objCampManger.getAllCampaigns();
                    return View("TempCampaign");
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }   
        
    }
}
