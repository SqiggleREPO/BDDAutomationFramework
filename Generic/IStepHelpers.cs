using OpenQA.Selenium;

namespace Generic
{
    public interface IStepHelpers
    {
        void NavigateToUrl(string url);
        void ClickElement(By locator);
        void EnterText(By locator, string text);
        string GetElementText(By locator);
        bool IsElementVisible(By locator);
        void WaitForElement(By locator, int timeoutSeconds = 10);
        void TakeScreenshot(string fileName);
    }
}