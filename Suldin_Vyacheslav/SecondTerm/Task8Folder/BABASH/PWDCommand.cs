namespace BABASH
{
    public class PWDCommand : Command
    {
        public PWDCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "pwd";
            parametres = args;
        }

        public override void Execute()
        {
            stdOut = session.GetCurrentDirectory();
        }

    }
}