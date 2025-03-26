using CHLeMudra.Data;
using CHLeMudra.Entity;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CHLeMudra.Controllers
{
    public class HomeController : Controller
    {
        // private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        IGenericRepository<Usermaster> _user;
        public HomeController()
        {

            _user = new UserRepository();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            

            return RedirectToAction("login", "Account");


            //if (UserAuthenticate.IsAuthenticated)
            //    return RedirectToAction("Dashboard", "Admin");


            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {

            //if (disposing && _user != null)
            //    _user.Dispose();

            base.Dispose(disposing);
        }
    }
}