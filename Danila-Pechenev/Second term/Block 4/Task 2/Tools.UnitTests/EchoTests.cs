namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;

public class EchoTests
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
    public void EchoTest()
    {
        interpreter.ExecuteLine("echo Hi! \"How\" 'are' y\"o\"u?", out string result);
        Assert.AreEqual("Hi! How are you?", result);
    }

    [Test]
    public void DoubleEchoTest()
    {
        interpreter.ExecuteLine("echo echo", out string result);
        Assert.AreEqual("echo", result);
    }

    [Test]
    public void ArgumentsNullTest()
    {
        bool success = listOfCommands[0].Execute(null, out string result, false, "");
        Assert.AreEqual("", result);
        Assert.AreEqual(false, success);
    }
}
