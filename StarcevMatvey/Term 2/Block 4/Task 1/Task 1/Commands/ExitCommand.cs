namespace Task_1.Commands
{
    public class ExitCommand : IExecutable
    {
        public ExitCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            Environment.Exit(0);

            return bash;
        }
    }
}
