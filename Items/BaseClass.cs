using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Configuration;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.Drawing.Imaging;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Interactions;

namespace Items
{
    public class BaseClass
    {

        public IWebDriver driver = null;
        public ExtentReports report = ExtentManager.getInstance();
        public ExtentTest test;
        public string srcName = null;

        public void OpenBrowser(string browserName)
        {

            string browser = browserName.ToLower();
            test.Log(LogStatus.Info, "Selected browser : " + browserName);

            if (browser.Equals("firefox"))
            {
                test.Log(LogStatus.Info, "Opening browser");
                driver = new FirefoxDriver();
                test.Log(LogStatus.Info, "Browser opened successfully");

            }
            else if (browser.Equals("chrome"))
            {
                test.Log(LogStatus.Info, "Opening browser");
                driver = new ChromeDriver(FileLocation.DriverDir);
                test.Log(LogStatus.Info, "Browser opened successfully");
            }
            else if (browser.Equals("safari"))
            {
                test.Log(LogStatus.Info, "Opening browser");
                driver = new SafariDriver();
                test.Log(LogStatus.Info, "Browser opened successfully");
            }
            else
            {
                ReportFail("Invalid browser");
            }

            driver.Manage().Window.Maximize();
            test.Log(LogStatus.Info, "Browser maximized");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            test.Log(LogStatus.Info, "Browser configured successfully");

        }

        public void NavigateTo(string url)
        {
            test.Log(LogStatus.Info, "Trying to navigate to :" + url);
            driver.Url = url;
            test.Log(LogStatus.Info, "Navigated to " + url + " successfully");
        }

        public void Type(string locator, string data)
        {
            test.Log(LogStatus.Info, "Trying to type " + data + " on " + locator);
            GetElement(locator).SendKeys(data);
            test.Log(LogStatus.Info, "Typed successfully");
        }

        public void PressEnter(string locator)
        {
            test.Log(LogStatus.Info, "Trying to press Enter button ");
            GetElement(locator).SendKeys(Keys.Enter);
            test.Log(LogStatus.Info, "Pressed Enter button successfully");
        }

        public void ClickAt(string locator)
        {
            test.Log(LogStatus.Info, "Trying to click at " + locator);
            GetElement(locator).Click();
            test.Log(LogStatus.Info, "Clicked successfully");
        }

        public void DoubleClickAt(string locator)
        {
            test.Log(LogStatus.Info, "Trying to Double-click at " + locator);
            Actions act = new Actions(driver);
            act.DoubleClick(GetElement(locator)).Build().Perform();
            test.Log(LogStatus.Info, "Double-clicked successfully");
        }

        public void ClearText(string locator)
        {
            GetElement(locator).SendKeys(Keys.Backspace);
            test.Log(LogStatus.Info, "Existing text has been cleared");
        }
        



        public bool IsElementPresent(string locator)
        {

            IList<IWebElement> ele = null;

            try
            {
                test.Log(LogStatus.Info, " Trying to find " + locator + " on the page");
                ele = driver.FindElements(By.XPath(ConfigurationManager.AppSettings[locator]));

            }
            catch (Exception e)
            {
                ReportFail(e.Message);
            }

            int totalEle = ele.Count;

            if (totalEle == 0)
            {
                return false;
            }

            else
            {
                return true;
            }


        }


        public void TakeScreenShotAttachToReport()
        {
            test.Log(LogStatus.Info, "Trying to take screen shot");
            string fileName = DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "") + ".JPG";
            string dir = FileLocation.ScreenShotDir;
            srcName = dir + fileName;
            Screenshot src = ((ITakesScreenshot)driver).GetScreenshot();
            src.SaveAsFile(srcName, ScreenshotImageFormat.Jpeg);
            test.Log(LogStatus.Info, "Screen shot taken successfully");
            test.Log(LogStatus.Info, "Screenshot is :" + srcName);
            test.Log(LogStatus.Info, "trying to attach screen shot to report");
            test.Log(LogStatus.Info, "Screenshot is attached :" + test.AddScreenCapture(srcName));

        }


        public bool VerifyElementText(string locator, string expectedText)
        {
            test.Log(LogStatus.Info, "trying to verify the element text");
            string eleText = GetElement(locator).Text;
            if (eleText.Equals(expectedText))
            {
                test.Log(LogStatus.Info, "Correct element text");
                return true;
            }
            else
            {
                test.Log(LogStatus.Info, "Incorrect element text");
                return false;
            }
        }

        public IWebElement GetElement(string locator)
        {
            IWebElement ele = null;
            try
            {
                test.Log(LogStatus.Info, "Trying to find the element :" + locator);
                ele = driver.FindElement(By.XPath(ConfigurationManager.AppSettings[locator]));
                test.Log(LogStatus.Info, "Element :" + locator + " is found");
            }
            catch (Exception e)
            {
                ReportFail(e.Message);
            }
            return ele;

        }

      
        public void ReportPass(string msg)
        {
            TakeScreenShotAttachToReport();
            test.Log(LogStatus.Info, msg);
            test.Log(LogStatus.Pass, "Test case executed successfully");
        }

        public void ReportFail(string msg)
        {
            TakeScreenShotAttachToReport();
            test.Log(LogStatus.Info, msg);
            test.Log(LogStatus.Fail, "Test Execution failed");

        }

        public void ReportIgnore(string msg)
        {
            test.Log(LogStatus.Skip, "Test is skipped ");
        }

        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}
