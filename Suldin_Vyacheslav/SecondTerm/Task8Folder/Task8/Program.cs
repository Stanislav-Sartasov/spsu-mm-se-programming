using System;
using Core;
using CommandResolverLib;
using CommandLib;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is BASH, but with truncated function\nTo check available commands use command <help>");
            var cc = new CommandCreator();
            var cr = new CommandResolver(cc);
            var session = new Session(cr);
            session.Start();
        }
    }
}
