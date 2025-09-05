using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Core.Drivers;
using System;
using System.IO;

namespace Generic
{
    public class StepHelpers : IStepHelpers
    {
        private IWebDriver Driver => DriverManager.Driver;

        public void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void ClickElement(By locator)
        {
            WaitForElement(locator);
            Driver.FindElement(locator).Click();
        }

        public void EnterText(By locator, string text)
        {
            WaitForElement(locator);
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public string GetElementText(By locator)
        {
            WaitForElement(locator);
            return Driver.FindElement(locator).Text;
        }

        public bool IsElementVisible(By locator)
        {
            try
            {
                var element = Driver.FindElement(locator);
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitForElement(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d => d.FindElement(locator));
        }

        public void TakeScreenshot(string fileName)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var path = Path.Combine("AppSpecFlow", "TestResults", $"{fileName}.png");
            screenshot.SaveAsFile(path);
        }
    }
}