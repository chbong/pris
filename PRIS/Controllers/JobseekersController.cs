using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRIS.Models;

namespace PRIS.Controllers
{
    public class JobseekersController : Controller
    {
        private SignUpEntities db = new SignUpEntities();

        // GET: Jobseekers
        public ActionResult Index()
        {
            return View(db.Jobseekers.ToList());
        }

        public ActionResult Main(string Name, int id)
        {
           
            if(Session["Email"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.username = Name;
                ViewBag.id = id;
                return View();
            }
            
        }

        public ActionResult Image()
        {
            var item = (from d in db.Jobseekers
                        select d).ToList();
            return View(item);
        }
        

        // GET: Jobseekers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobseeker jobseeker = db.Jobseekers.Find(id);
            if (jobseeker == null)
            {
                return HttpNotFound();
            }
            return View(jobseeker);
        }

        // GET: Jobseekers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobseekers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobseekerId,Name,Email,Password,PhoneNo,Qualification,FieldOfStudy,PreferredLocation,PreferredSalary,PreferredField,Resume,Picture")] Jobseeker jobseeker)
        {
            if (ModelState.IsValid)
            {
                db.Jobseekers.Add(jobseeker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobseeker);
        }

        // GET: Jobseekers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobseeker jobseeker = db.Jobseekers.Find(id);
            if (jobseeker == null)
            {
                return HttpNotFound();
            }
            return View(jobseeker);
        }

        // POST: Jobseekers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobseekerId,Name,Email,Password,PhoneNo,Qualification,FieldOfStudy,PreferredLocation,PreferredSalary,PreferredField,Resume,Picture")] Jobseeker jobseeker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobseeker).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobseeker);
        }

        // GET: Jobseekers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobseeker jobseeker = db.Jobseekers.Find(id);
            if (jobseeker == null)
            {
                return HttpNotFound();
            }
            return View(jobseeker);
        }

        // POST: Jobseekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobseeker jobseeker = db.Jobseekers.Find(id);
            db.Jobseekers.Remove(jobseeker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
