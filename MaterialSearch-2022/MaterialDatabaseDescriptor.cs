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
