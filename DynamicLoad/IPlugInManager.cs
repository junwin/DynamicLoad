using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicLoad
{
    /// <summary>
    /// Defines the behavior of a plugin manager, that can load
    /// and manage plugin for use in the host system
    /// </summary>
    public interface IPlugInManager
    {
        /// <summary>
        /// Load a set of plugins given a data binding colection of plugins
        /// </summary>
        /// <param name="plugins"></param>
        /// <returns>number loaded</returns>
        int LoadPlugins(string uid, List<IPlugInDef> plugins);

        /// <summary>
        /// Start all plugins
        /// </summary>
        /// <param name="uid">system allocated user id</param>
        void StartAll(string uid);

        /// <summary>
        /// Stop all pluginns
        /// </summary>
        /// <param name="uid"></param>
        void StopAll(string uid);

        /// <summary>
        /// Dynamically load some plugin (visible or non visible)
        /// </summary>
        /// <param name="uid">user id </param>
        /// <param name="path">path to the plugin</param>
        /// <returns></returns>
        IPlugin DynamicLoad(string uid, string path);

        
        /// <summary>
        /// Return a list of the current plugins
        /// </summary>
        /// <returns></returns>
        List<string> GetPlugInNames(string uid);

        /// <summary>
        /// Get the plugin using its name
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IPlugin GetPlugIn(string uid, string name);

        /// <summary>
        /// Add a plugin to the manager
        /// </summary>
        /// <param name="plugIn"></param>
        void AddPlugIn(string uid, IPlugin plugIn);
    }
}
