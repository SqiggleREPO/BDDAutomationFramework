using System;
using System.Collections.Generic;

namespace Core.Configuration
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private static readonly object _lock = new object();
        private IProjectConfig _currentConfig;

        private ConfigManager() { }

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public IProjectConfig CurrentConfig => _currentConfig;

        public void SetProject(IProjectConfig projectConfig)
        {
            _currentConfig = projectConfig;
        }
    }
}