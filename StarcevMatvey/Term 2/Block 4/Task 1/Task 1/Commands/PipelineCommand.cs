namespace Task_1.Commands
{
    public class PipelineCommand : IExecutable
    {
        public PipelineCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var newCurrentArgs = new List<string>();
            foreach (var arg in b.CurrentArguments)
            {
                var splitedArgs = arg.Split('\n');
                foreach (var a in splitedArgs)
                {
                    newCurrentArgs.Add(a.Trim('\r'));
                }
            }

            return b.WithCurrentArguments(newCurrentArgs);
        }
    }
}
