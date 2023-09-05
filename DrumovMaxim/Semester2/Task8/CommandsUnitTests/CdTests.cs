using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class CdTests
{
    [Test]
    public void LaunchBackTest()
    {
        var dir = Directory.GetCurrentDirectory();
        ICommand cd = new Cd();
        var arguments = new List<String>{ ".." };
        var initialDir = Directory.GetCurrentDirectory();
        var commandResult = cd.Launch(arguments);
        var secDir = Directory.GetCurrentDirectory();
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
            Assert.That(cd.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(cd.Output.Message.First().Message, Is.EqualTo(String.Empty));
            Assert.That(cd.Output.Message.First().ErrorOccured, Is.False);
            Assert.That(String.Join('\\', initialDir.Split('\\').SkipLast(1)), Is.EqualTo(Directory.GetCurrentDirectory()));
        });
    }

    [Test]
    public void LaunchBackAndForwardTest()
    {
        ICommand cd = new Cd();
        var arguments = new List<String>{ $"..\\{Directory.GetCurrentDirectory().Split('\\').Last()}" };
        var initialDir = Directory.GetCurrentDirectory();
        var commandResult = cd.Launch(arguments);
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
            Assert.That(cd.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(cd.Output.Message.First().Message, Is.EqualTo(String.Empty));
            Assert.That(cd.Output.Message.First().ErrorOccured, Is.False);
            Assert.That(initialDir, Is.EqualTo(Directory.GetCurrentDirectory()));
        });
    }

    [Test]
    public void FailTest()
    {
        ICommand cd = new Cd();
        var arguments = new List<String>{ "some/directory/that/does/not/exist" };
        var initialDir = Directory.GetCurrentDirectory();
        var commandResult = cd.Launch(arguments);
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Failed));
            Assert.That(cd.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(cd.Output.Message.First().Message, Is.EqualTo(String.Empty));
            Assert.That(cd.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(initialDir, Is.EqualTo(Directory.GetCurrentDirectory()));
        });
    }
    
    [Test]
    public void TooManyArgumentsTest()
    {
        ICommand cd = new Cd();
        var arguments = new List<String>{ "firstArguments", "someSecondArgument" };
        var initialDir = Directory.GetCurrentDirectory();
        var commandResult = cd.Launch(arguments);
        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.TooManyArguments));
            Assert.That(cd.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(cd.Output.Message.First().Message, Is.EqualTo(String.Empty));
            Assert.That(cd.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(initialDir, Is.EqualTo(Directory.GetCurrentDirectory()));
        });
    }
}