/*
The MIT License (MIT)

Copyright(c) 2022, Jim Sculley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace org.duckdns.buttercup.MaterialSearch
{
    public class ConfigInfo
    {
        private Dictionary<string, bool> selectedConfigs = new Dictionary<string, bool>();
        public ConfigInfo(ModelDoc2 targetDoc, Target target, IEnumerable<String> configNames = null)
        {
            this.TargetDoc = targetDoc;
            this.AppliesTo = target;
            this.ConfigNames = configNames;
            
        }
        public ModelDoc2 TargetDoc { get; set; }
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
