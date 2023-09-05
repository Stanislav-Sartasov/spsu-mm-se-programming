using System;
using System.Collections.Generic;
using Interfaces;
using Moq;
using NUnit.Framework;

namespace Task_1.UnitTests
{
    public class MyBashTests
    {
        [Test]
        public void GetPathToFileTest()
        {

            var path = "../../../TestFolder/";
            var bash = new MyBash(path);
            Assert.AreEqual("../../../TestFolder/test1.txt", bash.GetPathToFile("../../../TestFolder/test1.txt"));
            Assert.AreEqual(path + "test1.txt", bash.GetPathToFile(path + "test1.txt"));
            Assert.AreEqual("", bash.GetPathToFile("no file here"));

            Assert.Pass();
        }

        [Test]
        public void GetPathWithSlashTest()
        {
            var bash = new MyBash();
            Assert.AreEqual("test1.txt/", bash.GetPathWithSlash("test1.txt\\"));
            Assert.AreEqual("/", bash.GetPathWithSlash("\\"));
            Assert.AreEqual("test1.txt/", bash.GetPathWithSlash("test1.txt"));
            Assert.AreEqual("TestFolder/test.txt/", bash.GetPathWithSlash("TestFolder\\test.txt"));

            Assert.Pass();
        }

        [Test]
        public void GetLocalVarTest()
        {
            var localV = new Dictionary<string, string>();
            localV.Add("a", "b");
            var bash = new MyBash().WithLocalVar(localV);
            Assert.AreEqual("argument", bash.GetLocalVar("argument"));
            Assert.AreEqual("b", bash.GetLocalVar("a"));

            Assert.Pass();
        }
        
        [Test]
        public void ExecuteCommandsTest()
        {
            var bash = new MyBash("../../../TestFolder/");
            var mock = new Mock<IReader>();
            mock.Setup(x => x.Read()).Returns("cat TestFolder/test.txt | cat");
            mock.Setup(x => x.IsRead).Returns(true);
            bash.InitCommands(mock.Object);
            bash = bash.ExecuteCommands();
            Assert.AreEqual("1\r\n2\r\n3\r\n4\n5\r\n6\r\n7\r\n8\n", bash.Output);
            mock.Setup(x => x.Read()).Returns("wc test1.txt");
            bash.InitCommands(mock.Object);
            bash = bash.ExecuteCommands();
            Assert.AreEqual("4 4 10 test1.txt\n", bash.Output);
            bash = new MyBash("../../../TestFolder/TestFolder/");
            mock.Setup(x => x.Read()).Returns("pwd");
            bash.InitCommands(mock.Object);
            bash = bash.ExecuteCommands();
            Assert.AreEqual(new List<string> { "../../../TestFolder/TestFolder/", "test.txt", "test1.txt", "test2.txt"}, bash.CurrentArguments);
            mock.Setup(x => x.Read()).Returns("whoami");
            bash.InitCommands(mock.Object);
            bash = bash.ExecuteCommands();
            Assert.AreEqual(Environment.UserName + "\n", bash.Output);
            mock.Setup(x => x.Read()).Returns("$a=aa | echo a");
            bash.InitCommands(mock.Object);
            bash = bash.ExecuteCommands();
            Assert.AreEqual("aa\n", bash.Output);


            Assert.Pass();
        }

        [Test]
        public void InitCommandsTest()
        {
            var bash = new MyBash();
            var mock = new Mock<IReader>();
            mock.Setup(x => x.IsRead).Returns(false);
            mock.Setup(x => x.Read()).Returns(" ");
            bash.InitCommands(mock.Object);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            mock.Setup(x => x.Read()).Returns("==");
            bash.InitCommands(mock.Object);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            mock.Setup(x => x.Read()).Returns("| |");
            bash.InitCommands(mock.Object);
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);

            Assert.Pass();
        }
    }
}
