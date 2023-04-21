using System.Text;

namespace Task8.Commands
{
    internal class EchoCommand : ICommand
    {
        public string Name { get { return "echo"; } }

        public StringBuilder Motion(string arguments)
        {
            StringBuilder result = new StringBuilder();
            return result.AppendLine(arguments);
        }
    }
}
