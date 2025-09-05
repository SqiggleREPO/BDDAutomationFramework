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
                try
                {
                    // Take screenshot on failure
                    var screenshot = ((ITakesScreenshot)DriverManager.Driver).GetScreenshot();
                    
                    // Clean up scenario title for file name (remove invalid characters)
                    var cleanTitle = string.Join("_", scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));
                    var fileName = $"Error_{cleanTitle}_{DateTime.Now:yyyyMMddHHmmss}";
                    
                    // Ensure TestResults directory exists
                    var testResultsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");
                    if (!Directory.Exists(testResultsDir))
                    {
                        Directory.CreateDirectory(testResultsDir);
                    }
                    
                    var path = Path.Combine(testResultsDir, $"{fileName}.png");
                    screenshot.SaveAsFile(path);
                    
                    Console.WriteLine($"Test failed. Screenshot saved: {path}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save screenshot: {ex.Message}");
                }
            }

            // Clean up driver
            try
            {
                DriverManager.CloseDriver();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing driver: {ex.Message}");
            }
            
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