using Bash.App.BashComponents;
using NUnit.Framework;

namespace Bash.UnitTests.BashComponentsTests
{
	public class VariableTests
	{
		[Test]
		public void VariableCreateTest()
		{
			var variable = new Variable("Name of the variable", "Value of the variable");
			Assert.AreEqual("Name of the variable", variable.Name);
			Assert.AreEqual("Value of the variable", variable.Value);

			Assert.Pass();
		}
	}
}
