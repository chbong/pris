using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRIS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIS;
using PRIS.Controllers;
using System.Web.Mvc;

namespace PRIS.Controllers.Tests
{
    [TestClass()]
    public class RealControllerTests
    {
        [TestMethod()]
        public void MainPageTest()
        {
            RealController controller = new RealController();
            ViewResult result = controller.MainPage() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test2()
        {
            RealController controller = new RealController();
            ViewResult result = controller.PostNewJobs(10) as ViewResult;
            Assert.AreEqual(10, result.ViewBag.Id);
        }
        [TestMethod()]
        public void ViewJobseekerDetailsTest()
        {
            RealController controller = new RealController();
            ViewResult result = controller.ViewJobseekerDetails(5) as ViewResult;
            Assert.AreEqual(5, result.ViewBag.Id);
        }

        [TestMethod()]
        public void ViewJobseekerDetailsTest1()
        {
            RealController controller = new RealController();
            ViewResult result = controller.JobseekerMain() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test()
        {
            RealController controller = new RealController();
            ViewResult result = controller.PostNewJobs(5) as ViewResult;
            Assert.AreEqual(5, result.ViewBag.Id);
        }
    }


}