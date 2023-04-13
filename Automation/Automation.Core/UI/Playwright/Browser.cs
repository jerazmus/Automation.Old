using Automation.Core.Utilities;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

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

        public IBrowserActions Hover(string xpath)
        {
            return Execute($"Hover '{xpath}'", () =>
            {
                _page.HoverAsync(xpath).Wait();
            });
        }

        public IBrowserActions HoverAndClick(string hoverXpath, string clickXpath)
        {
            return Execute($"Hover '{hoverXpath}' and click '{clickXpath}'", () =>
            {
                _page.HoverAsync(hoverXpath).Wait();
                _page.ClickAsync(clickXpath).Wait();
            });
        }

        public IBrowserActions DragAndDrop(string sourceXpath, string targetXpath, int? targetOffsetY = null)
        {
            return Execute($"Drag and drop from '{sourceXpath}' to '{targetXpath}'", () =>
            {
                var pageDragAndDropOptions = targetOffsetY == null
                ? null
                : new PageDragAndDropOptions()
                {
                    TargetPosition = new TargetPosition()
                    {
                        Y = (float)targetOffsetY
                    }
                };

                _page.DragAndDropAsync(sourceXpath, targetXpath, pageDragAndDropOptions);
            });
        }

        public IBrowserActions Press(KeyboardKey key)
        {
            return Execute($"Press '{key}'", () =>
            {
                _page.Keyboard.PressAsync(key.ToString(), new KeyboardPressOptions()
                {
                    Delay = 100
                });
            });
        }

        public IBrowserActions Upload(string xpath, string path)
        {
            return Execute($"Upload '{xpath}' to '{path}'", () =>
            {
                _page.SetInputFilesAsync(xpath, path);
            });
        }

        // ---------------------------------------

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

        public IBrowserActions ExecuteScript(string script, bool throwOnError = true)
        {
            try
            {
                _page.EvaluateAsync(script).Wait();
            }
            catch (Exception ex)
            {
                if (throwOnError)
                {
                    throw new Exception("Cannot execute script", ex);
                }
            }

            return this;
        }

        // ---------------------------------------

        public IBrowserActions AssertUrlContains(string expectedValue)
        {
            return Execute($"Assert URL contains '{expectedValue}'", () =>
            {
                GetUrl().Should().Contain(expectedValue);
            });
        }

        public IBrowserActions AssertTextEquals(
            string xpath,
            string expectedText, 
            bool isPartialText = false,
            bool trim = false, 
            bool onlyTextContent = false)
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

        public IBrowserActions AssertValueEquals(
            string xpath,
            string expectedValue,
            bool isPartialValue = false,
            bool trim = false)
        {
            return Execute($"Assert '{xpath}' has value '{expectedValue}'", () =>
            {
                expectedValue = expectedValue ?? string.Empty;
                var elementVisibilityState = string.IsNullOrWhiteSpace(expectedValue)
                    ? WaitForSelectorState.Attached
                    : WaitForSelectorState.Visible;

                var element = WaitForVisibleElement(xpath, elementVisibilityState);
                var actualValue = element.InputValueAsync().Result;

                actualValue = trim ? actualValue.Trim() : actualValue;
                expectedValue = trim ? expectedValue.Trim() : expectedValue;

                if (isPartialValue)
                {
                    actualValue.Should().Contain(expectedValue);
                }
                else
                {
                    actualValue.Should().Be(expectedValue);
                }
            });
        }

        public IBrowserActions AssertAttributeEquals(
            string xpath,
            string attributeName,
            string expectedValue,
            bool isPartialValue = false,
            bool trim = false)
        {
            return Execute($"Assert '{xpath}' has attribute '{attributeName}' with value '{expectedValue}'", () =>
            {
                expectedValue = expectedValue ?? string.Empty;
                var element = WaitForVisibleElement(xpath, WaitForSelectorState.Attached);
                var actualValue = GetAttributeValue(element, attributeName);

                actualValue = trim ? actualValue.Trim() : actualValue;
                expectedValue = trim ? expectedValue.Trim() : expectedValue;

                if (isPartialValue)
                {
                    actualValue.Should().Contain(expectedValue);
                }
                else
                {
                    actualValue.Should().Be(expectedValue);
                }
            });
        }

        public IBrowserActions AssertIsDisplayed(string xpath)
        {
            return Execute($"Assert element '{xpath}' is displayed", () =>
            {
                var element = WaitForVisibleElement(xpath);
                var isDisplayed = element.IsVisibleAsync().Result;
                isDisplayed.Should().BeTrue();
            });
        }

        public IBrowserActions AssertIsNotDisplayed(string xpath)
        {
            return Execute($"Assert element '{xpath}' is not displayed", () =>
            {
                _page.WaitForSelectorAsync(xpath, new PageWaitForSelectorOptions()
                {
                    State = WaitForSelectorState.Hidden
                }).Wait();

                var isHidden = _page.IsHiddenAsync(xpath).Result;
                isHidden.Should().BeTrue();
            });
        }

        public IBrowserActions AssertTextDisplayed(string expectedText, bool doubleQuotation)
        {
            return AssertIsDisplayed(XPath.TextContains(expectedText, doubleQuotation));
        }

        public IBrowserActions AssertTextNotDisplayed(string expectedText)
        {
            return AssertIsNotDisplayed(XPath.TextContains(expectedText));
        }

        public IBrowserActions AssertIsEnabled(string xpath)
        {
            return Execute($"Assert element '{xpath}' is enabled", () =>
            {
                var element = WaitForVisibleElement(xpath);
                var isEnabled = element.IsEnabledAsync().Result;
                isEnabled.Should().BeTrue();
            });
        }

        public IBrowserActions AssertIsDisabled(string xpath)
        {
            return Execute($"Assert element '{xpath}' is disabled", () =>
            {
                var element = WaitForVisibleElement(xpath);
                var isDisabled = element.IsDisabledAsync().Result;
                isDisabled.Should().BeTrue();
            });
        }

        // ---------------------------------------

        public string GetUrl()
        {
            return _page.Url;
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

        // Get text value of many elements (for example in dropdown list)
        public List<string> GetElementsText(string name)
        {
            List<string> elementsText = null;

            Execute($"Get text of elements {name}", () =>
            {
                WaitForVisibleElement(name);
                elementsText = _page.QuerySelectorAllAsync(name)
                    .Result
                    .Select(e => e.TextContentAsync()
                        .Result)
                    .ToList();
            });

            return elementsText;
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

        // Get element's attribute value (this method allows to return empty string compared to default Playwright's one)
        private string GetAttributeValue(IElementHandle element, string name)
        {
            try
            {
                return element.GetAttributeAsync(name).Result ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
