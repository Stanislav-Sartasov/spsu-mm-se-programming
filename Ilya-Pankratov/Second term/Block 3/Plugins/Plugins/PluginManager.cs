using System.Reflection;

namespace Plugins
{
    public class PluginManager
    {
        public static List<IPlugin>? Plugins = null;

        public static void LoadExtensions(string path)
        {
            var pluginsList = new List<IPlugin>();
            var files = Directory.GetFiles(path, "*.dll");

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));

                var pluginTypes = assembly.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var plugin in pluginTypes)
                {
                    var pluginInstance = Activator.CreateInstance(plugin);
                    pluginsList.Add(pluginInstance as IPlugin);
                }
            }

            Plugins = pluginsList;
        }
    }
}
