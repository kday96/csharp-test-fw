using AutomationTests.Framework.Reporting;

[SetUpFixture]
public static class GlobalSetup
{
    [OneTimeSetUp]
    public static void Setup()
    {
        MyExtentReport.InitialiseReport();
    }

    [OneTimeTearDown]
    public static void TearDown()
    {
        MyExtentReport.CloseReport();
    }
}