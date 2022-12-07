namespace Automation.Core.Settings
{
    public class TestVariable : Attribute
    {
        public string Name { get; set; }

        public TestVariable(string name)
        {
            Name = name;
        }
    }
}
