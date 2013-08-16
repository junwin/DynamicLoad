using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicLoad
{

    /// <summary>
    /// Defines the behavior of a plugin - any plugin that
    /// the dynamic load library can process must support
    /// this interface
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Name or ID of the plugin provider
        /// </summary>
        string VendorName { get;}

        /// <summary>
        /// The plugin's factory will be used to create any
        /// objects that the plugin will support
        /// </summary>
        IFactory Factory { get; }

        /// <summary>
        /// Get the plugin's unique ID should be GUID
        /// </summary>
        string ID
        { get;}

       
        

        /// <summary>
        /// Set the APP Facade in the plugin - this can be used to give the plugin
        /// access to function in the host, though in this version the facade is not
        /// used
        /// </summary>
        /// <param name="facade"></param>
        void SetFacade(IFacade facade);

        

        /// <summary>
        /// Set the plugin user - cn be extended to provide simple 
        /// permissioning
        /// </summary>
        /// <param name="user"></param>
        void SetUserContext(IUser user);

        /// <summary>
        /// Start the plugin - you should throw an exception if this fails, the
        /// exception will then be logged.
        /// </summary>
        /// <param name="myState"></param>
        void Start(string myState);

        /// <summary>
        /// Stop the plugin- you should throw an exception if this fails, the
        /// exception will then be logged.
        /// </summary>
        void Stop();
    }


    /// <summary>
    /// Provides information about the type of plugin
    /// </summary>
    public enum GenericType { product, lib, none };

    
}
