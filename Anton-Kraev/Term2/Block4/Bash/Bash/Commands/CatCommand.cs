namespace Bash.Commands
{
    public class CatCommand : ICommand
    {
        public string Name => "cat";

        public string Execute(string args)
        {
            string result = "";
            using (var stream = new StreamReader(args))
            {
                result = stream.ReadToEnd();
            }
            return result;
        }
    }
}