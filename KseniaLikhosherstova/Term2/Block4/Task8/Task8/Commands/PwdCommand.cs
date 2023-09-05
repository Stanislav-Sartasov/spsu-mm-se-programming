using System.Text;

namespace Task8.Commands
{
    internal class PwdCommand
    {
        public string Name { get { return "pwd"; } }

        public void Motion()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(Environment.CurrentDirectory);

            var fileNames = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly)
                                     .Select(path => Path.GetFileName(path)).ToList();

            fileNames.ForEach(file => result.AppendLine(file));
        }
    }
}
