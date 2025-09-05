using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Drivers;
using System;

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

        [Then(@"the ""(.*)"" field should contain ""(.*)""")]
        public void ThenTheFieldShouldContain(string fieldName, string expectedText)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(fieldName.ToLower()))
            {
                var locator = currentPage.Elements[fieldName.ToLower()];
                var actualText = _stepHelpers.GetElementText(locator);
                Assert.AreEqual(expectedText, actualText, 
                    $"Field '{fieldName}' does not contain expected text");
            }
        }

        [Then(@"I take a screenshot named ""(.*)""")]
        public void ThenITakeAScreenshotNamed(string fileName)
        {
            _stepHelpers.TakeScreenshot(fileName);
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