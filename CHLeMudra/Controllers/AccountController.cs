using CHLeMudra.Data;
using CHLeMudra.Entity;
using CHLeMudra.Models;
using CHLeMudra.Web.Filters;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHLeMudra.Controllers
{
    public class AccountController : Controller
    {
        //private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        // IGenericRepository<Usermaster> _user;
        IUserRepository _user;

        public AccountController()
        {
            _user = new UserRepository();

        }

        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Options)]
        [HttpGet]
        [RestoreModelStateFromTempData]
        public ActionResult login()
        {

            //if (TempData["LoginModel"] != null )
            //{

            //    return View((LoginModel)TempData["LoginModel"]);
            //}
            if (TempData["Message"] != null)
                ViewBag.Message=(string)TempData["Message"];

            if (UserAuthenticate.IsAuthenticated)
                return RedirectToAction("Dashboard", "Admin");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Options)]
        [HttpPost]
        [SetTempDataModelState]
        public ActionResult SubmitLogin(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                Usermaster user = _user.DoLogin(Model.Username, Model.Password);
                if (user != null)
                {
                    // Generate Token
                    if (user.Status)
                    {
                       // TokenManagement TokenResp = _user.TokenManagement(user.Id);
                        AddLoginCookie(user.Id, user.UserName, user.Name, user.Role, "1");
                        if (user.Role == (int)Roles.Admin)
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                    }
                    else
                    {
                        ViewBag.Message = "Your account is in-active. Please contact to site admin";
                    }
                }
                else
                {
                    ViewBag.Message = "The username or password you entered is invalid";
                }
            }
            TempData["Message"] = ViewBag.Message;
         //   TempData["ModelState"] = ModelState;
            // return View("~/Views/Account/Login.cshtml", Model);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            var model = new ResetPasswordModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var users = _user.FindBy(c => c.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
                if (users != null)
                {

                    bool sucess = MessageServices.SendPassword(users);
                    if (sucess)
                    {
                        model.success = true;
                        model.msg = "To complete the password process look for an email in your inbox that provides further instructions.";

                    }
                    else
                    {
                        model.success = false;
                        model.msg = "Sorry ! Password confirmation Mail is Failed";
                    }
                }
                else
                {
                    model.success = false;
                    model.msg = "This email address " + model.Email + " does not exist";

                }
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ResetPassword1(string Email)
        {
            var users = _user.FindBy(c => c.Email.ToLower() == Email.ToLower()).FirstOrDefault();
            if (users != null)
            {

                bool sucess = MessageServices.SendPassword(users);
                if (sucess)
                {
                    return Json(new
                    {
                        sucess = true,
                        msg = "To complete the password process look for an email in your inbox that provides further instructions."
                    });
                }
                else
                {
                    return Json(new
                    {
                        sucess = false,
                        msg = "Sorry ! Password confirmation Mail is Failed"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    sucess = false,
                    msg = "This email address " + Email + " does not exist"
                });
            }
        }
        public ActionResult LogOut()
        {
            HttpContext context = System.Web.HttpContext.Current;
            UserAuthenticate.Logout(context);
            return RedirectToAction("Index", "Home");
        }
        void AddLoginCookie(int UserId, string UserName, string Name, int Role, string Token)
        {
            var cookie = new HttpCookie("CHLeMudra_UserSession");
            cookie.HttpOnly = true;
            // cookie.Value = UserId.ToString();
            cookie.Value = CommonHelper.Encrypt(Session.SessionID + "!" + UserId.ToString() + "!" + Role + "!" + Name + "!" + UserName + "!" + Token);
            //  cookie.Value = Session.SessionID + "!" + UserId.ToString()+"!"+ Email+"!"+ UserName+"!"+ Role;
            //cookie.Values["uID"] = CommonHelper.Encrypt(UserId.ToString());
            //cookie.Values["uEmail"] = CommonHelper.Encrypt(Email.ToString());
            //cookie.Values["uName"] = CommonHelper.Encrypt(UserName.ToString());
            //cookie.Values["uRole"] = CommonHelper.Encrypt(Role.ToString());
            cookie.Expires = DateTime.Now.AddHours(24);
            Response.Cookies.Add(cookie);

            var tokencookie = new HttpCookie("Token");
            tokencookie.HttpOnly = true;
            DateTime ExpiresOn = DateTime.Now.AddHours(24);
            //tokencookie.Value = CommonHelper.Encrypt(UserId + "|" + ExpiresOn.ToString());
            tokencookie.Value = Token; //CommonHelper.Encrypt(UserId + "|" + ExpiresOn.ToString());
            tokencookie.Expires = DateTime.Now.AddHours(20);
            Response.Cookies.Add(tokencookie);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _user != null)
                _user.Dispose();
            base.Dispose(disposing);
        }
    }
}