using System;
using System.Collections.Generic;
using System.Linq;

namespace org.duckdns.buttercup.MaterialSearch
{
    public class ConfigInfo
    {
        private Dictionary<string, bool> selectedConfigs = new Dictionary<string, bool>();
        public ConfigInfo(Target target, IEnumerable<String> configNames = null)
        {
            this.AppliesTo = target;
            this.ConfigNames = configNames;
            
        }
        public Target AppliesTo { get; set; }
        public IEnumerable<String> ConfigNames { get; private set; }

        public IEnumerable<string> SelectedConfigs
        {
            get
            {
                IEnumerable<string> result = null;
                switch (this.AppliesTo)
                {
                    case Target.ALL:
                        result = ConfigNames;
                        break;
                    case Target.CURRENT:
                        break;
                    case Target.SELECTED:
                        result = selectedConfigs.Where(kvp => kvp.Value).ToDictionary(i => i.Key, i => i.Value).Keys;
                        break;
                }
                return result;
            }
            private set { }
        }

            public void selectConfig(string configName)
            {
                selectedConfigs[configName] = true;
            }
    }

    public enum Target
    {
        CURRENT, ALL, SELECTED
    }
}
