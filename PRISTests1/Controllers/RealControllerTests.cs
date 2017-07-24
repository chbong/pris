using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRIS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIS;
using System.Web.Mvc;

namespace PRIS.Controllers.Tests
{
    [TestClass()]
    public class RealControllerTests
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