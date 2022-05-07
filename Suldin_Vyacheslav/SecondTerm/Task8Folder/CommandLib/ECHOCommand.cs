namespace CommandLib
{
    public class ECHOCommand : ACommand
    {
        public ECHOCommand(string[] args)
        {
            name = CommandName.echo;
            parametres = args;
        }

        public override void Run()
        {
            foreach (string arg in parametres)
            {
                stdOut += arg + " ";
            }
            stdOut = stdOut == null ? "": stdOut[..^1];
        }
    }
}