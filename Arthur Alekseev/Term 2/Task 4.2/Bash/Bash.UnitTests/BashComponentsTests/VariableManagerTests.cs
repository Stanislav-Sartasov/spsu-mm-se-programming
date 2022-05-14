using Bash.App;
using Bash.App.BashComponents;
using Bash.App.BashComponents.Exceptions;
using NUnit.Framework;

namespace Bash.UnitTests.BashComponentsTests
{
	public class VariableManagerTests
	{
		VariableManager varManager;

		[SetUp]
		public void SetUp()
		{
			varManager = new VariableManager();
		}

		[Test]
		public void VariableReplacementTest()
		{
			varManager.ReplaceVariables(new string[] { "$with", "=", "value0" });
			varManager.ReplaceVariables(new string[] { "$with", "=", "value1" });
			varManager.ReplaceVariables(new string[] { "$variable", "=", "value2" });
			varManager.ReplaceVariables(new string[] { "$some", "=", "will not show up" });

			string[] command = new string[] { "$some", "command", "$with", "$variable" };

			bool result = varManager.ReplaceVariables(command);

			Assert.AreEqual(false, result);

			Assert.AreEqual(new string[] { "$some", "command", "value1", "value2" }, command);

			Assert.Pass();
		}

		[Test]
		public void VariableAssignmentTest()
		{
			string[] command = new string[] { "$some", "=", "value" };

			bool result = varManager.ReplaceVariables(command);

			Assert.AreEqual(true, result);

			Assert.Pass();
		}

		[Test]
		public void VariableNotFoundTest()
		{
			string[] command = new string[] { "$some", "=", "$nonexistantvariable" };

			try
			{
				bool result = varManager.ReplaceVariables(command);
			}
			catch (VariableNotFoundException exception)
			{
				Assert.AreEqual("Variable $nonexistantvariable not found", exception.Message);
				Assert.Pass();
			}
			Assert.Pass();
		}

		[Test]
		public void IncorrectAssignmentTest()
		{
			string[] command = new string[] { "$some", "=", "abcdabcd", "normaldata" };

			try
			{
				bool result = varManager.ReplaceVariables(command);
			}
			catch (VariableAssignmentException exception)
			{
				Assert.AreEqual("Assignment should be done like $var_name = value", exception.Message);
				Assert.Pass();
			}
			Assert.Pass();
		}
	}
}
