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
                Type("ToDoBox", "Aman2");
                PressEnter("ToDoBox");
                //verify element has been added in the list
                if (ValidateElement("Element1", "Aman2"))
                {
                    ReportPass("Test case executed successfully");
                }


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
            
                if (ValidateElement("Element1", "Aman3"))
                {
                    test.Log(LogStatus.Info, "Element added successfully");
                }
                //edit the content of existing item

                DoubleClickAt("Element1");
                System.Threading.Thread.Sleep(2000);
                Type("ExistingElement", "23");
                PressEnter("ExistingElement");
               
                if (ValidateElement("Element1", "Aman323"))
                {
                    ReportPass("Test case executed successfully");
                }

            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }
            
        }


        [Test]
        public void CompleteAndReactiveItems()
        {
            try
            {
                test = report.StartTest("CompleteReactivateItem", "Check and uncheck existing item");
                OpenBrowser("Chrome");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                Type("ToDoBox", "Aman3");
                PressEnter("ToDoBox");

                //verify element has been added in the list
                ValidateElement("Element1", "Aman3");

                //edit the content of existing item
                //completed an item

                ClickAt("checkBox1");
                ClickAt("completedTab");

                if (IsElementPresent("Element1"))
                {
                    TakeScreenShotAttachToReport();
                    test.Log(LogStatus.Info, "Status of the element has been changed to COMPLETED");

                    // deactive the item
                    ClickAt("checkBox1");
                    if (!IsElementPresent("Element1"))
                    {
                        test.Log(LogStatus.Info, "Element status changed from COMPLETED");
                        ReportPass("Element status changed from COMPLETED");
                    }
                    else
                    {
                        ReportFail("Element status not changed from COMPLETED");
                        Assert.Fail("Element status not changed from COMPLETED");
                    }
                }

                else
                {
                    ReportFail("Element status not changed to COMPLETED");
                    Assert.Fail("Element status not changed to COMPLETED");
                }
                

              


            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }



        }


        [Test]
        public void AddMultipleItems()
        {
            try
            {
                test = report.StartTest("AddMultipleItems", "Adding multiple items on the list");
                OpenBrowser("Chrome");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                int i = 0;

                while (i < 2)
                {
                    Type("ToDoBox", "Aman" + i);
                    PressEnter("ToDoBox");
                    if (ValidateElement("Element1", "Aman" + i))
                    {
                        test.Log(LogStatus.Info, "Element has been added successfully");
                        TakeScreenShotAttachToReport();
                    }

                    i++;
                }

                ReportPass("Both the element has been added successfully");

         

            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }
        }


        [Test]
        public void MarkAllCompleted()
        {
            try
            {
                test = report.StartTest("MarkAllCompleted", "Marking all item as completed at a time");
                OpenBrowser("Chrome");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                int i = 0;

                while (i < 4)
                {
                    Type("ToDoBox", "Aman" + i);
                    PressEnter("ToDoBox");
                    if (ValidateElement("Element1", "Aman" + i))
                    {
                        test.Log(LogStatus.Info, "Element has been added successfully");
                        TakeScreenShotAttachToReport();
                    }

                    i++;
                }

                test.Log(LogStatus.Info, "All the elements have been added in the list successfully");

                ClickAt("mainCheckbox");

                ClickAt("completedTab");

                int j = 0;

                while (j < 4)
                {
                    if (IsElementPresent("Element" + (j + 1)))
                    {
                        TakeScreenShotAttachToReport();
                        test.Log(LogStatus.Info, "Status of the element has been changed to COMPLETED");
                                               
                    }

                    else
                    {
                        ReportFail("Element status not changed to COMPLETED");
                        Assert.Fail("Element status not changed to COMPLETED");
                    }

                    j++;
                }

                ReportPass("Test case Execution completed successfully");

            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }
        }


        public bool ValidateElement(string locator, string locatorText)
        {
            if (IsElementPresent(locator))
            {
                test.Log(LogStatus.Info, "To do list has an element");
                if (VerifyElementText(locator, locatorText))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
