namespace Task_1.Commands
{
    public class EchoCommand : IExecutable
    {
        public EchoCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            b = b.WithOutput("");
            foreach (var arg in b.CurrentArguments)
            {
                b = b.WithOutput(b.Output + b.GetLocalVar(arg) + "\n");
            }

            return b;
        }
    }
}
