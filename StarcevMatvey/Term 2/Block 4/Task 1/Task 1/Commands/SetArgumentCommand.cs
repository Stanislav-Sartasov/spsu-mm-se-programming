namespace Task_1.Commands
{
    public class SetArgumentCommand : IExecutable
    {
        public SetArgumentCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var arg = b.DequeueArgument();

            return b.WithCurrentArguments(new List<string> { arg });
        }
    }
}
