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
            errorMessenges = new List<string>(errors);
            Output = output;
            CurrentArguments = new List<string>(currArgs);
            arguments = new Queue<string>(args);
            argumentsForSetLocalVar = new Queue<(string name, string value)>(argsForLocalVar);
            LocalVar = new Dictionary<string, string>(localVar);
            Commands = new List<Command>(cmds);
        }

        public MyBash(MyBash bash)
        {
            Path = bash.Path;
            Invite = bash.Invite;
            errorMessenges = bash.GetErrorMessenges();
            Output = bash.Output;
            CurrentArguments = new List<string>(bash.CurrentArguments);
            arguments = new Queue<string>(bash.arguments);
            argumentsForSetLocalVar = new Queue<(string name, string value)>(bash.argumentsForSetLocalVar);
            LocalVar = new Dictionary<string, string>(bash.LocalVar);
            Commands = new List<Command>(bash.Commands);

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

        public MyBash WithArgumentsForSetLocalVar(Queue<(string name, string value)> args)
        {
            return new MyBash(Path, Invite, errorMessenges, Output, CurrentArguments, arguments,
                args, LocalVar, Commands);
        }

        public MyBash WithCurrentArguments(IReadOnlyList<string> currArgs)
        {
            return new MyBash(Path, Invite, errorMessenges, Output, currArgs, arguments,
                argumentsForSetLocalVar, LocalVar, Commands);
        }

        public MyBash WithErrorMessenges(List<string> errors)
        {
            return new MyBash(Path, Invite, errors, Output, CurrentArguments, arguments,
                argumentsForSetLocalVar, LocalVar, Commands);
        }

        public MyBash WithPath(string path)
        {
            return new MyBash(path, Invite, errorMessenges, Output, CurrentArguments, arguments,
                argumentsForSetLocalVar, LocalVar, Commands);
        }

        public MyBash WithOutput(string output)
        {
            return new MyBash(Path, Invite, errorMessenges, output, CurrentArguments, arguments,
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

        public MyBash ExecuteCommands()
        {
            var executer = new Executer();
            var b = new MyBash(this);
            foreach (var command in Commands) b = executer.ExecuteCommand(command, b);

            if (b.GetErrorMessenges().Count == 0) b = executer.ExecuteCommand(Command.Echo, b);

            return b;
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

        public string DequeueArgument()
        {
            return arguments.Dequeue();
        }

        public (string name, string value) DequeueArgumentForSetLocalVar()
        {
            return argumentsForSetLocalVar.Dequeue();
        }
    }
}
