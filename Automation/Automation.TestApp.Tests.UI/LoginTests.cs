using Automation.TestApp.Core.UI;
using Automation.TestApp.Core.Utilities;
using NUnit.Framework;

namespace Automation.TestApp.Tests.UI
{
    public class LoginTests : TestAppUITest
    {
        [Test]
        [Category("Login-Tests-01")]
        public void Login_Correct()
        {
            // Given
            LoginPage.Open();

            // When
            LoginPage.Login(UserProvider.StandardUser);

            // Then
            AssertUrlContains("inventory");
        }

        [Test]
        [Category("Login-Tests-02")]
        public void Login_Validation()
        {
            // Given
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

            Type(LoginPage.Username, UserProvider.StandardUser.Username)
                .Click(LoginPage.LoginButton)
                .AssertTextEquals(LoginPage.Error, LoginPage.WrongCredentials);

            Type(LoginPage.Password, UserProvider.StandardUser.Password)
                .Type(LoginPage.Username, "test")
                .AssertTextEquals(LoginPage.Error, LoginPage.WrongCredentials);

            Click(LoginPage.ErrorButton)
                .AssertIsNotDisplayed(LoginPage.Error);
        }
    }
}
