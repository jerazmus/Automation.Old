using Automation.Core.Settings;

namespace Automation.TestApp.Core.Settings
{
    public class TestAppTestSettings : TestSettings
    {
        public TestAppTestSettings(string settingsFilePath) : base(settingsFilePath)
        {
        }

        // 
        // Variables that take values from automation.list file
        //

        [TestVariable("SL_URL")]
        public string Url { get; set; }
    }
}
