using CommandManager;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BushTests
{
    public class SplitterTests
    {
        private string testedStroke;
        private Splitter splitter = new Splitter();

        [Test]
        public void ParseToSingleCommandsTest()
        {
            testedStroke = "$VAR=value";
            Assert.AreEqual(splitter.ParseToSingleCommands(testedStroke), Tuple.Create(false, new List<String>() { "$VAR", "value"} ));

            testedStroke = "cmd arg arg | cmd arg";
            Assert.AreEqual(splitter.ParseToSingleCommands(testedStroke), Tuple.Create(true, new List<String>() { "cmd arg arg ", " cmd arg" }));

            testedStroke = "cmd arg \"arg  |   arg\" | cmd \" $VAR \" arg";
            Assert.AreEqual(splitter.ParseToSingleCommands(testedStroke), Tuple.Create(true, new List<String>() { "cmd arg \"arg  |   arg\" ", " cmd \" $VAR \" arg" }));
        }

        [Test]
        public void ParseSubCommandTest()
        {
            testedStroke = " cmd  arg           arg";
            Assert.AreEqual(splitter.ParseSubCommand(testedStroke), Tuple.Create("cmd", new List<String>() { "arg", "arg" }));

            testedStroke = "   cmd    arg    \"arg     arg\"";
            Assert.AreEqual(splitter.ParseSubCommand(testedStroke), Tuple.Create("cmd", new List<String>() { "arg", "arg     arg" }));
        }
    }
}