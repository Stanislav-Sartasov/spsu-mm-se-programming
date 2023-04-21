using System;
using System.Collections.Generic;
using BashCommands;

namespace BashCommandsUnitTests.ClassesForCommandManagerTests
{
    internal abstract class AbstractTestCommand : ICommand
    {
        public abstract string FullName { get; }
        public abstract string ShortName { get; }

        public IEnumerable<string>? Execute(IEnumerable<string>? arguments)
        {
            throw new NotImplementedException();
        }
    }
}
