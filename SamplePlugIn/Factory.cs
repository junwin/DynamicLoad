using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePlugIn
{
    public class Factory : DynamicLoad.IFactory
    {
        public object CreateInstance(string typeName)
        {
            if (typeName == "SampleWorkClass")
            {
                return new SampleWorkClass();
            }
            else
            {
                return null;
            }
        }

        public List<string> GetAvailableTypeNames()
        {
            List<string> names = new List<string>();
            names.Add("SampleWorkClass");
            return names;
        }
    }
}
