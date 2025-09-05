using System.Collections.Generic;
namespace Core.Configuration
{
    public interface IProjectConfig
    {
        string ProjectName { get; }
        string BaseUrl { get; }
        string Environment { get; }
        Dictionary<string, string> GetConfigurationValues();
    }
}