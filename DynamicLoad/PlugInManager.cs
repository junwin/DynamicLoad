using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace DynamicLoad
{
    public class PlugInManager : IPlugInManager
    {
        private static volatile PlugInManager s_instance;
        private static object s_Token = new object();
        /// <summary>
        /// Logger
        /// </summary>
        public log4net.ILog _plugInLog = log4net.LogManager.GetLogger("PlugIn");
        /// <summary>
        /// list of all our plugins
        /// </summary>

        private Dictionary<string, IPlugin> _plugIns;

        public static PlugInManager Instance()
        {
            // Uses "Lazy initialization" and double-checked locking
            if (s_instance == null)
            {
                lock (s_Token)
                {
                    if (s_instance == null)
                    {
                        s_instance = new PlugInManager();
                    }
                }
            }
            return s_instance;
        }

        protected PlugInManager()
        {
            _plugIns = new Dictionary<string, IPlugin>();
        }

        public void AddPlugIn(string uid, IPlugin plugIn)
        {
            _plugIns.Add(plugIn.Name, plugIn);
            Factory.Instance().AddInstanceFactory(plugIn.Factory);
        }

        /// <summary>
        /// Load a set of plugins given a data binding colection of plugins
        /// </summary>
        /// <param name="plugins"></param>
        /// <returns>number loaded</returns>
        public int LoadPlugins(string uid, List<IPlugInDef> plugins)
        {
            int pluginCount = 0;
            try
            {
                foreach (IPlugInDef  plugIn in plugins)
                {
                    try
                    {
                        if (plugIn.Enabled)
                        {
                            string myFullPath = "";
                            if (plugIn.LoadPath.ToLower().StartsWith("http:"))
                            {
                                myFullPath = plugIn.LoadPath;
                            }
                            else
                            {
                                myFullPath = Path.GetFullPath(plugIn.LoadPath);
                            }
                            _plugInLog.Info("MainForm:Start:load plugin:" + myFullPath);
                            IPlugin myPi = DynamicLoad(uid, myFullPath);
                            if (myPi != null)
                            {
                                AddPlugIn(uid, myPi);
                                pluginCount++;
                            }
                            _plugInLog.Info("MainForm:Start:loadcompleted plugin:" + myFullPath);
                        }
                        else
                        {
                            _plugInLog.Info("MainForm:Start:plugin not enabled:" + plugIn.Name);
                        }
                    }
                    catch (Exception myE)
                    {
                        _plugInLog.Error("Start: processing plugin", myE);
                    }
                }
            }
            catch (Exception myE)
            {
                _plugInLog.Error("LoadPlugins", myE);
            }
            return pluginCount;
        }

        /// <summary>
        /// Start all plugins
        /// </summary>
        /// <param name="uid">system allocated user id</param>
        public void StartAll(string uid)
        {
            try
            {
                // now start all the plugins
                foreach (IPlugin myPi in _plugIns.Values)
                {
                    try
                    {
                         
                        myPi.Start("");
                    }
                    catch (Exception myE)
                    {
                        _plugInLog.Error("StartAll:loop", myE);
                    }
                }
            }
            catch (Exception myE)
            {
                _plugInLog.Error("StartAll", myE);
            }
        }

        /// <summary>
        /// Stop all pluginns
        /// </summary>
        /// <param name="uid"></param>
        public void StopAll(string uid)
        {
            try
            {
                // now start all the plugins
                foreach (IPlugin myPi in _plugIns.Values)
                {
                    try
                    {
                        myPi.Stop();
                    }
                    catch (Exception myE)
                    {
                        _plugInLog.Error("StopAll:loop", myE);
                    }
                }
            }
            catch (Exception myE)
            {
                _plugInLog.Error("StopAll", myE);
            }
        }

       

        /// <summary>
        /// Dynamically load some plugin (visible or non visible)
        /// </summary>
        /// <param name="uid">user id </param>
        /// <param name="path">path to the plugin</param>
        /// <returns></returns>
        public IPlugin DynamicLoad(string uid, string myPath)
        {
            IPlugin myPlugIn = null;
            try
            {
                _plugInLog.Info("DynamicLoad:" + myPath);
                System.Reflection.Assembly myAssy = null;
                if (myPath.ToLower().StartsWith("http:"))
                {
                    myAssy = System.Reflection.Assembly.LoadFrom(myPath);
                }
                else
                {
                    myAssy = System.Reflection.Assembly.LoadFile(myPath);
                }
                
                //System.Reflection.Assembly myAssy = System.Reflection.Assembly.Load(
                System.Type myIF;
                Object oTemp;
                foreach (System.Type myType in myAssy.GetTypes())
                {
                    myIF = myType.GetInterface("IPlugin");
                    if (myIF != null)
                    {
                        // Will call the constructor - guess you need to invoke the instance
                        // method
                        oTemp = myAssy.CreateInstance(myType.ToString());
                        myPlugIn = oTemp as IPlugin;
                        if (myPlugIn != null)
                        {
                            string myName = myPlugIn.Name;

                            // record the plugin in our list
                            //m_PlugIns.Add(myName, myPlugIn);

                            myPlugIn.SetFacade(Factory.Instance().AppFacade);
                            //myPlugIn.SetUserContext(m_UserContext);

                           
                        }
                    }
                }
                return myPlugIn;
            }
            catch (Exception myE)
            {
                _plugInLog.Error("DynamicLoad", myE);
                _plugInLog.Error("DynamicLoad:" + myPath);
                return null;
            }
        }

        public IPlugin GetPlugIn(string uid, string name)
        {
            return _plugIns[name];
        }

        public List<string> GetPlugInNames(string uid)
        {
            List<string> names = new List<string>();
            foreach (string name in _plugIns.Keys)
            {
                names.Add(name);
            }
            return names;
        }
    }
}
