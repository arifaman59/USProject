using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;

namespace Items
{
    [TestFixture]
    public class ToDoClass : BaseClass
    {

        [Test]
        public void AddItem()
        {
            try
            {
                test = report.StartTest("AddItem", "Adding a new item");
                OpenBrowser("Firefox");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                System.Threading.Thread.Sleep(2000);
                Type("ToDoBox", "Aman2");
                PressEnter("ToDoBox");
                //verify element has been added in the list
                ValidateElement("Element1", "Aman2");


            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }
        }


        [Test]
        public void EditExistingItem()
        {

            try
            {
                test = report.StartTest("EditExistingItem", "Modifying existing item");
                OpenBrowser("Chrome");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                Type("ToDoBox", "Aman3");
                PressEnter("ToDoBox");
                //verify element has been added in the list
                ValidateElement("Element1", "Aman3");

                //edit the content of existing item

                DoubleClickAt("Element1");
                Type("Element1", "AmanChanged3");
                PressEnter("Element1");
                ValidateElement("Element1", "AmanChanged3");


            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }



        }


        public void ValidateElement(string locator, string locatorText)
        {
            if (IsElementPresent(locator))
            {
                test.Log(LogStatus.Info, "To do list has an element");
                if (VerifyElementText(locator, locatorText))
                {
                    ReportPass("Element added successfully in the list");
                }
                else
                {
                    ReportFail("ToDo list is empty");
                    Assert.Fail("Element not added");
                }
            }
            else
            {
                ReportFail("ToDo list is empty");
                Assert.Fail("Element not added");
            }
        }



        [TearDown]
        public void CloseAll()
        {
            report.EndTest(test);
            report.Flush();
            CloseBrowser();
        }

    }
}
