using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarManager;

namespace VarManagerTests
{
    [TestClass]
    public class VarTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            string testKey = "testKey";
            string testValue = "testValue";

            Var var = new Var(testKey, testValue);

            Assert.AreEqual(testKey, var.Key);
            Assert.AreEqual(testValue, var.Value);
        }
    }
}