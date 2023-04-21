using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class UnknownCommandTests
{
    [Test]
    public void LaunchTest()
    {
        ICommand unknown = new UnknownCommand();
        var arguments = new List<String> { "someCommandThatDoesNotExist", "argument" };
        var commandResult = unknown.Launch(arguments);
        
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Failed));
            Assert.That(unknown.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(unknown.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(unknown.Output.Message.First().ErrorMessage, Is.EqualTo("Command was not found"));
            Assert.That(unknown.Output.Message.First().Message, Is.EqualTo(String.Empty));
        });
    }
}