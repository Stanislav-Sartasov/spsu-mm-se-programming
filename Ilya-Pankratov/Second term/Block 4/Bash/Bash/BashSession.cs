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
            Console.WriteLine("Hello, this is a simple bash terminal emulator!\n");

            while (true)
            {
                Console.Write("$ ");
                var executeResult = bash.Execute(Console.ReadLine());

                if (executeResult == "exit")
                {
                    break;
                }
                else
                {
                    Console.Write(executeResult);
                }
            }
        }
    }
}
