using CommandManager;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BushTests
{
    public class VariableManagerTests
    {
        private VariableManager storage = new VariableManager();

        [Test]
        public void AssignmentTests()
        {
            try
            {
                storage.TryToAssignmentVariable(new List<String>() { "$var", "val", "val" });
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(ex.Message, "Uncorrect assightment. It should be: $variable=value");
            }

            try
            {
                storage.TryToAssignmentVariable(new List<String>() { "var", "val"});
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(ex.Message, "Uncorrect assightment. It should be: $variable=value");
            }

            storage.TryToAssignmentVariable(new List<String>() { "$var", "val" });
            storage.TryToAssignmentVariable(new List<String>() { "$var", "\"val\"" });
            Assert.Pass();
        }

        [Test]
        public void ReplaceVariablesTests()
        {
            storage.TryToAssignmentVariable(new List<String>() { "$PATH", "123" });
            storage.TryToAssignmentVariable(new List<String>() { "$WAY", "456" });

            var args = new List<String>() { "$PATH arg $WAY smt $PATH", "smt \"$PATH   arg   $WAY\"" };
            var newArgs = storage.ReplaceVariables(args);
            Assert.AreEqual(newArgs, new List<String>() { "123 arg 456 smt 123", "smt \"123   arg   456\"" });

            try
            {
                storage.ReplaceVariables(new List<String>() { "$NOTHING"});
                Assert.Fail();
            }
            catch (KeyNotFoundException ex)
            {
                Assert.AreEqual(ex.Message, "$NOTHING variable not found");
            }
        }
    }
}