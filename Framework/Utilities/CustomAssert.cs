using System.Drawing;
using AventStack.ExtentReports;

namespace AutomationTests.Framework.Utilities;

public class CustomAssert
{
    private readonly ExtentTest? _test;

    public CustomAssert(ExtentTest? test)
    {
        _test = test;
    }

    public void That(bool condition, string message)
    {
        var node = _test?.CreateNode($"Assert that {message}");

        try
        {
            Assert.That(condition, message);
            node?.Pass($"{message}");
        }
        catch (AssertionException ex)
        {
            node?.Fail($"Assertion Failed: {message} - {ex.Message}");
            throw;
        }
    }

    public void TextEquals(string element, string expected, string actual)
    {
        var message = $"Assert text in {element} equals {expected}.";
        var node = _test?.CreateNode(message);

        try
        {
            Assert.That(expected == actual, message);
            node?.Pass($"Expected: {expected}, Actual: {actual}");
        }
        catch (AssertionException ex)
        {
            node?.Fail($"Expected: {expected}, Actual: {actual} - {ex.Message}");
            throw;
        }
    }

    public void TextContains(string element, string expectedSubstring, string actual)
    {
        var message = $"Assert text in {element} contains {expectedSubstring}.";
        var node = _test?.CreateNode(message);

        try
        {
            Assert.That(actual.Contains(expectedSubstring), message);
            node?.Pass($"Actual text: {actual} contains Expected substring: {expectedSubstring}");
        }
        catch (AssertionException ex)
        {
            node?.Fail($"Actual text: {actual} does not contain Expected substring: {expectedSubstring} - {ex.Message}");
            throw;
        }
    }

    public void IsTrue(bool condition, string message)
    {
        That(condition, message);
    }

    public void IsFalse(bool condition, string message)
    {
        That(!condition, message);
    }

    public void Log(string message)
    {
        var node = _test?.CreateNode(message);
        node?.Pass(message);
    }
}