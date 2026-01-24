using AutomationTests.Framework.Utilities;

namespace AutomationTests.Pages;

public class BasePage
{
    protected IWebDriver _driver;
    protected WaitUtil _wait;

    public BasePage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WaitUtil(driver);
    }

    protected string GetValueFromInput(IWebElement element)
    {
        return _wait.ForElementVisibility(element).GetAttribute("value");
    }

    protected bool IsElementDisplayed(IWebElement element)
    {
        try
        {
            return element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    protected bool IsElementEnabled(IWebElement element)
    {
        try
        {
            return element.Enabled;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    protected bool IsElementSelected(IWebElement element)
    {
        try
        {
            return element.Selected;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}