# Automation

## Template projects for automation tests

### Basically all my knowledge throughout the years in one repository for future uses.

### In `UITest` class, there is and option to choose which testing tool you want to use. By default Playwright is the main tool, but if you want to change it to Selenium, just uncomment Selenium's implementation and comment Playwright's one (although Selenium lacks implementation for `IBrowserActions` interface in `Browser` class)

### If you're using Playwright for the first time, remember to navigate to solution folder using PowerShell and execute command:

`pwsh Automation.Core\bin\Debug\net6.0\playwright.ps1 install`