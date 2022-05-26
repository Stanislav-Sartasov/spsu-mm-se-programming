using AbstractOperators;
using System;
using System.Collections.Generic;

namespace BushTests
{
    public class LoggerForTesting : ALogger
    {
        private List<List<String>> answers;
        private List<String> readedLines;
        private int index = -1;
        private int subIndex = 0;

        public LoggerForTesting(List<String> readedLines, List<List<String>> answers)
        {
            this.readedLines = readedLines;
            this.answers = answers;
        }

        public override void Log(string message)
        {
            if (message != answers[index + 1][subIndex])
                throw new Exception($"Something went swong {message} != {answers[index][subIndex]}");
            subIndex++;
        }

        public override string ReadLine()
        {
            index++;
            subIndex = 0;
            return readedLines[index];
        }
    }
}
