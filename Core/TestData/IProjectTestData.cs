using System.Collections.Generic;
namespace Core.TestData
{
    public interface IProjectTestData
    {
        string ProjectName { get; }
        Dictionary<string, string> GetUserCredentials();
        Dictionary<string, object> GetTestData(string dataSetName);
        string GetTestDataValue(string key);
    }
}