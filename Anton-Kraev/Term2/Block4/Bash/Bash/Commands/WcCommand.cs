namespace Bash.Commands
{
    public class WcCommand : ICommand
    {
        public string Name => "wc";

        public string Execute(string args)
        {
            var result = "";
            using (var reader = new StreamReader(args))
            {
                var text = reader.ReadToEnd();
                result += text.Length.ToString() + " bytes\n";
                result += (text.Count(x => x == '\n') + 1).ToString() + " lines\n";
                result += (text.Count(x => x == ' ' || x == '\n') + 1).ToString() + " words";
            }
            return result;
        }
    }
}