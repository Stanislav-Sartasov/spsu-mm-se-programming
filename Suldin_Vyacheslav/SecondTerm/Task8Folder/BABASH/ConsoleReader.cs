using System;

namespace BABASH
{
    public class ConsoleReader : IReader
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