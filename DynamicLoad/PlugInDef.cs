using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoad
{
    public class PlugInDef : IPlugInDef
    {
        public string LoadPath { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }

    }
}
