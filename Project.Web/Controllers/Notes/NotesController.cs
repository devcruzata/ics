using BAL.Note;
using Project.Entity;
using Project.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class NotesController : Controller
    {
        NoteManager objNoteManager = new NoteManager();
        SessionHelper session;
        //
        // GET: /Notes/

        [Authorize]
        [HttpPost]
        public ActionResult AjaxAddNote(string Title,string RelateTo, string Note, string RelatedTable)
        {
            objResponse Response = new objResponse();          
            session = new SessionHelper();
            try
            {
                Response = objNoteManager.AddNote(Convert.ToInt64(RelateTo),Note, session.UserSession.UserId, session.UserSession.UserId, RelatedTable);

                if (Response.ErrorCode == 0)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddNote conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
