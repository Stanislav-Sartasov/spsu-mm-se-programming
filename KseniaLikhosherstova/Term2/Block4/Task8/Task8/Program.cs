namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The program executes basic Bash commands: echo, cat, cd, pwd, wc.");
            Console.WriteLine("You can use input via console or script.");
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
