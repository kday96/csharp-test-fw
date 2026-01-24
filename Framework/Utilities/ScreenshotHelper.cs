namespace AutomationTests.Framework.Utilities;

public class ScreenshotHelper
{
    private readonly IWebDriver _driver;
    private readonly string _screenshotFolderPath;

    public ScreenshotHelper(IWebDriver driver)
    {
        _driver = driver;
        _screenshotFolderPath = Path.Combine(
            AppContext.BaseDirectory, "..", "..", "..", 
                "Reports_Screenshots", "Screenshots");

        if (!Directory.Exists(_screenshotFolderPath))
        {
            Directory.CreateDirectory(_screenshotFolderPath);
        }
    }


    public string CaptureScreenshot(string testName)
    {
        try
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"{testName}_{timestamp}.png";
            string filePath = Path.Combine(_screenshotFolderPath, fileName);

            screenshot.SaveAsFile(filePath);
            return filePath;
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"Error capturing screenshot: {ex.Message}");
            return string.Empty;
        }
    }
}