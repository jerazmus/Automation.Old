using Automation.Core.Settings;
using Automation.Core.UI.Playwright;
//using Automation.Core.UI.Selenium;
using Automation.Core.Utilities;
using Microsoft.Playwright;
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
            // Selenium Browser
            //_browser = new Browser(BrowserType.Chrome);

            // Playwright Browser
            var settings = TestSettingsProvider.Get();
            var browserContext = Provider.Create(settings.Headless);
            _browser = new Browser(browserContext);

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

        // Creates any instance of component
        public T GetComponent<T>() where T : Component
        {
            return (T)Activator.CreateInstance(typeof(T), _browser);
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

        public IBrowserActions Check(string xpath, bool check)
        {
            return _browser.Check(xpath, check);
        }

        public IBrowserActions SelectOption(string menuXpath, string optionXpath)
        {
            return _browser.SelectOption(menuXpath, optionXpath);
        }

        public IBrowserActions Hover(string xpath)
        {
            return _browser.Hover(xpath);
        }

        public IBrowserActions HoverAndClick(string hoverXpath, string clickXpath)
        {
            return _browser.HoverAndClick(hoverXpath, clickXpath);
        }

        public IBrowserActions DragAndDrop(string sourceXpath, string targetXpath, int? targetOffsetY = null)
        {
            return _browser.DragAndDrop(sourceXpath, targetXpath, targetOffsetY);
        }

        public IBrowserActions Press(KeyboardKey key)
        {
            return _browser.Press(key);
        }

        public IBrowserActions Upload(string xpath, string path)
        {
            return _browser.Upload(xpath, path);
        }

        // ---------------------------------------

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

        public IBrowserActions ExecuteScript(string script, bool throwOnError = true)
        {
            return _browser.ExecuteScript(script, throwOnError);
        }

        // ---------------------------------------

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return _browser.AssertUrlContains(expectedValue);
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedValue, bool isPartialText = false, bool trim = false, bool onlyTextContent = false)
        {
            return _browser.AssertTextEquals(xpath, expectedValue, isPartialText, trim, onlyTextContent);
        }

        public IBrowserActions AssertValueEquals(string xpath, string expectedValue, bool isPartialValue = false, bool trim = false)
        {
            return _browser.AssertValueEquals(xpath, expectedValue, isPartialValue, trim);
        }
        public IBrowserActions AssertAttributeEquals(string xpath, string attributeName, string expectedValue, bool isPartialValue = false, bool trim = false)
        {
            return _browser.AssertAttributeEquals(xpath, attributeName, expectedValue, isPartialValue, trim);
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return _browser.AssertIsDisplayed(xpath);
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return _browser.AssertIsNotDisplayed(xpath);
        }
        public IBrowserActions AssertTextDisplayed(string expectedText, bool doubleQuotation = false)
        {
            return _browser.AssertTextDisplayed(expectedText, doubleQuotation);
        }

        public IBrowserActions AssertTextNotDisplayed(string expectedText)
        {
            return _browser.AssertTextNotDisplayed(expectedText);
        }

        public IBrowserActions AssertIsEnabled(string xpath)
        {
            return _browser.AssertIsEnabled(xpath);
        }

        public IBrowserActions AssertIsDisabled(string xpath)
        {
            return _browser.AssertIsDisabled(xpath);
        }

        // ---------------------------------------

        public string GetUrl()
        {
            return _browser.GetUrl();
        }

        public string GetText(string name, WaitForSelectorState expectedState, bool onlyTextContent = false)
        {
            return _browser.GetText(name, expectedState, onlyTextContent);
        }

        public List<string> GetElementsText(string name)
        {
            return _browser.GetElementsText(name);
        }

        #endregion
    }
}
