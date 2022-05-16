namespace Task_1.Commands
{
    public class CdCommand : IExecutable
    {
        public CdCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var arg = b.CurrentArguments.Count == 0 ? "" : b.CurrentArguments.Last();
            arg = b.GetLocalVar(arg);

            if (Directory.Exists(arg))
            {
                b = b.WithPath(b.GetPathWithSlash(arg));
            }

            else if (Directory.Exists(bash.Path + arg))
            {
                b = b.WithPath(b.Path + b.GetPathWithSlash(arg));
            }

            else
            {
                var errors = b.GetErrorMessenges();
                errors.Add($"{b.Invite}: cd: {arg}: No such directory");
                b = b.WithErrorMessenges(errors);
            }

            return b.WithCurrentArguments(new List<string>());
        }
    }
}
