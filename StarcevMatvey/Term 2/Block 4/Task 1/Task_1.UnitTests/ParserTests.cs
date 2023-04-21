using NUnit.Framework;
using Enumerations;
using System.Collections.Generic;

namespace Task_1.UnitTests
{
    public class ParserTests
    {
        [Test]
        public void ParseTest()
        {
            var p = new Parser("echo \"123");
            p.Parse();
            Assert.IsFalse(p.IsParsed);

            p = new Parser("$a==a");
            p.Parse();
            Assert.IsFalse(p.IsParsed);

            p = new Parser("$a=\"aa");
            p.Parse();
            Assert.IsFalse(p.IsParsed);

            p = new Parser("$a=\"a a\"");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("a=a a", TypeOfParsedStr.DeclaringLocalVariable)
            }
            );

            p = new Parser("echo");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("echo", TypeOfParsedStr.Command)
            }
            );

            p = new Parser("something.exe");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("something.exe", TypeOfParsedStr.ProcessStart)
            }
            );

            p = new Parser("\"argument argument\"");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("argument argument", TypeOfParsedStr.Argument)
            }
            );

            p = new Parser("|");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("|", TypeOfParsedStr.PipeLine)
            }
            );

            p = new Parser("echo 123 | clear | | 123 exit pwd cat wc cd whoami");
            p.Parse();
            Assert.IsTrue(p.IsParsed);
            Assert.AreEqual(p.GetParsedString(), new List<(string str, TypeOfParsedStr type)>
            {
                ("echo", TypeOfParsedStr.Command),
                ("123", TypeOfParsedStr.Argument),
                ("|", TypeOfParsedStr.PipeLine),
                ("clear", TypeOfParsedStr.Command),
                ("|", TypeOfParsedStr.PipeLine),
                ("|", TypeOfParsedStr.PipeLine),
                ("123", TypeOfParsedStr.Argument),
                ("exit", TypeOfParsedStr.Command),
                ("pwd", TypeOfParsedStr.Command),
                ("cat", TypeOfParsedStr.Command),
                ("wc", TypeOfParsedStr.Command),
                ("cd", TypeOfParsedStr.Command),
                ("whoami", TypeOfParsedStr.Command)
            }
            );

            Assert.Pass();
        }
    }
}