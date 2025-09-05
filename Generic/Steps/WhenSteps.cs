using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Core.Drivers;
using AppTargets.Forms;
using System;
using System.Reflection;
using System.Threading;

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

        [When(@"I enter ""(.*)"" in textbox ""(.*)""")]
        public void WhenIEnterInTextbox(string text, string textboxName)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(textboxName.ToLower()))
            {
                var locator = currentPage.Elements[textboxName.ToLower()];
                _stepHelpers.EnterText(locator, text);
            }
        }

        [When(@"I wait for ""([^""]*)"" seconds")]
        public void WhenIWaitForSeconds(string secondsText)
        {
            var seconds = int.Parse(secondsText);
            seconds *= 1000;
            Thread.Sleep(seconds);
        }


        [When(@"I click button ""(.*)""")]
        public void WhenIClickButton(string buttonName)
        {
            var currentPage = GetCurrentPage();
            if (currentPage.Elements.ContainsKey(buttonName.ToLower()))
            {
                var locator = currentPage.Elements[buttonName.ToLower()];
                _stepHelpers.ClickElement(locator);
            }
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