using System.Text.Json;

namespace AutomationTests.Config;

public static class ConfigReader
{
    private static readonly string settingsPath = 
        Path.Combine(AppContext.BaseDirectory, "Config" , "appsettings.json");
    public static AppSettings? Settings { get; private set; }

    static ConfigReader()
    {
        LoadSettings();
    }

    private static void LoadSettings()
    {
        var json = File.ReadAllText(settingsPath);
        Settings = JsonSerializer.Deserialize<AppSettings>(json);
    }
}