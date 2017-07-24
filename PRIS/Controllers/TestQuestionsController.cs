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
    public class TestQuestionsController : Controller
    {
        private SignUpEntities db = new SignUpEntities();

        // GET: TestQuestions
        public ActionResult Index()
        {
            return View(db.TestQuestions.ToList());
        }

        public ActionResult List()
        {
            //var tests = db.Tests.Include(t => t.Jobseeker);
            var x = (from u in db.TestQuestions select u.Part).Distinct();
            ViewBag.Part = x;

            var y = (from u in db.TestAnswers where u.TestQuestionID == 1 select new { u.TestQuestionID, u.TestDescription}).ToList();
            ViewBag.Answer = y;

            var result = from u in db.TestQuestions orderby u.Part select u;
            return View(result);
        }

        // GET: TestQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestion testQuestion = db.TestQuestions.Find(id);
            if (testQuestion == null)
            {
                return HttpNotFound();
            }
            return View(testQuestion);
        }

        // GET: TestQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Part,Question")] TestQuestion testQuestion)
        {
            if (ModelState.IsValid)
            {
                db.TestQuestions.Add(testQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testQuestion);
        }

        // GET: TestQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestion testQuestion = db.TestQuestions.Find(id);
            if (testQuestion == null)
            {
                return HttpNotFound();
            }
            return View(testQuestion);
        }

        // POST: TestQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Part,Question")] TestQuestion testQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testQuestion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testQuestion);
        }

        // GET: TestQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestQuestion testQuestion = db.TestQuestions.Find(id);
            if (testQuestion == null)
            {
                return HttpNotFound();
            }
            return View(testQuestion);
        }

        // POST: TestQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestQuestion testQuestion = db.TestQuestions.Find(id);
            db.TestQuestions.Remove(testQuestion);
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
