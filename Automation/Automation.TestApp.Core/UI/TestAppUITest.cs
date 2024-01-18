using Automation.Core.Settings;
using Automation.Core.UI;
using Automation.TestApp.Core.UI.Pages;

namespace Automation.TestApp.Core.UI
{
    public abstract class TestAppUITest : UITest
    {
        // Get variables from TestAppTestSettings in a form of dictionary
        public TestSettings Settings { get; } = TestSettingsProvider.Get();

        // ---------------------------------------

        // Creates instancce of TestApp page, based on passed URL
        public T GetTestAppPage<T>() where T : Page
        {
            return GetPage<T>(Settings.TestApp.UiUrl);
        }

        // ---------------------------------------

        //
        // Instances of any page in TestApp project
        //

        protected LoginPage LoginPage
            => GetTestAppPage<LoginPage>();
    }
}
