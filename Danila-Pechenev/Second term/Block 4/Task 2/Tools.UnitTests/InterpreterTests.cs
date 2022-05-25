namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using System;
using System.IO;

public class InterpreterTests
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
    public void CommandWithDoubleQuotesTest()
    {
        interpreter.ExecuteLine("e\"ch\"o lalala", out string result);
        Assert.AreEqual("lalala", result);
    }

    [Test]
    public void CommandWithSingleQuotesTest()
    {
        interpreter.ExecuteLine("e'ch'o test", out string result);
        Assert.AreEqual("test", result);
    }

    [Test]
    public void PipelineEchoTest()
    {
        interpreter.ExecuteLine("echo test1 | echo test2", out string result);
        Assert.AreEqual("test2", result);
    }

    [Test]
    public void PipelineWcTest()
    {
        interpreter.ExecuteLine("echo text text text 0123456789 | wc", out string result);
        Assert.AreEqual("1    4    25", result);
    }

    [Test]
    public void PipelineCatTest()
    {
        interpreter.ExecuteLine("echo | echo \"abc 123 !\" | cat", out string result);
        Assert.AreEqual("abc 123 !", result);
    }

    [Test]
    public void PipelinePwdTest()
    {
        interpreter.ExecuteLine("echo qwerty | echo 123 | ls | pwd", out string result);
        Assert.AreEqual(Directory.GetCurrentDirectory().Replace("\\", "/"), result);
    }

    [Test]
    public void PipelineLsTest()
    {
        interpreter.ExecuteLine("echo qwerty | cat | ls", out string result);
        Assert.IsTrue(result.Length > 0);
        Assert.AreNotEqual("path does not exist", result);
    }

    [Test]
    public void PipelineExitTest()
    {
        Assert.AreEqual(ResultCode.Exit, interpreter.ExecuteLine("echo 'Hi, $name' | cat | wc | pwd | exit | echo abc", out string result));
        Assert.AreEqual("", result);
    }

    [Test]
    public void PipelineCdTest()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        interpreter.ExecuteLine("echo text text | wc | cat | cd ../../../../Tools.UnitTests", out string result);
        var newDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(currentDirectory);
        Assert.AreEqual("", result);
        Assert.AreEqual("Tools.UnitTests", newDirectory.Split("\\")[^1]);
    }

    [Test]
    public void PipelineNewVariableTest()
    {
        interpreter.ExecuteLine("echo test | wc | a=5 | cat", out string result);
        Assert.AreEqual("", result);
        Assert.AreEqual("5", Runtime.GetVariable("a"));
    }

    [Test]
    public void PipelineLaunchProgramTest()
    {
        interpreter.ExecuteLine("echo test | whoami | cat", out string result);
        Assert.IsTrue(result.Length > 0);
    }

    [Test]
    public void PipelineWithErrorsTest()
    {
        interpreter.ExecuteLine("cat ttt.txt | k = 5 | wc", out string result);
        var results = result.Split(Environment.NewLine);
        Assert.AreEqual("path does not exist", results[0]);
        Assert.AreEqual("unexpected sequence", results[1]);
        Assert.AreEqual("0    0    0", results[2]);
    }

    [Test]
    public void EmptyInputTest()
    {
        var code = interpreter.ExecuteLine("", out string result);
        Assert.AreEqual("", result);
        Assert.AreEqual(ResultCode.Success, code);
    }

    [Test]
    public void EmptyVariableTest()
    {
        interpreter.ExecuteLine("a=", out string result);
        Assert.AreEqual("unexpected sequence", result);
    }

    [Test]
    public void IncorrectVariableNameTest()
    {
        interpreter.ExecuteLine("ewrr!e=123", out string result);
        Assert.AreEqual("unexpected sequence", result);
    }

    [Test]
    public void EmptyVariableNameTest()
    {
        interpreter.ExecuteLine("=123", out string result);
        Assert.AreEqual("unexpected sequence", result);
    }

    [Test]
    public void IncorrectInputTest()
    {
        interpreter.ExecuteLine("echo 'qwerty", out string result);
        Assert.AreEqual("unexpected sequence", result);
    }

    [Test]
    public void UnknownCommandTest()
    {
        interpreter.ExecuteLine("ech 123", out string result);
        Assert.AreEqual("command not found", result);
    }

    [Test]
    public void UnknownCommandSpecialCharacterTest()
    {
        interpreter.ExecuteLine("$", out string result);
        Assert.AreEqual("command not found", result);
    }

    [Test]
    public void EmptyCommandsTest()
    {
        interpreter.ExecuteLine(" | echo 123 | ", out string result);
        Assert.AreEqual("123", result);
    }

    [Test]
    public void VariableNameStartsFromDigitTest()
    {
        interpreter.ExecuteLine("1var=123", out string result);
        Assert.AreEqual("unexpected sequence", result);
    }
}
