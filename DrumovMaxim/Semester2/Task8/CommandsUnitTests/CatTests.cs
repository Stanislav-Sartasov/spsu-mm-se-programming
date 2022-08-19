using NUnit.Framework;
using Commands;
using Commands.BashCommands;

namespace CommandsUnitTests;

public class CatTests
{
    [Test]
    public void LaunchTest()
    {
        ICommand cat = new Cat();
        var fileContent = "This is content of test file to check if commands cat and wc are working properly.\n" +
                          "This file contains 2 lines.";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetRandomFileName() + ".txt");
        File.WriteAllText(filePath, fileContent);
        var arguments = new List<String> { filePath };
        var commandResult = cat.Launch(arguments);

        try
        {
            Assert.Multiple(() =>
            {
                Assert.That(commandResult, Is.EqualTo(CommandResult.Success));
                Assert.That(cat.Output.Message.Count(), Is.EqualTo(1));
                Assert.That(cat.Output.Message.First().ErrorOccured, Is.False);
                Assert.That(cat.Output.Message.First().ErrorMessage, Is.EqualTo(String.Empty));
                Assert.That(cat.Output.Message.First().Message, Is.EqualTo(fileContent));
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
        ICommand cat = new Cat();
        var filePath = "fileThatDoesNotExist.txt";
        var arguments = new List<String> { filePath };
        var commandResult = cat.Launch(arguments);

        Assert.Multiple(() =>
        {
            Assert.That(commandResult, Is.EqualTo(CommandResult.Failed));
            Assert.That(cat.Output.Message.Count(), Is.EqualTo(1));
            Assert.That(cat.Output.Message.First().ErrorOccured, Is.True);
            Assert.That(cat.Output.Message.First().ErrorMessage, Is.EqualTo("fileThatDoesNotExist.txt: such file does not exist"));
            Assert.That(cat.Output.Message.First().Message, Is.EqualTo(String.Empty));
        });
    }
}