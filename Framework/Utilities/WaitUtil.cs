using Serilog;

namespace AutomationTests.Framework.Utilities;

public class WaitUtil
{
    private readonly IWebDriver? _driver;
    private const int _defaultElementTimeout = 10;

    public WaitUtil(IWebDriver? driver)
    {
        _driver = driver;
    }

    public IWebElement ForElementVisibility(By locator, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element visibility: {locator}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(driver => _driver.FindElement(locator).Displayed
            ? _driver.FindElement(locator)
            : null);
    }

    public IWebElement? ForElementVisibility(IWebElement element, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element visibility: {element}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultElementTimeout));
        return wait.Until(driver => element.Displayed ? element : null);
    }

    public bool ForElementToNotBeVisible(By locator, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element to not be visible: {locator}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(driver =>
        {
            try
            {
                return !_driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        });
    }

    public bool ForElementToNotBeVisible(IWebElement element, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element to not be visible: {element}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(driver =>
        {
            try
            {
                return !element.Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        });
    }

    public IWebElement ForElementToBeClickable(By locator, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element to be clickable: {locator}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(driver => _driver.FindElement(locator).Displayed && _driver.FindElement(locator).Enabled
            ? _driver.FindElement(locator)
            : null);
    }

    public IWebElement ForElementToBeClickable(IWebElement element, int timeoutSeconds = _defaultElementTimeout)
    {
        Log.Debug($"Waiting for element to be clickable: {element}");
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_defaultElementTimeout));
        return wait.Until(driver => (element.Displayed && element.Enabled) ? element : null);
    }
}