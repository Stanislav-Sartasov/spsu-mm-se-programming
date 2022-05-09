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

            var path = "../../../";
            var bash = new MyBash(path);
            Assert.AreEqual("../../../test1.txt", bash.GetPathToFile("../../../test1.txt"));
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
        public void SetArgumentTest()
        {
            var args = new Queue<string>();
            args.Enqueue("argument1");
            args.Enqueue("argument2");
            args.Enqueue("argument3");
            var bash = new MyBash().WithArguments(args);
            bash.SetArgument();
            Assert.AreEqual(new List<string> { "argument1" }, bash.CurrentArguments);
            bash.SetArgument();
            bash.SetArgument();
            Assert.AreEqual(new List<string> { "argument3" }, bash.CurrentArguments);

            Assert.Pass();
        }

        [Test]
        public void PipeLineTest()
        {
            var bash = new MyBash().WithCurrentArguments(new List<string> { "argument1\nargument2\nargument3" });
            bash.Pipeline();
            Assert.AreEqual(new List<string> { "argument1", "argument2", "argument3" }, bash.CurrentArguments);
            bash.Pipeline();
            Assert.AreEqual(new List<string> { "argument1", "argument2", "argument3" }, bash.CurrentArguments);

            Assert.Pass();
        }

        [Test]
        public void CdTest()
        {
            var bash = new MyBash().WithCurrentArguments(new List<string> { "not a file" });
            bash.Cd();
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../TestFolder" });
            bash.Cd();
            Assert.AreEqual("../../../TestFolder/", bash.Path);
            bash = new MyBash("../../../").WithCurrentArguments(new List<string> { "TestFolder" });
            bash.Cd();
            Assert.AreEqual("../../../TestFolder/", bash.Path);

            Assert.Pass();
        }

        [Test]
        public void WhoamiTest()
        {
            var bash = new MyBash();
            bash.Whoami();
            Assert.AreEqual(new List<string> { Environment.UserName }, bash.CurrentArguments);

            Assert.Pass();
        }

        [Test]
        public void WcTest()
        {
            var bash = new MyBash();
            bash.Wc();
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "" });
            bash.Wc();
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../test1.txt" });
            bash.Wc();
            Assert.AreEqual(new List<string> { "4 4 10" }, bash.CurrentArguments);

            Assert.Pass();
        }

        [Test]
        public void CatTest()
        {
            var bash = new MyBash();
            bash.Cat();
            Assert.AreEqual(0, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "" });
            bash.Cat();
            Assert.AreEqual(1, bash.GetErrorMessenges().Count);
            bash = new MyBash().WithCurrentArguments(new List<string> { "../../../test1.txt" });
            bash.Cat();
            Assert.AreEqual(new List<string> { "1\r\n2\r\n3\r\n4" }, bash.CurrentArguments);
            bash = new MyBash("../../../").WithCurrentArguments(new List<string> { "test1.txt", "test2.txt" });
            bash.Cat();
            Assert.AreEqual(new List<string> { "1\r\n2\r\n3\r\n4", "5\r\n6\r\n7\r\n8" }, bash.CurrentArguments);
            
            Assert.Pass();
        }

        [Test]
        public void PwdTest()
        {
            var bash = new MyBash("../../../TestFolder/");
            bash.Pwd();
            Assert.AreEqual(new List<string> { "../../../TestFolder/", "test.txt", "test1.txt", "test2.txt" }, bash.CurrentArguments);

            Assert.Pass();
        }

        [Test]
        public void EchoTest()
        {
            var bash = new MyBash();
            bash.Echo();
            Assert.AreEqual("", bash.Output);
            bash = new MyBash().WithCurrentArguments(new List<string> { "argument1", "argument2", "argument3" });
            bash.Echo();
            Assert.AreEqual("argument1\nargument2\nargument3\n", bash.Output);

            Assert.Pass();
        }

        [Test]
        public void ExecuteCommandsTest()
        {
            var bash = new MyBash("../../../");
            var mock = new Mock<IReader>();
            mock.Setup(x => x.Read()).Returns("cat TestFolder/test.txt | cat");
            mock.Setup(x => x.IsRead).Returns(true);
            bash.InitCommands(mock.Object);
            bash.ExecuteCommands();
            Assert.AreEqual("1\r\n2\r\n3\r\n4\n5\r\n6\r\n7\r\n8\n", bash.Output);
            mock.Setup(x => x.Read()).Returns("wc test1.txt");
            bash.InitCommands(mock.Object);
            bash.ExecuteCommands();
            Assert.AreEqual("4 4 10\n", bash.Output);
            bash = new MyBash("../../../TestFolder/");
            mock.Setup(x => x.Read()).Returns("pwd");
            bash.InitCommands(mock.Object);
            bash.ExecuteCommands();
            Assert.AreEqual(new List<string> { "../../../TestFolder/", "test.txt", "test1.txt", "test2.txt"}, bash.CurrentArguments);
            mock.Setup(x => x.Read()).Returns("whoami");
            bash.InitCommands(mock.Object);
            bash.ExecuteCommands();
            Assert.AreEqual(Environment.UserName + "\n", bash.Output);
            mock.Setup(x => x.Read()).Returns("$a=aa | echo a");
            bash.InitCommands(mock.Object);
            bash.ExecuteCommands();
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
