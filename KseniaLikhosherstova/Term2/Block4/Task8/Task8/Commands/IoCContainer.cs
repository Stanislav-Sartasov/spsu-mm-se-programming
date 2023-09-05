using Microsoft.Extensions.DependencyInjection;

namespace Task8.Commands
{
    internal static class IoCContainer
    {
        public static IEnumerable<ICommand> Container()
        {
            ServiceCollection commandCollection = new ServiceCollection();

            commandCollection.AddTransient<ICommand>(x => new CatCommand());
            commandCollection.AddTransient<ICommand>(x => new CdCommand());
            commandCollection.AddTransient<ICommand>(x => new EchoCommand());
            commandCollection.AddTransient<ICommand>(x => new WcCommand());


            var provider = commandCollection.BuildServiceProvider();

            var commands = provider.GetServices<ICommand>();
            return commands;
        }
    }
}
