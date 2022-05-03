namespace CommandLib
{
    public class EXITCommand : ACommand
    {
        public EXITCommand(string[] args)
        {
            Name = "exit";
            parametres = args;
        }

        public override void Run()
        {
            stdOut = null;
        }
    }
}