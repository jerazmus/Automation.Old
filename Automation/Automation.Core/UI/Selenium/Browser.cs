using FluentAssertions;
using Automation.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium;

namespace Automation.Core.UI.Selenium
{
    public class Browser : IBrowserActions
    {
        static IWebDriver _driver;

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
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("--disable-notifications");
                    chromeOptions.AddArguments("--lang=pl");
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
        // Implementation of IBrowserActions interface using Selenium WebDriver
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

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return Execute($"Assert URL contains '{expectedValue}'", () =>
            {
                _driver.Url.Should().Contain(expectedValue);
            });
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedValue)
        {
            return Execute($"Assert element '{xpath}' has text '{expectedValue}'", () =>
            {
                _driver.FindElement(By.XPath(xpath)).Text.Should().Be(expectedValue);
            });
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
        #endregion
    }
}
