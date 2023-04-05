using Automation.Core.Settings;
using Automation.Core.UI;
using Automation.TestApp.Core.Settings;
using Automation.TestApp.Core.UI.Pages;

namespace Automation.TestApp.Core.UI
{
    public abstract class TestAppUITest : UITest
    {
        // Get variables from SwagLabsTestSettings in a form of dictionary
        public TestAppTestSettings Settings { get; } = TestSettingsProvider.Get<TestAppTestSettings>();

        // ---------------------------------------

        // Creates instancce of SwagLabs page, based on passed URL
        public T GetSwagLabsPage<T>() where T : Page
        {
            return GetPage<T>(Settings.Url);
        }

        // ---------------------------------------

        //
        // Instances of any page in SwagLabs project
        //

        protected LoginPage LoginPage
            => GetSwagLabsPage<LoginPage>();
    }
}
