using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class PwdTests
{
    [Test]
    public void LaunchTest()
    {
        ICommand pwd = new Pwd();
        var arguments = new List<String>();
        var commandResult = pwd.Launch(arguments);
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
            Assert.That(pwd.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(pwd.Output.Message.First().Message, Is.EqualTo(Directory.GetCurrentDirectory()));
            Assert.That(pwd.Output.Message.First().ErrorOccured, Is.False);
        });
    }
}