using Microsoft.Extensions.Configuration;

namespace Automation.Core.Settings
{
    public static class TestSettingsProvider
    {
        public static TestSettings Get()
            => GetAppSettings();

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
