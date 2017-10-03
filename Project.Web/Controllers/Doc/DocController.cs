﻿using BAL.Document;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Doc
{
    public class DocController : Controller
    {
        DocumentManager objDocManager = new DocumentManager();
        SessionHelper session;
        //
        // GET: /Doc/

        [Authorize]
        //public ActionResult DocHome()
        //{
        //    session = new SessionHelper();
        //    DocModel objDocModel = new DocModel();
        //    objDocModel.doc = objDocManager.getDocs(Convert.ToInt64(session.UserSession.PIN),Convert.ToInt64(session.UserSession.UserId));
        //    return View(objDocModel);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult DeleteDoc(string Doc_ID_PK , string FileName,string FileID)
        {
            string response = "";
            session = new SessionHelper();
            try
            {
                response = objDocManager.DeleteDocument(Convert.ToInt64(Doc_ID_PK));
                string Doc = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID + "_" + FileName;
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + Doc;
                if (System.IO.File.Exists(newFilePath))
                {
                    System.IO.File.Delete(newFilePath);
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteDoc Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddNewDoc()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                
                Int64 LeadID = 0;
                

                if (Request.Form["LeadID"].ToString() != "")
                {
                    LeadID = Convert.ToInt64(Request.Form["LeadID"]);
                }
                
                
                string Title = Request.Form["Title"].ToString();               
                string fname;
                Guid FileID = System.Guid.NewGuid();

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


                    string newFileName = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID.ToString()+"_" + fname;


                    string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + newFileName;
                    file.SaveAs(newFilePath);

                    Response = objDocManager.AddDoc(Title, LeadID.ToString(), fname, session.UserSession.UserId.ToString(), FileID.ToString());
                    if (Response.ErrorCode == 0)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    } 
                }
                return Json("success", JsonRequestBehavior.AllowGet);
                       
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddNewDoc Post method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        public ActionResult DownLoad(string FileName, string FileID)
        {
            session = new SessionHelper();
            try
            {
                string Doc = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID + "_" + FileName;
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + Doc;
                string contentType = "application/pdf";
                return File(newFilePath, contentType, FileName);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Download Req", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View("500");
            }
        }

    }
}
