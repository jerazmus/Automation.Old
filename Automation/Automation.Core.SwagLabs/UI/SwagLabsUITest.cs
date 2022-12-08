using Automation.Core.Settings;
using Automation.Core.SwagLabs.Settings;
using Automation.Core.SwagLabs.UI.Pages;
using Automation.Core.UI;

namespace Automation.Core.SwagLabs.UI
{
    public abstract class SwagLabsUITest : UITest
    {
        // Get variables from SwagLabsTestSettings in a form of dictionary
        public SwagLabsTestSettings Settings { get; } = TestSettingsProvider.Get<SwagLabsTestSettings>();

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
