using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class VariableManagerTest
    {
        [Test]
        public void GetEnvironmentVariableTest()
        {
            var manager = new VariableManager();
            Assert.AreEqual(Environment.GetEnvironmentVariable("OS"), manager.GetVariable("OS"));
        }

        [Test]
        public void AddandGetLocalVariableTest()
        {
            var manager = new VariableManager();
            var varName = "someName";
            var varValue = "someValue";
            manager.AddVariable(varName, varValue);
            
            Assert.AreEqual(varValue, manager.GetVariable(varName));
        }
    }
}
