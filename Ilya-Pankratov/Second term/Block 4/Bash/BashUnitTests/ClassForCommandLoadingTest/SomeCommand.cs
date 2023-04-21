using System.Collections.Generic;
using BashCommands;

namespace BashUnitTests.ClassForCommandLoadingTest
{
    public class SomeCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public SomeCommand()
        {
            FullName = "Some";
            ShortName = "doSomething";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            if (arguments != null)
            {
                return new List<string>() { "Invalid arguments" };
            }

            return new List<string>() { "Something has been done" };
        }
    }
}
