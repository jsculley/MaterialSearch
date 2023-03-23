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
    public class TargetInfo
    {
        public TargetInfo(ModelDoc2 targetDoc, List<string> targetConfigs)
        {
            this.TargetDoc = targetDoc;
            this.TargetConfigs = targetConfigs;
            
        }
        public ModelDoc2 TargetDoc { get; set; }
        public List<string> TargetConfigs { get; set; }
        //public Dictionary<string, string> ConfigData { get; private set; }

        public bool isDerived(string configName)
        {
            Configuration c = TargetDoc.GetConfigurationByName(configName);
            return c.IsDerived();
        }

        /// <summary>
        /// Check if a config is an entry in a design table
        /// </summary>
        /// <param name="configName">the name of the configuration to check</param>
        /// <returns><b>true</b> if the configuration is in the design table, <b>false</b> otherwise</returns>
        /// This method cannot work effectively until
        /// SPR 289114 -- Would like API to tell whether configuration is driven by Design Table is implemented
        /// so we just return false for now
        public bool isDesignTableConfig(string configName)
        {
            return false;
        }
    }
}
