using NUnit.Framework;
using Plugins;

namespace PluginsTests
{
    public class PluginManagerTests
    {
        [Test]
        public void LoadExtensionsTest()
        {
            string path = "..\\..\\..\\..\\Extensions";
            PluginManager.LoadExtensions(path);

            Assert.IsTrue(PluginManager.Plugins.Count == 1);
        }
    }
}