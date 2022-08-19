using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class LsTests
{
    [Test]
    public void LaunchSuccessTest()
    {
        var entities = Directory.EnumerateFileSystemEntries(Directory.GetCurrentDirectory()).Select(x => x.Split('/').Last());
        ICommand ls = new Ls();
        var arguments = new List<String> { Directory.GetCurrentDirectory() };
        var commandResult = ls.Launch(arguments);
        
        Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
        Assert.That(ls.Output.Message.Count(), Is.EqualTo(1));
        Assert.IsFalse(ls.Output.Message.First().ErrorOccured);
        
        var resultMessage = ls.Output.Message.First().Message;
        foreach (var entity in entities)
        {
            Assert.That(resultMessage, Does.Contain(entity)); 
        }
    }

    [Test]
    public void LaunchFailTest()
    {
        ICommand ls = new Ls();
        var arguments = new List<String> { "some/path/that/does/not/exist" };
        var launchResult = ls.Launch(arguments);
        var errorMessage = ls.Output.Message.First().ErrorMessage;
        Assert.Multiple(() =>
        {
            Assert.That(launchResult, Is.EqualTo(CommandResult.Failed));
            Assert.That(ls.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(ls.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(ls.Output.Message.First().ErrorMessage, Is.EqualTo("some/path/that/does/not/exist: directory does not exists"));
        });
    }
}