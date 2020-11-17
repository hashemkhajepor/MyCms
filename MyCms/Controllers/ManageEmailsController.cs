using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCms.Controllers
{
    public class ManageEmailsController : Controller
    {
        // GET: ManageEmails
        public ActionResult ActivationEmails()
        {
            return PartialView();
        }
        public ActionResult RecoveryPassword()
        {
            return PartialView();
        }
    }
}