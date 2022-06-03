using NUnit.Framework;

namespace Bash.UnitTests.ToolsTests
{
    internal class VariableStorageTests
    {
        [Test]
        public void StoreTest()
        {
            VariableStorage storage = new VariableStorage();
            AssertThatVariableEstablished("I will become", "the king of shamans", storage);
            AssertThatVariableEstablished("I will become", "hokage", storage);
            Assert.That(storage.Get("ZE WARUDO!!!"), Is.EqualTo(""));
        }

        private void AssertThatVariableEstablished(string name, string value, VariableStorage storage)
        {
            storage.Add(name, value);
            Assert.That(storage.Get(name), Is.EqualTo(value));
        }
    }
}
