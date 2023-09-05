using AbstractOperators;
using FileExplorer;


namespace Logger
{
    public class BushLogger : ALogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }

        public override string ReadLine()
        {
            Console.Write(FileManager.GetCurrentDirectory() + "> ");
            return Console.ReadLine();
        }
    }
}
