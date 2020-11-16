using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyCms.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
              if(!db.UsersRepository.Get(u=>u.Email == register.Email.Trim().ToLower()).Any())
                {
                    Users user = new Users()
                    {
                        Email = register.Email.Trim().ToLower(),
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        RegisterDate = DateTime.Now,
                        UserName = register.UserName,
                        RoleID = 1
                    };
                    db.UsersRepository.Insert(user);
                    string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmails", user);
                    SendEmail.Send(user.Email, "فعال سازی حساب کاربری سایت خبری", body);
                    db.Save();
                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است");
                }
            }
            return View();
        }
        public ActionResult ActiveUser(string id)
        {
            var user = db.UsersRepository.Get(u => u.ActiveCode == id).SingleOrDefault();
            if(user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.Save();
            ViewBag.UserName = user.UserName;
            return View();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login , string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.UsersRepository.Get(u => u.Email == login.Email && u.Password == hashPassword).SingleOrDefault();
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMi);
                        return Redirect(ReturnUrl);
                    }
                    else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}