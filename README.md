# Automation

## Template solution with implementation of different testing tools for many projects at the same time.

#### The solution is pretty specific, because it was created with the idea of easily changing tools for UI testing - in this case Selenium and Playwright. The idea was created by real-world situation in which you have already written hundreds of tests and you want to implement new testing tool - in this case it was change from Selenium to Playwright. Because of the fact that Playwright is different, asynchronous and offers many built-in solutions, many workarounds were created to provide some kind of "abstract" layer of framework which enabled to use asynchronous Playwright in synchronous way. While not a good practice, the idea itself was a nice learning case, even if ultimately it was abandoned.

#### Things that were done in this solution specifically to address differences between Selenium and Playwright and how to work around them or some useful stuff:
- usage of global `appsettings.json` file for test project instead of `.runsettings` and `BrowserContextOptions` in Playwright (although you could use this in only-Playwright project as well).
- implementation of `IBrowserActions` interface which provides all of the necessary actions used by a browser - each action had to be implemented in both tools and to avoid problems with asynchronous Plawyright methods, every implementation was made synchronous using `.Wait()` and `.Result`. All of it was used instead of using built-in `PageTest` class which Playwright provides.
- instead of using built-in `ILocator` interface for locators, the `XPath` class was created which basically was own implementation of tools that the built-in methods of Playwright provide. The downside was the fact that you have to implement the whole class and write methods for each XPath, but the process of writing tests afterwards was more-or-less like using built-in Playwright's locators methods.
- used POP (Page Object Pattern) in this solution allowed to have an access to every single page object in your test methods by calling the `GetPage<T>` method, which created a new instance of page object each time the action on object was done. This helped with not creating page objects directly in test methods or required you to return page object in other page object's method.
- `TestLogger` class was a useful little tool that allowed to log every action in the console in real time (with the implementation of every `IBrowserActions` method logging itself). Not really necessary with Playwright's built-in debugger tool.

## If you want to run the solution globally:

#### In `UITest` class there is an option to choose which testing tool you want to use. By default Playwright is the main tool, but if you want to change it to Selenium, just uncomment Selenium's browser implementation and comment Playwright's one (although Selenium lacks implementation for `IBrowserActions` interface methods in it's `Browser` class).

#### If you're using Playwright for the first time, remember to build the solution, then navigate to solution folder using PowerShell and execute command:

`pwsh Automation.Core\bin\Debug\net6.0\playwright.ps1 install`