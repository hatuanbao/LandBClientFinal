using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LandBClient.Providers;
using LandBClient.Core;
using LandBClient.Models;
using System.Web.Security;
using LandBClient.Extensions;

namespace LandBClient.Controllers
{
    [MyAuthorize(Roles = "user")]//[Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthProvider _authprovider;
        public HomeController(IAuthProvider authprovider)
        {
            _authprovider = authprovider;
        }
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["CurrentUser"] != null)
            {
                TUser currentuserobj = new TUser();
                currentuserobj = Session["CurrentUser"] as TUser;
                if (currentuserobj.Role.ToLower() == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Home");
            }
            return View();
        }
        public ActionResult LoginUser()
        {
            TUser loginobj = new TUser();
            if (Session["CurrentUser"] != null)
            {
                loginobj = Session["CurrentUser"] as TUser;
                if (loginobj.Role.ToLower() == "admin")
                    return RedirectToAction("Home","Home");
            }
            else
            {
                return RedirectToAction("login","Home");
            }
            return View("login");
        }
        [HttpPost,AllowAnonymous]
        public ActionResult Login(LoginModel model,string returnUrl)
        {
            TUser currentuserobj = new TUser();
            try {
                if (ModelState.IsValid) {
                    string username = model.username;
                    string password = model.password;
                    currentuserobj = _authprovider.LoginCheck(username, password);
                    if (currentuserobj != null && currentuserobj.UserName!="" && currentuserobj.UserName!=null)
                    {
                        Session["CurrentUser"] = currentuserobj;
                        // TODO: fetch roles from your database based on the username
                        var roles = currentuserobj.Role.ToLower();//"Admin|SomeRole";
                        var ticket = new FormsAuthenticationTicket(
                            1,
                            username,
                            DateTime.Now,
                            DateTime.Now.AddMilliseconds(FormsAuthentication.Timeout.TotalMilliseconds),
                            false,
                            roles
                        );
                        var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        {
                            Domain = FormsAuthentication.CookieDomain,
                            HttpOnly = true,
                            Secure = FormsAuthentication.RequireSSL,
                        };

                        // Emit the authentication cookie which in addition to the username will
                        // contain the roles in the user data part
                        Response.AppendCookie(authCookie);
                       // FormsAuthentication.SetAuthCookie(username, false);
                        if (currentuserobj.Role.ToLower() == "admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }                      
                        else
                        {
                            return RedirectToAction("Home", "Home");
                        }
                    }
                    else
                    {
                         ViewBag.Msg="The user name or password provided is incorrect.";
                    }
                }
                else
                {
                    ViewBag.Msg = "The user name or password provided is incorrect.";
                }
            }
            catch(Exception ex) { }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            _authprovider.Logout();
            Session["CurrentUser"] = null;
            return View("Login");
        }
    }
}