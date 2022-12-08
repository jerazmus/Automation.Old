using Automation.Core.UI;
using Automation.Core.UI.Selenium;
using Automation.Core.Utilities;

namespace Automation.Core.SwagLabs.UI.Pages
{
    public class LoginPage : Page
    {
        public LoginPage(IBrowserActions browser, string baseUrl) : base(browser, baseUrl)
        {
        }

        // ---------------------------------------

        //
        // Assertion strings
        //

        public string EmptyUsername => "Epic sadface: Username is required";
        public string EmptyPassword => "Epic sadface: Password is required";
        public string WrongCredentials => "Epic sadface: Username and password do not match any user in this service";

        // ---------------------------------------

        //
        // XPaths for page's elements
        //
        
        public string Username
            => XPath.Element("username");
        public string Password
            => XPath.Element("password");
        public string LoginButton
            => XPath.Element("login-button");
        public string Error
            => XPath.Element("error");
        public string ErrorButton
            => XPath.ElementClass("error-button");

        // ---------------------------------------

        // Login method (using default values from 'automation.list' file)
        public LoginPage Login(string username = "standard_user", string password = "secret_sauce")
        {
            Type(Username, username);
            Type(Password, password);
            Click(LoginButton);

            return this;
        }
    }
}
