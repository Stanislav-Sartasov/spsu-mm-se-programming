namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using System;

public class CatTests
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
        interpreter.ExecuteLine("cat ../../../../Tools.UnitTests/TestFiles/test1.txt", out string result);
        Assert.AreEqual(2796, result.Split(Environment.NewLine).Length);
    }

    [Test]
    public void SecondTestFileTest()
    {
        interpreter.ExecuteLine("cat ../../../../Tools.UnitTests/TestFiles/test2.sh", out string result);
        Assert.AreEqual(234, result.Split(Environment.NewLine).Length);
    }

    [Test]
    public void NonexistentPathTest()
    {
        interpreter.ExecuteLine("cat ../../../../Tools.UnitTests/TestFiles/test3.txt", out string result);
        Assert.AreEqual("path does not exist", result);
    }

    [Test]
    public void AFewFilesTest()
    {
        interpreter.ExecuteLine("cat ../../../../Tools.UnitTests/TestFiles/test1.txt ../../../../Tools.UnitTests/TestFiles/test2.sh", out string result);
        Assert.AreEqual(2796 + 234, result.Split(Environment.NewLine).Length);
    }

    [Test]
    public void StdinTest()
    {
        interpreter.ExecuteLine("echo qwerty123text secondLine w ww www | cat", out string result);
        Assert.AreEqual("qwerty123text secondLine w ww www", result);
    }

    [Test]
    public void StdinWithIncorrectInputOfPreviousCommandTest()
    {
        interpreter.ExecuteLine("wc ttttt.ttt | cat", out string result);
        Assert.AreEqual("", result.Split(Environment.NewLine)[1]);
    }

    [Test]
    public void EmptyArgumentsLineTest()
    {
        interpreter.ExecuteLine("cat", out string result);
        Assert.AreEqual("", result);
    }
}
