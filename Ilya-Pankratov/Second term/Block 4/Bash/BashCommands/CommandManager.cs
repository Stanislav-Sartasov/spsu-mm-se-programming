using System.Reflection;

namespace BashCommands
{
    public class CommandManager
    {
        private List<Type> registredCommands;
        private List<ICommand> createdCommands;

        public CommandManager()
        {
            registredCommands = new List<Type>();
            createdCommands = new List<ICommand>();
            AddCommandsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public CommandManager(string assemblyPath) : this()
        {
            var assembly = Assembly.LoadFrom(assemblyPath);

            if (assembly != null)
            {
                AddCommandsFromAssembly(assembly);
            }
        }

        public CommandManager(IEnumerable<string> assemblyPaths) : this()
        {
            foreach (var assemblyPath in assemblyPaths)
            {
                var assembly = Assembly.LoadFrom(assemblyPath);

                if (assembly != null)
                {
                    AddCommandsFromAssembly(assembly);
                }
            }
        }

        public IEnumerable<ICommand> GetCommands()
        {
            var commands = new List<ICommand>();

            foreach (var command in registredCommands)
            {
                if (!createdCommands.Exists(x => x.GetType() == command))
                {
                    commands.Add(Activator.CreateInstance(command) as ICommand);
                }
            }

            createdCommands.AddRange(commands);
            return commands;
        }

        // if at least one of the command was not added return false else true
        public bool Register(IEnumerable<Type> commands)
        {
            var result = true;

            foreach (var command in commands)
            {
                if (!Register(command))
                {
                    result = false;
                }
            }

            return result;
        }

        public bool Register(Type command)
        {
            if (typeof(ICommand).IsAssignableFrom(command) && !registredCommands.Contains(command) && !command.IsInterface && !command.IsAbstract)
            {
                registredCommands.Add(command);
                return true;
            }

            return false;
        }

        // if at least one of the command was not removed return false else true
        public bool Remove(IEnumerable<Type> commands)
        {
            bool result = true;

            foreach (var command in commands)
            {
                if (!Remove(command))
                {
                    result = false;
                }
            }

            return result;
        }

        public bool Remove(Type command)
        {
            if (typeof(ICommand).IsAssignableFrom(command) && registredCommands.Contains(command))
            {
                registredCommands.Remove(command);
                return true;
            }

            return false;
        }

        public void AddCommandsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();

            if (types == null)
            {
                return;
            }

            Register(types);
        }

        public void AddCommandsFromAssembly(string? path)
        {
            if (path == null)
            {
                return;
            }

            if (!File.Exists(path))
            {
                path = null;
            }

            if (path != null)
            {
                var assembly = Assembly.LoadFrom(path);

                if (assembly == null)
                {
                    return;
                }

                AddCommandsFromAssembly(assembly);
            }
        }
    }
}
