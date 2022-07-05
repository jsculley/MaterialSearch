using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace org.duckdns.buttercup.MaterialSearch
{
    public class MaterialSearchResult
    {
        public string Library {get;}
        public string MaterialName { get; }
        public string Description { get;}

        public MaterialSearchResult(string library, string name, string description)
        {
            this.Library = library;
            this.MaterialName = name;
            this.Description = description;
        }
    }
}
