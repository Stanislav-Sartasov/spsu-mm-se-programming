namespace CommandLib
{
    public class EXITCommand : ACommand
    {
        public EXITCommand(string[] args)
        {
            name = CommandName.exit;
            parametres = args;
        }

        public override void Run()
        {
            stdOut = null;
        }
    }
}