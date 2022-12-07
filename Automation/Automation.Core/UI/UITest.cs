using Automation.Core.UI.Selenium;
using Automation.Core.Utilities;
using NUnit.Framework;

namespace Automation.Core.UI
{
    public abstract class UITest : Test, IBrowserActions
    {
        private IBrowserActions _browser;

        // ---------------------------------------

        //
        // Setup and teardown methods for every UI test
        // 

        [OneTimeSetUp]
        public void Setup()
        {
            _browser = new Browser(BrowserType.Chrome);
            TestLogger.Log("Open browser");
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestLogger.Log("Close browser");
            _browser.CloseSession();
        }

        // ---------------------------------------

        // Creates any instance of page, based on project (baseUrl is seperate for every project)
        public T GetPage<T>(string baseUrl) where T : Page
        {
            return (T)Activator.CreateInstance(typeof(T), _browser, baseUrl);
        }

        // ---------------------------------------

        //
        // Implementation of IBrowserActions interface
        //

        #region Browser Actions

        public IBrowserActions Click(string xpath)
        {
            return _browser.Click(xpath);
        }

        public IBrowserActions Type(string xpath, string text)
        {
            return _browser.Type(xpath, text);
        }

        public IBrowserActions NavigateToUrl(string url)
        {
            return _browser.NavigateToUrl(url);
        }

        public IBrowserActions MoveBack()
        {
            return _browser.MoveBack();
        }

        public IBrowserActions MoveForward()
        {
            return _browser.MoveForward();
        }

        public IBrowserActions CloseSession()
        {
            return _browser.CloseSession();
        }

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return _browser.AssertUrlContains(expectedValue);
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedValue)
        {
            return _browser.AssertTextEquals(xpath, expectedValue);
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return _browser.AssertIsDisplayed(xpath);
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return _browser.AssertIsNotDisplayed(xpath);
        }
        #endregion
    }
}
