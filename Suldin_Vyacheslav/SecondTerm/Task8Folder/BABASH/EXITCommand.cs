namespace BABASH
{
    public class EXITCommand : Command
    {
        public EXITCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "exit";
            parametres = args;
        }

        public override void Execute()
        {
            stdOut = null;
        }
    }
}