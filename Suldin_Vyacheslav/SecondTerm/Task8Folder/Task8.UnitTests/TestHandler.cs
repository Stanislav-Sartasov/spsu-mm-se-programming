using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Task8.UnitTests
{
    public class TestHandler : IHandler
    {
        public string[] Commands;
        private int current = -1;
        public TestHandler(string[] commands)
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
