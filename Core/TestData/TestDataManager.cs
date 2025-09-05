using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.TestData
{
    public class TestDataManager
    {
        private static TestDataManager _instance;
        private static readonly object _lock = new object();
        private IProjectTestData _currentTestData;

        private TestDataManager() { }

        public static TestDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TestDataManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public IProjectTestData CurrentTestData => _currentTestData;

        public void LoadProject(string projectName)
        {
            string className = $"ProjectSpecific.{projectName}.{projectName}TestData";
            Type type = Assembly.Load("ProjectSpecific").GetType(className);
            
            if (type == null)
            {
                throw new NotImplementedException($"Test data class not found for project: {projectName}");
            }

            _currentTestData = (IProjectTestData)Activator.CreateInstance(type);
        }
    }
}