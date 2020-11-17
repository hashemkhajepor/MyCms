using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;

namespace MyCms.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (ModelState.IsValid)
            {
                var user = db.UsersRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
                string oldPasswordhash = FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword, "MD5");
                if(user.Password == oldPasswordhash)
                {
                    string hashNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password, "MD5");
                    user.Password = hashNewPassword;
                    db.Save();
                    ViewBag.Success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نمی باشد");
                }
            }
            return View();
        }
    }
}