using System.Text;

namespace Task8.Commands
{
    internal class CdCommand : ICommand
    {
        public string Name { get { return "cd"; } }

        public StringBuilder Motion(string arguments)
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(arguments))
            {
                throw new ArgumentNullException("Command argument cannot be null or empty");
            }

            result.Append(Environment.CurrentDirectory = Path.IsPathRooted(arguments) ?
                arguments :
                Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, arguments)));
            return result;
        }
    }
}
