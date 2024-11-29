using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Models.Common
{
    public class ClassFolderMap
    {
        public string _name { get; set; }
        public string _namespace { get; set; }
        public ClassFolderMap(string name, string namespacevalue)
        {
            _name = name;
            _namespace = namespacevalue;
        }
    }
}
