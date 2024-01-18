using Automation.Core.UI;
using Automation.Core.Utilities;
using static Automation.TestApp.Core.Utilities.UserProvider;

namespace Automation.TestApp.Core.UI.Pages
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
        public LoginPage Login(User user)
        {
            Type(Username, user.Username);
            Type(Password, user.Password);
            Click(LoginButton);

            return this;
        }
    }
}
