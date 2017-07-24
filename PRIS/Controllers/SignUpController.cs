using PRIS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PRIS.Controllers
{
    public class SignUpController : Controller
    {
        SignUpEntities db = new SignUpEntities();

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(SignUpViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid) {
                //    var file = Request.Files[0];
//                byte[] uploadedFile = new byte[model.Picture.InputStream.Length];


                Jobseeker jobseeker = new Jobseeker();
                jobseeker.Name = model.Name;
                jobseeker.Email = model.Email;
                jobseeker.Password = model.Password;
                jobseeker.PhoneNo = model.PhoneNo;
                jobseeker.Qualification = model.Qualification;
                jobseeker.FieldOfStudy = model.FieldOfStudy;
                jobseeker.PreferredLocation = model.PreferredLocation;
                jobseeker.PreferredSalary = model.PreferredSalary;
                jobseeker.PreferredField = model.PreferredField;
 //           model.Picture.InputStream.Read(uploadedFile,0,uploadedFile.Length);

            db.Jobseekers.Add(jobseeker);
            db.SaveChanges();

            int lastId = jobseeker.JobseekerId;

            Language language = new Language();
            language.LanguageLearned = model.LanguageLearned;
            language.JobseekerId = lastId;

            db.Languages.Add(language);
            db.SaveChanges();

            WorkingExperience exp = new WorkingExperience();
            exp.WorkingExp = model.WorkingExp;
            exp.JobseekerId = lastId;

            db.WorkingExperiences.Add(exp);
            db.SaveChanges();

            Skill skill = new Skill();
            skill.SkillAcquired = model.SkillAcquired;
            skill.JobseekerId = lastId;

            db.Skills.Add(skill);
            db.SaveChanges();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("Details", new { id = lastProductId });
            }
            return View(model);

        }

        public ActionResult Index()
        {
            //           List<object> myModel = new List<object>();
            //           myModel.Add(db.Jobseekers.ToList());
            //           myModel.Add(db.Languages.ToList());
            //           return View(myModel);

            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == 32
                         select new SignUpViewModel()
                         {
                             LanguageLearned = language.LanguageLearned,
                             Name = name.Name,
                             SkillAcquired = skill.SkillAcquired,
                             WorkingExp = exp.WorkingExp,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,


                         };
            return View(result.ToList());


        }

        public ActionResult Details(int? id)
        {
            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             LanguageLearned = language.LanguageLearned,
                             Name = name.Name,
                             SkillAcquired = skill.SkillAcquired,
                             WorkingExp = exp.WorkingExp,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,


                         };
            return View(result.ToList());
        }

        public ActionResult Edit(int? id)
        {
            Jobseeker jobseeker = db.Jobseekers.Find(id);
            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             LanguageLearned = language.LanguageLearned,
                             Name = name.Name,
                             SkillAcquired = skill.SkillAcquired,
                             WorkingExp = exp.WorkingExp,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,


                         };
            return View(result);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Company jobseeker)
        {
            var userLoggedIn = db.Companies.SingleOrDefault(x => x.CompanyEmail == jobseeker.CompanyEmail && x.Password == jobseeker.Password);
            if (userLoggedIn != null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }
            return View();
        }

        public ActionResult InsertApplication(SignUpViewModel model, int id, int jid)
        {
            Application application = new Application();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            application.JobId = id;
            application.JobseekerId = jid;
            application.ApplicationDate = DateTime.Now;
            db.Applications.Add(application);
            db.SaveChanges();
            return new EmptyResult();

        }

        public ActionResult SavePost(SignUpViewModel model, int id, int jid)
        {
            Save save = new Save();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            save.JobId = id;
            save.JobseekerId = jid;
            db.Saves.Add(save);
            db.SaveChanges();
            return new EmptyResult();

        }

        public ActionResult ViewApplication(int? id)
        {
            var result = from jobseeker in db.Jobseekers
                         join application in db.Applications on jobseeker.JobseekerId equals application.JobseekerId
                         join job in db.Jobs on application.JobId equals job.JobId
                         where jobseeker.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             JobseekerId = jobseeker.JobseekerId,
                             Name = jobseeker.Name,
                             JobTitle = job.JobTitle,
                             JobDescription = job.JobDescription,
                             ApplicationDate = application.ApplicationDate,
                             Salary = job.Salary,
                             Location = job.Location,
                             JobRequirement = job.JobRequirement,


                             

                         };
            return View(result.ToList());
        }

        public ActionResult SaveApplication(int? id)
        {
            var result = from jobseeker in db.Jobseekers
                         join save in db.Saves on jobseeker.JobseekerId equals save.JobseekerId
                         join job in db.Jobs on save.JobId equals job.JobId
                         where jobseeker.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             JobseekerId = jobseeker.JobseekerId,
                             Name = jobseeker.Name,
                             JobTitle = job.JobTitle,
                             JobDescription = job.JobDescription,




                         };
            return View(result.ToList());
        }
    }
}