using Automation.Core.UI.Selenium;
using Automation.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium;

namespace Automation.Core.UI.Playwright
{
    internal class Browser : IBrowserActions
    {
        public Browser()
        {
        }

        // ---------------------------------------

        // Allows to live log every action during test progress and chain call another methods
        public IBrowserActions Execute(string log, Action action)
        {
            TestLogger.Log(log);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new AssertionException(log, ex);
            }
            return this;
        }

        // ---------------------------------------

        //
        // Implementation of IBrowserActions interface using Selenium WebDriver
        //

        #region Browser Actions

        public IBrowserActions Click(string xpath)
        {
            return Execute($"Click '{xpath}'", () =>
            {
            });
        }

        public IBrowserActions Type(string xpath, string text)
        {
            return Execute($"Type '{text}' into '{xpath}'", () =>
            {
            });
        }

        public IBrowserActions NavigateToUrl(string url)
        {
            return Execute($"Navigate to {url}", () =>
            {
            });
        }

        public IBrowserActions MoveBack()
        {
            return Execute($"Go back", () =>
            {
            });
        }

        public IBrowserActions MoveForward()
        {
            return Execute($"Go forward", () =>
            {
            });
        }

        public IBrowserActions CloseSession()
        {
            return Execute($"Close browser", () =>
            {
            });
        }

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return Execute($"Assert URL contains '{expectedValue}'", () =>
            {
            });
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedValue)
        {
            return Execute($"Assert element '{xpath}' has text '{expectedValue}'", () =>
            {
            });
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return Execute($"Assert element is displayed '{xpath}'", () =>
            {
            });
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return Execute($"Assert element is not displayed '{xpath}'", () =>
            {
            });
        }
        #endregion
    }
}
