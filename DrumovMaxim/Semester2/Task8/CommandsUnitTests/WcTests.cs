using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class WcTest
{
    [Test]
    public void LaunchTest()
    {
        ICommand wc = new Wc();
        var fileContent = "This is content of test file to check if commands cat and wc are working properly.\n" +
                          "This file contains 2 lines.";
        var fileName = Path.GetRandomFileName() + ".txt";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        File.WriteAllText(filePath, fileContent);
        var arguments = new List<String> { fileName };
        var commandResult = wc.Launch(arguments);
        var commandMessage = $"21 2 110 {fileName}";
        
        try
        {
            Assert.Multiple(() =>
            {
                Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
                Assert.That(wc.Output.Message.Count(), Is.EqualTo(1));
                Assert.That(wc.Output.Message.First().ErrorOccured, Is.False);
                Assert.That(wc.Output.Message.First().ErrorMessage, Is.EqualTo(String.Empty));
                Assert.That(wc.Output.Message.First().Message, Is.EqualTo(commandMessage));
            });
            File.Delete(filePath);
        }
        catch (Exception)
        {
            File.Delete(filePath);
        }
    }

    [Test]
    public void FailedLaunchTest()
    {
        ICommand wc = new Wc();
        var filePath = Path.GetRandomFileName() + ".txt";
        var arguments = new List<String> { filePath };
        var commandResult = wc.Launch(arguments);

        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Failed));
            Assert.That(wc.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(wc.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(wc.Output.Message.First().ErrorMessage, Is.EqualTo($"{filePath}: such file does not exist"));
            Assert.That(wc.Output.Message.First().Message, Is.EqualTo(String.Empty));
        });
    }
}