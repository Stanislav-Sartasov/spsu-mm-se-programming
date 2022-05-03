using System;

namespace Core
{
    public class ConsoleHandler : IHandler
    {
        public string GetLine()
        {
            return Console.ReadLine();
        }

        public void Show(string line)
        {
            Console.WriteLine(line);
        }
    }
}