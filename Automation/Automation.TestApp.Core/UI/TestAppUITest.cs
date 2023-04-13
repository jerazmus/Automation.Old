using Automation.Core.Settings;
using Automation.Core.UI;
using Automation.TestApp.Core.Settings;
using Automation.TestApp.Core.UI.Pages;

namespace Automation.TestApp.Core.UI
{
    public abstract class TestAppUITest : UITest
    {
        // Get variables from TestAppTestSettings in a form of dictionary
        public TestAppTestSettings Settings { get; } = TestSettingsProvider.Get<TestAppTestSettings>();

        // ---------------------------------------

        // Creates instancce of TestApp page, based on passed URL
        public T GetTestAppPage<T>() where T : Page
        {
            return GetPage<T>(Settings.Url);
        }

        // ---------------------------------------

        //
        // Instances of any page in TestApp project
        //

        protected LoginPage LoginPage
            => GetTestAppPage<LoginPage>();
    }
}
