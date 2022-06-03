using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bash.Commands;

namespace Bash
{
    internal static class IoCContainer
    {
        internal static List<Type> Commands { get; private set; } = new List<Type>()
        {
            typeof(Exit), typeof(ChangeDirectory), typeof(Concatenate), typeof(Echo), typeof(ListFiles), typeof(NewProcess), typeof(PrintWorkingDirectory), typeof(WordCount), typeof(Clear), typeof(Help)
        };

        internal static IEnumerable<ICommand> GetCommands()
        {
            var builder = new ContainerBuilder();

            foreach (var type in Commands)
            {
                builder.RegisterType(type).As<ICommand>();
            }

            var container = builder.Build();

            return container.Resolve<IEnumerable<ICommand>>();
        }

        internal static bool DisconnectCommand(Type siteType) => Commands.Remove(siteType);

        internal static bool ConnectCommand(Type siteType)
        {
            bool condition = !Commands.Contains(siteType);
            if (condition)
            {
                Commands.Add(siteType);
            }
            return condition;
        }
    }
}
