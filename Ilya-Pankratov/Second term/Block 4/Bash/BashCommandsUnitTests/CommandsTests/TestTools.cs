using System.IO;

namespace BashCommandsUnitTests
{
    internal static class TestTools
    {
        public static string FindPathToTestFiles()
        {
            var directory = Directory.GetCurrentDirectory();
            var counter = 0;

            while (!directory.EndsWith("Bash"))
            {
                directory = Path.Combine(directory.Split('\\')[..^1]);

                if (counter++ > 10)
                {
                    return "TestFile directory is missing";
                }
            }

            if (Directory.Exists("TestFiles"))
            {
                return "TestFile directory is missing";
            }

            return Path.Combine(directory, "TestFiles");
        }
    }
}
