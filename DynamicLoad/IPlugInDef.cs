using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoad
{
    /// <summary>
    /// provides a definition of plugin in terms of its name and
    /// location
    /// </summary>
    public interface IPlugInDef
    {
        /// <summary>
        /// Load path can be the local file system or some URL
        /// </summary>
        string LoadPath { get; set; }

        /// <summary>
        /// Defines if a plugin is enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Specifies the name of the plugin
        /// </summary>
        string Name { get; set; }
    }
}
