using System;
using System.Collections.Generic;
using BashCommands;

namespace BashCommandsUnitTests.ClassesForCommandManagerTests
{
    public class TestCommand : ICommand
    {
        public string FullName { get; }
        public string ShortName { get; }

        public TestCommand()
        {
            FullName = "Test";
            ShortName = "test";
        }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            throw new NotImplementedException();
        }
    }
}
