using NUnit.Framework;
using Enumerations;
using System.Collections.Generic;

namespace Task_1.UnitTests
{
    public class AnalyzerTests
    {
        [Test]
        public void GetCommandTest()
        {
            var data = new List<(string str, TypeOfParsedStr type)>();
            var a = new Analyzer(data);
            Assert.AreEqual(a.GetCommand("echo"), Command.Echo);
            Assert.AreEqual(a.GetCommand("exit"), Command.Exit);
            Assert.AreEqual(a.GetCommand("pwd"), Command.Pwd);
            Assert.AreEqual(a.GetCommand("cat"), Command.Cat);
            Assert.AreEqual(a.GetCommand("cd"), Command.Cd);
            Assert.AreEqual(a.GetCommand("wc"), Command.Wc);
            Assert.AreEqual(a.GetCommand("whoami"), Command.Whoami);
            Assert.AreEqual(a.GetCommand("|"), Command.Pipeline);
            Assert.AreEqual(a.GetCommand("$"), Command.SetLocalVar);
            Assert.AreEqual(a.GetCommand("clear"), Command.Clear);
            Assert.AreEqual(a.GetCommand("not a command"), Command.Exit);
        }

        [Test]
        public void AnalyzeTest()
        {
            var data = new List<(string str, TypeOfParsedStr type)> { ("not a command", TypeOfParsedStr.Argument)};
            var a = new Analyzer(data);
            a.Analyze();
            Assert.IsFalse(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("arg", TypeOfParsedStr.Argument), ("arg", TypeOfParsedStr.Argument) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsFalse(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("|", TypeOfParsedStr.PipeLine), ("|", TypeOfParsedStr.PipeLine) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsFalse(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("echo", TypeOfParsedStr.Command), ("echo", TypeOfParsedStr.Command) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsFalse(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> 
            { 
                ("a=a", TypeOfParsedStr.DeclaringLocalVariable), 
                ("b=b", TypeOfParsedStr.DeclaringLocalVariable) 
            };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsFalse(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("echo", TypeOfParsedStr.Command) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.AreEqual(new List<Command> { Command.Echo }, a.GetCommands());
            Assert.IsTrue(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("echo", TypeOfParsedStr.Command), ("arg", TypeOfParsedStr.Argument) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.AreEqual(new List<Command> { Command.SetArgument, Command.Echo }, a.GetCommands());
            var q = new Queue<string>();
            q.Enqueue("arg");
            Assert.AreEqual(q, a.GetArguments());
            Assert.IsTrue(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)> { ("a=a", TypeOfParsedStr.DeclaringLocalVariable) };
            a = new Analyzer(data);
            a.Analyze();
            Assert.AreEqual(new List<Command> { Command.SetLocalVar }, a.GetCommands());
            var qq = new Queue<(string name, string value)>();
            qq.Enqueue(("a", "a"));
            Assert.AreEqual(qq, a.GetArgumentsForSetLocalVar());
            Assert.IsTrue(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)>
            {
                ("echo", TypeOfParsedStr.Command),
                ("|", TypeOfParsedStr.PipeLine),
                ("echo", TypeOfParsedStr.Command)
            };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsTrue(a.IsAnalyzed);

            data = new List<(string str, TypeOfParsedStr type)>
            {
                ("echo", TypeOfParsedStr.Command),
                ("123", TypeOfParsedStr.Argument),
                ("|", TypeOfParsedStr.PipeLine),
                ("echo", TypeOfParsedStr.Command),
                ("123", TypeOfParsedStr.Argument),
            };
            a = new Analyzer(data);
            a.Analyze();
            Assert.IsTrue(a.IsAnalyzed);

            Assert.Pass();
        }
    }
}
