using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class ComandHandlerTests
    {
        [Test]
        public void VariableAssignmentAndGettingTest()
        {
            var handler = new CommandHandler();
            var varName = "myVar";
            var varValue = "myValue";

            var firstParsedCommand = new string[]
            {
                $"${varName}={varValue}"
            };

            var secondParsedCommand = new string[]
            {
                "echo", $"${varName}"
            };

            var commandResult = handler.Execute(firstParsedCommand);
            Assert.That(commandResult.Count() == 1);
            Assert.That(commandResult.First() == String.Empty);

            commandResult = handler.Execute(secondParsedCommand);
            Assert.That(commandResult.Count() == 1);
            Assert.That(commandResult.First() == varValue);
        }

        [Test]
        public void PipeLineTest()
        {
            var handler = new CommandHandler();

            var parsedCommand = new string[]
            {
                "pwd", "|", "echo"
            };

            var commandResult = handler.Execute(parsedCommand);

            Assert.That(commandResult.Count() == 1);
            Assert.AreEqual(Directory.GetCurrentDirectory(), commandResult.First());
        }

        [Test]
        public void AdditionalBashCommandLoadingTest()
        {
            var handler = new CommandHandler();
            var assemblyPath = Assembly.GetExecutingAssembly().Location;

            var firstParsedCommand = new string[]
            {
                $"&{assemblyPath}"
            };

            var secondParsedCommand = new string[]
            {
                "doSomething"
            };

            var thirdParsedCommand = new string[]
            {
                "doSomething", "argument"
            };

            var commandResult = handler.Execute(firstParsedCommand);
            Assert.That(commandResult.Count() == 1);
            Assert.That(commandResult.First() == String.Empty);

            commandResult = handler.Execute(secondParsedCommand);
            Assert.That(commandResult.Count() == 1);
            Assert.That(commandResult.First() == "Something has been done");

            commandResult = handler.Execute(thirdParsedCommand);
            Assert.That(commandResult.Count() == 1);
            Assert.That(commandResult.First() == "Invalid arguments");
        }
    }
}
