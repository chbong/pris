using PRIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRIS.Controllers
{
    public class ApplicationController : Controller
    {
        SignUpViewModel db = new SignUpViewModel();
        SignUpEntities a = new SignUpEntities();

        // GET: Application
        public ActionResult Index()
        {
            SignUpEntities a = new SignUpEntities();
            var x = from u in a.Applications
                    where u.Job.CompanyId == 1
                    orderby u.ApplicationDate descending
                    select u;
            return View(x);
        }

        public ActionResult AA()
        {
            
            var x = from u in a.Applications
                    where u.JobId == 1
                    orderby u.Job.JobTitle ascending, u.ApplicationDate descending
                    select u;

            var movies = from m in a.Applications
                         orderby m.Job.JobTitle ascending, m.ApplicationDate descending
                         where m.Job.CompanyId == 1
                         select m;

            return View(movies);
        }



        public ActionResult Main()
        {
            return View();
        }

        public ActionResult InsertApplication(SignUpViewModel model, int JobId)
        {
            Application application = new Application();

            application.JobId = JobId;
            application.JobseekerId = JobId;
            application.ApplicationDate = DateTime.Now;

       //     db.Jobseekers.Add();
            return View();
            
        }
    }
}