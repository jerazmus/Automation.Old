using FluentAssertions;
using Automation.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium;
using Microsoft.Playwright;

namespace Automation.Core.UI.Selenium
{
    public class Browser : IBrowserActions
    {
        static IWebDriver _driver;

        public static string LanguageInfo = "--lang=pl";
        public static string BrowserResolution = "--start-maximized";
        public static string Headless = "headless";

        // ---------------------------------------

        public Browser(BrowserType browserType)
        {
            _driver = SetBrowser(browserType);
        }

        // ---------------------------------------

        // Sets up browser driver + it's options
        public IWebDriver SetBrowser(BrowserType type)
        {
            switch (type)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments(LanguageInfo);
                    chromeOptions.AddArguments(BrowserResolution);
                    chromeOptions.AddArguments(Headless);
                    return new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Safari:
                    return new SafariDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    return new ChromeDriver();
            }
        }

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
        // Implementation of IBrowserActions interface using Selenium
        //

        #region Browser Actions

        public IBrowserActions Click(string xpath)
        {
            return Execute($"Click '{xpath}'", () =>
            {
                _driver.FindElement(By.XPath(xpath)).Click();
            });
        }

        public IBrowserActions Type(string xpath, string text)
        {
            return Execute($"Type '{text}' into '{xpath}'", () =>
            {
                _driver.FindElement(By.XPath(xpath)).SendKeys(text);
            });
        }

        public IBrowserActions Check(string xpath, bool check)
        { 
            return this;
        }

        public IBrowserActions SelectOption(string menuXpath, string optionXPath)
        {
            return this;
        }

        public IBrowserActions Hover(string xpath)
        {
            return this;
        }

        public IBrowserActions HoverAndClick(string hoverXpath, string clickXpath)
        {
            return this;
        }

        public IBrowserActions DragAndDrop(string sourceXpath, string targetXpath, int? targetOffsetY = null)
        {
            return this;
        }

        public IBrowserActions Press(KeyboardKey key)
        {
            return this;
        }

        public IBrowserActions Upload(string xpath, string path)
        {
            return this;
        }

        // ---------------------------------------

        public IBrowserActions NavigateToUrl(string url)
        {
            return Execute($"Navigate to {url}", () =>
            {
                _driver.Navigate().GoToUrl(url);
            });
        }

        public IBrowserActions MoveBack()
        {
            return Execute($"Go back", () =>
            {
                _driver.Navigate().Back();
            });
        }

        public IBrowserActions MoveForward()
        {
            return Execute($"Go forward", () =>
            {
                _driver.Navigate().Forward();
            });
        }

        public IBrowserActions CloseSession()
        {
            return Execute($"Close browser", () =>
            {
                _driver.Close();
            });
        }

        public IBrowserActions ExecuteScript(string script, bool throwOnError = true)
        {
            return this;
        }

        // ---------------------------------------

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return Execute($"Assert URL contains '{expectedValue}'", () =>
            {
                _driver.Url.Should().Contain(expectedValue);
            });
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedText, bool isPartialText = false, bool trim = false, bool onlyTextContent = false)
        {
            return Execute($"Assert element '{xpath}' has text '{expectedText}'", () =>
            {
                _driver.FindElement(By.XPath(xpath)).Text.Should().Be(expectedText);
            });
        }

        public IBrowserActions AssertValueEquals(string xpath, string expectedValue, bool isPartialValue = false, bool trim = false)
        {
            return this;
        }

        public IBrowserActions AssertAttributeEquals(string xpath, string attributeName, string expectedValue, bool isPartialValue = false, bool trim = false)
        {
            return this;
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return Execute($"Assert element is displayed '{xpath}'", () =>
            {
                _driver.FindElement(By.XPath(xpath)).Displayed.Should().BeTrue();
            });
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return Execute($"Assert element is not displayed '{xpath}'", () =>
            {
                try
                {
                    _driver.FindElement(By.XPath(xpath));
                }
                catch (NoSuchElementException)
                {
                    TestLogger.Log("NoSuchElementException is taken as true");
                    Assert.True(true);
                }
            });
        }

        public IBrowserActions AssertTextDisplayed(string expectedText, bool doubleQuotation = false)
        {
            return this;
        }

        public IBrowserActions AssertTextNotDisplayed(string expectedText)
        {
            return this;
        }

        public IBrowserActions AssertIsEnabled(string xpath)
        {
            return this;
        }

        public IBrowserActions AssertIsDisabled(string xpath)
        {
            return this;
        }

        // ---------------------------------------

        public string GetUrl()
        {
            return _driver.Url;
        }

        // Get element's text
        public string GetText(string name, WaitForSelectorState expectedState, bool onlyTextContent = false)
        {
            return string.Empty;
        }

        // Get text value of many elements (for example in dropdown list)
        public List<string> GetElementsText(string name)
        {
            List<string> elementsText = null;

            Execute($"Get text of elements {name}", () =>
            {
            });

            return elementsText;
        }
        #endregion
    }
}
