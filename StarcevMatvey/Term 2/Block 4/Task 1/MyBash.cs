using Enumerations;
using Interfaces;

namespace Task_1
{
    public class MyBash
    {
        public string Path { get; private set; }
        public string Invite { get; private set; }
        private List<string> errorMessenges;
        public string Output { get; private set; }
        public IReadOnlyList<string> CurrentArguments;
        private Queue<string> arguments;
        private Queue<(string name, string value)> argumentsForSetLocalVar;
        public IReadOnlyDictionary<string, string> LocalVar;
        public IReadOnlyList<Command> Commands;

        public MyBash(string path = "/", string invite = "mybash")
        {
            Path = path;
            Invite = invite;
            errorMessenges = new List<string>();
            Output = "";
            CurrentArguments = new List<string>();
            arguments = new Queue<string>();
            argumentsForSetLocalVar = new Queue<(string name, string value)>();
            LocalVar = new Dictionary<string, string>();
            Commands = new List<Command>();
        }

        public MyBash(string path, string invite, List<string> errors, string output,
            IReadOnlyList<string> currArgs, Queue<string> args, Queue<(string name, string value)> argsForLocalVar,
            IReadOnlyDictionary<string, string> localVar, IReadOnlyList<Command> cmds)
        {
            Path = path;
            Invite = invite;
            errorMessenges = errors;
            Output = output;
            CurrentArguments = currArgs;
            arguments = args;
            argumentsForSetLocalVar = argsForLocalVar;
            LocalVar = localVar;
            Commands = cmds;
        }

        public MyBash WithLocalVar(Dictionary<string, string> localV)
        {
            return new MyBash(Path, Invite, errorMessenges, Output, CurrentArguments, arguments,
                argumentsForSetLocalVar, localV, Commands);
        }

        public MyBash WithArguments(Queue<string> args)
        {
            return new MyBash(Path, Invite, errorMessenges, Output, CurrentArguments, args,
                argumentsForSetLocalVar, LocalVar, Commands);
        }

        public MyBash WithCurrentArguments(IReadOnlyList<string> currArgs)
        {
            return new MyBash(Path, Invite, errorMessenges, Output, currArgs, arguments,
                argumentsForSetLocalVar, LocalVar, Commands);
        }

        public void InitCommands(IReader reader)
        {
            var stringOfCommands = reader.Read();
            errorMessenges.Clear();

            if (!reader.IsRead)
            {
                errorMessenges.Add(reader.ErrorMessenge);
                return;
            }


            var parser = new Parser(stringOfCommands);
            parser.Parse();

            if (!parser.IsParsed)
            {
                errorMessenges.Add(parser.ErrorMessenge);
                return;
            }


            var analyzer = new Analyzer(parser.GetParsedString());
            analyzer.Analyze();

            if (!analyzer.IsAnalyzed)
            {
                errorMessenges.Add(analyzer.ErrorMessenge);
                return;
            }


            argumentsForSetLocalVar = analyzer.GetArgumentsForSetLocalVar();
            arguments = analyzer.GetArguments();
            Commands = analyzer.GetCommands();
        }

        public void ExecuteCommands()
        {
            foreach (var command in Commands)
            {
                switch ((int)command)
                {
                    case 0: 
                        Exit(); 
                        break;
                    case 1:
                        Pwd();
                        break;
                    case 2:
                        Echo();
                        break;
                    case 3:
                        Cat();
                        break;
                    case 4:
                        Wc();
                        break;
                    case 5:
                        Whoami();
                        break;
                    case 6:
                        Cd();
                        break;
                    case 7:
                        Pipeline();
                        break;
                    case 8:
                        SetLocalVar();
                        break;
                    case 9:
                        Clear();
                        break;
                    case 10:
                        SetArgument();
                        break;
                }
            }

            Echo();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Echo()
        {
            Output = "";
            foreach (var arg in CurrentArguments)
            {
                Output += GetLocalVar(arg) + "\n";
            }
        }

        public void Pwd()
        {
            var newCurrentArguments = new List<string>();
            newCurrentArguments.Add(Path);

            foreach (var fileName in Directory.GetFileSystemEntries(Path))
            {
                newCurrentArguments.Add(new string(fileName.Skip(Path.Length).ToArray()));
            }

            CurrentArguments = newCurrentArguments;
        }

        public void Cat()
        {
            var newCurrentArguments = new List<string>();

            foreach (var arg in CurrentArguments)
            {
                var pathToFile = GetPathToFile(GetLocalVar(arg));

                if (pathToFile == "")
                {
                    errorMessenges.Add($"{Invite}: cat: {arg}: No such file");
                    CurrentArguments = new List<string>();
                    return;
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
                    errorMessenges.Add($"{Invite}: cat: {arg}: Unable to open file");
                    CurrentArguments = new List<string>();
                    return;
                }

                newCurrentArguments.Add(fileData);
            }

            CurrentArguments = newCurrentArguments;
        }

        public void Wc()
        {
            var countOfLines = 0;
            var countOfWords = 0;
            var countOfBytes = 0;

            if (CurrentArguments.Count == 0)
            {
                errorMessenges.Add($"{Invite}: wc: : No such file");
                CurrentArguments = new List<string>();
                return;
            }

            var arg = GetLocalVar(CurrentArguments.Last());
            var pathToFile = GetPathToFile(arg);

            if (pathToFile == "")
            {
                errorMessenges.Add($"{Invite}: wc: {arg}: No such file");
                CurrentArguments = new List<string>();
                return;
            }

            try
            {
                using (var sr = new StreamReader(pathToFile))
                {
                    while(!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        countOfLines++;
                        if (line != null) countOfWords += line.Split().Length;
                    }
                }
                countOfBytes = (int)(new FileInfo(pathToFile).Length);
            }
            catch
            {
                errorMessenges.Add($"{Invite}: wc: {arg}: Unable to open file");
                CurrentArguments = new List<string>();
                return;
            }

            CurrentArguments = new List<string> { $"{countOfLines} {countOfWords} {countOfBytes}" };
        }

        public void Whoami()
        {
            CurrentArguments = new List<string> { Environment.UserName };
        }

        public void Cd()
        {
            var arg = CurrentArguments.Count == 0 ? "" : CurrentArguments.Last();
            arg = GetLocalVar(arg);

            if (Directory.Exists(arg))
            {
                Path = GetPathWithSlash(arg);
            }

            else if (Directory.Exists(Path + arg))
            {
                Path += GetPathWithSlash(arg);
            }

            else
            {
                errorMessenges.Add($"{Invite}: cd: {arg}: No such directory");
                CurrentArguments = new List<string>();
                return;
            }

            CurrentArguments = new List<string>();
        }

        public void Pipeline()
        {
            var newCurrentArgs = new List<string>();
            foreach (var arg in CurrentArguments)
            {
                var splitedArgs = arg.Split('\n');
                foreach (var a in splitedArgs)
                {
                    newCurrentArgs.Add(a.Trim('\r'));
                }
            }
            CurrentArguments = newCurrentArgs;
        }

        public void SetLocalVar()
        {
            var v = argumentsForSetLocalVar.Dequeue();
            if (LocalVar.ContainsKey(v.name)) LocalVar = LocalVar.Where(x => x.Key != v.name).ToDictionary(x => x.Key, x => x.Value);
            var d = new Dictionary<string, string>(LocalVar)
            {
                { v.name, v.value }
            };
            LocalVar = new Dictionary<string, string>(d);
            CurrentArguments = new List<string>();
        }

        public void Clear()
        {
            Console.Clear();
            CurrentArguments = new List<string>();
        }

        public void SetArgument()
        {
            var arg = arguments.Dequeue();
            CurrentArguments = new List<string> { arg };
        }

        public string GetLocalVar(string arg)
        {
            if (LocalVar.Keys.Contains(arg))
            {
                return LocalVar[arg];
            }

            return arg;
        }

        public string GetPathWithSlash(string path)
        {
            path = path.Replace('\\', '/');
            if (path.Last() != '/') path += "/";

            return path;
        }

        public string GetPathToFile(string arg)
        {
            var pathToFile = "";

            if (File.Exists(arg)) pathToFile = arg;
            else if (File.Exists(Path + arg)) pathToFile = Path + arg;

            return pathToFile;
        }

        public List<string> GetErrorMessenges()
        {
            return new List<string>(errorMessenges);
        }
    }
}
