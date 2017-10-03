using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Campaigns;
using BAL.Utility;
using Project.Entity;

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
        public ActionResult SendCampaign()
        {
            objResponse response = new objResponse();
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

    }
}
