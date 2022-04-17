using NUnit.Framework;
using Plugins;

namespace PluginsTests
{
    public class PluginManagerTests
    {
        [Test]
        public void LoadExtensionsTest()
        {
            string path = "D:\\GitHub works SPBU\\spsu-mm-se-programming\\Ilya-Pankratov\\Second term\\Block 3\\Plugins\\Extensions";
            PluginManager.LoadExtensions(path);

            Assert.IsTrue(PluginManager.Plugins.Count == 1);
        }
    }
}