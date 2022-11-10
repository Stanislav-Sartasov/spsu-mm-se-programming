using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class EchoTests
{
    [Test]
    public void LaunchTest()
    {
        ICommand echo = new Echo();
        var arguments = new List<String> { "firstArgument", "secondArgument" };
        var commandResult = echo.Launch(arguments);
        
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
            Assert.That(echo.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(echo.Output.Message.First().Message, Is.EqualTo("firstArgument secondArgument"));
            Assert.That(echo.Output.Message.First().ErrorOccured, Is.False);
        });
    }
}