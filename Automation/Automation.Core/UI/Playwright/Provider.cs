using Automation.Core.Utilities;
using Microsoft.Playwright;

namespace Automation.Core.UI.Playwright
{
    public abstract class Provider
    {
        private static object _synchronizationObject = new();
        private static IPlaywright _playwright;
        private static IBrowser _browser;

        public static IBrowserContext Create(bool headless)
        {
            TestLogger.Log("Create Playwright Browser");

            lock (_synchronizationObject)
            {
                if (_playwright == null)
                {
                    _playwright = Microsoft.Playwright.Playwright.CreateAsync().Result;
                    _browser = _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        IgnoreDefaultArgs = new[] { "--disable-extensions", "--auth-server-whitelist" },
                        Headless = headless
                    }).Result;
                }

                return _browser.NewContextAsync(new BrowserNewContextOptions()
                {
                    ViewportSize = new ViewportSize()
                    {
                        Width = 1800,
                        Height = 900
                    },
                    IgnoreHTTPSErrors = true
                }).Result;
            }
        }
    }
}
