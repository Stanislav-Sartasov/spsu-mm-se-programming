namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;

public class LsTests
{
    static List<ICommand> listOfCommands = new List<ICommand>
        {
            new Echo(),
            new Pwd(),
            new Ls(),
            new Cd(),
            new Cat(),
            new Wc()
        };

    Interpreter interpreter = new Interpreter(listOfCommands);

    [Test]
    public void WithArgumentTest()
    {
        interpreter.ExecuteLine("ls ../../../", out string result);
        Assert.IsTrue(result.Length > 0);
        Assert.AreNotEqual("path does not exist", result);
    }

    [Test]
    public void NonexistentPathTest()
    {
        interpreter.ExecuteLine("ls lala/lolo", out string result);
        Assert.AreEqual("path does not exist", result);
    }

    [Test]
    public void EmptyArgumentsLineTest()
    {
        interpreter.ExecuteLine("ls", out string result);
        Assert.IsTrue(result.Length > 0);
        Assert.AreNotEqual("path does not exist", result);
    }
}
