using BashComponents;
using NUnit.Framework;

namespace BashComponentsUnitTests;

public class CommandManagerTests
{
    [Test]
    public void EchoRunTest()
    {
        var manager = new CommandManager();
        var lexemes = new List<String> { "echo", "firstArgument", "|", "echo", "secondArgument" };
        var result = manager.Run(lexemes);
        Assert.That(result, Is.EqualTo("secondArgument firstArgument"));
    }
    
    [Test]
    public void PwdRunTest()
    {
        var manager = new CommandManager();
        var lexemes = new List<String> { "pwd", "someParameter" };
        var result = manager.Run(lexemes);
        Assert.That(result, Is.EqualTo(Directory.GetCurrentDirectory()));
    }
    
    [Test]
    public void CdRunTest()
    {
        var manager = new CommandManager();
        var lexemes = new List<String> { "cd", ".." };
        var result = manager.Run(lexemes);
        Assert.That(result, Is.EqualTo(String.Empty));
    }
    
    [Test]
    public void CatRunTest()
    {
        var manager = new CommandManager();
        var fileName = Path.GetRandomFileName() + ".txt";
        var lexemes = new List<String> { "cat", fileName };
        var result = manager.Run(lexemes);
        Assert.That(result, Is.EqualTo($"cat: {fileName}: such file does not exist"));
    }
    
    [Test]
    public void WcRunTest()
    {
        var manager = new CommandManager();
        var fileName = Path.GetRandomFileName() + ".txt";
        var lexemes = new List<String> { "wc", fileName };
        var result = manager.Run(lexemes);
        Assert.That(result, Is.EqualTo($"wc: {fileName}: such file does not exist"));
    }
}