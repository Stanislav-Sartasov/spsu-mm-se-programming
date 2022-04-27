using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PluginLibrary
{
    public class LibraryLoader
    {
        private string libraryPath;

        public LibraryLoader(string path)
        {
            libraryPath = path;
        }

        public Assembly LoadLibrary()
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(libraryPath);
                return assembly;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
