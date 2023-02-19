namespace Task_1.Commands
{
    public class WhoamiCommand : IExecutable
    {
        public WhoamiCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            return b.WithCurrentArguments(new List<string> { Environment.UserName });
        }
    }
}
