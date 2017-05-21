using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using System.Threading;

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

                    //edit the content of existing item

                    DoubleClickAt("Element1");
                    System.Threading.Thread.Sleep(2000);
                    Type("ExistingElement", "23");
                    PressEnter("ExistingElement");

                    if (ValidateElement("Element1", "Aman323"))
                    {
                        test.Log(LogStatus.Info, "Element has been updated");
                        ReportPass("Test case executed successfully");
                    }

                    else
                    {
                        ReportFail("Element could not be updated");
                        Assert.Fail("Element could not be updated");
                    }
                    
                    
                }

                else
                {
                    ReportFail("Error while adding the element");
                    Assert.Fail("Error while adding the element");
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
                if (ValidateElement("Element1", "Aman3"))
                {
                    //edit the content of existing item
                    //completed an item

                    ClickAt("checkBox1");
                    ClickAt("completedTab");

                    if (ValidateElement("Element1", "Aman3"))
                    {
                        TakeScreenShotAttachToReport();
                        test.Log(LogStatus.Info, "Status of the element has been changed to COMPLETED");

                        // deactive the item
                        ClickAt("checkBox1");
                        if (!ValidateElement("Element1", "Aman3"))
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


        [Test]
        public void ClearAllCompletedItems()
        {
            try
            {
                test = report.StartTest("ClearAllCompletedItems", "Clearing all completed items by clicking the link");
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
                        
                    }

                    i++;
                }

                TakeScreenShotAttachToReport();
                test.Log(LogStatus.Info, "All the elements have been added in the list successfully");

                ClickAt("mainCheckbox");
                ClickAt("completedTab");
                TakeScreenShotAttachToReport();

                int j = 0;

                while (j < 4)
                {
                    if (IsElementPresent("Element" + (j + 1)))
                    {
                        test.Log(LogStatus.Info, "Status of the element has been changed to COMPLETED");

                    }

                    else
                    {
                        ReportFail("Element status not changed to COMPLETED");
                        Assert.Fail("Element status not changed to COMPLETED");
                    }

                    j++;
                }

                ClickAt("clearCompletedLink");
                TakeScreenShotAttachToReport();

                int k = 0;
                while (k < 4)
                {
                    if (IsElementPresent("Element" + (k + 1)))
                    {
                        ReportFail("Element status not changed to COMPLETED");
                        Assert.Fail("Element status not changed to COMPLETED");

                    }

                   k++;
                }

                test.Log(LogStatus.Info, "All items have been removed from the list");
                ReportPass("Test case Execution completed successfully");

            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
            }
        }



        [Test]
        public void ClearSingleItem()
        {
            try
            {
                test = report.StartTest("ClearSingleItem", "Clearing a single item from the list");
                OpenBrowser("Chrome");
                NavigateTo("http://todomvc.com");
                ClickAt("AngularJS_link");
                int i = 0;

                while (i < 4)
                {
                    Type("ToDoBox", "Aman" + i);
                    PressEnter("ToDoBox");
                    if (ValidateElement("Element" + (i+1), "Aman" + i))
                    {
                        test.Log(LogStatus.Info, "Element has been added successfully");
                        
                    }

                    i++;
                }

                TakeScreenShotAttachToReport();
                System.Threading.Thread.Sleep(1000);
                test.Log(LogStatus.Info, "All the elements have been added in the list successfully");
                test.Log(LogStatus.Info, "Now trying to delete the first element");

                DeletSingleItem("Element1", "deleteFirstItem");
                TakeScreenShotAttachToReport();

                //Verify the first item is deleted and 2nd item has been moved to item1

                if (!ValidateElement("Element1", "Aman" + 0))
                {
                    ReportPass("Element has been removed from the list");
                }
                else
                {
                    ReportFail("Element has not been deleted from the list");
                    Assert.Fail("Element has not been deleted from the list");
                }


            }
            catch (Exception e)
            {
                ReportFail("Error while executing the test case : " + e.Message);
                Assert.Fail(e.Message);
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
