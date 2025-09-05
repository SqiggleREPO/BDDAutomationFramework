using Core.TestData;
using System.Collections.Generic;

namespace ProjectSpecific.Project1
{
    public class Project1TestData : IProjectTestData
    {
        public string ProjectName => "Project1";

        private readonly Dictionary<string, string> _testDataValues;
        private readonly Dictionary<string, object> _dataSets;

        public Project1TestData()
        {
            _testDataValues = new Dictionary<string, string>
            {
                { "BaseUrl", "https://www.saucedemo.com" },
                { "AdminUsername", "standard_user" },
                { "AdminPassword", "secret_sauce" },
                { "UserUsername", "standard_user" },
                { "UserPassword", "secret_sauce" },
                { "ApiEndpoint", "https://api.project1.example.com/v1" }
            };

            _dataSets = new Dictionary<string, object>
            {
                { "ValidUsers", new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string> { { "username", "admin" }, { "password", "pass123" } },
                        new Dictionary<string, string> { { "username", "user1" }, { "password", "pass456" } }
                    }
                },
                { "InvalidUsers", new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string> { { "username", "invalid" }, { "password", "wrong" } }
                    }
                }
            };
        }

        public Dictionary<string, string> GetUserCredentials()
        {
            return new Dictionary<string, string>
            {
                { "admin", _testDataValues["AdminPassword"] },
                { "user1", _testDataValues["UserPassword"] }
            };
        }

        public Dictionary<string, object> GetTestData(string dataSetName)
        {
            if (_dataSets.ContainsKey(dataSetName))
            {
                return new Dictionary<string, object> { { dataSetName, _dataSets[dataSetName] } };
            }
            return new Dictionary<string, object>();
        }

        public string GetTestDataValue(string key)
        {
            return _testDataValues.ContainsKey(key) ? _testDataValues[key] : null;
        }
    }
}