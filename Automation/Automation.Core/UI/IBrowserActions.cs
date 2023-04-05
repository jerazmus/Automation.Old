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
        public IBrowserActions NavigateToUrl(string url);
        public IBrowserActions MoveBack();
        public IBrowserActions MoveForward();
        public IBrowserActions CloseSession();
        public IBrowserActions AssertUrlContains(string expectedValue);
        public IBrowserActions AssertTextEquals(string xpath, string expectedText, bool isPartialText = false, bool trim = false, bool onlyTextContent = false);
        public IBrowserActions AssertIsDisplayed(string xpath);
        public IBrowserActions AssertIsNotDisplayed(string xpath);
    }
}
