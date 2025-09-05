using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Drivers;
using System;
using System.Threading;

namespace Generic.Steps
{
    [Binding]
    public class ThenSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly StepHelpers _stepHelpers;

        public ThenSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _stepHelpers = new StepHelpers();
        }

        [Then(@"I should see ""(.*)"" element")]
        public void ThenIShouldSeeElement(string elementName)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(elementName.ToLower()))
            {
                var locator = currentPage.Elements[elementName.ToLower()];
                bool isVisible = _stepHelpers.IsElementVisible(locator);
                Assert.IsTrue(isVisible, $"Element '{elementName}' is not visible");
            }
        }

        [Then(@"I should see text ""(.*)""")]
        public void ThenIShouldSeeText(string expectedText)
        {
            var pageSource = DriverManager.Driver.PageSource;
            Assert.IsTrue(pageSource.Contains(expectedText), 
                $"Text '{expectedText}' was not found on the page");
        }

        [Then(@"I take a screenshot named ""(.*)""")]
        public void ThenITakeAScreenshotNamed(string fileName)
        {
            _stepHelpers.TakeScreenshot(fileName);
        }

        [Then(@"wait ""([^""]*)"" seconds")]
        public void ThenWaitSeconds(string secondsText)
        {
            var seconds = int.Parse(secondsText);
            seconds *= 1000;
            Thread.Sleep(seconds);
        }

        [Then(@"textbox ""(.*)"" contains ""(.*)""")]
            public void ThenTextboxContains(string textboxName, string text)
                {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(textboxName.ToLower()))
            {
                var locator = currentPage.Elements[textboxName.ToLower()];
                _stepHelpers.EnterText(locator, text);
            }
        }

                [Then(@"page ""(.*)"" is displayed")]
            public void ThenPageIsDisplayed(string pageName)
                {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(pageName.ToLower()))
            {
                var locator = currentPage.Elements[pageName.ToLower()];
                _stepHelpers.EnterText(locator, pageName);
            }
        }

        private Core.FormBase GetCurrentPage()
        {
            var pageName = _scenarioContext.ContainsKey("CurrentPage")
                ? _scenarioContext["CurrentPage"].ToString()
                : "FirstPage";

            var pageType = System.Reflection.Assembly.Load("AppTargets")
                .GetType($"AppTargets.Forms.{pageName}");

            return (Core.FormBase)Activator.CreateInstance(pageType);
        }
    }
}