using Newtonsoft.Json;

namespace Automation.Core.Settings
{
    public class TestSettings
    {
        [JsonProperty("Headless")]
        public bool Headless { get; set; }

        [JsonProperty("TestApp")]
        public TestAppSettings TestApp { get; set; }
    }

    public class TestAppSettings
    {
        [JsonProperty("UiUrl")]
        public string UiUrl { get; set; }

        [JsonProperty("ApiUrl")]
        public string ApiUrl { get; set; }
    }

    public class Root
    {
        [JsonProperty("TestSettings")]
        public TestSettings TestSettings { get; set; }
    }
}
