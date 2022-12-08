using NUnit.Framework;
using System.Collections.Concurrent;

namespace Automation.Core.Settings
{
    public static class TestSettingsProvider
    {
        private static ConcurrentDictionary<Type, TestSettings> _variables = new();

        // ---------------------------------------

        // Creates instance of TestSettings object based on the settings file ('automation.list')
        public static T Get<T>() where T : TestSettings
        {
            return (T)_variables.GetOrAdd(typeof(T), (type) =>
            {
                var settingsFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "automation.list");
                return (T)Activator.CreateInstance(type, settingsFilePath);
            });
        }
    }
}
