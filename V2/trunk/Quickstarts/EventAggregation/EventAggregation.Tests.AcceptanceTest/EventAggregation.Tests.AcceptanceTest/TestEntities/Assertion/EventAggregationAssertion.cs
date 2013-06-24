//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestLibrary.Common;
using AcceptanceTestLibrary.UIAWrapper;
using AcceptanceTestLibrary.ApplicationHelper;

using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Text;
using System.Windows.Automation.Provider;
using EventAggregation.Tests.AcceptanceTest.TestEntities.Page;
using Core.UIItems.ListBoxItems;
using Core.UIItems;
using Core.UIItems.WindowItems;
using Core.UIItems.Finders;
using System.Globalization;
using System.Diagnostics;

namespace EventAggregation.Tests.AcceptanceTest.TestEntities.Assertion
{
    public static class EventAggregationAssertion<TApp>
        where TApp : AppLauncherBase, new()
    {
        #region Silverlight
        public static void AssertAddFundToCustomer()
        {
            AutomationElement customerCombobox = EventAggregationPage<TApp>.CustomerCombo;
            //1. Get the handle of the Customer combo box and select Customer1
            Assert.IsNotNull(customerCombobox, "Could not find Customer combobox");

            customerCombobox.Expand();
            System.Threading.Thread.Sleep(3000);

            AutomationElement customerItem = customerCombobox.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultCustomer")));
            Assert.IsNotNull(customerItem, "Could not find Customer1 item in customer combobox");

            Core.InputDevices.Mouse.Instance.Location = new System.Drawing.Point((int)Math.Floor(customerItem.Current.BoundingRectangle.X), (int)Math.Floor(customerItem.Current.BoundingRectangle.Y));
            Core.InputDevices.Mouse.Instance.Click();

            //2. Get the handle of the Fund combo box and select FundA
            AutomationElement fundCombobox = EventAggregationPage<TApp>.FundCombo;
            Assert.IsNotNull(fundCombobox, "Could not find Fund combobox");

            fundCombobox.Expand();
            System.Threading.Thread.Sleep(3000);

            AutomationElement fundItem = fundCombobox.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultFund")));
            Assert.IsNotNull(fundItem, "Could not find FundA item in fund combobox");

            Core.InputDevices.Mouse.Instance.Location = new System.Drawing.Point((int)Math.Floor(fundItem.Current.BoundingRectangle.X), (int)Math.Floor(fundItem.Current.BoundingRectangle.Y));
            Core.InputDevices.Mouse.Instance.Click();

            //3. Get the handle of the Add button and click on it
            AutomationElement addButton = EventAggregationPage<TApp>.AddFundButton;
            Assert.IsNotNull(addButton, "Could not find Add button");

            addButton.Click();

            //4. Get the handle of Activity Label and check the content
            Assert.AreEqual(EventAggregationPage<TApp>.ActivityLabelElement.Current.Name, GetDataFromTestDataFile("Customer1ActivityLabelText"));

        }

        public static void AssertAddMultipleFundsToCustomer()
        {

            AutomationElement customerCombobox = EventAggregationPage<TApp>.CustomerCombo;
            //1. Get the handle of the Customer combo box and select Customer1
            Assert.IsNotNull(customerCombobox, "Could not find Customer combobox");

            customerCombobox.Expand();
            System.Threading.Thread.Sleep(3000);

            AutomationElement customerItem = customerCombobox.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultCustomer")));
            Assert.IsNotNull(customerItem, "Could not find Customer1 item in customer combobox");

            Core.InputDevices.Mouse.Instance.Location = new System.Drawing.Point((int)Math.Floor(customerItem.Current.BoundingRectangle.X), (int)Math.Floor(customerItem.Current.BoundingRectangle.Y));
            Core.InputDevices.Mouse.Instance.Click();

            //2. Get the handle of the Fund combo box and select FundA
            AutomationElement fundCombobox = EventAggregationPage<TApp>.FundCombo;
            Assert.IsNotNull(fundCombobox, "Could not find Fund combobox");

            fundCombobox.Expand();
            System.Threading.Thread.Sleep(3000);

            AutomationElement fundItem = fundCombobox.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultFund")));
            Assert.IsNotNull(fundItem, "Could not find FundA item in fund combobox");

            Core.InputDevices.Mouse.Instance.Location = new System.Drawing.Point((int)Math.Floor(fundItem.Current.BoundingRectangle.X), (int)Math.Floor(fundItem.Current.BoundingRectangle.Y));
            Core.InputDevices.Mouse.Instance.Click();

            //3. Get the handle of the Add button and click on it
            AutomationElement addButton = EventAggregationPage<TApp>.AddFundButton;
            Assert.IsNotNull(addButton, "Could not find Add button");

            addButton.Click();

            bool isTextFound = false;
            foreach (AutomationElement textBox in EventAggregationPage<TApp>.AllTextBoxes)
            {
                if (textBox.Current.Name.Equals(GetDataFromTestDataFile("DefaultFund"), StringComparison.CurrentCulture))
                {
                    isTextFound = true;
                    break;
                }
            }

            Assert.IsTrue(isTextFound, "FundA is not added");
        }

        private static AutomationElement GetHandleByAutomationId(string controlId)
        {
            AutomationElement win = EventAggregationPage<TApp>.Window;

            //find control using AutomationElement of window 
            return win.GetHandleById<TApp>(controlId);
        }

        #endregion

        #region Desktop
        public static void DesktopAssertAddMultipleFundsToCustomer()
        {
            //Add Default fund to default customer
            EventAggregationPage<TApp>.CustomerComboBox.Select(GetDataFromTestDataFile("DefaultCustomer"));
            EventAggregationPage<TApp>.FundComboBox.Select(GetDataFromTestDataFile("DefaultFund"));
            EventAggregationPage<TApp>.AddButton.Click();

            //Select another fund to default customer
            EventAggregationPage<TApp>.FundComboBox.Select(GetDataFromTestDataFile("AnotherFund"));
            EventAggregationPage<TApp>.AddButton.Click();

            //Assert Activity View
            Assert.AreEqual(EventAggregationPage<TApp>.ActivityLabel.Text, GetDataFromTestDataFile("Customer1ActivityLabelText"));

            Assert.IsNotNull(EventAggregationPage<TApp>.GetFundsLabel(GetDataFromTestDataFile("DefaultFund")));
            Assert.IsNotNull(EventAggregationPage<TApp>.GetFundsLabel(GetDataFromTestDataFile("AnotherFund")));
        }



        public static void DesktopAssertAddRepeatedFundsToCustomer()
        {

            int numberOfAddClicks = 3;

            //Add Default fund to default customer repeatedly.
            EventAggregationPage<TApp>.CustomerComboBox.Select(GetDataFromTestDataFile("DefaultCustomer"));
            EventAggregationPage<TApp>.FundComboBox.Select(GetDataFromTestDataFile("DefaultFund"));
            EventAggregationPage<TApp>.AddButton.Click();
            EventAggregationPage<TApp>.AddButton.Click();
            EventAggregationPage<TApp>.AddButton.Click();

            Assert.AreEqual(EventAggregationPage<TApp>.ActivityLabel.Text, GetDataFromTestDataFile("Customer1ActivityLabelText"));

            //For every Add button click, Check if the selected fund is added to the selected customer repeatedly.
            for (int count = 0; count < numberOfAddClicks; count++)
            {
                Assert.IsNotNull(EventAggregationPage<TApp>.GetFundsLabel(GetDataFromResourceFile("DefaultFund")));
            }
        }

        public static void DesktopAssertEachCustomerShouldHaveAnActivityView()
        {

            //For every customer in the customer combo box,check if a corresponding article view is displayed
            for (int count = 0; count < EventAggregationPage<TApp>.CustomerComboBox.Items.Count - 1; count++)
            {
                Assert.IsNotNull(EventAggregationPage<TApp>.GetFundsLabelByAutomationId(GetDataFromResourceFile("ActivityLabelTextValue") + " " + EventAggregationPage<TApp>.CustomerComboBox.Items[count].Text, GetDataFromResourceFile("ActivityLabel")));
            }
        }

        public static void DesktopAssertSelectedFundIsAddedOnlyToTheSelectedCustomer()
        {
            string[] selectedCustomer = new string[2];
            string[] selectedFund = new string[2];

            const int CUSTOMERS_IN_DROPDOWN = 2;

            for (int loopCounter = 0; loopCounter < CUSTOMERS_IN_DROPDOWN; loopCounter++)
            {
                EventAggregationPage<TApp>.CustomerComboBox.Select(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("Customer" + loopCounter.ToString(CultureInfo.InvariantCulture)));
                selectedCustomer[loopCounter] = EventAggregationPage<TApp>.CustomerComboBox.Items[loopCounter].Text;

                EventAggregationPage<TApp>.FundComboBox.Select(new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("Fund" + loopCounter.ToString(CultureInfo.InvariantCulture)));
                selectedFund[loopCounter] = EventAggregationPage<TApp>.FundComboBox.Items[loopCounter].Text;

                EventAggregationPage<TApp>.AddButton.Click();
            }

            int counter = 0;
            foreach (AutomationElement element in EventAggregationPage<TApp>.ElementsInActivityView)
            {
                if (counter < CUSTOMERS_IN_DROPDOWN)
                {
                    // Find the text box and the fund text associated.
                    AutomationElement textBox = element.SearchInRawTreeByName(GetDataFromResourceFile("ActivityLabel"));
                    string textBoxValue = textBox.Current.Name;

                    string expectedValue = GetDataFromResourceFile("ActivityLabelTextValue") + " " + selectedCustomer[counter].ToString();
                    Assert.AreEqual(expectedValue, textBoxValue);

                    // Find the fund values
                    AutomationElement fund = element.SearchInRawTreeByName(selectedFund[counter].ToString());
                    Assert.AreEqual(fund.Current.Name, selectedFund[counter].ToString());
                    counter++;
                }
            }
        }

        private static string GetDataFromResourceFile(string keyName)
        {
            return new ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue(keyName);
        }

        private static string GetDataFromTestDataFile(string keyName)
        {
            return new ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue(keyName);
        }
        #endregion
    }
}
