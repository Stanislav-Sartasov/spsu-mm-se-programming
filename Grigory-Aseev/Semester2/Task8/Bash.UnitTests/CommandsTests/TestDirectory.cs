using Bash.Commands;

namespace Bash.UnitTests.CommandsTests
{
    internal static class TestDirectory
    {
        static ChangeDirectory cd = new ChangeDirectory();

        public static void SetDirectory()
        {
            cd.Execute(new string[0]);
            cd.Execute(new string[] { @"..\..\..\..\Bash.UnitTests\TestFiles" });
        }

        public static void SetFolder()
        {
            SetDirectory();
            cd.Execute(new string[] { "TestFolder" });
        }
    }
}
