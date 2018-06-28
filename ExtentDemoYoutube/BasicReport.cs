using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework;

namespace ExtentDemoYoutube
{
    [TestFixture]
    public class BasicReport
    {
        public ExtentReports extent;
        public ExtentTest test;

        [OneTimeSetUp]
        public void StartReport()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            string reportPath = projectPath + "Reportes\\MyOwnReport.html";
            extent = new ExtentReports(reportPath, true);

            extent.AddSystemInfo("Host Name", "Alex")
                .AddSystemInfo("Enviroment", "QA")
                .AddSystemInfo("User Name", "Alex Moreno");

            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        [Test]
        public void DemoReportPass()
        {
            test = extent.StartTest("DemoReportPass");
            Assert.IsTrue(true);
            test.Log(LogStatus.Pass, "Assert Pass as condition is True");
        }

        [Test]
        public void DemoReportFail()
        {
            test = extent.StartTest("DemoReportFail");
            Assert.IsTrue(false);
            test.Log(LogStatus.Pass, "Assert Pass as condition is Fail");
        }

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var StackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace+ "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Log(LogStatus.Fail, StackTrace + errorMessage);
            }
            extent.EndTest(test);
        }
        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
            extent.Close(); 
        }
    }
}
