using Bash.BashComponents;
using Bash.LocalVariables;
using NUnit.Framework;

namespace BashTests
{
    public class ComponentsTests
    {
        VariableManager manager = new VariableManager();
        Executer executer = new Executer();

        [Test]
        public void ExecuterGoodCommandTest()
        {
            Assert.AreEqual("abc", executer.Execute("echo", "abc"));
        }

        [Test]
        public void ExecuterBadCommandTest()
        {
            try
            {
                executer.Execute("dsasd", "dsaodsa");
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void CreateVariableTest()
        {
            manager.SetVariableValue("first", "1");
            Assert.AreEqual("1", manager.GetVariableValue("first"));
        }

        [Test]
        public void ChangeVariableValueTest()
        {
            manager.SetVariableValue("first", "one");
            Assert.AreEqual("one", manager.GetVariableValue("first"));
        }

        [Test]
        public void GetEnvironmentVaribleTest()
        {
            Assert.AreEqual(Environment.GetEnvironmentVariable("OS"), manager.GetVariableValue("OS"));
        }

        [Test]
        public void NotFindVariableTest()
        {
            Assert.AreEqual(null, manager.GetVariableValue("second"));
        }

        [Test]
        public void InvalidVariableNameTest()
        {
            try
            {
                manager.SetVariableValue("$32*", "dasgre");
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Invalid variable name, the variable name must consist of letters, numbers and underscores", ex.Message);
                Assert.AreEqual("BadRequestException", ex.GetType().Name);
            }
        }
    }
}