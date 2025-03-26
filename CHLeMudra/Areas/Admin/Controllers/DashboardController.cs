
using CHLeMudra.Data;
using CHLeMudra.Models;
using CHLeMudra.Web.Filters;
using System.Web.Mvc;
using System.Linq;
using System;

namespace CHLeMudra.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        IUserRepository _user;
      
        public DashboardController()
        {
            _user = new UserRepository();
          
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            int userId = UserAuthenticate.UserId;
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassWord model)
        {

            if (ModelState.IsValid)
            {
                var user = _user.GetUser(UserAuthenticate.UserId);
                if (user != null)
                {
                    if (user.Password == model.OldPassword)
                    {
                        user.Password = model.NewPassword;
                        bool Success = _user.Update(user);
                        ViewBag.Message = Success ? "Password has been changed." : "The Login Id Or Password You enterd Is Invalid";
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Please enter correct current password.");
                    }
                }
            }

            return View(model);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _user != null)
                _user.Dispose();

            base.Dispose(disposing);
        }

    }
}