namespace Bash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintDescription();
            new BashInterpreter().Run();
        }

        public static void PrintDescription()
        {
            Console.WriteLine("This is elementary Bash. Available commands:\n" +
                "1) echo [arg1] [arg2] ... - display arguments, quotes should be used to escape control characters\n" +
                "for example, use echo \"$&#\" for display $&#\n" +
                "2) exit - exit the interpreter\n" +
                "3) pwd - display the current working directory and the list of files in it\n" +
                "4) cat [file path] - display the contents of the file\n" +
                "5) wc [file path] - display the number of lines, words and bytes in the file\n" +
                "6) cd [new directory] - changes the current working directory\n" +
                "Other features\n" +
                "1) $var=abc - creates a local variable 'var' and assigns it the value 'abc'\n" +
                "2) $var - using the value of a local variable(environment variables are also available) 'var'\n" +
                "for example, echo $OS\n" +
                "3) | - pipelining processing of the commands\n");
        }
    }
}