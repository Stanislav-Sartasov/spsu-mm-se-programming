using System.Linq;

namespace Bash.Commands
{
    public class PwdCommand : ICommand
    {
        public string Name => "pwd";

        public string Execute(string args)
        {
            var dir = Directory.GetCurrentDirectory();
            var result = dir;
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                result += "\n" + file;
            }
            return result;
        }
    }
}