using PRIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRIS.Controllers
{
    public class LoginController : Controller
    {
        private SignUpEntities db = new SignUpEntities();
        // GET: Login

        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                return RedirectToAction("main", "jobseekers", new { Email = Session["Email"].ToString() });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Jobseeker jobseeker)
        {
            var userLoggedIn = db.Jobseekers.SingleOrDefault(x => x.Email == jobseeker.Email && x.Password == jobseeker.Password);
            if (userLoggedIn != null)
            {
                Session["Email"] = jobseeker.Email;
                var registedUserID = db.Jobseekers.FirstOrDefault(x => x.Email == jobseeker.Email);

                int id = registedUserID.JobseekerId;
                string name = registedUserID.Name;
                return RedirectToAction("main", "jobseekers", new { id = id, Name = name });
            }
            else { 
            return View();
            }
        }

        public ActionResult Menu()
        {
            return View();
        }
    }
}