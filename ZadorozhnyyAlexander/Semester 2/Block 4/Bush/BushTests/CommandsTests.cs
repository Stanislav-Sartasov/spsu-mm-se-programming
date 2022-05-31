using AbstractOperators;
using Commands;
using Exceptions;
using FileExplorer;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BushTests
{
    public class CommandsTests
    {
        public bool dir = FileManager.SetDirectory("..\\..\\..\\TestFiles");

        [Test]
        public void CatTest()
        {
            ACommand cat = new Cat();

            var args = new List<String>() { "firstTest.txt" };
            Assert.AreEqual(cat.Execute(args, new List<String>()), new List<String>() { "first\r\ntestFile" });

            var input = new List<String>() { "some input\n" };
            Assert.AreEqual(cat.Execute(args, input), new List<String>() { "some input\nfirst\r\ntestFile" });

            args.Add("secondTest.txt");
            Assert.AreEqual(cat.Execute(args, new List<String>()), new List<String>() { "first\r\ntestFilesecond testFile" });

            args.Add("notExist.txt");
            Assert.AreEqual(cat.Execute(args, new List<String>()), new List<String>() { "first\r\ntestFilesecond testFileThis file didn't exist!" });
        }

        [Test]
        public void CdTest()
        {
            ACommand cd = new Cd();
            var args = new List<String>() { "TestDirectory" };
            Assert.AreEqual(cd.Execute(args, new List<String>()), new List<String>());

            FileManager.SetDirectory("..");
            args.Add("Test2Directory");
            Assert.AreEqual(cd.Execute(args, new List<String>()), new List<String>());

            FileManager.SetDirectory("..");
            Assert.AreEqual(cd.Execute(args, new List<String>() { "some input" }), new List<String>());

            FileManager.SetDirectory("..");
            var newArgs = new List<String>() { "NotExistDirectory" };
            Assert.AreEqual(cd.Execute(newArgs, new List<String>()), new List<String>() { "This path doesn't exist!" });
        }

        [Test]
        public void EchoTest()
        {
            ACommand echo = new Echo();

            var args = new List<String>() { "123", "456" };
            Assert.AreEqual(echo.Execute(args, new List<String>() { "some input" }), new List<String>() { "123 456" });
        }

        [Test]
        public void ExitTest()
        {
            ACommand exit = new Exit();

            var args = new List<String>();
            try
            {
                exit.Execute(args, new List<String>() { "some input" });
                Assert.Fail();
            }
            catch (ExitException ex)
            {
                Assert.AreEqual(ex.ExitCode, 0);
            }
           
            args.Add("-10");
            try
            {
                exit.Execute(args, new List<String>() { "some input" });
                Assert.Fail();
            }
            catch (ExitException ex)
            {
                Assert.AreEqual(ex.ExitCode, -10);
            }

            args.Add("1");
            try
            {
                exit.Execute(args, new List<String>() { "some input" });
                Assert.Fail();
            }
            catch (ExitException ex)
            {
                Assert.AreEqual(ex.ExitCode, -10);
            }

            var newArgs = new List<String>() { "str" };
            try
            {
                exit.Execute(newArgs, new List<String>() { "some input" });
                Assert.Fail();
            }
            catch (ExitException ex)
            {
                Assert.AreEqual(ex.ExitCode, 0);
            }
        }

        [Test]
        public void PwdTest()
        {
            ACommand pwd = new Pwd();

            var pwdRes = pwd.Execute(new List<String>(), new List<String>() { "some input" });
            pwdRes.Sort();

            var rightRes = new List<String>() { FileManager.GetCurrentDirectory(), "\\TestDirectory", "firstTest.txt", "secondTest.txt" };
            rightRes.Sort();

            Assert.AreEqual(pwdRes, rightRes);
        }

        [Test]
        public void WcTest()
        {
            ACommand wc = new Wc();

            var args = new List<String>() { "firstTest.txt" };
            Assert.AreEqual(wc.Execute(args, new List<String>()), new List<String>() { "firstTest.txt\t2\t2\t15" });

            var input = new List<String>() { "some input" };
            Assert.AreEqual(wc.Execute(args, input), new List<String>() { "Input stream\t1\t2\t10", "firstTest.txt\t2\t2\t15" });

            args.Add("secondTest.txt");
            Assert.AreEqual(wc.Execute(args, new List<String>()), new List<String>() { "firstTest.txt\t2\t2\t15", "secondTest.txt\t1\t2\t15" });

            args.Add("notExist.txt");
            Assert.AreEqual(wc.Execute(args, new List<String>()), new List<String>() { "firstTest.txt\t2\t2\t15", "secondTest.txt\t1\t2\t15", "This file or directory didn't exist!" });
        }
    }
}