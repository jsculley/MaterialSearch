using System;
using System.Xml.Linq;
namespace org.duckdns.buttercup.MaterialSearch
{
    /// <summary>
    /// A wrapper around a name and XML element from a material database file
    /// </summary>
    public class MaterialDatabaseDescriptor : IComparable
    {
        /// <summary>
        /// The backing object for the Database property
        /// </summary>
        private XElement materialDatabaseElement;
        public MaterialDatabaseDescriptor(string name, XElement materialDatabaseElement)
        {
            this.Name = name;
            this.Database = materialDatabaseElement;
        }
        public string Name { get; private set; }
        public XElement Database { get; private set; }
        public int CompareTo(object obj)
        {
            MaterialDatabaseDescriptor mdd = obj as MaterialDatabaseDescriptor;
            return this.Name.CompareTo(mdd.Name);
        }

        public override String ToString()
        {
            return this.Name;
        }
    }
}
