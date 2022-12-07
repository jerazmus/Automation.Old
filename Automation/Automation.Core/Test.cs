using Automation.Core.Utilities;
using NUnit.Framework;

namespace Automation.Core
{
    public abstract class Test
    {
        // Runs before every test
        [SetUp]
        public void BeforeTest()
        {
            TestLogger.Log($"Start test: {DateTime.Now}");
        }

        // Runs after every test
        [TearDown]
        public void AfterTest()
        {
            TestLogger.Log($"End test: {DateTime.Now}");
            TestLogger.Log($"{TestContext.CurrentContext.Result.Outcome.Status}");
        }
    }
}
