using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Core.Drivers;
using AppTargets.Forms;
using System;
using System.Reflection;

namespace Generic.Steps
{
    [Binding]
    public class WhenSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly StepHelpers _stepHelpers;

        public WhenSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _stepHelpers = new StepHelpers();
        }

        [When(@"I click on ""(.*)"" element")]
        public void WhenIClickOnElement(string elementName)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(elementName.ToLower()))
            {
                var locator = currentPage.Elements[elementName.ToLower()];
                _stepHelpers.ClickElement(locator);
            }
            else
            {
                throw new InvalidOperationException($"Element '{elementName}' not found on current page");
            }
        }

        [When(@"I enter ""(.*)"" into ""(.*)"" field")]
        public void WhenIEnterIntoField(string text, string fieldName)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(fieldName.ToLower()))
            {
                var locator = currentPage.Elements[fieldName.ToLower()];
                _stepHelpers.EnterText(locator, text);
            }
        }

        [When(@"I wait for ""(.*)"" seconds")]
        public void WhenIWaitForSeconds(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        private Core.FormBase GetCurrentPage()
        {
            var pageName = _scenarioContext.ContainsKey("CurrentPage") 
                ? _scenarioContext["CurrentPage"].ToString() 
                : "FirstPage";

            var pageType = Assembly.Load("AppTargets")
                .GetType($"AppTargets.Forms.{pageName}");

            if (pageType == null)
            {
                throw new InvalidOperationException($"Page class '{pageName}' not found");
            }

            return (Core.FormBase)Activator.CreateInstance(pageType);
        }
    }
}