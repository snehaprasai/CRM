using CRM.Pagination.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRM.Pagination.Authentication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel lvm,string ReturnURL)
        {
            if (ModelState.IsValid)
            {
                 if (Membership.ValidateUser(lvm.Username, lvm.Password))
                {
                    FormsAuthentication.SetAuthCookie(lvm.Username, true);
                    if (ReturnURL != null)
                    {
                        return Redirect(ReturnURL);
                    }
                    return Redirect("~/Account");
                }
            }
            return View(lvm);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Login");
        }
    }
}