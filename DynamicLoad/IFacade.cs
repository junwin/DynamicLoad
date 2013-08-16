using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoad
{
    /// <summary>
    /// This models some facade that the host using the dynamic load
    /// may want to expose to the plugins that are loaded. Typically it
    /// is used to provide host function to the plugin.
    /// </summary>
    public interface IFacade
    {
    }
}
