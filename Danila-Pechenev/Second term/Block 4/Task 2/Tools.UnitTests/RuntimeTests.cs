namespace Tools.UnitTests;
using NUnit.Framework;
using Tools;

public class RuntimeTests
{
    [Test]
    public void AddNonexistentVariableTest()
    {
        Runtime.AddVariable("name", "Danya");
        Assert.IsTrue(Runtime.LocalVariables.ContainsKey("name"));
        Assert.AreEqual("Danya", Runtime.LocalVariables["name"]);
    }

    [Test]
    public void AddExistentVariableTest()
    {
        Runtime.AddVariable("var", "5");
        Runtime.AddVariable("var", "7");
        Assert.IsTrue(Runtime.LocalVariables.ContainsKey("var"));
        Assert.AreEqual("7", Runtime.LocalVariables["var"]);
    }
}
