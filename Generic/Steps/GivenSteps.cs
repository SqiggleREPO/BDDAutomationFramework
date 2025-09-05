using TechTalk.SpecFlow;
using Core.TestData;
using Core.Configuration;
using Core.Drivers;
using System;

namespace Generic.Steps
{
    [Binding]
    public class GivenSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly StepHelpers _stepHelpers;

        public GivenSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _stepHelpers = new StepHelpers();
        }

        [Given(@"I am testing ""(.*)""")]
        public void GivenIAmTesting(string projectName)
        {
            // Load project-specific test data
            TestDataManager.Instance.LoadProject(projectName);
            
            // Store project name in scenario context for later use
            _scenarioContext["ProjectName"] = projectName;
            
            // Initialize driver if not already done
            if (DriverManager.Driver == null)
            {
                DriverManager.InitializeDriver("edge");
            }

            Console.WriteLine($"Loaded test data for project: {projectName}");
        }

        [Given(@"I have navigated to the application")]
        public void GivenIHaveNavigatedToTheApplication()
        {
            var projectData = TestDataManager.Instance.CurrentTestData;
            var baseUrl = projectData.GetTestDataValue("BaseUrl");
            _stepHelpers.NavigateToUrl(baseUrl);
        }

        [Given(@"I am on the ""(.*)"" page")]
        public void GivenIAmOnThePage(string pageName)
        {
            _scenarioContext["CurrentPage"] = pageName;
            // Additional navigation logic can be added here
        }
    }
}