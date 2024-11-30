using Newtonsoft.Json;

namespace CityPowerAndLight.Config;

public static class AppConfig
{
    public static void ParseAndSetEnvironmentVariables()
    {
        using StreamReader reader = new("Config/env.json");
        var json = reader.ReadToEnd();
        var vars = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        foreach ((string key, string value) in vars)
        {
            Console.WriteLine(key);
            Environment.SetEnvironmentVariable(key, value);
        }
    }

}