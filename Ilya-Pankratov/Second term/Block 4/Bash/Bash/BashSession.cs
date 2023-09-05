namespace Bash
{
    public class BashSession : IBash
    {
        private readonly BashEmulator bash;

        public BashSession()
        {
            bash = new BashEmulator();
        }

        public void Start()
        {
            Console.WriteLine("Hello, this is a simple bash terminal emulator!\n\n" +
                              "Available commands:\n" +
                              "ls - list directories and files in working directory\n" +
                              "cd [directoryName] - change directory\n" +
                              "[driveName]: - change drive\n" +
                              "pwd - print working directory\n" +
                              "wc [file]- show lines, words and bytes in specified file\n" +
                              "cat [file]- show content of specified file\n" +
                              "echo [input] - print input in terminal\n" +
                              "$ - is used for variables\n" +
                              "| - command pipeline\n" +
                              "&[assemblyPath] - load new commands to Bash\n");

            while (true)
            {
                Console.Write("$ ");
                var executeResult = bash.Execute(Console.ReadLine());
                Console.Write(ConsoleOutputManager.GetConsoleOutput(executeResult.ToList()));
            }
        }
    }
}
