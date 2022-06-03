using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class CommandInfoTest
    {
        [Test]
        public void ConstructorTest()
        {
            var cmdName = "someName";
            var cmdArguments = new List<string>() { "some argument" };
            var cmdInfo = new CommandInfo(cmdName, cmdArguments);
            Assert.IsNotNull(cmdInfo);
            Assert.That(cmdInfo.ShortName == cmdName);
            Assert.That(cmdArguments.Count == 1);
            Assert.That(cmdArguments.First() == cmdArguments.First());
        }

        [Test]
        public void AddArgumentsTest()
        {
            var cmdName = "someName";
            var cmdArguments = new List<string>() { "some argument" };
            var additionalArguments = new List<string>() { "some new argument" };
            var cmdInfo = new CommandInfo(cmdName, cmdArguments);

            cmdInfo.AddArguments(additionalArguments);
            Assert.That(cmdInfo.Arguments is not null);
            Assert.That(cmdInfo.Arguments.Count == 2);
            Assert.That(cmdInfo.Arguments.Last() == additionalArguments.First());
        }

        [Test]
        public void AddNullArgumentsTest()
        {
            var cmdName = "someName";
            var cmdInfo = new CommandInfo(cmdName, null);
            cmdInfo.AddArguments(null);

            Assert.That(cmdInfo.Arguments is null);
        }

        [Test]
        public void InsertArgumentFirst()
        {
            var cmdName = "someName";
            var cmdArguments = new List<string>() { "some argument" };
            var cmdInfo = new CommandInfo(cmdName, cmdArguments);
            

            // insert one argument
            var additionalArgument = "something";
            cmdInfo.InsertArgumentFirst(additionalArgument);
            
            Assert.That(cmdInfo.Arguments is not null);
            Assert.That(cmdInfo.Arguments.Count == 2);
            Assert.That(cmdInfo.Arguments.First() == additionalArgument);

            // insert more than one argument
            var additionalArguments = new List<string>()
                { 
                    "fisr new argument", 
                    "second new argument"
                };

            cmdInfo.InsertArgumentsFirst(additionalArguments);
            Assert.That(cmdInfo.Arguments is not null);
            Assert.That(cmdInfo.Arguments.Count == 4);
            Assert.That(cmdInfo.Arguments.First() == additionalArguments.First());
            Assert.That(cmdInfo.Arguments[1] == additionalArguments[1]);
        }

        [Test]
        public void InsertNullArgumentFirst()
        {
            var cmdName = "someName";
            var cmdInfo = new CommandInfo(cmdName, null);
            cmdInfo.InsertArgumentFirst(null);
            cmdInfo.InsertArgumentsFirst(null);

            Assert.That(cmdInfo.Arguments is null);
        }

        // this test deals with a case when user wants to add argument when Collection of arguments is not created
        [Test]
        public void InsertOrAddSomething()
        {
            var cmdName = "someName";
            var someArgument = "argument";
            var cmdInfo = new CommandInfo(cmdName, null);
            cmdInfo.InsertArgumentFirst(someArgument);

            Assert.That(cmdInfo.Arguments is  not null);
            Assert.That(cmdInfo.Arguments.Count == 1);
            Assert.That(cmdInfo.Arguments.First() == someArgument);
        }
    }
}
