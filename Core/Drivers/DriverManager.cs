using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using System.IO;

namespace Core.Drivers
{
    public class DriverManager
    {
        private static IWebDriver _driver;
        private static readonly string DriverPath = @"D:\Drivers";

        public static IWebDriver Driver => _driver;

        public static IWebDriver InitializeDriver(string browserType = "edge")
        {
            if (_driver != null)
                return _driver;

            switch (browserType.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-gpu");
                    var chromeService = ChromeDriverService.CreateDefaultService(DriverPath);
                    _driver = new ChromeDriver(chromeService, chromeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    var firefoxService = FirefoxDriverService.CreateDefaultService(DriverPath);
                    _driver = new FirefoxDriver(firefoxService, firefoxOptions);
                    _driver.Manage().Window.Maximize();
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--start-maximized");
                    var edgeService = EdgeDriverService.CreateDefaultService(DriverPath);
                    _driver = new EdgeDriver(edgeService, edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Browser type '{browserType}' is not supported");
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return _driver;
        }

        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}