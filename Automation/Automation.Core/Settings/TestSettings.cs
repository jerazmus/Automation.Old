using Newtonsoft.Json;

namespace Automation.Core.Settings
{
    public class TestSettings
    {
        // Model for TestSettings section of appsettings.json file

        [JsonProperty("Headless")]
        public bool Headless { get; set; }

        [JsonProperty("TestApp")]
        public TestAppSettings TestApp { get; set; }
    }

    public class TestAppSettings
    {
        // Model for application section of appsettings.json file

        [JsonProperty("UiUrl")]
        public string UiUrl { get; set; }

        [JsonProperty("ApiUrl")]
        public string ApiUrl { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }

    public class Root
    {
        // Model for root element of appsettings.json file

        [JsonProperty("TestSettings")]
        public TestSettings TestSettings { get; set; }
    }
}
