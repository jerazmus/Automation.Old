namespace Automation.Core.UI
{
    public abstract class Component
    {
        protected IBrowserActions Browser { get; private set; }

        // ---------------------------------------

        public Component(IBrowserActions browser)
        {
            Browser = browser;
        }
    }
}
