using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task4.Sdk;
using System.IO;

namespace Task4
{
    public class PluginLoader
    {
        private Dictionary<string, Assembly> libraries { get; set;}

        public PluginLoader()
        {
            libraries = new Dictionary<string, Assembly>();
        }

        public List<IPluginBot> GetPlugins(string[] paths)
        {
            foreach (var path in paths)
                if (Directory.Exists(path)) 
                    libraries.Add(path, null);
                

            var plugins = new List<IPluginBot>();
            var keys = libraries.Keys.ToArray();
            foreach (var dllPath in keys)
            {
                var thisDllPlugins = GetDirectoryPlugins<IPluginBot>(dllPath);
                if (thisDllPlugins.Count == 0)
                {
                    Console.WriteLine($"seems like no plugins in \n {dllPath}, add .dll\n");
                }
                else
                {
                    Assembly value = libraries[dllPath];
                    libraries.Remove(dllPath);
                    libraries[thisDllPlugins[0].GetType().Name] = value;

                    Console.WriteLine($"Finded plugins in {dllPath}: Choose one\n");
                    foreach (var thisDllPlugin in thisDllPlugins)
                    {
                        Console.WriteLine(thisDllPlugin.GetType().Name);
                    }
                    plugins.AddRange(thisDllPlugins);

                }
            }
            return plugins;
        }     
        public List<T> GetDirectoryPlugins<T>(string path)
            where T : class
        {
            var plugins = new List<T>();

            var findedDlls = Directory.EnumerateFiles(path, path.Split("\\").Last() + "*.dll", SearchOption.AllDirectories);

            var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), findedDlls.First()));

            libraries[path] = assembly;

            var pluginTypes = assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface).ToArray();

            foreach (var pluginType in pluginTypes)
            {
                var pluginInstance = Activator.CreateInstance(pluginType) as T;
                plugins.Add(pluginInstance);
            }

            return plugins;
        }

        public Assembly GetAsm(string key)
        {
            return libraries[key];
        }

    }
}
