using BashComponents;
using NUnit.Framework;

namespace BashComponentsUnitTests;

public class VariableManagerTests
{
    [Test]
    public void AddAndUnpackVariableTest()
    {
        var manager = new VariableManager();
        var name = "$name";
        var value = "value";
        var inputAddValue = new List<String> { name, value };
        var inputUnpackValue = new List<String> { "The $name of science is enormous!" };
        var correctResult = "The value of science is enormous!";
        
        manager.AddVariable(inputAddValue);
        var unpackedValue = manager.UnpackVariables(inputUnpackValue);
        Assert.That(unpackedValue.Count(), Is.EqualTo(1));
        Assert.That(unpackedValue.First(), Is.EqualTo(correctResult));
    }
}