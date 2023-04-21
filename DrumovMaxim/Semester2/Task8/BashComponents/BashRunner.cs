namespace BashComponents;

public class BashRunner
{
    public void Run()
    {
        var parser = new InputParser();
        var commandManger = new CommandManager();
        
        while (true)
        {
            Console.Write("$ ");
            var userInput = Console.ReadLine();
            var lexemes = parser.Parse(userInput);
            var bashOutput = commandManger.Run(lexemes);
            Console.WriteLine(bashOutput);
        }
    }
}