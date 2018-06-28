using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentDemoYoutube
{
    [TestFixture]
    public class CapturarPantalla
    {
        ExtentReports extent;
        ExtentTest test;
        IWebDriver driver;

        [OneTimeSetUp]
        public void Init()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reportes\\ExtentScreenshot.html";
            extent = new ExtentReports(reportPath);
        }

        [Test] 
        public void CaptureScreenShot()
        {
            test = extent.StartTest("CaptureScreenShot");
            driver = new ChromeDriver();
            test.Log(LogStatus.Info, "Ingresa al sitio www.automationtesting.in");
            driver.Navigate().GoToUrl("http://www.automationtesting.in");
            string title = driver.Title;
            test.Log(LogStatus.Info, "Se compara titulo "+title);
            Assert.AreEqual("Home-Selenium Webdriver Appium Complete Tutorial", title);
            test.Log(LogStatus.Pass, "Test Passed");
        }

        [TearDown]
        public void GetResults()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = GetScreenShot.Capture(driver, "ScreenshotName");
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(test);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            driver.Close();
            extent.Flush();
            extent.Close();
        }
    }
}
