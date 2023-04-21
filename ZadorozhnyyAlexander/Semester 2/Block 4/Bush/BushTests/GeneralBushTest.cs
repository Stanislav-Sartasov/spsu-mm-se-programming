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
                new List<String>() { "Task 8 Bush(Bash)\n" +
                "That program can accomplish this list of commands: " +
                "\n1) echo [arg1] [arg2] .. [argN] - return arguments to standart output" +
                "\n2) cat [file1] [file2] .. [fileN] - returns contents of file specified, if file does not exist returns \"This file didn't exist!\"" +
                "\n + supports pipelined processing. *Watch 9)" +
                "\n3) wc [file1] [file2] .. [fileN] - returns lines, words and byte length of the files specified, if file does not exist returns \"This file or directory didn't exist!\"" +
                "\n + supports pipelined processing. *Watch 9)" +
                "\n4) cd [directory] - changes the directory to absolute or relative directory specified, returns nothing if successful, returns  \"This path doesn't exist!\"" +

                "\n5) pwd - returns name of directory and all the subdirectories and files within" +
                "\n6) exit [code] - exits the application with code specified. By default - 0." +
                "\n<-------Special Opportunities------->" +
                "\n7) If the command is not supported, Bush wil be tried to start app with the same name. Input stream and arguments will be passed to the app" +
                "\n8) You can assign local variables. Example: $varname=value" +
                "\n9) | used to a pipelined processing. Example: [cmd1 arg arg] | [cmd2 arg arg] | ..., outputs of cmd1 will become inputs of cmd2.\n" },
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