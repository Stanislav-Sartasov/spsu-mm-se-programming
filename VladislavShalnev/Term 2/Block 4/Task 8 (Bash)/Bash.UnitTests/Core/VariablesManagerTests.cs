using NUnit.Framework;

namespace Bash.Core.UnitTests;

public class VariablesManagerTests
{
	private VariablesManager _variablesManager;

	[SetUp]
	public void Setup() =>
		_variablesManager = new VariablesManager();

	[Test]
	public void ReadVariablesTest()
	{
		_variablesManager.ReadVariables("$a1=\"test test\" \"  test $b2=$a1$c1 abc $d=3\"$a=1 $1=b");
		
		Assert.AreEqual(2, _variablesManager.Variables.Count);
		Assert.AreEqual("\"test test\"", _variablesManager.Variables["a1"]);
		Assert.AreEqual("1", _variablesManager.Variables["a"]);
	}
	
	[Test]
	public void ReplaceVariablesAssignmentsTest()
	{
		string actual = _variablesManager
			.ReplaceVariablesAssignments("$a1=test \"  test $b2=$a1$c1 abc $d=3\"$a=1 $1=b");

		string expected = " \"  test $b2=$a1$c1 abc $d=3\" $1=b";
		
		Assert.AreEqual(expected, actual);
	}
	
	[Test]
	public void ReplaceVariablesTest()
	{
		_variablesManager.Variables["a"] = "1";
		_variablesManager.Variables["b"] = "2";
		
		string actual = _variablesManager
			.ReplaceVariables("$a \" test $b\"");

		string expected = "1 \" test 2\"";
		
		Assert.AreEqual(expected, actual);
	}
}