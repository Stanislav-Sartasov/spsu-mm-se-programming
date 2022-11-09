namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = string.Empty;

                if (args.Length > 0)
                {
                    arguments = string.Join(' ', args);
                    ExecuteScript(arguments);
                }
                else
                {
                    while (true)
                    {
                        arguments = Console.ReadLine();

                        if (arguments == "exit") break;

                        ExecuteScript(arguments);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to execute script: {e.Message}");
                Console.ReadKey();
            }
        }

        private static void ExecuteScript(string arguments)
        {
            Interpreter interpreter = new Interpreter(arguments);

            var res = interpreter.ExecuteScript(out string result, out string error);

            Console.WriteLine(result);

            if (!res)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
