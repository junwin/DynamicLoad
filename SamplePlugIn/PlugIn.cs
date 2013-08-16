using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePlugIn
{
    public class PlugIn : DynamicLoad.IPlugin
    {
        Factory _factory;
        public PlugIn()
        {
            _factory = new Factory();
        }

        public string Name
        {
            get { return "SamplePlugIn"; }
        }

        public string GroupName
        {
            get { return "Test"; }
        }

        public string VendorName
        {
            get { return "TestVendor"; }
        }

        public DynamicLoad.IFactory Factory
        {
            get { return _factory; }
        }

        public string ID
        {
            get { return System.Guid.NewGuid().ToString(); }
        }

        public void SetFacade(DynamicLoad.IFacade facade)
        {
            
        }

        public void SetUserContext(DynamicLoad.IUser user)
        {
            //throw new NotImplementedException();
        }

        public void Start(string myState)
        {
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
