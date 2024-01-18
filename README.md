# Automation

## Template projects for API and UI automation tests (with both Playwright and Selenium implementation)

### Basically my knowledge gathered throughout the years in one repository.

#### The solution is pretty specific, because it was created with the idea of easily changing tools for UI testing - in this case Selenium and Playwright. Because of that, many classes are created to provide some kind of "abstract" layer of framework, instead of using default built-in methods, e.g. Playwright's locators (to manage both tools, framework is based on using XPaths, however it provides `XPath` class to make it easier).

#### Also, instead of using default asynchronous Playwright's methods, the framework is created to allow using synchronous implementation of Selenium - that's why implementation of `IBrowserActions` methods returns interface type itself instead of `Task`. **This is not recommended, as well as using XPaths with Playwright**, however it allows to easily switch between tool's implementation without a need to rewrite test methods and helps with method chaining.

### In `UITest` class there is an option to choose which testing tool you want to use. By default Playwright is the main tool, but if you want to change it to Selenium, just uncomment Selenium's browser implementation and comment Playwright's one (although Selenium lacks implementation for `IBrowserActions` interface methods in it's `Browser` class).

### If you're using Playwright for the first time, remember to build the solution, then navigate to solution folder using PowerShell and execute command:

`pwsh Automation.Core\bin\Debug\net6.0\playwright.ps1 install`

### For API tests there is a small API application prepared in the `Application/` solution. Further information about setting it up is in it's README file.