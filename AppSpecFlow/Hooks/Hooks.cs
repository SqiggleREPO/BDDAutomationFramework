using TechTalk.SpecFlow;
using Core.Drivers;
using OpenQA.Selenium;
using System;
using System.IO;

namespace AppSpecFlow.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine($"Starting scenario: {scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                // Take screenshot on failure
                var screenshot = ((ITakesScreenshot)DriverManager.Driver).GetScreenshot();
                var fileName = $"Error_{scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMddHHmmss}";
                var path = Path.Combine("TestResults", $"{fileName}.png");
                screenshot.SaveAsFile(path);
                
                Console.WriteLine($"Test failed. Screenshot saved: {path}");
            }

            // Clean up driver
            DriverManager.CloseDriver();
            
            Console.WriteLine($"Completed scenario: {scenarioContext.ScenarioInfo.Title}");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"Starting feature: {featureContext.FeatureInfo.Title}");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine($"Completed feature: {featureContext.FeatureInfo.Title}");
        }
    }
}