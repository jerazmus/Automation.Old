using Automation.Core.Utilities;

namespace Automation.Core.Settings
{
    public class TestSettings
    {
        public TestSettings(string settingsFilePath)
        {
            var fileVariables = ReadSettingsFile(settingsFilePath);
            SetVariables(fileVariables);
        }

        // ---------------------------------------

        // Read settings file (for this project 'automation.list') and create dictionary with variables
        private Dictionary<string, string> ReadSettingsFile(string settingsFilePath)
        {
            var settings = new Dictionary<string, string> ();

            if (File.Exists(settingsFilePath))
            {
                var lines = File.ReadAllLines(settingsFilePath);
                foreach (var line in lines)
                {
                    var equalSign = line.IndexOf('=');
                    if (equalSign < 1)
                    {
                        continue;
                    }
                    
                    var key = line.Substring(0, equalSign);
                    var value = line.Substring(equalSign + 1);

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }
                    settings.Add(key, value);
                }
            }
            return settings;
        }

        // Get variable value, first try to get it from environmental variables, then settings file
        private string GetVariableValue(Dictionary<string, string> fileVariables, string variableName)
        {
            var environmentVarible = Environment.GetEnvironmentVariable(variableName);
            if (!string.IsNullOrWhiteSpace(environmentVarible))
            {
                TestLogger.Log($"Reading environment variable {variableName}");
                return environmentVarible;
            }
            if (fileVariables.ContainsKey(variableName))
            {
                TestLogger.Log($"Reading file variable {variableName}");
                return fileVariables[variableName];
            }
            TestLogger.Log($"Cannot find variable {variableName}");
            return string.Empty;
        }
         
        // Set values for variables based on object with attribute 'TestVariable', assign boolean value if possible, else get value from environment or file
        private void SetVariables(Dictionary<string, string> fileVariables) 
        {
            foreach (var property in GetType().GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(TestVariable), true);
                if (attributes.Length < 0)
                {
                    continue;
                }

                var variableName = ((TestVariable)attributes[0]).Name;
                var variableValue = (object)null;

                if(property.PropertyType.IsAssignableFrom(typeof(bool)))
                {
                    variableValue = bool.Parse(GetVariableValue(fileVariables, variableName));
                }
                else
                {
                    variableValue = GetVariableValue(fileVariables, variableName);
                }
                property.SetValue(this, variableValue);
            }
        }
    }
}
