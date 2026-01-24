using AutomationTests.Pages;
using AventStack.ExtentReports.Model;

namespace AutomationTests.Tests.LoginTests;

public class LoginTest : BaseTest
{
    public LoginPage LOGINPAGE;

    [SetUp]
    public void Setup() 
    {
        LOGINPAGE = new LoginPage(_driver);
    }

    [Test]
    public void LoginPage_ElementsCheck()
    {
        AssertK.That(LOGINPAGE.IsTitleDisplayed(), "Login page title is displayed");
    }

    [Test]
    public void LoginPage_SuccessfulLogin()
    {
        #region Data
        string username = "standard_user";
        string password = "secret_sauce";
        #endregion

        AssertK.Log("Enter username and password, then click login button");
        LOGINPAGE.LoginAs(username, password);
        AssertK.Log("Login successful");
    }

    [Test]
    public void LoginPage_EnterUsername_CheckInput()
    {
        AssertK.Log("Enter username in username input field");
        LOGINPAGE.EnterUsername("standard_user");
        AssertK.TextEquals("Username Input", "standard_user", LOGINPAGE.GetTextInUsernameInput());
        AssertK.That(LOGINPAGE.IsLoginButtonDisplayed(), "Login button is displayed");
    }
}