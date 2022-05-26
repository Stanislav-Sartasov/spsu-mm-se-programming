using AbstractOperators;
using BushRealisation;
using Commands;
using Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BushTests
{
    public class GeneralBushTests
    {
        private Dictionary<string, ACommand> commands = new Dictionary<string, ACommand>();
        private ALogger logger;
        private ARunner runner;

        [Test]
        public void GeneralBushTest()
        {
            commands.Add("echo", new Echo()); // We can test bash only at one-two command, because all commands was tested at another UnitTest file.
            commands.Add("cat", new Cat());
            commands.Add("exit", new Exit());

            logger = new LoggerForTesting(new List<String>()
            {
                "$VAR=123",
                "$VARSEC=456",
                "echo arg $VAR    \"$VARSEC     $VAR\"",
                "echo arg $VAR | cat",
                "NOTHING=54",
                "echo $NOTHING",
                "NOTEXISTCOMMAND something",
                "exit"
            },
            new List<List<String>>() 
            { 
                new List<String>() { "\r\n" }, 
                new List<String>() { "\r\n" }, 
                new List<String>() { "arg 123 456     123", "\r\n" },
                new List<String>() { "arg 123", "\r\n" },
                new List<String>() { "Uncorrect assightment. It should be: $variable=value", "\r\n" },
                new List<String>() { "$NOTHING variable not found", "\r\n" },
                new List<String>() { "Process was complited!", "\r\n" }
            });
            runner = new RunnerForTesting(logger);
            try
            {
                new Bush(logger, runner, commands).Run(); // If something goes wrong, logger throw Exception that answer and received value are not the same
            }
            catch(ExitException ex)
            {
                Assert.AreEqual(ex.ExitCode, 0);
            }
            catch(Exception)
            {
                Assert.Fail();
            }
            
            Assert.Pass();
            
        }
    }
}