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
    public class WorkingExperiencesController : Controller
    {
        private SignUpEntities db = new SignUpEntities();

        // GET: WorkingExperiences
        public ActionResult Index()
        {
            var workingExperiences = db.WorkingExperiences.Include(w => w.Jobseeker);
            return View(workingExperiences.ToList());
        }

        // GET: WorkingExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingExperience workingExperience = db.WorkingExperiences.Find(id);
            if (workingExperience == null)
            {
                return HttpNotFound();
            }
            return View(workingExperience);
        }

        // GET: WorkingExperiences/Create
        public ActionResult Create()
        {
            ViewBag.JobseekerId = new SelectList(db.Jobseekers, "JobseekerId", "Name");
            return View();
        }

        // POST: WorkingExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WEId,JobseekerId,WorkingExp")] WorkingExperience workingExperience)
        {
            if (ModelState.IsValid)
            {
                db.WorkingExperiences.Add(workingExperience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobseekerId = new SelectList(db.Jobseekers, "JobseekerId", "Name", workingExperience.JobseekerId);
            return View(workingExperience);
        }

        // GET: WorkingExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingExperience workingExperience = db.WorkingExperiences.Find(id);
            if (workingExperience == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobseekerId = new SelectList(db.Jobseekers, "JobseekerId", "Name", workingExperience.JobseekerId);
            return View(workingExperience);
        }

        // POST: WorkingExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WEId,JobseekerId,WorkingExp")] WorkingExperience workingExperience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workingExperience).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobseekerId = new SelectList(db.Jobseekers, "JobseekerId", "Name", workingExperience.JobseekerId);
            return View(workingExperience);
        }

        // GET: WorkingExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingExperience workingExperience = db.WorkingExperiences.Find(id);
            if (workingExperience == null)
            {
                return HttpNotFound();
            }
            return View(workingExperience);
        }

        // POST: WorkingExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkingExperience workingExperience = db.WorkingExperiences.Find(id);
            db.WorkingExperiences.Remove(workingExperience);
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
