namespace Automation.Core.Utilities
{
    public static class XPath
    {
        // Returns XPath with containing text, with additional parameter for double quoatation in text
        public static string TextContains(string text, bool doubleQuotation = false)
            => doubleQuotation
                ? $"//*[contains(.,\"{text}\")]"
                : $"//*[contains(.,'{text}')]";

        // Returns XPath based on data-test name or ID
        public static string Element(string name)
            => $"//*[@data-test='{name}' or @id='{name}']";

        // Returns XPath based on class
        public static string ElementClass(string className)
            => $"//*[contains(@class, '{className}')]";
    }
}
