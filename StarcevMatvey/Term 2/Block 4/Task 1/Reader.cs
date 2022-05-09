using static System.Console;
using Interfaces;

namespace Task_1
{
    public class Reader : IReader
    {
        public bool IsRead { get; private set; }
        public string ErrorMessenge { get; private set; }

        public Reader()
        {
            IsRead = false;
            ErrorMessenge = "Reader was not called";
        }

        public string Read()
        {
            var input = ReadLine();
            if (input == null || input.Length == 0)
            {
                ErrorMessenge = "Empty input";
                return null;
            }

            if (input.First() == ' ')
            {
                ErrorMessenge = "Input must not start with space";
                return null;
            }

            IsRead = true;

            return input;
        }
    }
}
