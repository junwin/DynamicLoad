
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicLoad
{
    /// <summary>
    /// Provide access to external objects and the kaitrade facade
    /// </summary>
    public class Factory : IFactory
    {
        /// <summary>
        /// Singleton OrderManager
        /// </summary>
        private static volatile Factory s_instance;

        /// <summary>
        /// used to lock the class during instantiation
        /// </summary>
        private static object s_Token = new object();

        /// <summary>
        /// Logger
        /// </summary>
        public log4net.ILog m_Log = log4net.LogManager.GetLogger("Kaitrade");

        /// <summary>
        /// Main facade used by the app
        /// </summary>
        private IFacade _appFacade = null;

        private List<IFactory> _factories;



        public static Factory Instance()
        {
            // Uses "Lazy initialization" and double-checked locking
            if (s_instance == null)
            {
                lock (s_Token)
                {
                    if (s_instance == null)
                    {
                        s_instance = new Factory();
                    }
                }
            }
            return s_instance;
        }

        protected Factory()
        {
            _factories = new List<IFactory>();
        }

        public IFacade AppFacade
        {
            get
            {
                return _appFacade;
            }
            set
            {
                _appFacade = value;
            }
        }

        /// <summary>
        /// Add some external instance factory that will be used to create algos of a given type - this is used to allow
        /// plugins and other external assemblies register their oen algos
        /// </summary>
        /// <param name="algoType"></param>
        /// <param name="factory"></param>
        public void AddInstanceFactory(IFactory factory)
        {

            _factories.Add(factory);

        }

        /// <summary>
        /// Remove some external factory that is set up to create objects
        /// </summary>
        /// <param name="factory"></param>
        public void RemoveInstanceFactory(IFactory factory)
        {
        }



        /// <summary>
        /// Get a named statistical algo

        private List<string> getInternalTypeNames()
        {
            List<string> names = new List<string>();
            names.Add("KTStandard");

            return names;
        }
        /// <summary>
        /// Get an order routing alg
        /// </summary>
        /// <returns></returns>
        public Object CreateInstance(string myName)
        {
            Object myAlg = null;

            switch (myName)
            {
                case "KTStandard":
                    //myAlg = new ORAlgStandard();
                    break;

                default:
                    break;
            }

            if (myAlg == null)
            {
                foreach (IFactory factory in _factories)
                {
                    try
                    {
                        myAlg = factory.CreateInstance(myName) ;
                    }
                    catch (Exception)
                    {
                    }
                    if (myAlg != null)
                    {
                        break;
                    }
                }
            }
            return myAlg;
        }

        /// <summary>
        /// Return a list of ORStrategyAlgorithmNames
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailableTypeNames()
        {
            List<string> myRet = getInternalTypeNames();

            foreach (IFactory factory in _factories)
            {
                try
                {
                    foreach (string algoName in factory.GetAvailableTypeNames())
                    {
                        myRet.Add(algoName);
                    }
                }
                catch (Exception)
                {
                }
            }
            return myRet;
        }
    }
}
