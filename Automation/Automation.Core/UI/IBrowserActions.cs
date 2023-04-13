using Automation.Core.Utilities;
using Microsoft.Playwright;

namespace Automation.Core.UI
{
    public interface IBrowserActions
    {
        //
        // Methods that are usually used during UI tests
        //

        public IBrowserActions Click(string xpath);
        public IBrowserActions Type(string xpath, string text);
        public IBrowserActions Check(string xpath, bool check);
        public IBrowserActions SelectOption(string menuXpath, string optionXPath);
        public IBrowserActions Hover(string xpath);
        public IBrowserActions HoverAndClick(string hoverXpath, string clickXpath);
        public IBrowserActions DragAndDrop(string sourceXpath, string targetXpath, int? targetOffsetY = null);
        public IBrowserActions Press(KeyboardKey key);
        public IBrowserActions Upload(string xpath, string path);

        // ---------------------------------------

        public IBrowserActions NavigateToUrl(string url);
        public IBrowserActions MoveBack();
        public IBrowserActions MoveForward();
        public IBrowserActions CloseSession();
        public IBrowserActions ExecuteScript(string script, bool throwOnError = true);

        // ---------------------------------------

        public IBrowserActions AssertUrlContains(string expectedValue);
        public IBrowserActions AssertTextEquals(string xpath, string expectedText, bool isPartialText = false, bool trim = false, bool onlyTextContent = false);
        public IBrowserActions AssertValueEquals(string xpath, string expectedValue, bool isPartialValue = false, bool trim = false);
        public IBrowserActions AssertAttributeEquals(string xpath, string attributeName, string expectedValue, bool isPartialValue = false, bool trim = false);
        public IBrowserActions AssertIsDisplayed(string xpath);
        public IBrowserActions AssertIsNotDisplayed(string xpath);
        public IBrowserActions AssertTextDisplayed(string expectedText, bool doubleQuotation = false);
        public IBrowserActions AssertTextNotDisplayed(string expectedText);
        public IBrowserActions AssertIsEnabled(string xpath);
        public IBrowserActions AssertIsDisabled(string xpath);

        // ---------------------------------------

        public string GetUrl();
        public string GetText(string name, WaitForSelectorState expectedState, bool onlyTextContent = false);
        public List<string> GetElementsText(string name);
    }
}
