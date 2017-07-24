using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRIS.Controllers;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void ViewJobseekerDetailsTest()
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
