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
            var cc = new CommandCreator();
            var cr = new CommandResolver(cc);
            var session = new Session(cr);
            session.Start();
        }
    }
}
