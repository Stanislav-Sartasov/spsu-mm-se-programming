namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using System.IO;

public class PwdTests
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
    public void WithoutArgumentsTest()
    {
        interpreter.ExecuteLine("pwd", out string result);
        Assert.AreEqual(Directory.GetCurrentDirectory().Replace("\\", "/"), result);
    }

    [Test]
    public void WithArgumentsTest()
    {
        interpreter.ExecuteLine("pwd echo lala lolo \"qwerty\" !!!", out string result);
        Assert.AreEqual(Directory.GetCurrentDirectory().Replace("\\", "/"), result);
    }
}
