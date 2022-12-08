using Automation.Core.SwagLabs.UI;
using Automation.Core.SwagLabs.UI.Pages;
using NUnit.Framework;

namespace Automation.Tests.UI.SwagLabs
{
    public class LoginTests : SwagLabsUITest
    {
        [Category("Login-Tests-01")]
        [Test]
        public void Login_Correct()
        {
            // Given
            LoginPage.Open();

            // When
            LoginPage.Login(Settings.StandardUser, Settings.Password);

            // Then
            AssertUrlContains("inventory");
        }

        [Category("Login-Tests-02")]
        [Test]
        public void Login_Validation()
        {
            //Given
            LoginPage.Open();

            // When & Then
            Click(LoginPage.LoginButton)
                .AssertTextEquals(LoginPage.Error, LoginPage.EmptyUsername);

            Type(LoginPage.Username, "test")
                .Click(LoginPage.LoginButton)
                .AssertTextEquals(LoginPage.Error, LoginPage.EmptyPassword);

            Type(LoginPage.Password, "test")
                .Click(LoginPage.LoginButton)
                .AssertTextEquals(LoginPage.Error, LoginPage.WrongCredentials);

            Type(LoginPage.Username, "standard_user")
                .Click(LoginPage.LoginButton)
                .AssertTextEquals(LoginPage.Error, LoginPage.WrongCredentials);

            Type(LoginPage.Password, "secret_sauce")
                .Type(LoginPage.Username, "test")
                .AssertTextEquals(LoginPage.Error, LoginPage.WrongCredentials);

            Click(LoginPage.ErrorButton)
                .AssertIsNotDisplayed(LoginPage.Error);
        }
    }
}
