using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Campaigns;
using Project.Entity;
using Project.Web.Models;

namespace Project.Web.Controllers.EmailTemplate
{
    public class EmailTemplateController : Controller
    {
        CampaignsManager objCampManager = new CampaignsManager();
        //
        // GET: /EmailTemplate/

        [Authorize]
        public ActionResult TemplateHome()
        {
            TemplateModel objModel = new TemplateModel();
            objModel.templates = objCampManager.GetAllTemplate();

            return View(objModel);
        }

        [Authorize]
        public ActionResult AddTemplate(string name ,string template)
        {
            objResponse response = new objResponse();
            TemplateModel objTempModel = new TemplateModel();
            try
            {
                response = objCampManager.AddTemplate(name,template);

                if (response.ErrorCode == 0)
                {
                    if (response.ErrorMessage != "Template With Same Title Already Exists")
                    {
                        objTempModel.templates = objCampManager.GetAllTemplate();
                        return View("AjaxTemplates", objTempModel);
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
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult GetTemplateForEdit(string TemplateId)
        {
            objResponse response = new objResponse();
            try
            {
                TemplateModel objModel = new TemplateModel();
                response = objCampManager.GetTemplateForEdit(Convert.ToInt64(TemplateId));

                if (response.ErrorCode == 0)
                {
                    objModel.TemplateID = Convert.ToInt64(response.ResponseData.Tables[0].Rows[0]["Template_ID_Auto_PK"]);
                    objModel.title = response.ResponseData.Tables[0].Rows[0]["title"].ToString();
                    objModel.body = response.ResponseData.Tables[0].Rows[0]["templateBody"].ToString();
                    return Json(objModel, JsonRequestBehavior.AllowGet);
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
        public ActionResult EditTemplate(string TemplateId,string name, string template)
        {
            objResponse response = new objResponse();
            TemplateModel objTempModel = new TemplateModel();
            try
            {
                response = objCampManager.EditTemplate(Convert.ToInt64(TemplateId), name, template);

                if (response.ErrorCode == 0)
                {                  
                        objTempModel.templates = objCampManager.GetAllTemplate();
                        return View("AjaxTemplates", objTempModel);                 
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
        public ActionResult DeleteTemplate(string TemplateId)
        {
            objResponse response = new objResponse();
            TemplateModel objTempModel = new TemplateModel();
            try
            {
                response = objCampManager.DaleteTemplate(Convert.ToInt64(TemplateId));
                if (response.ErrorCode == 0)
                {
                    objTempModel.templates = objCampManager.GetAllTemplate();
                    return View("AjaxTemplates", objTempModel);
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

    }
}
