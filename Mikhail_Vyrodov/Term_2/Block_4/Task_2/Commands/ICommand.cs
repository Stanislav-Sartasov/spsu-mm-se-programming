using System;

namespace Commands
{
    public interface ICommand
    {
        public string ApplyCommand(string[] arguments);

        public string Name { get; }
    }
}
