namespace Automation.Core.UI.Selenium
{
    public interface IBrowserActions
    {
        //
        // Methods that are usually used during UI or API tests
        //

        public IBrowserActions Click(string xpath);
        public IBrowserActions Type(string xpath, string text);
        public IBrowserActions NavigateToUrl(string url);
        public IBrowserActions MoveBack();
        public IBrowserActions MoveForward();
        public IBrowserActions CloseSession();
        public IBrowserActions AssertUrlContains(string expectedValue);
        public IBrowserActions AssertTextEquals(string xpath, string expectedValue);
        public IBrowserActions AssertIsDisplayed(string xpath);
        public IBrowserActions AssertIsNotDisplayed(string xpath);
    }
}
