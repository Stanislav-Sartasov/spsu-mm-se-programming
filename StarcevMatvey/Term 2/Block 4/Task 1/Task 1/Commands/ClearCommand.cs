namespace Task_1.Commands
{
    public class ClearCommand : IExecutable
    {
        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            Console.Clear();

            return b.WithCurrentArguments(new List<string>());
        }
    }
}
