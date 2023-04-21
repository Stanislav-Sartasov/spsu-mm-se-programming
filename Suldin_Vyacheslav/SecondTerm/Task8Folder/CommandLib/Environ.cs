using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommandLib
{
    public static class Environ
    {
        private static Dictionary<string, string> localVariables = new Dictionary<string, string>()
        {
            {"$HOME", Directory.GetCurrentDirectory() }
        };
        private static string curentDirectory = Directory.GetCurrentDirectory();

        public static string GetCurrentDirectory()
        {
            return curentDirectory;
        }
        public static void SetCurrentDirectory(string path)
        {
            curentDirectory = path;
        }

        public static IReadOnlyDictionary<string, string> GetVars()
        {
            return localVariables as IReadOnlyDictionary<string, string>;
        }
        public static string GetLocalVar(string key)
        {
            return localVariables[key];
        }
        public static void SetLocalVar(string key, string value)
        {
            try
            {
                localVariables.Add("$" + key, value);
            }
            catch (ArgumentException)
            {
                localVariables["$" + key] = value;
            }
        }

        public static void DefaultSet()
        {
            curentDirectory = Directory.GetCurrentDirectory();
            localVariables = new Dictionary<string, string>()
            {
                 {"$HOME", curentDirectory }
            };
        }
    }
}
