using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class EmptyCommandTest
{
    [Test]
    public void LaunchTest()
    {
        ICommand empty = new EmptyCommand();
        var arguments = new List<String> { "firstArgument", "secondArgument" };
        var commandResult = empty.Launch(arguments);
        
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
            Assert.That(empty.Output.Message.Count(), Is.EqualTo(0));
        });
    }
}