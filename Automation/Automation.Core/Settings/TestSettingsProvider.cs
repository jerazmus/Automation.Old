using Microsoft.Extensions.Configuration;

namespace Automation.Core.Settings
{
    public static class TestSettingsProvider
    {
        // Returns an object with all values for test settings imported from appsettings.json file
        public static TestSettings Get()
            => GetAppSettings();

        // Simpy gets all values from appsettings.json file based on TestSettings object mapping
        private static TestSettings GetAppSettings()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var testSettings = config.GetSection("TestSettings").Get<TestSettings>();
            config.GetSection("TestSettings").Bind(testSettings);
            return testSettings;
        }
    }
}
