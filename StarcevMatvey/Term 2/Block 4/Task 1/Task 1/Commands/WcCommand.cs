namespace Task_1.Commands
{
    public class WcCommand : IExecutable
    {
        public WcCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            if (b.CurrentArguments.Count == 0)
            {
                var errors = b.GetErrorMessenges();
                errors.Add($"{b.Invite}: wc: : No such file");
                return b.WithCurrentArguments(new List<string>()).WithErrorMessenges(errors);
            }

            var newCurrArgs = new List<string>();
            foreach (var a in b.CurrentArguments)
            {
                var countOfLines = 0;
                var countOfWords = 0;
                var countOfBytes = 0;
                var arg = b.GetLocalVar(a);
                var pathToFile = b.GetPathToFile(arg);

                if (pathToFile == "")
                {
                    var errors = b.GetErrorMessenges();
                    errors.Add($"{b.Invite}: wc: {arg}: No such file");
                    b = b.WithErrorMessenges(errors);
                }

                else
                {
                    try
                    {
                        using (var sr = new StreamReader(pathToFile))
                        {
                            while (!sr.EndOfStream)
                            {
                                var line = sr.ReadLine();
                                countOfLines++;
                                if (line != null) countOfWords += line.Split().Length;
                            }
                        }

                        countOfBytes = (int)(new FileInfo(pathToFile).Length);

                        newCurrArgs.Add($"{countOfLines} {countOfWords} {countOfBytes} {arg}");
                    }
                    catch
                    {
                        var errors = b.GetErrorMessenges();
                        errors.Add($"{b.Invite}: wc: {arg}: Unable to open file");
                        b = b.WithErrorMessenges(errors);
                    }
                }
            }

            return b.WithCurrentArguments(newCurrArgs);
        }
    }
}
