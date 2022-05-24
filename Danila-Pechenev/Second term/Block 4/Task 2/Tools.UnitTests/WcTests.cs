namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using System;

public class WcTests
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
    public void FirstTestFileTest()
    {
        interpreter.ExecuteLine("wc ../../../../Tools.UnitTests/TestFiles/test1.txt", out string result);
        Assert.AreEqual("2796    55974    614951", result);
    }

    [Test]
    public void SecondTestFileTest()
    {
        interpreter.ExecuteLine("wc ../../../../Tools.UnitTests/TestFiles/test2.sh", out string result);
        Assert.AreEqual("234    1159    8070", result);
    }

    [Test]
    public void NonexistentPathTest()
    {
        interpreter.ExecuteLine("wc ../../../../Tools.UnitTests/TestFiles/test3.txt", out string result);
        Assert.AreEqual("path does not exist", result);
    }

    [Test]
    public void AFewFilesTest()
    {
        interpreter.ExecuteLine("wc ../../../../Tools.UnitTests/TestFiles/test1.txt ../../../../Tools.UnitTests/TestFiles/test2.sh", out string result);
        Assert.AreEqual("2796    55974    614951" + Environment.NewLine + "234    1159    8070", result);
    }

    [Test]
    public void StdinTest()
    {
        interpreter.ExecuteLine("echo qwerty123text secondWord test tes te t | wc", out string result);
        Assert.AreEqual("1    6    38", result);
    }

    [Test]
    public void StdinWithIncorrectInputOfPreviousCommandTest()
    {
        interpreter.ExecuteLine("cat ttttt.ttt | wc", out string result);
        Assert.AreEqual("0    0    0", result.Split(Environment.NewLine)[1]);
    }

    [Test]
    public void EmptyArgumentsLineTest()
    {
        interpreter.ExecuteLine("wc", out string result);
        Assert.AreEqual("", result);
    }
}
