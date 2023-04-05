using Automation.Core.Utilities;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace Automation.Core.UI.Playwright
{
    internal class Browser : IBrowserActions
    {
        private IBrowserContext _context;

        private IPage _page;

        // ---------------------------------------

        public Browser(IBrowserContext context)
        {
            _context = context;
            _page = _context.NewPageAsync().Result;
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
        // Implementation of IBrowserActions interface using Playwright
        //

        #region Browser Actions

        public IBrowserActions Click(string xpath)
        {
            return Execute($"Click '{xpath}'", () =>
            {
                _page.ClickAsync(xpath).Wait();
            });
        }

        public IBrowserActions Type(string xpath, string text)
        {
            return Execute($"Type '{text}' into '{xpath}'", () =>
            {
                _page.FillAsync(xpath, text ?? string.Empty).Wait();
            });
        }

        public IBrowserActions Check(string xpath, bool check)
        {
            var checkValue = _page.GetAttributeAsync(xpath, "aria-checked").Result;
            if ((check && checkValue == "false") || (!check && checkValue == "true"))
            {
                _page.ClickAsync(xpath).Wait();
            }

            while (_page.GetAttributeAsync(xpath, "aria-checked").Result != check.ToString().ToLower())
            {
                _page.ClickAsync(xpath).Wait();
            }

            return this;
        }

        public IBrowserActions SelectOption(string menuXpath, string optionXPath)
        {
            return Execute($"Selecct option '{optionXPath}' from menu '{menuXpath}'", () =>
            {
                _page.ClickAsync(menuXpath).Wait();
                _page.ClickAsync(optionXPath).Wait();
            });
        }

        public IBrowserActions NavigateToUrl(string url)
        {
            return Execute($"Navigate to {url}", () =>
            {
                _page.GotoAsync(url).Wait();
            });
        }

        public IBrowserActions MoveBack()
        {
            return Execute($"Go back", () =>
            {
                _page.GoBackAsync().Wait();
            });
        }

        public IBrowserActions MoveForward()
        {
            return Execute($"Go forward", () =>
            {
                _page.GoForwardAsync().Wait();
            });
        }

        public IBrowserActions CloseSession()
        {
            return Execute($"Close browser", () =>
            {
                _page.CloseAsync().Wait();
            });
        }

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return Execute($"Assert URL contains '{expectedValue}'", () =>
            {
                _page.Url.Should().Contain(expectedValue);
            });
        }

        public IBrowserActions AssertTextEquals(string xpath, string expectedText, bool isPartialText = false, bool trim = false, bool onlyTextContent = false)
        {
            return Execute($"Assert element '{xpath}' has text '{expectedText}'", () =>
            {
                expectedText = expectedText ?? string.Empty;
                var elementVisibilityState = string.IsNullOrWhiteSpace(expectedText) 
                    ? WaitForSelectorState.Attached 
                    : WaitForSelectorState.Visible;

                var actualText = GetText(xpath, elementVisibilityState, onlyTextContent);

                actualText = trim ? actualText.Trim() : actualText;
                expectedText = trim ? expectedText.Trim() : expectedText;

                if (isPartialText)
                {
                    actualText.Should().Contain(expectedText);
                }
                else
                {
                    actualText.Should().Be(expectedText);
                }
            });
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return Execute($"Assert element is displayed '{xpath}'", () =>
            {
                var element = WaitForVisibleElement(xpath);
                var isDisplayed = element.IsVisibleAsync().Result;
                isDisplayed.Should().BeTrue();
            });
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return Execute($"Assert element is not displayed '{xpath}'", () =>
            {
                _page.WaitForSelectorAsync(xpath, new PageWaitForSelectorOptions()
                {
                    State = WaitForSelectorState.Hidden
                }).Wait();

                var isHidden = _page.IsHiddenAsync(xpath).Result;
                isHidden.Should().BeTrue();
            });
        }
        #endregion

        // ---------------------------------------

        //
        // Suplementary methods
        //

        // Manual wait for element
        private IElementHandle WaitForVisibleElement(string xpath, WaitForSelectorState state = WaitForSelectorState.Visible)
        {
            return _page.WaitForSelectorAsync(xpath, new PageWaitForSelectorOptions()
            {
                State = state
            }).Result;
        }

        // Get element's text
        public string GetText(string name, WaitForSelectorState expectedState, bool onlyTextContent = false)
        {
            var element = WaitForVisibleElement(name, expectedState);
            var innerText = element.InnerTextAsync().Result ?? string.Empty;
            var textContent = element.TextContentAsync().Result ?? string.Empty;
            return onlyTextContent
                ? textContent
                : innerText.Trim().Contains(textContent.Trim(), StringComparison.OrdinalIgnoreCase)
                    ? innerText
                    : textContent + innerText;
        }
    }
}
