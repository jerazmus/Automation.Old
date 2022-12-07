using NUnit.Framework;

namespace Automation.Core.Utilities
{
    public static class TestLogger
    {
        // Writes live logs during the test run
        public static void Log(string log)
        {
            TestContext.Progress.WriteLine(log);
            TestContext.Out.WriteLine(log);
        }
    }
}
