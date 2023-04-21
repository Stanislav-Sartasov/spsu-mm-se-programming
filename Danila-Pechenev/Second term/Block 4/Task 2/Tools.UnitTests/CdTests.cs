namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using System.IO;

public class CdTests
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
    string currentDirectory = Directory.GetCurrentDirectory();
    string newDirectory = "";

    [Test]
    public void DotsTest()
    {
        interpreter.ExecuteLine("cd ..", out string result);
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("", result);
    }

    [Test]
    public void RelativePathTest()
    {
        interpreter.ExecuteLine("cd ../../../../Tools.UnitTests", out string result);
        newDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("", result);
        Assert.AreEqual("Tools.UnitTests", newDirectory.Split("\\")[^1]);
    }

    [Test]
    public void AbsolutePathTest()
    {
        interpreter.ExecuteLine("cd /", out string result);
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("", result);
    }

    [Test]
    public void NonexistentPathTest()
    {
        interpreter.ExecuteLine("cd lala/lala", out string result);
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("path does not exist", result);
    }

    [Test]
    public void EmptyArgumentsLineTest()
    {
        interpreter.ExecuteLine("cd", out string result);
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("", result);
    }
}
