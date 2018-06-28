using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentDemoYoutube
{
    [TestFixture]
    public class CreatingSetpLogs
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
        public void StepLogsGeneration()
        {
            test = extent.StartTest("StepLogsGeneration");
            test.Log(LogStatus.Info, "StartTest() method will return the extent test object");
            test.Log(LogStatus.Info, "I am in actual test method");
            test.Log(LogStatus.Info, "We can write the actual test logic in the test"); 
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.EndTest(test);
            test.Log(LogStatus.Info, "EndTest() method will stop capturing the information");
            extent.Flush();
            test.Log(LogStatus.Info, "Flash() method will push the data to report");
            extent.Close();
            test.Log(LogStatus.Info, "Close() method will clear all the resources");
        } 
    }
}
