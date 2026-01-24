namespace AutomationTests.Pages;

public class LoginPage : BasePage
{
    private readonly IWebDriver _driver;

    #region Web Elements
    private IWebElement Title => _driver.FindElement(By.CssSelector("div[class='login_logo']"));
    private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
    private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
    #endregion

    public LoginPage(IWebDriver driver) : base(driver)
    {
        _driver = driver;
    }

    #region Methods

    public LoginPage EnterUsername(string username)
    {
        _wait.ForElementVisibility(UsernameInput).Clear();
        _wait.ForElementVisibility(UsernameInput).SendKeys(username);
        return this;
    }

    public string GetTextInUsernameInput()
    {
        return GetValueFromInput(UsernameInput);
    }

    public LoginPage EnterPassword(string password)
    {
        _wait.ForElementVisibility(PasswordInput).Clear();
        _wait.ForElementVisibility(PasswordInput).SendKeys(password);
        return this;
    }

    public string GetTextInPasswordInput()
    {
        return GetValueFromInput(PasswordInput);
    }

    public ProductsPage ClickLoginButton()
    {
        _wait.ForElementVisibility(LoginButton).Click();
        return new ProductsPage(_driver);
    }

    public ProductsPage LoginAs(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        return ClickLoginButton();
    }

    public bool IsTitleDisplayed()
    {
        return IsElementDisplayed(Title);
    }

    public bool IsUsernameInputDisplayed()
    {
        return IsElementDisplayed(UsernameInput);
    }

    public bool IsPasswordInputDisplayed()
    {
        return IsElementDisplayed(PasswordInput);
    }

    public bool IsLoginButtonDisplayed()
    {
        return IsElementEnabled(LoginButton);
    }
    #endregion
}