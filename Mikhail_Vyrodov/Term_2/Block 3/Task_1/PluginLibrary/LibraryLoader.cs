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
        public string ExceptionMessage;

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
                ExceptionMessage = ex.Message;
                throw ex;
            }
        }
    }
}
