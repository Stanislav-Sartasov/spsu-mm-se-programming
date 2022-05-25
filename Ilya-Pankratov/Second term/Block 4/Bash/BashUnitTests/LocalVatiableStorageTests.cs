using System;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class LocalVariableStorageTests
    {
        [Test]
        public void AddAndGetTest()
        {
            var storage = new LocalVariablesStorage();
            var varName = "someVar";
            var varValue = "someValue";
            
            Assert.DoesNotThrow(() => storage.Add(varName, varValue, false));
            Assert.AreEqual(varValue, storage.Get(varName));
        }

        [Test]
        public void ChanheConstVariableTest()
        {
            var storage = new LocalVariablesStorage();
            var varName = "someConstVar";
            var varValue = "someConstValue";
            var secondVarValue = "someNewValue";

            Assert.DoesNotThrow(() => storage.Add(varName, varValue, true));
            Assert.DoesNotThrow(() => storage.Add(varName, secondVarValue, false));
            Assert.AreEqual(varValue, storage.Get(varName));
        }
    }
}