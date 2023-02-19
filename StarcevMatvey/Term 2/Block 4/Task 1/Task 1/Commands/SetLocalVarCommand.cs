namespace Task_1.Commands
{
    public class SetLocalVarCommand : IExecutable
    {
        public SetLocalVarCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var v = b.DequeueArgumentForSetLocalVar();
            if (b.LocalVar.ContainsKey(v.name))
                b = b.WithLocalVar(b.LocalVar.Where(x => x.Key != v.name)
                    .ToDictionary(x => x.Key, x => x.Value));
            var d = new Dictionary<string, string>(b.LocalVar)
            {
                { v.name, v.value }
            };

            return b = b.WithLocalVar(d).WithCurrentArguments(new List<string>());
        }
    }
}
