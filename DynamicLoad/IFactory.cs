using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoad
{
    /// <summary>
    /// A general purpose factory used to create instances of
    /// obects in a plugin. All plugins must provide a factory
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create an instance of some named type - returns
        /// null if the name is not regognized
        /// </summary>
        /// <param name="typeName">name of the type to create</param>
        /// <returns>returns null if the name is not regognized</returns>
        object CreateInstance(string typeName);

        /// <summary>
        /// provide a list of the names of types that the factory
        /// can create an object for.
        /// </summary>
        /// <returns>list of names that the factory can create</returns>
        List<string> GetAvailableTypeNames();
    }
}
