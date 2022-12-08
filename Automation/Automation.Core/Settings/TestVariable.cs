namespace Automation.Core.Settings
{
    public class TestVariable : Attribute
    {
        //
        // Creates attribute with Name property to use for variables from settings file
        //

        public string Name { get; set; }

        public TestVariable(string name)
        {
            Name = name;
        }
    }
}
