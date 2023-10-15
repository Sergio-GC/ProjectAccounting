using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsServiceBLL
{
    public sealed class ConfigManager
    {
        private static readonly ConfigManager instance = new ConfigManager();
        private int _defaultDays;

        static ConfigManager() { }
        private ConfigManager()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            _defaultDays = 5;//int.Parse(Environment.GetEnvironmentVariable("DEFAULT_DAYS"));
        }

        public static ConfigManager Instance
        {
            get
            {
                return instance;
            }
        }

        public int defaultDays
        {
            get { return _defaultDays; }
        }
    }
}
