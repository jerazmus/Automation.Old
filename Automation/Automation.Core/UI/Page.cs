using Automation.Core.UI.Selenium;

namespace Automation.Core.UI
{
    public abstract class Page : IBrowserActions
    {
        protected IBrowserActions _browser;
        protected string BaseUrl { get; set; }

        // ---------------------------------------

        public Page(IBrowserActions browser, string baseUrl)
        {
            _browser = browser;
            BaseUrl = baseUrl;
        }

        // ---------------------------------------

        // Opens the page with passed BaseUrl which is defined separately for every project
        public Page Open()
        {
            NavigateToUrl(BaseUrl);
            return this;
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
