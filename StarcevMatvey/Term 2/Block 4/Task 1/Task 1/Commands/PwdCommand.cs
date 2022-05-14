namespace Task_1.Commands
{
    public class PwdCommand : IExecutable
    {
        public PwdCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var newCurrentArguments = new List<string>();
            newCurrentArguments.Add(b.Path);

            foreach (var fileName in Directory.GetFileSystemEntries(b.Path))
            {
                newCurrentArguments.Add(new string(fileName.Skip(b.Path.Length).ToArray()));
            }

            return b.WithCurrentArguments(newCurrentArguments);
        }
    }
}
