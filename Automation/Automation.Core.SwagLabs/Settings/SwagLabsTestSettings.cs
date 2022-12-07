using Automation.Core.Settings;

namespace Automation.Core.SwagLabs.Settings
{
    public class SwagLabsTestSettings : TestSettings
    {
        // 
        // Variables that take values from automation.list file
        //

        [TestVariable("SL_URL")]
        public string Url { get; set; }

        [TestVariable("SL_STANDARD_USER")]
        public string StandardUser { get; set; }

        [TestVariable("SL_LOCKED_OUT_USER")]
        public string LockedOutUser { get; set; }

        [TestVariable("SL_PROBLEM_USER")]
        public string ProblemUser { get; set; }

        [TestVariable("SL_PERFORMANCE_GLITCH_USER")]
        public string PerformanceGlitchUser { get; set; }

        [TestVariable("SL_PASSWORD")]
        public string Password { get; set; }
    }
}
