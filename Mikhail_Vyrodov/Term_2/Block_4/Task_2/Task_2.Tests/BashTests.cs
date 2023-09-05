using NUnit.Framework;
using System.Collections.Generic;
using Task_2;
using System.IO;
using System;

namespace Task_2.Tests
{
    public class BashTests
    {

        [Test]
        public void PwdTest()
        {
            CommandApplier cmdApplier = new CommandApplier();
            string result = "";
            result += "Current directory:\n";
            result += String.Format("\t{0}\n", Directory.GetCurrentDirectory());
            result += "Inner directories:\n";
            foreach (string dir in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                result += String.Format("\t{0}\n", dir);
            }
            result += "Inner files:\n";
            foreach (string dir in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                result += String.Format("\t{0}\n", dir);
            }
            string testAnswer = cmdApplier.ApplySimpleCommand("pwd");
            Assert.AreEqual(testAnswer, result);
        }

        [Test]
        public void EchoTest()
        {
            CommandApplier cmdApplier = new CommandApplier();
            List<string> commands = new List<string>();
            commands.Add("$variable=12345");
            commands.Add("echo \"I'm thinking about $variable\"");
            commands.Add("exit");
            string result = "I'm thinking about 12345";
            Assert.AreEqual(result, cmdApplier.ReadCommands(commands));
        }

        [Test]
        public void ComplexCommandTest()
        {
            string actualResult = "Words in file - 4\nStrings in file - 3\nFile size in bytes - 27";
            CommandApplier cmdApplier = new CommandApplier();
            string testAnswer = cmdApplier.ApplyComplexCommand("cat ..\\..\\..\\Data\\filePath.txt | wc", "", true);
            Assert.AreEqual(actualResult, testAnswer);
        }

        [Test]
        public void OtherCommandTest()
        {
            string actualResult = "Can't recognize the command or name of program\nPlease use quotes to set a program name";
            CommandApplier cmdApplier = new CommandApplier();
            string testAnswer = cmdApplier.ApplySimpleCommand("wrong path");
            Assert.AreEqual(actualResult, testAnswer);
            actualResult = "No program or command found with this name";
            testAnswer = cmdApplier.ApplySimpleCommand("\"wrongPath\"");
            Assert.AreEqual(actualResult, testAnswer);
        }
    }
}