namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;

public class ExitTests
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
    public void ExitTest()
    {
        Assert.AreEqual(ResultCode.Exit, interpreter.ExecuteLine("exit", out string result));
    }
}
