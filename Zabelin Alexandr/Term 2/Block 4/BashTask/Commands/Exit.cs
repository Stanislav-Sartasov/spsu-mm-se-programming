using System;

namespace Commands
{
    public class Exit : ICommand
    {
        public bool RequiredArgs { get; }
        public bool NeedToBePrinted { get; private set; } = false;
        public bool IsSystem { get; private set; } = true;


        public string[] Run(string[] args)
        {
            Environment.Exit(0);

            return args;
        }
    }
}
