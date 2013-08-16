using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoadTest
{
    [TestClass]
    public class CreateSampleTest
    {
        [TestMethod]
        public void CreateDynamicLoad()
        {
            DynamicLoad.IPlugInManager piManager = DynamicLoad.PlugInManager.Instance();

            Assert.IsNotNull(piManager);
        }

        [TestMethod]
        public void LoadPlugIn()
        {
            DynamicLoad.IPlugInDef piDef = new DynamicLoad.PlugInDef();
            piDef.LoadPath = @"C:\Users\John\Documents\GitHub\DynamicLoad\SamplePlugIn\bin\Debug\SamplePlugIn.dll";
            piDef.Name = "TEST";
            piDef.Enabled = true;

            List<DynamicLoad.IPlugInDef> piDefs = new List<DynamicLoad.IPlugInDef>();
            piDefs.Add(piDef);

            DynamicLoad.IPlugInManager piManager = DynamicLoad.PlugInManager.Instance();

            Assert.IsNotNull(piManager);

            int count = piManager.LoadPlugins("john", piDefs);
            Assert.IsTrue(1 == count);

            List<string> typeNames = DynamicLoad.Factory.Instance().GetAvailableTypeNames();

            Assert.IsTrue(typeNames.Count == 2);

            Object workClass = DynamicLoad.Factory.Instance().CreateInstance(typeNames[1]);

            Assert.IsNotNull(workClass);

            
        }
    }
}
