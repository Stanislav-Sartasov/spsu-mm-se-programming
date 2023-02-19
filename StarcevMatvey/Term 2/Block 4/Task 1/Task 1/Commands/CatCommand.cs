namespace Task_1.Commands
{
    public class CatCommand : IExecutable
    {
        public CatCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);                                                                                                       
            var newCurrentArguments = new List<string>();

            foreach (var arg in b.CurrentArguments)
            {
                var pathToFile = b.GetPathToFile(b.GetLocalVar(arg));

                if (pathToFile == "")
                {
                    var errors = b.GetErrorMessenges();
                    errors.Add($"{bash.Invite}: cat: {arg}: No such file");
                    b = b.WithErrorMessenges(errors);
                }

                var fileData = "";
                try
                {
                    using (var sr = new StreamReader(pathToFile))
                    {
                        fileData = sr.ReadToEnd();
                    }
                }
                catch
                {
                    var errors = b.GetErrorMessenges();
                    errors.Add($"{b.Invite}: cat: {arg}: Unable to open file");
                    b = bash.WithErrorMessenges(errors);
                }

                newCurrentArguments.Add(fileData);
            }

            return b.WithCurrentArguments(newCurrentArguments);
        }
    }
}
