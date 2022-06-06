using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Commands
{
    public class CatCommand : ICommand
    {
        public string Name { get; set; } = "cat ";

        public string ApplyCommand(string[] arguments)
        {
            if (File.Exists(arguments[0]))
            {
                using (StreamReader reader = new StreamReader(arguments[0]))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            else
            {
                return "No file found with this name";
            }
        }
    }
}
