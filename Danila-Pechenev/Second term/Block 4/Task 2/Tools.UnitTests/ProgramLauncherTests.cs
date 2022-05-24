namespace Tools.UnitTests;
using NUnit.Framework;
using Tools;

public class ProgramLauncherTests
{
    [Test]
    public void LaunchProgramTest()
    {
        bool success = Programlauncher.TryLaunchProgram("whoami", null, out string text);
        Assert.IsTrue(success);
        Assert.IsTrue(text.Length > 0);
    }
}
