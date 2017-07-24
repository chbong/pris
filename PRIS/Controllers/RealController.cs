using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRIS.Models;
using System.Data.Entity.Validation;
using System.Globalization;

namespace PRIS.Controllers
{
    public class RealController : Controller
    {
        SignUpEntities db = new SignUpEntities();

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult EmployerMainPage()
        {
            return View();
        }

        public ActionResult JobseekerMainPage()
        {
            return View();
        }

        public ActionResult JobseekerMainPage2()
        {
            return View();
        }

        public ActionResult JobseekerMainPage3()
        {
            return View();
        }

        public ActionResult JobseekerMainPage4()
        {
            return View();
        }

        public ActionResult JobseekerMain()
        {
            return View();
        }

        public ActionResult RegisterMain()
        {
            return View();
        }

        public ActionResult JobseekerMain2()
        {
            return View();
        }

        public ActionResult EmployerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployerLogin(Company company)
        {
            var userLoggedIn = db.Companies.SingleOrDefault(x => x.CompanyEmail == company.CompanyEmail && x.Password == company.Password);         
            if (userLoggedIn != null)
            {
                Session["Email"] = company.CompanyEmail;
                var registedUserID = db.Companies.FirstOrDefault(x => x.CompanyEmail == company.CompanyEmail);

                int id = registedUserID.CompanyId;
                //string name = registedUserID.Name;
                return RedirectToAction("CompantDetails", "Real", new { id = id });
            }
            else
            {
                //int lastProductId = db.Companies.Max(item => item.CompanyId);
                //var result = db.Companies.Where(w => w.CompanyId.Equals(lastProductId));
                //return RedirectToAction("B", "Real", new { id = lastProductId });
                ViewBag.Error = "Incorrect Email or Password";
                return View();
            }
        }
        
        public ActionResult JobseekerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JobseekerLogin(Jobseeker jobseeker)
        {
            var userLoggedIn = db.Jobseekers.SingleOrDefault(x => x.Email == jobseeker.Email && x.Password == jobseeker.Password);
            if (userLoggedIn != null)
            {
                Session["Email"] = jobseeker.Email;
                var registedUserID = db.Jobseekers.FirstOrDefault(x => x.Email == jobseeker.Email);

                int id = registedUserID.JobseekerId;
                return RedirectToAction("JobseekerJobSearch", "Real", new { id = id });
            }
            else
            {
                ViewBag.Error = "Incorrect Email or Password";
                return View();
            }
        }

        public ActionResult JobseekerPageTwo()
        {
            var jobseeker = new Jobseeker();
            jobseeker.CreatePhoneNumbers(2);
            jobseeker.CreatePhoneNumbers2(2);
            jobseeker.CreatePhoneNumbers3(2);
            jobseeker.CreatePhoneNumbers4(2);
            return View(jobseeker);
        }

        [HttpPost]
        public ActionResult JobseekerPageTwo(Jobseeker jobseeker, HttpPostedFileBase image1, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    jobseeker.Picture = new byte[file1.InputStream.Length];
                    file1.InputStream.Read(jobseeker.Picture, 0, jobseeker.Picture.Length);                    
                }

                if (image1 != null)
                {
                    jobseeker.Resume = new byte[image1.ContentLength];
                    image1.InputStream.Read(jobseeker.Resume, 0, image1.ContentLength);
                }                           

                foreach (Skill phone in jobseeker.Skills.ToList())
                {
                    if (phone.DeleteSkill == true)
                    {
                        jobseeker.Skills.Remove(phone);
                    }
                }

                foreach (Language phone in jobseeker.Languages.ToList())
                {
                    if (phone.DeleteLanguage == true)
                    {
                        jobseeker.Languages.Remove(phone);
                    }
                }

                foreach (WorkingExperience phone in jobseeker.WorkingExperiences.ToList())
                {
                    if (phone.DeleteWE == true)
                    {
                        jobseeker.WorkingExperiences.Remove(phone);
                    }
                }

                foreach (Qualification phone in jobseeker.Qualifications.ToList())
                {
                    if (phone.DeleteQualification == true)
                    {
                        jobseeker.Qualifications.Remove(phone);
                    }
                }

                db.Jobseekers.Add(jobseeker);
                db.SaveChanges();
            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("InsertTestResult", new { id = lastProductId });
            
        }

        public ActionResult EditA(int id)
        {
            Jobseeker employee = db.Jobseekers.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditA(Jobseeker employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                foreach (var item in employee.Languages)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("List");
            }
            return View(employee);
        }

        public ActionResult EditJobseekerDetail(int id)
        {
            Jobseeker employee = db.Jobseekers.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditJobseekerDetail(Jobseeker employee, int id, HttpPostedFileBase image1, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {

                if (image1 != null && file1 != null)
                {
                    var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;

                    if (result.Count() != 0)
                    {
                        var dbuser = result.First();
                        employee.Resume = new byte[image1.ContentLength];
                        image1.InputStream.Read(employee.Resume, 0, image1.ContentLength);

                        employee.Picture = new byte[file1.ContentLength];
                        file1.InputStream.Read(employee.Picture, 0, file1.ContentLength);

                        dbuser.Resume = employee.Resume;
                        dbuser.Picture = employee.Picture;
                        dbuser.Name = employee.Name;
                        dbuser.Email = employee.Email;
                        dbuser.PhoneNo = employee.PhoneNo;
                        dbuser.Address = employee.Address;
                        dbuser.PreferredLocation = employee.PreferredLocation;
                        dbuser.PreferredField = employee.PreferredField;
                        dbuser.PreferredSalary = employee.PreferredSalary;

                        foreach (var item in employee.Languages)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Qualifications)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Skills)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.WorkingExperiences)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.SaveChanges();

                    }
                }

                else if (image1 != null && file1 == null)
                {
                    var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;

                    if (result.Count() != 0)
                    {
                        var dbuser = result.First();
                        employee.Resume = new byte[image1.ContentLength];
                        image1.InputStream.Read(employee.Resume, 0, image1.ContentLength);                        

                        dbuser.Resume = employee.Resume;
                        dbuser.Name = employee.Name;
                        dbuser.Email = employee.Email;
                        dbuser.PhoneNo = employee.PhoneNo;
                        dbuser.Address = employee.Address;
                        dbuser.PreferredLocation = employee.PreferredLocation;
                        dbuser.PreferredField = employee.PreferredField;
                        dbuser.PreferredSalary = employee.PreferredSalary;

                        foreach (var item in employee.Languages)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Qualifications)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Skills)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.WorkingExperiences)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                    }
                }

                else if (image1 == null && file1 != null)
                {
                    var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;

                    if (result.Count() != 0)
                    {
                        var dbuser = result.First();
                        employee.Picture = new byte[file1.ContentLength];
                        file1.InputStream.Read(employee.Picture, 0, file1.ContentLength);

                        dbuser.Picture = employee.Picture;
                        dbuser.Name = employee.Name;
                        dbuser.Email = employee.Email;
                        dbuser.PhoneNo = employee.PhoneNo;
                        dbuser.Address = employee.Address;
                        dbuser.PreferredLocation = employee.PreferredLocation;
                        dbuser.PreferredField = employee.PreferredField;
                        dbuser.PreferredSalary = employee.PreferredSalary;

                        foreach (var item in employee.Languages)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Qualifications)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Skills)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.WorkingExperiences)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                    }
                }

                else
                {
                    var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;

                    if (result.Count() != 0)
                    {
                        var dbuser = result.First();
                        
                        dbuser.Name = employee.Name;
                        dbuser.Email = employee.Email;
                        dbuser.PhoneNo = employee.PhoneNo;
                        dbuser.Address = employee.Address;
                        dbuser.PreferredLocation = employee.PreferredLocation;
                        dbuser.PreferredField = employee.PreferredField;
                        dbuser.PreferredSalary = employee.PreferredSalary;

                        foreach (var item in employee.Languages)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Qualifications)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.Skills)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }

                        foreach (var item in employee.WorkingExperiences)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("ViewJobseekerDetails", new { id = id });
            }
            return View(employee);
        }

        public ActionResult ViewJobseekerDetails(int id)
        {
            var mail = Session["Email"];
            if (mail == null)
            {
                return RedirectToAction("JobseekerMain2", "Real");
            }

            Jobseeker employee = db.Jobseekers.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /*
        [HttpPost]
        public ActionResult JobseekerPageTwo(Company company)
        {
            var userLoggedIn = db.Companies.SingleOrDefault(x => x.CompanyEmail == company.CompanyEmail && x.Password == company.Password);
            if (userLoggedIn != null)
            {
                return View("A", "Real");
            }
            else
            {
                int lastProductId = db.Companies.Max(item => item.CompanyId);
                var result = db.Companies.Where(w => w.CompanyId.Equals(lastProductId));
                return RedirectToAction("B", "Real", new { id = lastProductId });
            }
        }
        */

        public ActionResult JobseekerSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobseekerSignUp([Bind(Include = "JobseekerId,Name,Email,Password,JobseekerGender,Address,PhoneNo,Qualification,FieldofStudy,PreferredLocation, PreferredSalary, PreferredSkill, Picture, Resume")] Jobseeker jobseeker, string Gender, HttpPostedFileBase image1, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file1 != null)
                {
                    Response.Write("<script>alert('Data inserted successfully')</script>");

                    jobseeker.Picture = new byte[file1.InputStream.Length];
                    file1.InputStream.Read(jobseeker.Picture, 0, jobseeker.Picture.Length);

                    //jobseeker.Picture = new byte[file1.ContentLength];
                    //file1.InputStream.Read(jobseeker.Picture, 0, file1.ContentLength);

                    //jobseeker.Resume = new byte[file1.ContentLength];
                    //file1.InputStream.Read(jobseeker.Picture, 0, file1.ContentLength);
                }

                if (image1 != null)
                {
                    Response.Write("<script>alert('Data inserted successfully')</script>");
                    jobseeker.Resume = new byte[image1.ContentLength];
                    image1.InputStream.Read(jobseeker.Resume, 0, image1.ContentLength);
                }
                db.Jobseekers.Add(jobseeker);
                db.SaveChanges();
                int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
                string selectedGender = jobseeker.JobseekerGender;
/*
                var result = db.Jobseekers.Where(w => w.JobseekerId.Equals(lastProductId));
                foreach(var item in result)
                {
                    
                    item.JobseekerGender = Gender;
                    db.SaveChanges();
                }
*/           
                return RedirectToAction("JobseekerSignUpPageTwo", new { id = lastProductId });
            }        
            return View(jobseeker);            
        }

        public ActionResult InsertTestResult()
        {
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertTestResult([Bind(Include = "TestId, JobseekerId, Question1, Question2, Question3, Question4, Question5")] Test test, int id, int Q1, int Q2, int Q4, int Q5)
        {
         int Part1 = Q1 + Q2 + Q4 + Q5;
            /*            int Part2 = Q6 + Q7 + Q8 + Q9 + Q10 + Q11 + Q12;
                       int Part3 = Q13 + Q14 + Q15 + Q16 + Q17 + Q18 + Q19;
                       int Part4 = Q20 + Q21 + Q22 + Q23 + Q24 + Q25 + Q26 + Q27 + Q28 + Q29 + Q30 + Q31 + Q32 + Q33;
                       ViewBag.P1 = Part1;     */

            if (ModelState.IsValid)
            {
                /* test.Question1 = Q1;        
                 test.Question2 = Q2;
                 test.Question3 = Q3;
                 test.Question4 = Q4;
                 test.Question5 = Q5;
                 test.Question6 = Q6;
                 test.Question7 = Q7;
                 test.Question8 = Q8;
                                test.Question9 = Q9;
                                test.Question10 = Q10;
                                test.Question11 = Q11;
                                test.Question12 = Q12;
                                test.Question13 = Q13;
                                test.Question14 = Q14;
                                test.Question15 = Q15;
                                test.Question16 = Q16;
                                test.Question17 = Q17;
                                test.Question18 = Q18;
                                test.Question19 = Q19;
                                test.Question20 = Q20;
                                test.Question21 = Q21;
                                test.Question22 = Q22;
                                test.Question23 = Q23;
                                test.Question24 = Q24;
                                test.Question25 = Q25;
                                test.Question26 = Q26;
                                test.Question27 = Q27;
                                test.Question28 = Q28;
                                test.Question29 = Q29;
                                test.Question30 = Q30;
                                test.Question31 = Q31;
                                test.Question32 = Q32;
                                test.Question33 = Q33;

                 test.Part2 = Part2;
                 test.Part3 = Part3;
                 test.Part4 = Part4;           
 */
                test.Part = 1;
                test.Total = Part1;
                test.JobseekerId = id;
                db.Tests.Add(test);
                db.SaveChanges();
                int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
                return RedirectToAction("UpdateTestResult", new { id = lastProductId });
            }

            
            return View(test);
        }

        public ActionResult UpdateTestResult()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTestResult([Bind(Include = "TestId,JobseekerId,Question1,Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11")] Test test, int id, int Q6, int Q7, int Q8, int Q9, int Q10, int Q11)
        {
            int Part2 = Q6 + Q7 + Q8 + Q9 + Q10 + Q11;

            if (ModelState.IsValid)
            {
                test.Part = 2;
                test.Total = Part2;
                test.JobseekerId = id;
                db.Tests.Add(test);
                db.SaveChanges();
            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("UpdateTestResult2", new { id = lastProductId });
        }

        public ActionResult UpdateTestResult2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTestResult2([Bind(Include = "TestId,JobseekerId,Question1,Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12, Question13, Question14, Question15, Question16, Question17, Question18, Question19")] Test test, int id, int Q12, int Q13, int Q14, int Q15, int Q16, int Q17, int Q18, int Q19)
        {
            int Part3 = Q12 + Q13 + Q14 + Q15 + Q16 + Q17 + Q18 + Q19;

            if (ModelState.IsValid)
            {
                test.Part = 3;
                test.Total = Part3;
                test.JobseekerId = id;
                db.Tests.Add(test);
                db.SaveChanges();
            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("UpdateTestResult3", new { id = lastProductId });
        }

        public ActionResult UpdateTestResult3()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTestResult3([Bind(Include = "TestId,JobseekerId,Question1,Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12, Question13, Question14, Question15, Question16, Question17, Question18, Question19, Question20, Question21, Question22, Question23, Question24, Question25, Question26, Question27, Question28, Question29, Question30, Question31, Question32, Question33")] Test test, int id, int Q20, int Q21, int Q22, int Q23, int Q24, int Q25, int Q26, int Q27, int Q28, int Q29, int Q30, int Q31, int Q32, int Q33)
        {
            int Part4 = Q20 + Q21 + Q22 + Q23 + Q24 + Q25 + Q26 + Q27 + Q28 + Q29 + Q30 + Q31 + Q32 + Q33;

            if (ModelState.IsValid)
            {
                test.Part = 4;
                test.Total = Part4;
                test.JobseekerId = id;
                db.Tests.Add(test);
                db.SaveChanges();
            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("ViewJobseekerDetails", new { id = lastProductId });
        }


        public ActionResult JobseekerViewTestResult(int? id)
        {
            var email = db.Jobseekers
                .Where(c => c.JobseekerId == id)
                .Select(c => c.Email)
                .SingleOrDefault();

            var mail = Session["Email"].ToString();
            var identity = db.Jobseekers
                .Where(c => c.Email == mail)
                .Select(c => c.JobseekerId)
                .SingleOrDefault();

            if (Session["Email"] == null)
            {
                return RedirectToAction("JobseekerMain2", "Real");
            }
            if (Session["Email"].ToString() != email)
            {
                ViewBag.Error = "You can only view your own test result";
                var result2 = from u in db.Tests
                             where (u.JobseekerId == identity)
                             select new SignUpViewModel()
                             {
                                 JobseekerId = u.JobseekerId,
                                 Part = u.Part,
                                 Total = u.Total,
                             };
                return View(result2);
            }

            //var result = from u in db.Tests where (u.JobseekerId == id) select u;
            //var result2 = result.First();
            var result = from u in db.Tests
                         where (u.JobseekerId == id)
                         select new SignUpViewModel()
                         {
                             JobseekerId = u.JobseekerId,
                             Part = u.Part,
                             Total = u.Total,
                         };
            ViewBag.Id = id;
            var name = db.Jobseekers
                .Where(c => c.JobseekerId == id)
                .Select(c => c.Name)
                .SingleOrDefault();
            ViewBag.Name = name;
            return View(result);
        }

        public ActionResult TestResult(int? id)
        {
            var result = from u in db.Tests where (u.JobseekerId == id) select u;
            var result2 = result.First();
            return View(result2);
        }

        public ActionResult EmployerViewTestResult(int? id, int cid)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }
            var result = from u in db.Tests
                         where (u.JobseekerId == id)
                         select new SignUpViewModel()
                         {
                             JobseekerId = u.JobseekerId,
                             Part = u.Part,
                             Total = u.Total,
                         };
            
            ViewBag.Cid = cid;
            var name = db.Companies
               .Where(c => c.CompanyId == cid)
               .Select(c => c.CompanyName)
               .SingleOrDefault();
            ViewBag.Name = name;
            return View(result);
        }

        public ActionResult JobseekerSignUpPageTwo(int id)
        {
            ViewBag.Id = id;
            var lang = new SignUpViewModel();
            lang.Languages = new List<Language>()
            {new Language() { LanguageId = 0, JobseekerId = 0, LanguageLearned = ""}
            };
            return View(lang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobseekerSignUpPageTwo(SignUpViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;
                if(result.Count()!=0)
                {
                    var dbuser = result.First();
                    dbuser.Qualification = model.Qualification;
                    dbuser.FieldOfStudy = model.FieldOfStudy;
                    db.SaveChanges();
                }

                WorkingExperience we = new WorkingExperience();
                we.JobseekerId = id;
                we.WorkingExp = model.WorkingExp;

                db.WorkingExperiences.Add(we);
                db.SaveChanges();

                Skill skill = new Skill();
                skill.JobseekerId = id;
                skill.SkillAcquired = model.SkillAcquired;

                db.Skills.Add(skill);
                db.SaveChanges();

                var lang = new SignUpViewModel();
                lang.Languages = new List<Language>();

                foreach (var i in model.Languages)
                {
                    ViewBag.Message = "Data successfully saved!";
                    db.Languages.Add(i);
                }
                db.SaveChanges();
                ModelState.Clear();
                model.Languages = new List<Language> { new Language { LanguageId = 0, JobseekerId = 0, LanguageLearned = "" } };

            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("JobseekerSignUpPageThree", new { id = lastProductId });
        }       

        public ActionResult JobseekerSignUpPageThree()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobseekerSignUpPageThree(Jobseeker js, int id, string Location)
        {
            var result = from u in db.Jobseekers where (u.JobseekerId == id) select u;
            if (result.Count() != 0)
            {
                var dbuser = result.First();
                dbuser.PreferredField = js.PreferredField;
                dbuser.PreferredLocation = Location;
                dbuser.PreferredSalary = js.PreferredSalary;
                db.SaveChanges();
            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("InsertTestResult", new { id = lastProductId });
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterMain([Bind(Include = "JobseekerId,Name,Email,Password,PhoneNo,Qualification,FieldofStudy,PreferredLocation, PreferredSalary, PreferredSkill, Picture, Resume")] Jobseeker jobseeker)
        {
            if (ModelState.IsValid)
            {
                db.Jobseekers.Add(jobseeker);
                db.SaveChanges();
                int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
                return RedirectToAction("RegisterPageTwo", new { id = lastProductId });
            }
            
            return View(jobseeker);
        }

        public ActionResult RegisterPageTwo(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPageTwo([Bind(Include = "JobseekerId,Name,Email,Password,PhoneNo,Qualification,FieldofStudy,PreferredLocation, PreferredSalary, PreferredSkill, Picture, Resume")] Jobseeker jobseeker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobseeker).State = EntityState.Modified;
                db.SaveChanges();
                int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
                return RedirectToAction("RegisterPageThree", new { id = lastProductId }); 
            }
            return View(jobseeker);
        }

        public ActionResult RegisterPageThree(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPageThree(SignUpViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Language language = new Language();
                language.JobseekerId = id;
                language.LanguageLearned = model.LanguageLearned;

                db.Languages.Add(language);
                db.SaveChanges();

                WorkingExperience we = new WorkingExperience();
                we.JobseekerId = id;
                we.WorkingExp = model.WorkingExp;

                db.WorkingExperiences.Add(we);
                db.SaveChanges();

                Skill skill = new Skill();
                skill.JobseekerId = id;
                skill.SkillAcquired = model.SkillAcquired;

                db.Skills.Add(skill);
                db.SaveChanges();

            }
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            return RedirectToAction("JobseekerDetails", new { id = lastProductId });
        }
        */

        public ActionResult JobseekerDetails(int? id)
        {
            /*
            var result = from u in db.Tests where (u.JobseekerId == id) select u;
            var result2 = result.First();
            return View(result2);

            IQueryable<Language> languages = from language in db.Languages
                                             where language.JobseekerId == id
                                             select language;
            Language[] lArray = languages.ToArray();
            */

            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             Password = name.Password,
                             PhoneNo = name.PhoneNo,
                             Qualification = name.Qualification,
                             FieldOfStudy = name.FieldOfStudy,
                             LanguageLearned = language.LanguageLearned,
                             SkillAcquired = skill.SkillAcquired,
                             Name = name.Name,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,
                             Picture = name.Picture,
                             Resume = name.Resume,


                         };


            var a1 = result.First();

            return View(result);
        }

        public ActionResult JobseekerViewJobDetails(int id, int jid)
        {
            ViewBag.Id = id;
            ViewBag.Jid = jid;

            Job job = db.Jobs.Find(jid);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult JobseekerViewJobDetailFromApplication(int id, int jid)
        {
            ViewBag.Id = id;
            ViewBag.Jid = jid;

            Job job = db.Jobs.Find(jid);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult JobseekerViewJobDetailFromSave(int id, int jid)
        {
            ViewBag.Id = id;
            ViewBag.Jid = jid;

            Job job = db.Jobs.Find(jid);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult EmployerViewJobDetails(int id, int cid)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }

            ViewBag.Id = id;
            ViewBag.Cid = cid;

            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        public FilePathResult DownloadExampleFiles(string fileName)
        {
            return new FilePathResult(string.Format(@"~\Files\{0}", fileName + ".txt"), "text/plain");
        }

        public ActionResult JobSearch(string searchString, int id)
        {
            var preferredLocation = (from j in db.Jobseekers
                                    where j.JobseekerId == id
                                    select j.PreferredLocation).Single();
                                    
            var movies = from m in db.Jobs
                         orderby m.JobId descending
                         where m.Location == preferredLocation 
                         select m;        
                
            ViewBag.Id = id;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.JobDescription.Contains(searchString) ||
                                      s.JobTitle.Contains(searchString))
                                      .OrderByDescending(x => x.JobId);
            }

            return View(movies);
        }

        public ActionResult JobseekerJobSearch(string searchString, int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("JobseekerMain2", "Real");
            }

            var response  = db.Tests.Where(y => y.JobseekerId.Equals(id)).FirstOrDefault();
            if (response == null)
            {
                return RedirectToAction("InsertTestResult", new { id = id });
            }

            var preferredLocation = (from j in db.Jobseekers
                                     where j.JobseekerId == id
                                     select j.PreferredLocation).Single();
            

            var preferredSalary = (from j in db.Jobseekers
                                     where j.JobseekerId == id
                                     select j.PreferredSalary).Single();
            

            var movies = from m in db.Jobs
                         orderby m.JobId ascending
                         where m.Location == preferredLocation && m.Salary >= preferredSalary 
                         select m;

            ViewBag.Id = id;

            var name = db.Jobseekers
                .Where(c => c.JobseekerId == id)
                .Select(c => c.Name)
                .SingleOrDefault();
            ViewBag.Name = name;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = from m in db.Jobs
                         orderby m.PostingDate descending
                         where m.JobDescription.Contains(searchString) || m.JobTitle.Contains(searchString) || m.Location.Contains(searchString) || m.JobRequirement.Contains(searchString)
                         select m;
            }
            else
            {
                movies = from m in db.Jobs
                         orderby m.JobId ascending
                         where m.Location == preferredLocation && m.Salary >= preferredSalary
                         select m;
            }

            return View(movies);
        }

        public ActionResult Sort(int id, string element)
        {
            if(element == "salary")
            {
                var preferredLocation = (from j in db.Jobseekers
                                         where j.JobseekerId == id
                                         select j.PreferredLocation).Single();

                var movies = from m in db.Jobs
                             orderby m.JobId descending
                             where m.Location == preferredLocation
                             select m;

                ViewBag.Id = id;



                return View(movies);
            }
            return View();
            
        }

        public ActionResult searchJobseekers(string searchString, int id)
        {
            ViewBag.Id = id;           

            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;        

            var movies = from m in db.Jobseekers
                         select m;

            var res = from jobseeker in db.Jobseekers
                      join test in db.Tests on jobseeker.JobseekerId equals test.JobseekerId
                      select new SignUpViewModel()
                      {
                          JobseekerId = jobseeker.JobseekerId,
                          Name = jobseeker.Name,

                      };

            var result = from job in db.Jobs
                         where job.CompanyId == id
                         select new { job.JobTitle, job.Test };

            var x = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle, test.Total
                    where job.CompanyId == id
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                    };            
            
            if (!String.IsNullOrEmpty(searchString))
            {
                x = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle, test.Total
                    where job.CompanyId == id && m.Name.Contains(searchString) || job.CompanyId == id && m.PreferredLocation.Contains(searchString) 
                         select new SignUpViewModel()
                         {
                             Name = m.Name,
                             Email = m.Email,
                             JobTitle = job.JobTitle,
                             PhoneNo = m.PhoneNo,
                             Address = m.Address,
                             PreferredField = m.PreferredField,
                             PreferredLocation = m.PreferredLocation,
                             PreferredSalary = m.PreferredSalary,
                             Resume = m.Resume,
                             JobseekerId = m.JobseekerId,
                         };
            }

            return View(x);
        }


        public ActionResult searchJobseekers2(string searchString, int id)
        {
            ViewBag.Id = id;

            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;


            

            var x = from m in db.Jobs
                    where m.CompanyId == id
                    orderby m.JobTitle
                    select new SignUpViewModel
                    {
                        JobTitle = m.JobTitle,
                        Test = m.Test,
                    };

            var z = from m in db.Jobs
                    where m.CompanyId == id
                    orderby m.JobTitle
                    select m.JobTitle;

            ViewBag.Try2 = x;
            ViewBag.Try = z;


            var e = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle ascending, test.Total descending, (from m in db.Jobseekers
                                                                            join test in db.Tests on m.JobseekerId equals test.JobseekerId
                                                                            join job in db.Jobs on test.Part equals job.Test
                                                                            where job.CompanyId == id
                                                                            select new { }
                                                                            ) descending
                    where job.CompanyId == id
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                        Total = test.Total
                    };
            ViewBag.Test3 = e;

            var y = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test 
                    orderby job.JobTitle ascending, test.Total descending
                    where job.CompanyId == id
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                        Total = test.Total
                    };            


            return View(y);
        }

        public ActionResult ViewJobseeker(string searchString, int id)
        {
            ViewBag.Id = id;

            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;



            var x = from m in db.Jobs
                    where m.CompanyId == id
                    orderby m.JobTitle
                    select new SignUpViewModel
                    {
                        JobTitle = m.JobTitle,
                        Test = m.Test,
                    };

            var z = from m in db.Jobs
                    where m.CompanyId == id
                    orderby m.JobTitle
                    select m.JobTitle;

            ViewBag.Try2 = x;
            ViewBag.Try = z;


            var e = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle ascending, test.Total descending, (from m in db.Jobseekers
                                                                            join test in db.Tests on m.JobseekerId equals test.JobseekerId
                                                                            join job in db.Jobs on test.Part equals job.Test
                                                                            where job.CompanyId == id
                                                                            select new { }
                                                                            ) descending
                    where job.CompanyId == id
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                        Total = test.Total
                    };
            ViewBag.Test3 = e;

            var y = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle ascending, test.Total descending
                    where job.CompanyId == id
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                        Total = test.Total
                    };


            return View(y);
        }

        public ActionResult InsertApplication(int id, int jid)
        {           
            Application application = new Application();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);

             application.JobId = id;
             application.JobseekerId = jid;
             application.ApplicationDate = DateTime.Now;
             db.Applications.Add(application);
             db.SaveChanges();
                   
            return RedirectToAction("ViewApplication", new { id = jid });

        }

        public ActionResult InsertApplicationFromSave(SignUpViewModel model, int id, int jid)
        {
            Application application = new Application();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            application.JobId = id;
            application.JobseekerId = jid;
            application.ApplicationDate = DateTime.Now;
            db.Applications.Add(application);
            db.SaveChanges();
            return RedirectToAction("ViewSavePost", new { id = jid });

        }

        public ActionResult SavePost(SignUpViewModel model, int id, int jid)
        {
            Save save = new Save();
            int lastProductId = db.Jobseekers.Max(item => item.JobseekerId);
            save.JobId = id;
            save.JobseekerId = jid;
            db.Saves.Add(save);
            db.SaveChanges();
            return RedirectToAction("JobseekerJobSearch", new { id = jid });

        }

        public ActionResult ViewApplication(int? id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("JobseekerMain2", "Real");
            }
            ViewBag.Id = id;
            var name = db.Jobseekers
                .Where(c => c.JobseekerId == id)
                .Select(c => c.Name)
                .SingleOrDefault();
            ViewBag.Name = name;
            var result = (from jobseeker in db.Jobseekers
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
                             PostingDate = job.PostingDate,
                             ClosingDate = job.ClosingDate,
                             Location = job.Location,
                             Salary = job.Salary,
                             ShortDescription = job.ShortDescription,
                             CompanyLogo = job.Company.CompanyLogo,
                             JobId = job.JobId,

                         }).ToList();
            return View(result);
        }

        public ActionResult ViewSavePost(int? id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("JobseekerMain2", "Real");
            }
            ViewBag.Id = id;
            var name = db.Jobseekers
                .Where(c => c.JobseekerId == id)
                .Select(c => c.Name)
                .SingleOrDefault();
            ViewBag.Name = name;
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
                             PostingDate = job.PostingDate,
                             ClosingDate = job.ClosingDate,
                             Location = job.Location,
                             Salary = job.Salary,
                             ShortDescription = job.ShortDescription,
                             CompanyLogo = job.Company.CompanyLogo,
                             JobId = job.JobId,

                         };
            return View(result.ToList());
        }

        public ActionResult EmployerSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerSignUp([Bind(Include = "CompanyId,CompanyName,CompanyEmail,Password,CompanyPhoneNo,CompanyAddress,CompanyWebsite,CompanyInformation, CompanyLogo, CompanyBenefits")] Company company, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    company.CompanyLogo = new byte[image1.ContentLength];
                    image1.InputStream.Read(company.CompanyLogo, 0, image1.ContentLength);
                }

                db.Companies.Add(company);
                db.SaveChanges();
                ViewBag.Name = company.CompanyName;
                int lastProductId = db.Companies.Max(item => item.CompanyId);            
                return RedirectToAction("CompantDetails", new { id = lastProductId });
            }
            return View(company);
        }

        /*
        public ActionResult SaveEditCompany(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEditCompany(Company company, int id)
        {
            if (ModelState.IsValid)
            {
                var result = from u in db.Companies where (u.CompanyId == id) select u;
                if (result.Count() != 0)
                {

                    var dbuser = result.First();
                    dbuser.CompanyName = company.CompanyName;
                    dbuser.Password = company.Password;
                    dbuser.CompanyInformation = company.CompanyInformation;
                    dbuser.CompanyBenefits = company.CompanyBenefits;
                    db.SaveChanges();
                }

                int lastProductId = db.Companies.Max(item => item.CompanyId);
                return RedirectToAction("CompantDetails", new { id = lastProductId });
            }
            return View(company);
        }


        public ActionResult CompanyMain()
        {
            return View();
        }

        public ActionResult EmployerRegisterMain()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerRegisterMain([Bind(Include = "CompanyId,CompanyName,Email,Password,PhoneNo,Address,CompanyWebsite,CompanyInformation")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                int lastProductId = db.Companies.Max(item => item.CompanyId);
                return RedirectToAction("CompantDetails", new { id = lastProductId });
            }

            return View(company);
        }

        public ActionResult CompantDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;
            return View(company);
        }

        public ActionResult EditCompanyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company job = db.Companies.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompanyDetails([Bind(Include = "CompanyId,CompanyName,CompanyEmail,Password,CompanyPhoneNo,CompanyAddress,CompanyWebsite,CompanyInformation, CompanyLogo, CompanyBenefits")] Company job, int id, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    var result = from u in db.Companies where (u.CompanyId == id) select u;

                    if(result.Count() !=0)
                    {
                        var dbuser = result.First();
                        job.CompanyLogo = new byte[image1.ContentLength];
                        image1.InputStream.Read(job.CompanyLogo, 0, image1.ContentLength);

                        dbuser.CompanyLogo = job.CompanyLogo;
                        dbuser.CompanyName = job.CompanyName;
                        dbuser.CompanyEmail = job.CompanyEmail;
                        dbuser.Password = job.Password;
                        dbuser.CompanyPhoneNo = job.CompanyPhoneNo;
                        dbuser.CompanyAddress = job.CompanyAddress;
                        dbuser.CompanyWebsite = job.CompanyWebsite;
                        dbuser.CompanyInformation = job.CompanyInformation;
                        dbuser.CompanyBenefits = job.CompanyBenefits;
                        db.SaveChanges();

                    }
                    
                    int lastProductId = id;
                    return RedirectToAction("CompantDetails", new { id = id });
                }
                else
                {
                    var result = from u in db.Companies where (u.CompanyId == id) select u;

                    if (result.Count() != 0)
                    {
                        var dbuser = result.First();
                        
                        dbuser.CompanyName = job.CompanyName;
                        dbuser.CompanyEmail = job.CompanyEmail;
                        dbuser.Password = job.Password;
                        dbuser.CompanyPhoneNo = job.CompanyPhoneNo;
                        dbuser.CompanyAddress = job.CompanyAddress;
                        dbuser.CompanyWebsite = job.CompanyWebsite;
                        dbuser.CompanyInformation = job.CompanyInformation;
                        dbuser.CompanyBenefits = job.CompanyBenefits;
                        db.SaveChanges();

                    }

                    int lastProductId = id;
                    return RedirectToAction("CompantDetails", new { id = lastProductId });
                }
            }
            return View();
        }

        public ActionResult PostNewJobs(int id)
        {
            ViewBag.Id = id;
            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostNewJobs([Bind(Include = "JobId,CompanyId,JobTitle,Salary,JobRequirement,JobDescription,Location,ShortDescription,PostingDate,ExpiredDate,Test")] Job job, int id, string test)
        {
            if (ModelState.IsValid)
            {
                job.CompanyId = id;
                job.PostingDate = DateTime.Now;
                ViewBag.A = test;
                if (test == "Experience")
                    job.Test = 1;
                else if (test == "Dependent")
                    job.Test = 2;
                else if (test == "Sociable")
                    job.Test = 3;
                else if (test == "High Awareness of Emotions")
                    job.Test = 4;
                db.Jobs.Add(job);
                db.SaveChanges();
                ViewBag.Id = id;
                return RedirectToAction("PostNewJobs", new { id = id });
            }
            return View();
        }


        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostNewJobs([Bind(Include = "JobId,CompanyId,JobTitle,Salary,JobRequirement,JobDescription,Location,ShortDescription,PostingDate,ExpiredDate")] Job job, int id)
        {
            //var a = datetimepicker.ToString();

            if (ModelState.IsValid)
            {
                job.CompanyId = id;
                job.PostingDate = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();
                int lastProductId = id;
                return RedirectToAction("PostNewJobs", new { id = lastProductId });
            }
            return View();
        }
        */

        public ActionResult DisplayAllJobs(int? id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }
            ViewBag.Id = id;
            var name = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = name;
            var jobs = db.Jobs.Include(j => j.Company).OrderByDescending(f => f.PostingDate).Where(f=>f.CompanyId==id);
            return View(jobs.ToList());
        }
        
         public ActionResult DisplayAppliedJobseeker(int? id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }
            ViewBag.Id = id;
            var n = db.Companies
                .Where(c => c.CompanyId == id)
                .Select(c => c.CompanyName)
                .SingleOrDefault();
            ViewBag.Name = n;

            var x = from m in db.Jobseekers
                    join test in db.Tests on m.JobseekerId equals test.JobseekerId
                    join job in db.Jobs on test.Part equals job.Test
                    orderby job.JobTitle, test.Total
                    select new SignUpViewModel()
                    {
                        Name = m.Name,
                        Email = m.Email,
                        JobTitle = job.JobTitle,
                        PhoneNo = m.PhoneNo,
                        Address = m.Address,
                        PreferredField = m.PreferredField,
                        PreferredLocation = m.PreferredLocation,
                        PreferredSalary = m.PreferredSalary,
                        Resume = m.Resume,
                        JobseekerId = m.JobseekerId,
                    };

            var result = from name in db.Jobseekers
                         join application in db.Applications on name.JobseekerId equals application.JobseekerId
                         join job in db.Jobs on application.JobId equals job.JobId
                         where application.Job.CompanyId == id
                         orderby application.Job.JobTitle, application.ApplicationDate
                         select new SignUpViewModel()
                         {
                             Password = name.Password,
                             PhoneNo = name.PhoneNo,
                             Qualification = name.Qualification,
                             FieldOfStudy = name.FieldOfStudy,
                             Name = name.Name,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,
                             Picture = name.Picture,
                             Resume = name.Resume,
                             JobTitle = job.JobTitle,
                             JobDescription = job.JobDescription,
                             ApplicationDate = application.ApplicationDate,
                             PreferredLocation = name.PreferredLocation,
                             PreferredField = name.PreferredField,
                             PreferredSalary = name.PreferredSalary,

                         };

            /* Original Display
            var result = from name in db.Jobseekers
                         join application in db.Applications on name.JobseekerId equals application.JobseekerId
                         join job in db.Jobs on application.JobId equals job.JobId
                         where application.Job.CompanyId == id 
                         orderby application.Job.JobTitle
                         select new SignUpViewModel()
                         {
                             Password = name.Password,
                             PhoneNo = name.PhoneNo,
                             Qualification = name.Qualification,
                             FieldOfStudy = name.FieldOfStudy,
                             Name = name.Name,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,
                             Picture = name.Picture,
                             Resume = name.Resume,
                             JobTitle = job.JobTitle,
                             JobDescription = job.JobDescription,
                             ApplicationDate = application.ApplicationDate,
                             PreferredLocation = name.PreferredLocation,
                             PreferredField = name.PreferredField,
                             PreferredSalary = name.PreferredSalary,

                         };
            */

            return View(result.ToList());
        }

        public ActionResult JobDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult EditJobPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult EditJobPost2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost2([Bind(Include = "JobId,CompanyId,JobTitle,Salary,Location,JobRequirement,JobDescription,ShortDescription,PostingDate")] Job job, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                int lastProductId = job.CompanyId;
                return RedirectToAction("DisplayAllJobs", new { id = lastProductId });
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost([Bind(Include = "JobId,CompanyId,JobTitle,Salary,Location,JobRequirement,JobDescription,ShortDescription,PostingDate")] Job job, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                int lastProductId = job.CompanyId;
                return RedirectToAction("DisplayAllJobs", new { id = lastProductId });
            }
            return View(job);
        }

        public ActionResult DeleteJobPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJobPost(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            int lastProductId = job.CompanyId;
            return RedirectToAction("DisplayAllJobs", new { id = lastProductId });
        }

        public ActionResult CompanyViewJobseekerDetails(int? id)
        {
            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select new SignUpViewModel()
                         {
                             Password = name.Password,
                             PhoneNo = name.PhoneNo,
                             Qualification = name.Qualification,
                             FieldOfStudy = name.FieldOfStudy,
                             LanguageLearned = language.LanguageLearned,
                             Name = name.Name,
                             SkillAcquired = skill.SkillAcquired,
                             WorkingExp = exp.WorkingExp,
                             JobseekerId = name.JobseekerId,
                             Email = name.Email,


                         };
            return View(result.ToList());
        }

        public ActionResult EmployerViewJobseekerDetail(int? id, int cid)
        {
            /*
            var result = from u in db.Tests where (u.JobseekerId == id) select u;
            var result2 = result.First();
            return View(result2);

            IQueryable<Language> languages = from language in db.Languages
                                             where language.JobseekerId == id
                                             select language;
            Language[] lArray = languages.ToArray();
            */
            ViewBag.Id = cid;

            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }

            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select name;


            var a1 = result.First();

            return View(a1);
        }

        public ActionResult EmployerViewJobseekerDetailsFromApplied(int? id, int cid)
        {

            ViewBag.Id = cid;
            if (Session["Email"] == null)
            {
                return RedirectToAction("EmployerMainPage", "Real");
            }

            var result = from language in db.Languages
                         join name in db.Jobseekers on language.JobseekerId equals name.JobseekerId
                         join skill in db.Skills on name.JobseekerId equals skill.JobseekerId
                         join exp in db.WorkingExperiences on skill.JobseekerId equals exp.JobseekerId
                         where name.JobseekerId == id
                         select name;


            var a1 = result.First();

            return View(a1);
        }

        public ActionResult EditJobPost3(int? id, int cid)
        {
            ViewBag.Cid = cid;
            ViewBag.Id = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost3([Bind(Include = "JobId,CompanyId,JobTitle,Salary,Location,JobRequirement,JobDescription,ShortDescription,PostingDate,ExpiredDate,ClosingDate")] Job job, int? id, int cid, string test)
        {
            if (ModelState.IsValid)
            {
                //ViewBag.t = test;                 

                //db.Entry(job).State = EntityState.Modified;
                //db.SaveChanges();

                var result = from u in db.Jobs where (u.JobId == id) select u;           

                if (result.Count() != 0)
                {
                    var dbuser = result.First();
                    if (test == "1")
                    {
                        dbuser.Test = 1;
                        db.SaveChanges();
                    }
                    else if (test == "2")
                    {
                        dbuser.Test = 2;
                        db.SaveChanges();
                    }
                    else if (test == "3")
                    {
                        dbuser.Test = 3;
                        db.SaveChanges();
                    }
                    else if (test == "4")
                    {
                        dbuser.Test = 4;
                        db.SaveChanges();
                    }
                    else
                    {
                        dbuser.Test = 5;
                        db.SaveChanges();
                    }

                    dbuser.JobTitle = job.JobTitle;
                    dbuser.JobRequirement = job.JobRequirement;
                    dbuser.JobDescription = job.JobDescription;
                    dbuser.Location = job.Location;
                    dbuser.ShortDescription = job.ShortDescription;
                    dbuser.Salary = job.Salary;
                    db.SaveChanges();

                }

                //var result = (from u in db.Jobs where (u.JobId == id) select new { u.Test}).Single();

                //job.Test = test;
                //db.SaveChanges();

                int lastProductId = job.CompanyId;
                return RedirectToAction("DisplayAllJobs", new { id = lastProductId });
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult RegisterMainA(SignUpViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //    var file = Request.Files[0];
                //                byte[] uploadedFile = new byte[model.Picture.InputStream.Length];


                Jobseeker jobseeker = new Jobseeker();
                jobseeker.Name = model.Name;
                jobseeker.Email = model.Email;
                jobseeker.Password = model.Password;
                jobseeker.PhoneNo = model.PhoneNo;
               
                //           model.Picture.InputStream.Read(uploadedFile,0,uploadedFile.Length);

                db.Jobseekers.Add(jobseeker);
                db.SaveChanges();

               
                return RedirectToAction("Index", "Jobseekers");
            }
            return View(model);

        }
    }
}
