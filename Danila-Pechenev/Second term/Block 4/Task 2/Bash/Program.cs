namespace Bash;
using Tools;

public class Program
{
    public static void Main(string[] args)
    {
        var listOfCommands = new List<ICommand>
        {
            new Echo(),
            new Pwd(),
            new Ls(),
            new Cd(),
            new Cat(),
            new Wc()
        };
        var interpreter = new Interpreter(listOfCommands);
        while (true)
        {
            Console.Write(">>> ");
            string? line = Console.ReadLine();
            if (line != null)
            {
                ResultCode code = interpreter.ExecuteLine(line, out string result);
                if (code == ResultCode.Exit)
                {
                    break;
                }
                else if (result.Length > 0)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
