using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BABASH;

namespace Task8.UnitTests
{
    public class TestReader : IReader
    {
        public string[] Commands;
        private int current = -1;
        public TestReader(string[] commands)
        {
            this.Commands = commands;
        }
        public string GetLine()
        {
            current++;
            return Commands[current];
        }

        public void Show(string line)
        {
            Commands[current] = line;
        }
    }
}
